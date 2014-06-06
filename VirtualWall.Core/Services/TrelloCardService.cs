using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using VirtualWall.Core.Models;
using VirtualWall.Core.Models.Trello;
using XTest;

namespace VirtualWall.Core.Services {

    public class TrelloCardService : ICardService {

        private readonly HttpClient httpClient;

        private List<TrelloCard> cards;
        private List<TrelloList> _trelloLists; 

        private List<TrelloBoard> boards; 

        public TrelloCardService()
        {
            httpClient = new HttpClient();
        }

        public List<TrelloCard> GetCards(string boardShortLink) {
            const string url = "https://api.trello.com/1/boards/{0}/cards?key=a4ecd323fd1e3db088fc6b9a6fa1f896&token=ebca992dd255dac76c55d301559a0e1ec6e5d53b70cce287f37cbb600a745f12";

            var constructedUrl = string.Format(url, boardShortLink);

            cards = httpClient
                .GetAsync(constructedUrl)
                .Result
                .Content
                .ReadAsAsync<TrelloCard[]>()
                .Result
                .ToList();
            setCardStatuses();
            return cards;
        }

        public List<TrelloBoard> GetBoards() 
        {
            const string url = "https://trello.com/1/members/my/boards?key=a4ecd323fd1e3db088fc6b9a6fa1f896&token=ebca992dd255dac76c55d301559a0e1ec6e5d53b70cce287f37cbb600a745f12";

            boards = httpClient
                .GetAsync(url)
                .Result
                .Content
                .ReadAsAsync<TrelloBoard[]>()
                .Result
                .ToList();

            return boards;
        }

        public TrelloCard GetCardForId(string id) {
            return cards.FirstOrDefault(x => x.ShortLink == id);
        }

        void setCardStatuses()
        {
            GetBoardLists();
            foreach (var trelloCard in cards)
            {
                var list = _trelloLists.FirstOrDefault(x => x.Id == trelloCard.IdList);
                if (list != null)
                {
                    trelloCard.SwimLane = list.Name;    
                }
            }
        }
        void GetBoardLists()
        {
            string listUrl = "https://api.trello.com/1/boards/Dq1HMaEc/lists?key=a4ecd323fd1e3db088fc6b9a6fa1f896&token=ebca992dd255dac76c55d301559a0e1ec6e5d53b70cce287f37cbb600a745f12";
            _trelloLists = httpClient
               .GetAsync(listUrl)
               .Result
               .Content
               .ReadAsAsync<TrelloList[]>()
               .Result
               .ToList();
        }
    }
}
