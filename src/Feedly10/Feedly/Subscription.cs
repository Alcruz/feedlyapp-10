using System.Collections.Generic;

namespace App.Feedly
{
	public class Subscription
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Website { get; set; }
		public List<Category> Categories { get; set; }
	}
}