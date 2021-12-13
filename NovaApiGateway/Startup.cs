using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using IdentityServer4.AccessTokenValidation;

namespace NovaApiGateway
{
    public class Startup
    {
        public  IConfiguration Configuration { set; get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
           
        }

        //public Startup(IConfiguration configuration,IHostingEnvironment env)
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(env.ContentRootPath)
        //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        //        .AddJsonFile("Ocelot.json")
        //        .AddEnvironmentVariables();

        //    Configuration = builder.Build();
        //}
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //var configuration = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true);

            //new WebHostBuilder()
            //  .UseKestrel()
            //  .UseContentRoot(Directory.GetCurrentDirectory())
            //  .ConfigureAppConfiguration((hostingContext, config) =>
            //  {
            //      config
            //          .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
            //          .AddJsonFile("appsettings.json", true, true)
            //          .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
            //          .AddJsonFile("Ocelot.json")
            //          .AddEnvironmentVariables();

            //var identityUrl = Configuration.GetValue<string>("IdentityUrl");
            // var authenticationProviderKey = "IdentityApiKey";

            //  var authenticationProviderKey = "TestKey";

            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //var identityUrl = Configuration.GetValue<string>("IdentityUrl");

            services.AddAuthentication()
            .AddIdentityServerAuthentication("IdentityApiKey", options =>
            {
                
                options.ApiName = "Gateway";
                options.Authority = "https://localhost:5006";
                // options.SupportedTokens = SupportedTokens.Both;
                options.ApiSecret = "GatewaySecret";

                options.RequireHttpsMetadata = false;
                options.SupportedTokens = SupportedTokens.Both;


            
            });



            //.AddJwtBearer("hossamauth",options=> {
            //    options.Authority = "https://localhost:5006";



            //})
            //;


            // var authenticationProviderKey = "IdentityUrl";
            //Action<IdentityServerAuthenticationOptions> options = o =>
            //{
            //    o.Authority = "https://localhost:44343";
            //    o.ApiName = "api";
            //    o.SupportedTokens = SupportedTokens.Both;
            //    o.ApiSecret = "secret";
            //};

            //services.AddAuthentication()
            //    .AddIdentityServerAuthentication(authenticationProviderKey, options);

            services.AddOcelot();
        }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           // app.UseAuthentication();
            app.UseRouting();

            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

            //app.UseOcelot().Wait();

            app.UseOcelot().Wait();
            //app.UseAuthentication().UseOcelot().Wait();
          
        }
    }
}
