using System.Collections.Generic;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using VirtualWall.Core.Models.Trello;
using VirtualWall.Core.Services;

namespace VirtualWall.Core.ViewModels 
{
    public class BoardsViewModel : MvxViewModel
    {
        private readonly ICardService _cardService;

        public BoardsViewModel(ICardService cardService)
        {
            _cardService = cardService;

            Boards = _cardService.GetBoards();
        }

        public IList<TrelloBoard> Boards { get; set; }

        private bool _isBusy;
        public bool IsBusy {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(() => IsBusy); }
        }

        public ICommand ShowCardsCommand 
        {
            get 
            {
                return new MvxCommand<TrelloBoard>(item =>
                {
                    if (IsBusy)
                        return;
                    
                    IsBusy = true;
                    ShowViewModel<CardsViewModel>(new CardsViewModel.Nav() { Id = item.ShortLink });
                });
            }
        }
    }
}
