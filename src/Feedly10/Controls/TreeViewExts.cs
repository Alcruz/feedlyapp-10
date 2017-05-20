using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeViewControl;
using Windows.UI.Xaml;

namespace App.Controls
{
	public class TreeViewExts : DependencyObject
	{
		public static readonly DependencyProperty RootNodesProperty = 
			DependencyProperty.RegisterAttached("RootNodes", typeof(IList), typeof(TreeViewExts), new PropertyMetadata(null, new PropertyChangedCallback(OnRootNodesChanged)));

		private static void OnRootNodesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (!(d is TreeView treeView)) throw new InvalidOperationException("This property must be attatched to a TreeView Control");

			var rootNodes = GetRootNodes(d);
			foreach (var treeNode in rootNodes)
			{
				treeView.RootNode.Add(treeNode);
			}
		}

		public static IList GetRootNodes(DependencyObject target)
		{
			return (IList)target.GetValue(RootNodesProperty);
		}

		public static void SetRootNodes(DependencyObject target, IList rootNodes)
		{
			target.SetValue(RootNodesProperty, rootNodes);
		}

	}
}
