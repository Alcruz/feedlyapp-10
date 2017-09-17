using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace App.Models
{
	public class GlobalSubscription : AbstractSubscription
	{
		private GlobalSubscription(string id, string title, BitmapImage icon) : base(id, title)
		{
			Icon = icon;
		}

		public static GlobalSubscription All(string userId) => new GlobalSubscription($"user/{userId}/category/global.all", "All", null);
		public static GlobalSubscription ReadLater(string userId) => new GlobalSubscription($"user/{userId}/category/global.must", "Read Later", null);
	}
}
