namespace DWH.Services.GetNewRecords
{
    public interface INewRecordsService
    {
        string GetNewRecordsAsJSON(string type, DateTime lastCreationTime);
    }
}
