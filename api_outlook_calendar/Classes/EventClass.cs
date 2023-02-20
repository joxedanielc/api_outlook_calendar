using System;
namespace api_outlook_calendar
{
	public class EventClass
	{
		public EventClass()
		{
            this.Body = new BodyEvent()
            {
                ContentType = "html"
            };

            this.Start = new EventDateTime()
            {
                TimeZone = "Pacific Standard Time"
            };

            this.End = new EventDateTime()
            {
                TimeZone = "Pacific Standard Time"
            };
        }

        public EventDateTime Start { get; set; }

        public EventDateTime End { get; set; }

        public BodyEvent Body { get; set; }

        public string Subject { get; set; }

        public class BodyEvent
        {
            public string ContentType { get; set; }
            public string Content { get; set; }
        }

        public class EventDateTime
        {
            public DateTime DateTime { get; set; }
            public string TimeZone { get; set; }
        }
    }
}

