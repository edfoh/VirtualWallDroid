﻿using System.Collections.Generic;
using VirtualWall.Core.Models.Trello;
using XTest;

namespace VirtualWall.Core.Services {
    public interface ICardService {
        List<TrelloCard> GetCards(string boardShortLink);
        TrelloCard GetCardForId(string id);
        List<TrelloBoard> GetBoards();
    }
}
