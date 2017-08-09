namespace App.Feedly
{
	public class Category
	{
		public string Id { get; set; }
		public string Label { get; set; }

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