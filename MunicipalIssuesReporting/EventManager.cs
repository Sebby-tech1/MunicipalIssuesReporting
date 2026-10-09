using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalIssueReporting
{
    public static class EventManager
    {
        // SortedDictionary: events organised by date (keys are always in date order)
        public static SortedDictionary<DateTime, List<EventItem>> EventsByDate { get; } =
            new SortedDictionary<DateTime, List<EventItem>>();

        // Dictionary: quick lookup of events by category
        public static Dictionary<string, List<EventItem>> EventsByCategory { get; } =
            new Dictionary<string, List<EventItem>>(StringComparer.OrdinalIgnoreCase);

        // Sets: unique categories and unique event dates
        public static HashSet<string> Categories { get; } =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        public static HashSet<DateTime> EventDates { get; } = new HashSet<DateTime>();

        // Priority queue: urgent announcements are served first
        public static SimplePriorityQueue<EventItem> Announcements { get; } =
            new SimplePriorityQueue<EventItem>();

        // Queue: the last few categories the user searched or viewed (oldest dropped first)
        public static Queue<string> RecentInterests { get; } = new Queue<string>();

        // Stack: events the user opened (most recent on top)
        public static Stack<EventItem> RecentlyViewed { get; } = new Stack<EventItem>();

        private const int MaxInterests = 10;
        private static int _nextId = 1;

        static EventManager()
        {
            SeedEvents();
        }

        public static void AddEvent(string title, string category, DateTime date,
                                    string location, string description, int priority)
        {
            var ev = new EventItem
            {
                Id = _nextId++,
                Title = title,
                Category = category,
                Date = date.Date,
                Location = location,
                Description = description,
                Priority = priority
            };

            List<EventItem> onDate;
            if (!EventsByDate.TryGetValue(ev.Date, out onDate))
            {
                onDate = new List<EventItem>();
                EventsByDate[ev.Date] = onDate;
            }
            onDate.Add(ev);

            List<EventItem> inCategory;
            if (!EventsByCategory.TryGetValue(category, out inCategory))
            {
                inCategory = new List<EventItem>();
                EventsByCategory[category] = inCategory;
            }
            inCategory.Add(ev);

            Categories.Add(category);
            EventDates.Add(ev.Date);

            if (priority <= 2) Announcements.Enqueue(ev, priority);
        }

        public static List<EventItem> Search(string category, DateTime? fromDate, string keyword)
        {
            IEnumerable<EventItem> results;

            if (!string.IsNullOrWhiteSpace(category))
            {
                List<EventItem> inCategory;
                results = EventsByCategory.TryGetValue(category, out inCategory)
                    ? (IEnumerable<EventItem>)inCategory
                    : new List<EventItem>();
            }
            else
            {
                results = EventsByDate.Values.SelectMany(list => list);
            }

            if (fromDate.HasValue)
                results = results.Where(e => e.Date >= fromDate.Value.Date);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string k = keyword.Trim();
                results = results.Where(e =>
                    e.Title.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    e.Description.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    e.Location.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            return results.OrderBy(e => e.Date).ThenBy(e => e.Priority).ToList();
        }

        public static void RecordInterest(string category)
        {
            if (string.IsNullOrWhiteSpace(category)) return;
            RecentInterests.Enqueue(category);
            while (RecentInterests.Count > MaxInterests) RecentInterests.Dequeue();
        }

        public static void RecordSearch(string category, string keyword, List<EventItem> results)
        {
            if (!string.IsNullOrWhiteSpace(category))
            {
                RecordInterest(category);
            }
            else if (!string.IsNullOrWhiteSpace(keyword))
            {
                foreach (string c in results.Select(e => e.Category).Distinct())
                    RecordInterest(c);
            }
        }

        public static void MarkViewed(EventItem ev)
        {
            if (RecentlyViewed.Count == 0 || RecentlyViewed.Peek().Id != ev.Id)
                RecentlyViewed.Push(ev);
            RecordInterest(ev.Category);
        }

        public static List<string> TopInterests(int count)
        {
            return RecentInterests
                .GroupBy(c => c, StringComparer.OrdinalIgnoreCase)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(count)
                .ToList();
        }

        // Suggests upcoming events in the user's most common categories that they haven't opened yet
        public static List<EventItem> GetRecommendations(int max)
        {
            List<string> top = TopInterests(2);
            if (top.Count == 0) return new List<EventItem>();

            var viewedIds = new HashSet<int>(RecentlyViewed.Select(e => e.Id));

            return EventsByDate.Values
                .SelectMany(list => list)
                .Where(e => e.Date >= DateTime.Today
                            && !viewedIds.Contains(e.Id)
                            && top.Contains(e.Category, StringComparer.OrdinalIgnoreCase))
                .OrderBy(e => e.Date)
                .Take(max)
                .ToList();
        }

        // Sample data, dated relative to today so events are always upcoming
        private static void SeedEvents()
        {
            DateTime d = DateTime.Today;

            AddEvent("Water Supply Interruption", "Utilities", d.AddDays(1), "Northern suburbs",
                "Planned maintenance on the main pipeline from 08:00 to 16:00. Please store water in advance.", 1);
            AddEvent("Main Road Resurfacing: Road Closure", "Roads", d.AddDays(2), "Main Road, CBD",
                "Lanes will be closed between 06:00 and 18:00. Use alternative routes.", 2);
            AddEvent("Load Shedding Schedule Update", "Utilities", d.AddDays(2), "All areas",
                "A revised load shedding timetable takes effect. Check your area schedule on the municipal website.", 2);
            AddEvent("Community Clean-Up Day", "Community", d.AddDays(3), "Central Park",
                "Join your neighbours to clean up the park. Gloves and bags are provided.", 3);
            AddEvent("Free Health Screening Clinic", "Health", d.AddDays(4), "Community Hall",
                "Free blood pressure, glucose and BMI checks for all residents.", 3);
            AddEvent("Heritage Day Street Festival", "Culture", d.AddDays(5), "Church Street",
                "Food stalls, live music and traditional dance performances.", 3);
            AddEvent("Municipal Soccer Tournament", "Sports", d.AddDays(6), "Municipal Stadium",
                "Local teams compete for the mayor's trophy. Entry is free.", 3);
            AddEvent("Town Hall Public Meeting: Budget", "Community", d.AddDays(7), "Town Hall",
                "Residents are invited to comment on the proposed municipal budget.", 3);
            AddEvent("Recycling Drop-Off Weekend", "Environment", d.AddDays(8), "Civic Centre car park",
                "Bring paper, glass, plastic and e-waste for free recycling.", 3);
            AddEvent("Youth Career Expo", "Education", d.AddDays(9), "Library Hall",
                "Meet employers and training providers. Bring your CV.", 3);
            AddEvent("Charity Fun Run", "Sports", d.AddDays(10), "Riverside Park",
                "5 km and 10 km routes. All proceeds go to local charities.", 3);
            AddEvent("Flu Vaccination Drive", "Health", d.AddDays(11), "Local clinics",
                "Free flu vaccinations for pensioners and children under five.", 3);
            AddEvent("Art and Craft Market", "Culture", d.AddDays(12), "Market Square",
                "Local artists and crafters showcase and sell their work.", 3);
            AddEvent("Neighbourhood Watch Training", "Safety", d.AddDays(13), "Police Station Hall",
                "Learn how to start and run a neighbourhood watch in your area.", 3);
        }
    }
}