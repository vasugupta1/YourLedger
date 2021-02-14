using System;
using Google.Cloud.PubSub.V1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YourLedger.Common.Models.PubSub;
using YourLedger.Services.AlphaVantage;
using YourLedger.Services.AlphaVantage.Interface;
using YourLedger.Services.PubSub;
using YourLedger.WebApi.Services.PubSub.Interface;
using YourLedger.WebApi.Models.Config;

namespace YourLedger.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var configValues = new ConfigValues();
            Configuration.GetSection(nameof(ConfigValues))
            .Bind(configValues);

            if(configValues.Enviroment.IsDev)
            {
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", 
                configValues.Enviroment.LocalCred);
            }

            services.AddSingleton<IAlphaVantageService>(new AlphaVantageService(
                    configValues.Exchange.Url, 
                    configValues.Exchange.ApiKey));
                    
           var topic = TopicName.FromProjectTopic(
               configValues.Gcp.ProjectId, 
               configValues.Gcp.TopicId);

            services.AddSingleton<IPubSubService<StockMessage,CryptoMessage>>(
                new PubSubService(PublisherClient.CreateAsync(topic).GetAwaiter().GetResult()));    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}