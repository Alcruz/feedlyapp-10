using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Feedly10.App.Models
{
	public class Category : UIModel
	{
		private ISet<Subscription> _subscriptions;

		public Category(Feedly.Category category) : base(category.Id)
		{
			Label = category.Label;
			IsExpanded = true;
			ToggleExpandCommand = new RelayCommand(ToggleExpand);
			_subscriptions = new HashSet<Subscription>();
		}

		public string Label { get; private set; }
		public bool IsExpanded { get; private set; }
		public ICommand ToggleExpandCommand { get; private set; }

		public IImmutableSet<Subscription> Subscriptions => _subscriptions.ToImmutableHashSet();

		internal void AddSubscription(Subscription subscription)
		{
			if (!_subscriptions.Contains(subscription))
			{
				_subscriptions.Add(subscription);
				subscription.AddCategory(this);
			}
		}

		private void ToggleExpand()
		{
			IsExpanded = !IsExpanded;
			RaisePropertyChanged(nameof(IsExpanded));
		}
	}
}
