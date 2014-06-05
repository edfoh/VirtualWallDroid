using System;
using Android.App;
using Android.Content;
using VirtualWall.Core.Nfc;
using VirtualWall.Droid.Views;

namespace VirtualWall.Droid.Services {
    public class NfcService : INfcService
    {
        Activity _activity;
        public Action<string> DisplayCardAction { get; set; }


        private readonly ITagGeneratorService _tagGeneratorService;

        public NfcService(ITagGeneratorService tagGeneratorService)
        {
            _tagGeneratorService = tagGeneratorService;
        }

        public void WriteCardAction(string cardId)
        {
            var intent = new Intent(_activity, typeof(WriteNfcActivity));
            var nfcStringData = _tagGeneratorService.GenerateTagString(cardId);
            intent.PutExtra(StringConstants.WriteNfMessageKey, nfcStringData);
            _activity.StartActivity(intent);
        }

        public void DeepLinkTo(string url)
        {
            _activity.StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse(url)));
        }

        public void RegisterActivity(Activity activity)
        {
            _activity = activity;
        }
    }
}