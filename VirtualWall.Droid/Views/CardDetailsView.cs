using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;

namespace VirtualWall.Droid.Views 
{

    [Activity(Label = "Card Details")]
    public class CardDetailsView : MvxActivity {

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.CardDetails);            
        }


    }
}