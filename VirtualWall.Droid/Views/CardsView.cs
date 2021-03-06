using Android.App;
using Android.OS;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Droid.Views;
using VirtualWall.Core.Nfc;
using VirtualWall.Droid.Services;

namespace VirtualWall.Droid.Views
{
    [Activity(Label = "Cards")]
    public class CardsView : MvxActivity
    {
        private INfcService _nfcService;
        private INfcService NfcService { get { return _nfcService = _nfcService ?? Mvx.Resolve<INfcService>(); } }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var nfcService = NfcService as NfcService;
            if(nfcService != null)
            {
                nfcService.RegisterActivity(this);
            }
            SetContentView(Resource.Layout.CardsView);
        }
    }
}