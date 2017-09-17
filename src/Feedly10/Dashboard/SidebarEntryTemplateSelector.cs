using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App.Dashboard
{
	public class SidebarEntryTemplateSelector : DataTemplateSelector
	{
		public DataTemplate CategoryTemplate { get; set; }
		public DataTemplate SubscriptionTemplate { get; set; }

		protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
		{
			var frameworkElement = container as FrameworkElement;
			switch (item)
			{
				case AbstractSubscription subscription:
					return SubscriptionTemplate;
				case Category category:
					return CategoryTemplate;
			}
			throw new InvalidOperationException();
		}
	}
}
