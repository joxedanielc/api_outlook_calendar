using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_outlook_calendar.Controllers
{
    [Route("api/[controller]")]
    public class RefreshTokenController : Controller
    {
        string credentialsFile = "Files/credentials.json";
        string tokensFile = "Files/tokens.json";

        [HttpGet]
        public string RefreshToken()
        {
            var finalResponse = "{}";
            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));
            JObject tokens = JObject.Parse(System.IO.File.ReadAllText(tokensFile));

            RestRequest restRequest = new RestRequest();

            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            restRequest.AddParameter("client_id", credentials["client_id"].ToString());
            restRequest.AddParameter("scope", credentials["scopes"].ToString());
            restRequest.AddParameter("redirect_uri", credentials["redirect_url"].ToString());
            restRequest.AddParameter("refresh_token", tokens["refresh_token"].ToString());
            restRequest.AddParameter("grant_type", "refresh_token");
            restRequest.AddParameter("client_secret", credentials["client_secret"].ToString());

            RestClient restClient = new RestClient("https://login.microsoftonline.com/common/oauth2/v2.0/token");
            var response = restClient.Post(restRequest);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                finalResponse = response.Content;
                System.IO.File.WriteAllText(tokensFile, response.Content);
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(finalResponse, options);

            return jsonString;
        }
    }
}

