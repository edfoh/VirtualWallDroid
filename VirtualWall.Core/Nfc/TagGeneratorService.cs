using System;
using System.Collections.Generic;
using System.Linq;

namespace VirtualWall.Core.Nfc {
    public class TagGeneratorService : ITagGeneratorService
    {
        public TagData RetrieveData(string nfcData)
        {
            return new TagData(nfcData);    
        }
        
        public string GenerateTagString(string cardId)
        {
            return string.Format("{0}:{1}", TagData.CardIdentifier, cardId);
        }        
    }

    public class TagData
    {
        public static string CardIdentifier = "CardId";

        public TagData(string nfcData)
        {
            if (Validate(nfcData))
            {
                CardId = FindCardId(nfcData);
            }            
        }

        private bool Validate(string nfcData)
        {
            List<string> cardIdKvp = SplitNfcData(nfcData);
            return cardIdKvp.Count ==2 && cardIdKvp[0] == CardIdentifier && !string.IsNullOrEmpty(cardIdKvp[1]);
        }

        private string FindCardId(string nfcData)
        {
            List<string> cardIdKvp = SplitNfcData(nfcData);
            return cardIdKvp[1];
        }

        private List<string> SplitNfcData(string nfcData)
        {
            // nfc data should be like CardId:XDFASFZCCD
             return nfcData.Split(new[] {":"}, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public string CardId { get; private set;}

        public bool IsValid { get { return !string.IsNullOrEmpty(CardId); } } 

    }
}
