using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalIssueReporting
{
    public partial class LocalEventsForm : Form
    {
        private bool _refreshing;

        public LocalEventsForm()
        {
            InitializeComponent();

            // Unique categories (a set) fill the dropdown
            cmbCategory.Items.Add("All Categories");
            foreach (string category in EventManager.Categories.OrderBy(c => c))
                cmbCategory.Items.Add(category);
            cmbCategory.SelectedIndex = 0;

            // Unique event dates (a set) limit the date picker to the range that has events
            dtpFrom.MinDate = EventManager.EventDates.Min();
            dtpFrom.MaxDate = EventManager.EventDates.Max();
            dtpFrom.Checked = false;

            ShowAnnouncementBanner();
            ShowEvents(EventManager.Search(null, null, null));
            RefreshSidePanels();
        }

        private void ShowAnnouncementBanner()
        {
            if (EventManager.Announcements.Count == 0)
            {
                lblAnnouncement.Text = "No announcements at the moment.";
                return;
            }

            EventItem top = EventManager.Announcements.Peek();
            int more = EventManager.Announcements.Count - 1;
            lblAnnouncement.Text = "ANNOUNCEMENT: " + top.Title +
                " (" + top.Date.ToString("dd MMM") + ")" +
                (more > 0 ? "   +" + more + " more" : "");
        }

        private void ShowEvents(List<EventItem> events)
        {
            lvEvents.BeginUpdate();
            lvEvents.Items.Clear();

            foreach (EventItem ev in events)
            {
                var item = new ListViewItem(new[]
                {
                    ev.Date.ToString("ddd, dd MMM yyyy"), ev.Title, ev.Category, ev.Location
                });
                item.Tag = ev;
                if (ev.Priority == 1) item.ForeColor = Color.Firebrick;
                else if (ev.Priority == 2) item.ForeColor = Color.DarkOrange;
                lvEvents.Items.Add(item);
            }

            lvEvents.EndUpdate();
            lblEventsHeader.Text = "Upcoming events and announcements (" + events.Count + ")";
        }

        private void RefreshSidePanels()
        {
            _refreshing = true;

            lstRecommended.Items.Clear();
            List<EventItem> recommendations = EventManager.GetRecommendations(5);
            if (recommendations.Count == 0)
            {
                lstRecommended.Items.Add("Search or open events to get suggestions.");
            }
            else
            {
                foreach (EventItem ev in recommendations) lstRecommended.Items.Add(ev);
            }

            List<string> top = EventManager.TopInterests(2);
            lblRecommended.Text = top.Count == 0
                ? "Recommended for you"
                : "Recommended (based on " + string.Join(", ", top) + ")";

            lstRecent.Items.Clear();
            foreach (EventItem ev in EventManager.RecentlyViewed.Take(5))
                lstRecent.Items.Add(ev);

            _refreshing = false;
        }

        private void AppendFormatted(string text, Font font)
        {
            rtbDetails.SelectionStart = rtbDetails.TextLength;
            rtbDetails.SelectionLength = 0;
            rtbDetails.SelectionFont = font;
            rtbDetails.AppendText(text);
        }

        private void ShowDetails(EventItem ev)
        {
            rtbDetails.Clear();
            AppendFormatted(ev.Title + Environment.NewLine,
                new Font("Segoe UI", 12F, FontStyle.Bold));
            AppendFormatted(ev.Date.ToString("dddd, dd MMMM yyyy") + "  |  " + ev.Category +
                "  |  " + ev.Location + Environment.NewLine + Environment.NewLine,
                new Font("Segoe UI", 9.5F, FontStyle.Italic));
            AppendFormatted(ev.Description, new Font("Segoe UI", 10F));
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string category = cmbCategory.SelectedIndex > 0
                ? cmbCategory.SelectedItem.ToString() : null;
            DateTime? fromDate = dtpFrom.Checked ? (DateTime?)dtpFrom.Value.Date : null;
            string keyword = txtKeyword.Text.Trim();

            List<EventItem> results = EventManager.Search(category, fromDate, keyword);
            EventManager.RecordSearch(category, keyword, results);

            ShowEvents(results);
            RefreshSidePanels();

            if (results.Count == 0)
            {
                MessageBox.Show("No events matched your search. Try a different category, date or keyword.",
                    "No results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbCategory.SelectedIndex = 0;
            dtpFrom.Checked = false;
            txtKeyword.Clear();
            rtbDetails.Clear();
            ShowEvents(EventManager.Search(null, null, null));
        }

        private void lvEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvEvents.SelectedItems.Count == 0) return;

            var ev = (EventItem)lvEvents.SelectedItems[0].Tag;
            ShowDetails(ev);
            EventManager.MarkViewed(ev);
            RefreshSidePanels();
        }

        private void lstRecommended_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_refreshing) return;
            var ev = lstRecommended.SelectedItem as EventItem;
            if (ev == null) return;

            ShowDetails(ev);
            EventManager.MarkViewed(ev);
            RefreshSidePanels();
        }

        private void lstRecent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_refreshing) return;
            var ev = lstRecent.SelectedItem as EventItem;
            if (ev != null) ShowDetails(ev);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}