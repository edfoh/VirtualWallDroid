using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualWall.Core.ViewModels;

namespace VirtualWall.Core.Services {

    public interface ICardService {
        List<StoryCard> GetCards();
        CardDetail GetCardForId(string id);
    }
    public class CardService : ICardService {
        public List<StoryCard> GetCards() {

            return Enumerable.Range(1, 50).Select(x => new StoryCard { Description = "Desc" + x, Owner = "Fist Last" + x, Id = x.ToString()}).ToList(); ;

        }

        public CardDetail GetCardForId(string id)
        {
            var cardDetail = new CardDetail
            {
                Description = "Some description",
                Owner = "Bruce Lee"
            };
            cardDetail.CardActivities.Add(new CardActivity { Comment = "comment 1", Commenter = "Ronny", CreateDate = DateTime.Now });
            cardDetail.CardActivities.Add(new CardActivity { Comment = "comment 2", Commenter = "Jason", CreateDate = DateTime.Now.AddDays(-1) });
            return cardDetail;
        }
    }

    public class StoryCard {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }

    }

    public class CardDetail {
        public CardDetail() {
            CardActivities = new List<CardActivity>();
        }

        public string Description { get; set; }

        public string Id { get; set; }

        public string Owner { get; set; }

        public IList<CardActivity> CardActivities { get; set; }
    }

    public class CardActivity {
        public string Comment { get; set; }

        public string Commenter { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
