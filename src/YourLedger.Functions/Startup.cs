using System;
using Google.Cloud.Functions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using YourLedger.Functions.Services.Storage.Interface;
using YourLedger.Functions.Services.Storage;
using YourLedger.Functions.Models.Config;
using Microsoft.Extensions.Configuration;
using System.IO;
using YourLedger.Functions.Services.DataProcesser.Interface;
using YourLedger.Common.Models.PubSub;
using YourLedger.Common.Models.UserData;
using YourLedger.Functions.Services.DataProcesser;
using Microsoft.Extensions.Logging;

namespace YourLedger.Functions
{
    public class Startup : FunctionsStartup
    {
       public override void ConfigureServices(WebHostBuilderContext context, IServiceCollection services)
       {
           var config = new Config();
            context.Configuration
                .GetSection(nameof(Config))
                .Bind(config);
           
           if(config.isDev == true)
           {
               Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", 
               config.LocalCred);
           }

           services.AddSingleton<IStorageService<UserEquity>>(
                new StorageService<UserEquity>(
                    Google.Cloud.Storage.V1.StorageClient.Create(),
            config.BucketNames.StockBucket,
            config.FileType));

            services.AddSingleton<IDataProcessor<StockMessage, UserEquity>>(new DataProcessor());

            var test = new StorageService<UserEquity>(
                    Google.Cloud.Storage.V1.StorageClient.Create(),
            config.BucketNames.StockBucket,
            config.FileType);

            // var testing = test.FileExists("testing/voo.json").GetAwaiter().GetResult();
            // var obtest = test.GetFile("testing/voo.json").GetAwaiter().GetResult();


            // services.AddSingleton<IDataProcessor<StockMessage, UserEquity>>(new DataProcessor());
       } 

       public override void ConfigureAppConfiguration(WebHostBuilderContext context, IConfigurationBuilder configuration)
        {
            configuration
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
            
        }
    }
}