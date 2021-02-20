namespace YourLedger.WebApi.Models.Config
{
    public class ConfigValues
    {
        public Enviroment Enviroment {get;set;}
        public Exchange Exchange {get;set;}
        public Gcp Gcp {get;set;}
    }

    public class Enviroment
    {
        public bool IsDev {get;set;}
        public string LocalCred {get;set;}
    }
    public class Exchange
    {
        public string Url {get;set;}
        public string ApiKey {get;set;}
    }
    public class Gcp
    {
        public string ProjectId {get;set;}
        public Topic Topic {get;set;}
    }

    public class Topic
    {
        public string StockTopic {get;set;}
        public string CryptoTopic {get;set;} 
    }
}