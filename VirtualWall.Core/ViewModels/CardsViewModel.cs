using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using VirtualWall.Core.Models.Trello;
using VirtualWall.Core.Nfc;
using VirtualWall.Core.Services;

namespace VirtualWall.Core.ViewModels
{
    public class CardsViewModel : MvxViewModel
    {
        private readonly ICardService _cardService;
        private List<TrelloCard> _trelloCards;
        private List<TrelloCard> _storyCards;

        private string _searchTerm;

        public CardsViewModel(ICardService cardService, INfcService nfcService)

        {
            _cardService = cardService;

            nfcService.DisplayCardAction = i =>
            {
                var card = _cardService.GetCardForId(i);
                nfcService.DeepLinkTo(card.ShortUrl);
            };
           
        }

        public class Nav {
            public string Id { get; set; }
        }

        private string _id;
        public void Init(Nav navigation) {

            // get card detail from service using id
            _id = navigation.Id;
            Refresh();
        }

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value; 
                RaisePropertyChanged(() => SearchTerm);
                Filter();
            }
        }

        private void Filter()
        {
            StoryCards = _trelloCards.Where(x => x.Desc.Contains(SearchTerm) || x.Name.Contains(SearchTerm)).ToList();
        }


        
        public List<TrelloCard> StoryCards
        {
            get { return _storyCards; }
            set { _storyCards = value; RaisePropertyChanged(() => StoryCards); }
        }

        public ICommand ShowDetailCommand {
            get {
                return new MvxCommand<TrelloCard>(item => ShowViewModel<CardDetailsViewModel>(new CardDetailsViewModel.Nav() { Id = item.ShortLink }));
            }
        }

        private void ClearFilter()
        {
            SearchTerm = string.Empty;
        }
        public ICommand ClearFilterCommand
        {
            get
            {
                return new MvxCommand(ClearFilter);
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new MvxCommand(Refresh);
            }
        }

        private void Refresh()
        {
            StoryCards = _trelloCards = _cardService.GetCards(_id);
        }
    }

    

    
}
