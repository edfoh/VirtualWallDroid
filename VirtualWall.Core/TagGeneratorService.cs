using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWall.Core {
    public class TagGeneratorService 
    {
        public TagData RetrieveData(string nfcData)
        {
            
        }
    }

    public class TagData
    {
        private string const string CardIdentifier = "CardId";

        public TagData(string nfcData)
        {
            string[] cardId = nfcData.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            CardId = 
        }

        public string CardId { get; private set;}
    }
}
