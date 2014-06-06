namespace VirtualWall.Core.Models.Trello
{
    public class TrelloBoard
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Desc { get; set; }

        public string Url { get; set; }

        public string ShortLink { get; set; }

        public bool Closed { get; set; }
    }
}