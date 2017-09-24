using Feedly10.App.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedly10.App.Dashboard
{
	public class Entry : UIModel
	{
		public Entry(Feedly.Entry entryDto) : base(entryDto.Id)
		{
			OriginId = entryDto.OriginId;
			Fingerprint = entryDto.Fingerprint;
			Updated = entryDto.Updated;
			Title = entryDto.Title;
			Published = entryDto.Published;
			Crawled = entryDto.Crawled;
			Author = entryDto.Author;
			Summary = ExtractText(entryDto.Summary?.Content);
			ThumbnailUrl = entryDto.Visual?.Url?? string.Empty;
			Content = entryDto.Content?.Content ?? string.Empty;
			Unread = entryDto.Unread;
		}

		public string OriginId { get; private set; }
		public string Fingerprint { get; private set; }
		public long Updated { get; private set; }
		public string Title { get; private set; }
		public long Published { get; private set; }
		public long Crawled { get; private set; }
		public string Author { get; private set; }
		public string Summary { get; private set; }
		public string Content { get; private set; }
		public bool Unread { get; private set; }
		public string ThumbnailUrl { get; private set; }

		private static string ExtractText(string html)
		{
			if (html == null)
			{
				return string.Empty;
			}

			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(html);

			return htmlDocument.DocumentNode.InnerText;
		}
	}
}
