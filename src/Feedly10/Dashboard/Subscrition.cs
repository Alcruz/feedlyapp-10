using GalaSoft.MvvmLight;
using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Net.Http;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;

namespace App.Dashboard
{
	public class Subscription : ObservableObject
	{
		private BitmapImage favicon;

		public Subscription(Dtos.Subscription dto)
		{
			Id = dto.Id;
			Title = dto.Title;
			Website = dto.Website;
			Categories = dto.Categories.ToImmutableList();
			Task.Run(DownloadFavicon);
		}

		public string Id { get; }
		public string Title { get; }
		public string Website { get; }
		public ImmutableList<Category> Categories { get; }
		public BitmapImage Favicon
		{
			get => this.favicon;
			private set => Set(ref this.favicon, value);
		}

		private async Task DownloadFavicon()
		{
			using (var downloader = new FaviconDownloader(Website))
			{
				var bytes = await downloader.Download();
				if (bytes != null && bytes.Length != 0)
					await DispatcherHelper.ExecuteOnUIThreadAsync(async () =>
					{
						BitmapImage bitmap = new BitmapImage();
						using (var stream = new InMemoryRandomAccessStream())
						{
							await stream.WriteAsync(bytes.AsBuffer());
							stream.Seek(0);
							await bitmap.SetSourceAsync(stream);
						}
						Favicon = bitmap;
					});
			}
		}
	}

	public class FaviconDownloader : IDisposable
	{
		private const string linkRegex = "<link\\s*(?:(?<name>[^\\s\0\"\'>=]+)=[\"\'](?<val>[^\"\'\0]*)[\"\']\\s*)*\\s*\\/?>";
		private const string urlRegex = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";

		public FaviconDownloader(string url)
		{
			Url = new Uri(url);
			HttpClient = new HttpClient(new HttpClientHandler
			{
				AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
			});
		}

		public Uri Url { get; }
		public HttpClient HttpClient { get; }

		public async Task<byte[]> Download()
		{
			if (Url == null) return null;

			var response1 = await DownloadFaviconFromDefaultUrl();
			if (response1.IsSuccessStatusCode)
			{
				return await response1.Content.ReadAsByteArrayAsync();
			}

			var response2 = await DownloadFromUrlInsideHtml();
			if (response2.IsSuccessStatusCode)
			{
				return await response2.Content.ReadAsByteArrayAsync();
			}

			return null;
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (HttpClient != null) HttpClient.Dispose();
			}
		}

		private async Task<HttpResponseMessage> DownloadFromUrlInsideHtml()
		{
			var builder = new UriBuilder(Url);
			builder.Path = string.Empty;
			var response = await HttpClient.GetAsync(builder.Uri);
			if (response.IsSuccessStatusCode)
			{
				var html = await response.Content.ReadAsStringAsync();
				var matches = Regex.Matches(html, linkRegex, RegexOptions.Multiline);
				foreach (Match match in matches)
				{
					var attrsName = match.Groups["name"];
					var attrsVal = match.Groups["val"];

					var zipAttrs = attrsName.Captures.Cast<Capture>().Zip(attrsVal.Captures.Cast<Capture>(), (first, second) => (attr: first.Value, value: second.Value));

					var relAttr = zipAttrs.FirstOrDefault(t => t.attr.Equals("rel", StringComparison.OrdinalIgnoreCase));
					if (relAttr.value.Equals("icon", StringComparison.OrdinalIgnoreCase) || relAttr.value.Equals("shortcut icon"))
					{
						var hrefAttr = zipAttrs.FirstOrDefault(t => t.attr.Equals("href", StringComparison.OrdinalIgnoreCase));
						if (Regex.IsMatch(hrefAttr.value, urlRegex))
						{
							return await DownloadFaviconiconFromUrl(new Uri(hrefAttr.value));
						}
						builder.Path = hrefAttr.value;
						return await DownloadFaviconiconFromUrl(builder.Uri);
					}
				}
			}
			return null;
		}

		private Task<HttpResponseMessage> DownloadFaviconiconFromUrl(Uri url)
		{
			return HttpClient.GetAsync(url);
		}

		private async Task<HttpResponseMessage> DownloadFaviconFromDefaultUrl()
		{
			var builder = new UriBuilder(Url);
			builder.Path = "favicon.ico";
			return await DownloadFaviconiconFromUrl(builder.Uri);
		}
	}

	namespace Dtos
	{
		public class Subscription
		{
			public string Id { get; set; }
			public string Title { get; set; }
			public string Website { get; set; }
			public List<Category> Categories { get; set; }
		}
	}
}