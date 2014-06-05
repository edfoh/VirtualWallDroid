using System.Collections.Generic;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using VirtualWall.Core.Models.Trello;
using VirtualWall.Core.Nfc;
using VirtualWall.Core.Services;

namespace VirtualWall.Core.ViewModels
{
    public class FirstViewModel : MvxViewModel
    {
        public FirstViewModel(ICardService cardService, INfcService nfcService)
        {
            nfcService.DisplayCardAction = i =>
            {
                var card = cardService.GetCardForId(i);
                nfcService.DeepLinkTo(card.ShortUrl);
            };
            
            StoryCards = cardService.GetCards();
        }

        public List<TrelloCard> StoryCards { get; set; }

        public ICommand ShowDetailCommand {
            get {
                return new MvxCommand<TrelloCard>(item => ShowViewModel<CardDetailsViewModel>(new CardDetailsViewModel.Nav() { Id = item.ShortLink }));
            }
        }
    }

    

    
}
