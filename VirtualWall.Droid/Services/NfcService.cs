using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using VirtualWall.Core.Nfc;

namespace VirtualWall.Droid.Services {
    public class NfcService : INfcService
    {
        public Action<int> DisplayCardAction { get; set; }
    }
}