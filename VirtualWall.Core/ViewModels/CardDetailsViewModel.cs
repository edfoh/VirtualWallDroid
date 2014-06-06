using System.Collections.Generic;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using VirtualWall.Core.Models.Trello;
using VirtualWall.Core.Nfc;
using VirtualWall.Core.Services;

namespace VirtualWall.Core.ViewModels 
{
    public class CardDetailsViewModel : MvxViewModel 
    {
        private readonly ICardService _cardService;
        private readonly INfcService _nfcService;

        public CardDetailsViewModel(ICardService cardService, INfcService nfcService)
        {
            _cardService = cardService;
            _nfcService = nfcService;
        }

        public class Nav {
            public string Id { get; set; }
        }

        public void Init(Nav navigation) {

            // get card detail from service using id
            CardDetail = _cardService.GetCardForId(navigation.Id);
        }

        public TrelloCard CardDetail { get; private set; }

        public IList<TrelloMember> CardActivities { get { return CardDetail.TrelloMembers; } }

        public ICommand WriteNfcCommand {
            get {
                return new MvxCommand(() => _nfcService.WriteCardAction(CardDetail.ShortLink));
            }
        }

        public ICommand DeepLinkCommand {
            get {
                return new MvxCommand(() => _nfcService.DeepLinkTo(CardDetail.ShortUrl));
            }
        }
    }

    
}
