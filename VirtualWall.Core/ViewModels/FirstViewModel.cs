using System.Collections.Generic;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
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
                var test = i;
                ShowViewModel<CardDetailsViewModel>(new CardDetailsViewModel.Nav { Id = i });
            };
                
            StoryCards = cardService.GetCards();
        }

        public List<StoryCard> StoryCards { get; set; }

        public ICommand ShowDetailCommand {
            get {
                return new MvxCommand<StoryCard>(item => ShowViewModel<CardDetailsViewModel>(new CardDetailsViewModel.Nav() { Id = item.Id }));
            }
        }
    }

    

    
}
