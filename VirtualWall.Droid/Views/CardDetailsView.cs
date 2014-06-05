using Android.App;
using Android.Content;
using Android.Nfc;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;

namespace VirtualWall.Droid.Views 
{

    [Activity(Label = "View for FirstViewModel")]
    public class CardDetailsView : MvxActivity {

        private NfcAdapter nfcAdapter;
        private IntentFilter[] writeTagFilters;
        private PendingIntent nfcPendingIntent;
        private bool writeProtect = false;
        private Context context;

        private string textToWrite;

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.CardDetails);            
        }


    }
}