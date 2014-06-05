namespace VirtualWall.Core.Models
{
	public abstract class Card
	{
		public abstract string Name { get; set; }

		public abstract string Description { get; set; }

		public abstract string Status { get; set; }

		public abstract string Url { get; set; } 
	}
}

