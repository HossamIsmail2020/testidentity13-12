using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NovaApiGateway.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [ApiController]
    [Route("[controller]")]
    //[Authorize]

    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        //[Route("GetToken")]
        public async Task<TokenResponse> Get(credinitial cred)
        {
            //string username="";
            //string password="";
            var identityServerClient = _httpClientFactory.CreateClient();

            var discoveryDoc = await identityServerClient.GetDiscoveryDocumentAsync("https://localhost:5006");
            //result is token response
            var tokenResponse = await identityServerClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discoveryDoc.TokenEndpoint,
                ClientId = "Gateway",
                ClientSecret = "GatewaySecret",
                Scope = "weatherApi.read",
                UserName= cred.username,
                Password = cred.password,
            });;

            return tokenResponse;

        }
    }
}


