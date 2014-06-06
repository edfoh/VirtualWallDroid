using System.Collections.Generic;
using VirtualWall.Core.Models.Trello;
using XTest;

namespace VirtualWall.Core.Services {
    public interface ICardService {
        List<TrelloCard> GetCards();
        TrelloCard GetCardForId(string id);
    }
}
