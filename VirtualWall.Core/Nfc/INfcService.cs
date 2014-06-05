using System;

namespace VirtualWall.Core.Nfc {
    public interface INfcService 
    {
        Action<string> DisplayCardAction { get; set; }
        void WriteCardAction(string cardId);

        void DeepLinkTo(string url);
    }
}
