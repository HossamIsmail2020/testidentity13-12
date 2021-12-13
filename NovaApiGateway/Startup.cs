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
          public void ConfigureServices(IServiceCollection services)
        {
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

            app.UseOcelot().Wait();
          
        }
    }
}
