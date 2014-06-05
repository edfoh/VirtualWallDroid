using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWall.Core.Nfc {
    public interface INfcService 
    {
        Action<string> DisplayCardAction { get; set; }
    }
}
