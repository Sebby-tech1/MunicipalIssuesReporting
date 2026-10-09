## Requirements
- Windows 10/11
- Visual Studio 2022 with the ".NET desktop development" workload
- .NET Framework 4.8

## How to compile
1. Open `MunicipalIssueReporting.sln` in Visual Studio.
2. Choose Build > Rebuild Solution (Ctrl+Shift+B).

## How to run
Press F5 in Visual Studio, or run `bin\Debug\MunicipalIssueReporting.exe`
after building.

## Local Events and Announcements (Part 2)
1. From the Main Menu click "Local Events and Announcements".
2. Use Category, From date (tick the box to enable it) and Keyword, then click Search.
3. Click an event to see its details. Red rows are urgent announcements.
4. "Recommended" suggests upcoming events based on the categories you
   searched or opened most. "Recently viewed" lists events you opened.

### Data structures used
- SortedDictionary: events organised by date (EventsByDate)
- Dictionary: events looked up by category (EventsByCategory)
- HashSet: unique categories and unique event dates
- Stack: recently viewed events
- Queue: the last 10 categories searched or viewed (drives recommendations)
- Priority queue (SimplePriorityQueue): urgent announcements are shown first

Sample events are generated relative to today's date, so they are always upcoming.