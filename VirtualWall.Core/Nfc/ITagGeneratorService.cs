namespace VirtualWall.Core.Nfc
{
    public interface ITagGeneratorService
    {
        TagData RetrieveData(string nfcData);
        string GenerateTagString(string cardId);
    }
}