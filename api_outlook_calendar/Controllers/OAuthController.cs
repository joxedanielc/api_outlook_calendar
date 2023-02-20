using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_outlook_calendar.Controllers
{
    [Route("api/[controller]")]
    public class OAuthController : Controller
    {
        string credentialsFile = "Files/credentials.json";
        
        [HttpGet]
        public ActionResult OauthRedirect()
        {

            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));

            var responseUri = "";

            RestRequest restRequest = new RestRequest();
            restRequest.AddParameter("client_id", credentials["client_id"].ToString());
            restRequest.AddParameter("scope", credentials["scopes"].ToString());
            restRequest.AddParameter("redirect_uri", credentials["redirect_url"].ToString());
            restRequest.AddParameter("response_type", "code");
            restRequest.AddParameter("state", "testcalendaroutlook");
            restRequest.AddParameter("response_mode", "query");


            RestClient restClient = new RestClient("https://login.microsoftonline.com/common/oauth2/v2.0/authorize?");
            var response = restClient.Get(restRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                responseUri = response.ResponseUri.ToString();
            }

            return Redirect(responseUri);
        }
    }
}

