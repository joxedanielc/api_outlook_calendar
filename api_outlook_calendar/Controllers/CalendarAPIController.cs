using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using static api_outlook_calendar.EventClass;
using System.Text.Json;
using System.Dynamic;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_outlook_calendar.Controllers
{
    [Route("api/[controller]")]
    public class CalendarAPIController : Controller
    {
        string tokensFile = "Files/tokens.json";

        // GET api/events
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

        // GET api/events/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public EventClass Get(int id)
        {
            var response = new EventClass();

            return response;
        }

        // POST api/events
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
            //restRequest.AddParameter("application/json", newEvent);
            restRequest.AddJsonBody(newEvent);


            RestClient restClient = new RestClient("https://graph.microsoft.com/v1.0/me/calendar/events");
            var response = restClient.Post(restRequest);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var content = response.Content;
                EventDataClass? weatherForecast =
                JsonSerializer.Deserialize<EventDataClass>(content);
                finalResponse = weatherForecast;
            }

            return finalResponse;
        }

        // PUT api/events/5
        [HttpPut("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public void Put([FromBody] EventClass updatedEvent)
        {
        }

        // DELETE api/events/5
        [HttpDelete("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public void Delete()
        {
        }
    }
}

