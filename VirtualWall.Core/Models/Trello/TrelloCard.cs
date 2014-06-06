using System.Collections.Generic;

namespace VirtualWall.Core.Models.Trello
{
    public class TrelloCard
	{
		public string Name { get;  set; }

		public string Desc { get;  set; }

		public string ShortUrl { get;  set; }

        public string ShortLink { get; set; }

		public string SwimLane { get; set; }

	    public string IdList { get; set; }

        public List<TrelloMember> TrelloMembers { get; set; }
	}

    public class TrelloMember
    {
        public string Id { get; set; }
        public object AvatarHash { get; set; }
        public string FullName { get; set; }
        public string Initials { get; set; }
        public string Username { get; set; }
    }
}

