using System;

namespace EntityLayer.Dto
{
    public class ContentCalendarDto
    {
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public bool allDay { get; set; }
    }
}