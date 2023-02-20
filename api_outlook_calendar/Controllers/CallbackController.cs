using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_outlook_calendar.Controllers
{
    [Route("api/[controller]")]
    public class CallbackController : Controller
    {
        string credentialsFile = "Files/credentials.json";
        string tokensFile = "Files/tokens.json";

        [HttpGet]
        public string Callback(string code, string state, string error)
        {

            var finalResponse = "{}";
            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));

            if (!string.IsNullOrWhiteSpace(code))
            {
                RestRequest restRequest = new RestRequest();
                restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                restRequest.AddParameter("client_id", credentials["client_id"].ToString());
                restRequest.AddParameter("scope", credentials["scopes"].ToString());
                restRequest.AddParameter("redirect_uri", credentials["redirect_url"].ToString());
                restRequest.AddParameter("code", code);
                restRequest.AddParameter("grant_type", "authorization_code");
                restRequest.AddParameter("client_secret", credentials["client_secret"].ToString());
                //Toh69217
                RestClient restClient = new RestClient("https://login.microsoftonline.com/common/oauth2/v2.0/token");
                var response = restClient.Post(restRequest);
                
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    System.IO.File.WriteAllText(tokensFile, response.Content);
                    finalResponse = "Token and RefreshToken generated and saved successfully";
                }
            }

            return finalResponse;
        }
    }
}

