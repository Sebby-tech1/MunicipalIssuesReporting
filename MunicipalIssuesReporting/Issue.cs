using System;

namespace MunicipalIssueReporting
{
    public class Issue
    {
        public int Id { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AttachmentPath { get; set; }
        public DateTime ReportedAt { get; set; } = DateTime.Now;
    }
}