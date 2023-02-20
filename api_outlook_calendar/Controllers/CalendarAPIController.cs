using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_outlook_calendar.Controllers
{
    [Route("api/[controller]")]
    public class CalendarAPIController : Controller
    {
        string tokensFile = "Files/tokens.json";

        // GET
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IEnumerable<EventDataClass> Get()
        {
            List<EventDataClass> finalResponse = new List<EventDataClass>();
            JObject tokens = JObject.Parse(System.IO.File.ReadAllText(tokensFile));

            RestRequest restRequest = new RestRequest();

            restRequest.AddHeader("Authorization", "Bearer " + tokens["access_token"].ToString());
            restRequest.AddHeader("Content-Type", "application/json");

            RestClient restClient = new RestClient("https://graph.microsoft.com/v1.0/me/calendar/events");
            var response = restClient.Get(restRequest);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JObject content = JObject.Parse(response.Content);
                var eventValues = content["value"].ToObject<IEnumerable<EventDataClass>>();

                foreach (var eventData in eventValues)
                {
                    finalResponse.Add(eventData);
                }
            }

            return finalResponse.ToArray<EventDataClass>();
        }

        // GET
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public EventDataClass Get(string id)
        {
            var finalResponse = new EventDataClass();
            JObject tokens = JObject.Parse(System.IO.File.ReadAllText(tokensFile));

            RestRequest restRequest = new RestRequest();

            restRequest.AddHeader("Authorization", "Bearer " + tokens["access_token"].ToString());
            restRequest.AddHeader("Content-Type", "application/json");

            RestClient restClient = new RestClient($"https://graph.microsoft.com/v1.0/me/calendar/events/{id}");
            var response = restClient.Get(restRequest);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content;
                EventDataClass? responseValue =
                JsonSerializer.Deserialize<EventDataClass>(content);
                finalResponse = responseValue;

            }

            return finalResponse;
        }

        // POST
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public EventDataClass Post([FromBody] EventClass newEvent)
        {
            var finalResponse = new EventDataClass();

            JObject tokens = JObject.Parse(System.IO.File.ReadAllText(tokensFile));

            RestRequest restRequest = new RestRequest();

            restRequest.AddHeader("Authorization", "Bearer " + tokens["access_token"].ToString());
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddJsonBody(newEvent);

            RestClient restClient = new RestClient("https://graph.microsoft.com/v1.0/me/calendar/events");
            var response = restClient.Post(restRequest);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var content = response.Content;
                EventDataClass? responseValue =
                JsonSerializer.Deserialize<EventDataClass>(content);
                finalResponse = responseValue;
            }

            return finalResponse;
        }

        // PUT
        [HttpPut("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public Tuple<EventDataClass?, string?> Put(string id, [FromBody] EventClass updatedEvent)
        {
            JObject tokens = JObject.Parse(System.IO.File.ReadAllText(tokensFile));

            RestRequest restRequest = new RestRequest();

            string errorResponse = "";
            var eventDataResponse = new EventDataClass();

            restRequest.AddHeader("Authorization", "Bearer " + tokens["access_token"].ToString());
            restRequest.AddHeader("Content-Type", "application/json");
            //restRequest.AddParameter("application/json", newEvent);
            restRequest.AddJsonBody(updatedEvent);


            RestClient restClient = new RestClient($"https://graph.microsoft.com/v1.0/me/calendar/events/{id}");
            var response = restClient.Patch(restRequest);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content;
                EventDataClass? responseValue =
                JsonSerializer.Deserialize<EventDataClass>(content);
                eventDataResponse = responseValue;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                errorResponse = $"The event with id: '{id}' was not found.";
            }

            return Tuple.Create<EventDataClass?, string?>(eventDataResponse, errorResponse);
        }

        // DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public string Delete(string id)
        {
            string finalResponse = "";
            JObject tokens = JObject.Parse(System.IO.File.ReadAllText(tokensFile));

            RestRequest restRequest = new RestRequest();

            restRequest.AddHeader("Authorization", "Bearer " + tokens["access_token"].ToString());
            restRequest.AddHeader("Content-Type", "application/json");

            RestClient restClient = new RestClient($"https://graph.microsoft.com/v1.0/me/calendar/events/{id}");
            var response = restClient.Delete(restRequest);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                finalResponse = $"The event with id: '{id}' was deleted.";

            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                finalResponse = $"The event with id: '{id}' was not found.";
            }

            return finalResponse;
        }
    }
}

