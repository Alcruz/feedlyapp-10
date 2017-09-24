using Feedly10.App.Models;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Feedly10.App.Dashboard
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
