using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Dashboard
{
	public class Category : UIModel
	{
		public Category(Feedly.Category category)
		{
			Id = category.Id;
			Label = category.Label;
		}

		public string Label { get; private set; }

		public override bool Equals(object obj)
		{
			if (obj is Category category)
				return Id.Equals(category.Id);
			else
				return false;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}
