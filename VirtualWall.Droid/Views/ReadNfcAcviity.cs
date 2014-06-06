using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Nfc;
using Android.Nfc.Tech;
using Android.OS;
using Android.Util;
using Android.Widget;
using Cirrious.CrossCore;
using Java.IO;
using Java.Lang;
using VirtualWall.Core.Nfc;

namespace VirtualWall.Droid.Views
{
    [Activity(Label = "Read NFC"),
        IntentFilter(new[] { "android.nfc.action.NDEF_DISCOVERED", "android.nfc.action.TECH_DISCOVERED" },
            DataMimeType = "text/plain",
            Categories = new[] { "android.intent.category.DEFAULT" }),
        MetaData("android.nfc.action.TECH_DISCOVERED", Value = "@xml/nfc_tech_list")]			
    public class ReadNfcAcviity : Activity
    {
        private NfcAdapter _nfcAdapter;
        
        private INfcService _nfcService;
        private INfcService NfcService { get { return _nfcService = _nfcService ?? Mvx.Resolve<INfcService>(); } }

        private ITagGeneratorService _tagGeneratorService;
        private ITagGeneratorService TagGeneratorService { get { return _tagGeneratorService = _tagGeneratorService ?? Mvx.Resolve<ITagGeneratorService>(); } }

        protected override void OnCreate(Bundle bundle) 
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Nfc);

            _nfcAdapter = NfcAdapter.GetDefaultAdapter(this);

            if (_nfcAdapter == null) {
                // Stop here, we definitely need NFC
                Toast.MakeText(this, "This device doesn't support NFC.", ToastLength.Long).Show();
                this.Finish();
                return;

            }

            if (!_nfcAdapter.IsEnabled) {
                Toast.MakeText(Application.Context, "NFC is disabled.", ToastLength.Long);
            } else {
                Toast.MakeText(Application.Context, "Please scan an NFC", ToastLength.Long);                
            }

            HandleIntent(this.Intent);
        }

        protected override void OnResume() {
            base.OnResume();
            SetupForegroundDispatch(this, _nfcAdapter);
        }

        protected override void OnPause() {
            StopForegroundDispatch(this, _nfcAdapter);
            base.OnPause();
        }

        protected override void OnNewIntent(Intent intent) {
            HandleIntent(intent);
        }

        private void HandleIntent(Intent intent) {
            string action = intent.Action;
            if (NfcAdapter.ActionNdefDiscovered.Equals(action)) {
                string type = intent.Type;
                if (StringConstants.MimeType.Equals(type)) {

                    Tag tag = (Tag)intent.GetParcelableExtra(NfcAdapter.ExtraTag);
                    ReadAsync(tag);
                } else {
                    Log.Debug("VirtualWall", "Wrong mime type: " + type);
                }
            } else if (NfcAdapter.ActionTechDiscovered.Equals(action)) {

                // In case we would still use the Tech Discovered Intent
                Tag tag = (Tag)intent.GetParcelableExtra(NfcAdapter.ExtraTag);
                string[] techList = tag.GetTechList();
                string searchedTech = typeof(Ndef).Name;

                foreach (string tech in techList) {
                    if (searchedTech.Equals(tech)) {
                        ReadAsync(tag);
                        break;
                    }
                }
            }
        }

        private void ReadAsync(Tag tag) {
            
            Task.Factory
                .StartNew(() =>
                    ReadTagAsync(tag)
                    )
                .ContinueWith(task =>
                        RunOnUiThread(() =>
                                OnSuccessfulRead(task.Result)
                            )
                    );
        }

        private string ReadTagAsync(Tag tag) {
            Ndef ndef = Ndef.Get(tag);
            if (ndef == null) {
                // NDEF is not supported by this Tag.
                return null;
            }

            NdefMessage ndefMessage = ndef.CachedNdefMessage;

            NdefRecord[] records = ndefMessage.GetRecords();
            foreach (NdefRecord ndefRecord in records) {
                if (ndefRecord.Tnf == NdefRecord.TnfWellKnown) {
                    try {
                        return ReadText(ndefRecord);
                    } catch (UnsupportedEncodingException ex) {
                        Log.Error("VirtualWall", "Unsupported Encoding", ex);
                    }
                }
            }

            return null;
        }

        private string ReadText(NdefRecord record) {
            return Encoding.UTF8.GetString(record.GetPayload());
        }

        private void OnSuccessfulRead(string nfcData) {
            if (NfcService.DisplayCardAction == null)
            {
                Toast.MakeText(Application.Context, "Cannot read card id from nfc...", ToastLength.Long).Show();
            }
            else
            {
                var tagData = TagGeneratorService.RetrieveData(nfcData);
                if (!tagData.IsValid)
                {
                     Toast.MakeText(Application.Context, "No card id found....", ToastLength.Long).Show();                    
                }                
                else
                {
                    NfcService.DisplayCardAction(tagData.CardId);           
                }               
                Finish();                
            }            
        }

        public static void SetupForegroundDispatch(Activity activity, NfcAdapter adapter) {
            Intent intent = new Intent(activity.ApplicationContext, activity.GetType());
            intent.SetFlags(ActivityFlags.SingleTop);

            PendingIntent pendingIntent = PendingIntent.GetActivity(activity.ApplicationContext, 0, intent, 0);

            IntentFilter[] filters = new IntentFilter[1];
            string[][] techList = new string[][] { };

            // Notice that this is the same filter as in our manifest.
            filters[0] = new IntentFilter();
            filters[0].AddAction(NfcAdapter.ActionNdefDiscovered);
            filters[0].AddCategory(Intent.CategoryDefault);
            try {
                filters[0].AddDataType(StringConstants.MimeType);
            } catch (Android.Content.IntentFilter.MalformedMimeTypeException) {
                throw new RuntimeException("Check your mime type.");
            }
            adapter.EnableForegroundDispatch(activity, pendingIntent, filters, techList);
        }

        public static void StopForegroundDispatch(Activity activity, NfcAdapter adapter) {
            adapter.DisableForegroundDispatch(activity);
        }
    }
}