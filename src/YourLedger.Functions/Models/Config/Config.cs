namespace YourLedger.Functions.Models.Config
{
    public class Config
    {
        public bool isDev {get;set;}
        public string LocalCred {get;set;}
        public string ProjectId {get;set;}
        public BucketNames BucketNames {get;set;}
        public string FileType {get;set;}
    }

    public class BucketNames
    {
        public string StockBucket {get;set;}
        public string CryptoBucket {get;set;}
    }
}