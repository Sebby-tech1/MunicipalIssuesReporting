using System;

namespace MunicipalIssueReporting
{
    public class EventItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        // 1 = urgent announcement, 2 = announcement, 3 = normal event
        public int Priority { get; set; } = 3;

        public override string ToString()
        {
            return Date.ToString("dd MMM yyyy") + " - " + Title;
        }
    }
}