using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace App.Models
{
	public abstract class AbstractSubscription : UIModel
	{
		private BitmapImage icon;

		protected AbstractSubscription(string id, string title) : base(id)
		{
			Title = title;
		}

		public string Title { get; }

		public BitmapImage Icon
		{
			get => this.icon;
			protected set => Set(ref this.icon, value);
		}
	}
}
