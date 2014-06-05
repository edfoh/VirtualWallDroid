using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.ViewModels;
using VirtualWall.Core.Services;

namespace VirtualWall.Core.ViewModels 
{
    public class CardDetailsViewModel : MvxViewModel 
    {
        private readonly ICardService _cardService;

        public CardDetailsViewModel(ICardService cardService)
        {
            _cardService = cardService;
        }

        public class Nav {
            public string Id { get; set; }
        }

        public void Init(Nav navigation) {

            // get card detail from service using id
            CardDetail = _cardService.GetCardForId(navigation.Id);
        }

        public CardDetail CardDetail { get; private set; }

        public IList<CardActivity> CardActivities { get { return CardDetail.CardActivities; } }        
    }

    
}
