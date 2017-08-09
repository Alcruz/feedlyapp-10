using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Dashboard
{
	public abstract class UIModel : ObservableObject
	{
		public string Id { get; protected set; }
	}
}
