﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedly10.App.Models
{
	public abstract class UIModel : ObservableObject
	{
		public string Id { get; protected set; }

		protected UIModel(string id) 
		{
			Id = id ?? throw new ArgumentNullException(nameof(id));
		}

		public override bool Equals(object obj)
		{
			var uiModel = obj as UIModel;
			if (uiModel == null)
			{
				return false;
			}

			return Id.Equals(uiModel.Id, StringComparison.Ordinal);
		}

		public override int GetHashCode() => Id.GetHashCode();
	}
}
