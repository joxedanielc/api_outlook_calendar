using System;
namespace api_outlook_calendar
{
	public class EventClass
	{
		public EventClass()
		{
            this.Body = new BodyEvnt()
            {
                ContentType = "html"
            };

            this.Start = new EventDT()
            {
                TimeZone = "Pacific Standard Time"
            };

            this.End = new EventDT()
            {
                TimeZone = "Pacific Standard Time"
            };
        }

        public EventDT Start { get; set; }

        public EventDT End { get; set; }

        public BodyEvnt Body { get; set; }

        public string Subject { get; set; }

        public class BodyEvnt
        {
            public string ContentType { get; set; }
            public string Content { get; set; }
        }

        public class EventDT
        {
            public DateTime DateTime { get; set; }
            public string TimeZone { get; set; }
        }
    }
}

