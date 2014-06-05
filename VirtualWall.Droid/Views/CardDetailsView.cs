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

            //textToWrite = Intent.GetStringExtra(StringConstants.WriteNfMessageKey);

            //context = Application.Context;
            //nfcAdapter = NfcAdapter.GetDefaultAdapter(this);
            //nfcPendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this,
            //    typeof(WriteNfcActivity)).AddFlags(ActivityFlags.SingleTop
            //        | ActivityFlags.ClearTop), 0);
            //IntentFilter discovery = new IntentFilter(NfcAdapter.ActionTagDiscovered);
            //IntentFilter ndefDetected = new IntentFilter(NfcAdapter.ActionNdefDiscovered);
            //IntentFilter techDetected = new IntentFilter(NfcAdapter.ActionTechDiscovered);
            // Intent filters for writing to a tag  
            // writeTagFilters = new IntentFilter[] { discovery }; 
        }


    }
}