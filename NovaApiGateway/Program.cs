using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NovaApiGateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //await SendDataFromGateway();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureAppConfiguration((host, config) =>
            { //config.SetBasePath(host.HostingEnvironment)
                config.AddJsonFile("Ocelot.json")
               .AddJsonFile($"Ocelot.{host.HostingEnvironment.EnvironmentName}.json", optional: true);

            });


        //Base code from: http://zetcode.com/csharp/httpclient/
        //public static  async Task<string> SendDataFromGateway()
        //{
        //    //The data that needs to be sent. Any object works.
        //    var pocoObject = new
        //    {
        //        grant_type = "password",
        //        scope= "weatherApi.read",
        //        client_id = "Gateway",
        //        client_secret= "GatewaySecret",
        //        username= "procoder",
        //        password= "password",
        //    };

        //    //Converting the object to a json string. NOTE: Make sure the object doesn't contain circular references.
        //    string json = JsonConvert.SerializeObject(pocoObject);

        //    //Needed to setup the body of the request
        //    StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        //    //The url to post to.
        //    var url = "https://localhost:5006/connect/token";
        //    var client = new HttpClient();

        //    //Pass in the full URL and the json string content
        //    var response = await client.PostAsync(url, data);

        //    //It would be better to make sure this request actually made it through
        //    string result = await response.Content.ReadAsStringAsync();

        //    //close out the client
        //    client.Dispose();

        //    return result;
        //}
    }
}
