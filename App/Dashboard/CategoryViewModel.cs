using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace App.Dashboard
{
    public class CategoryViewModel: ViewModelBase
    {
        private Category _category;
        private bool _isCollapsed;

        public Category Category { get { return _category; } set { Set(ref _category, value); } }
        public List<Subscrition> Subscriptions { get; set; }

        public bool IsCollapsed
        {
            get { return _isCollapsed; }
            set { Set(ref _isCollapsed, value);  }
        }

        public CategoryViewModel(Category category, List<Subscrition> subscriptions)
        {
            Category = category;
            Subscriptions = subscriptions;
        }
    }
}
