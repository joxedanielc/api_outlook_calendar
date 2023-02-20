using System;
namespace api_outlook_calendar
{
	public class EventDataClass
	{
		public EventDataClass()
		{
        }

        public string id { get; set; }

        public EventDateTime start { get; set; }

        public EventDateTime end { get; set; }

        public BodyEvent body { get; set; }

        public string subject { get; set; }

        public class BodyEvent
        {
            public string contentType { get; set; }
            public string content { get; set; }
        }

        public class EventDateTime
        {
            public DateTime dateTime { get; set; }
            public string timeZone { get; set; }
        }
    }
}

