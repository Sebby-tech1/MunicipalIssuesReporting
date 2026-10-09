using System;
using System.Drawing;
using System.Windows.Forms;

namespace MunicipalIssueReporting
{
    partial class LocalEventsForm
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel layout, content;
        private FlowLayoutPanel filterRow;
        private Label lblTitle, lblAnnouncement, lblCategory, lblFrom, lblKeyword;
        private Label lblEventsHeader, lblDetailsHeader, lblRecommended, lblRecent;
        private ComboBox cmbCategory;
        private DateTimePicker dtpFrom;
        private TextBox txtKeyword;
        private Button btnSearch, btnClear, btnBack;
        private ListView lvEvents;
        private RichTextBox rtbDetails;
        private ListBox lstRecommended, lstRecent;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private static void StyleButton(Button b, string text)
        {
            b.Text = text;
            b.FlatStyle = FlatStyle.Flat;
            b.BackColor = Color.SteelBlue;
            b.ForeColor = Color.White;
            b.UseVisualStyleBackColor = false;
            b.AutoSize = true;
            b.Margin = new Padding(10, 3, 0, 3);
        }

        private static void StyleHeader(Label l, string text, int left)
        {
            l.AutoSize = true;
            l.Text = text;
            l.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            l.ForeColor = Color.SteelBlue;
            l.Margin = new Padding(left, 8, 0, 3);
        }

        private static void StyleFilterLabel(Label l, string text)
        {
            l.AutoSize = true;
            l.Text = text;
            l.Margin = new Padding(8, 8, 3, 0);
        }

        private void InitializeComponent()
        {
            layout = new TableLayoutPanel();
            content = new TableLayoutPanel();
            filterRow = new FlowLayoutPanel();
            lblTitle = new Label();
            lblAnnouncement = new Label();
            lblCategory = new Label();
            lblFrom = new Label();
            lblKeyword = new Label();
            lblEventsHeader = new Label();
            lblDetailsHeader = new Label();
            lblRecommended = new Label();
            lblRecent = new Label();
            cmbCategory = new ComboBox();
            dtpFrom = new DateTimePicker();
            txtKeyword = new TextBox();
            btnSearch = new Button();
            btnClear = new Button();
            btnBack = new Button();
            lvEvents = new ListView();
            rtbDetails = new RichTextBox();
            lstRecommended = new ListBox();
            lstRecent = new ListBox();
            layout.SuspendLayout();
            content.SuspendLayout();
            filterRow.SuspendLayout();
            SuspendLayout();

            // Title and announcement banner
            lblTitle.AutoSize = true;
            lblTitle.Text = "Local Events and Announcements";
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.SteelBlue;
            lblTitle.Margin = new Padding(0, 0, 0, 8);

            lblAnnouncement.AutoSize = false;
            lblAnnouncement.Dock = DockStyle.Fill;
            lblAnnouncement.Height = 34;
            lblAnnouncement.AutoEllipsis = true;
            lblAnnouncement.TextAlign = ContentAlignment.MiddleLeft;
            lblAnnouncement.Padding = new Padding(10, 0, 0, 0);
            lblAnnouncement.BackColor = Color.Firebrick;
            lblAnnouncement.ForeColor = Color.White;
            lblAnnouncement.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAnnouncement.Margin = new Padding(0, 0, 0, 8);

            // Search and filter row
            StyleFilterLabel(lblCategory, "Category:");
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Width = 150;

            StyleFilterLabel(lblFrom, "From date:");
            dtpFrom.Format = DateTimePickerFormat.Short;
            dtpFrom.ShowCheckBox = true;
            dtpFrom.Width = 130;

            StyleFilterLabel(lblKeyword, "Keyword:");
            txtKeyword.Width = 150;

            StyleButton(btnSearch, "Search");
            btnSearch.Click += btnSearch_Click;
            StyleButton(btnClear, "Clear");
            btnClear.BackColor = Color.DimGray;
            btnClear.Click += btnClear_Click;

            filterRow.Dock = DockStyle.Fill;
            filterRow.AutoSize = true;
            filterRow.WrapContents = true;
            filterRow.Controls.Add(lblCategory);
            filterRow.Controls.Add(cmbCategory);
            filterRow.Controls.Add(lblFrom);
            filterRow.Controls.Add(dtpFrom);
            filterRow.Controls.Add(lblKeyword);
            filterRow.Controls.Add(txtKeyword);
            filterRow.Controls.Add(btnSearch);
            filterRow.Controls.Add(btnClear);

            // Main content area
            StyleHeader(lblEventsHeader, "Upcoming events and announcements", 0);
            StyleHeader(lblDetailsHeader, "Event details", 0);
            StyleHeader(lblRecommended, "Recommended for you", 10);
            StyleHeader(lblRecent, "Recently viewed", 10);

            lvEvents.Dock = DockStyle.Fill;
            lvEvents.View = View.Details;
            lvEvents.FullRowSelect = true;
            lvEvents.GridLines = true;
            lvEvents.HideSelection = false;
            lvEvents.MultiSelect = false;
            lvEvents.Font = new Font("Segoe UI", 9.5F);
            lvEvents.Columns.Add("Date", 130);
            lvEvents.Columns.Add("Title", 230);
            lvEvents.Columns.Add("Category", 100);
            lvEvents.Columns.Add("Location", 150);
            lvEvents.SelectedIndexChanged += lvEvents_SelectedIndexChanged;

            rtbDetails.Dock = DockStyle.Fill;
            rtbDetails.ReadOnly = true;
            rtbDetails.BackColor = Color.White;
            rtbDetails.BorderStyle = BorderStyle.FixedSingle;
            rtbDetails.Text = "Select an event to see its details.";

            lstRecommended.Dock = DockStyle.Fill;
            lstRecommended.IntegralHeight = false;
            lstRecommended.HorizontalScrollbar = true;
            lstRecommended.Font = new Font("Segoe UI", 9.5F);
            lstRecommended.Margin = new Padding(10, 0, 0, 0);
            lstRecommended.SelectedIndexChanged += lstRecommended_SelectedIndexChanged;

            lstRecent.Dock = DockStyle.Fill;
            lstRecent.IntegralHeight = false;
            lstRecent.HorizontalScrollbar = true;
            lstRecent.Font = new Font("Segoe UI", 9.5F);
            lstRecent.Margin = new Padding(10, 0, 0, 0);
            lstRecent.SelectedIndexChanged += lstRecent_SelectedIndexChanged;

            content.Dock = DockStyle.Fill;
            content.ColumnCount = 2;
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62F));
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
            content.RowCount = 4;
            content.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            content.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            content.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            content.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            content.Controls.Add(lblEventsHeader, 0, 0);
            content.Controls.Add(lvEvents, 0, 1);
            content.Controls.Add(lblDetailsHeader, 0, 2);
            content.Controls.Add(rtbDetails, 0, 3);
            content.Controls.Add(lblRecommended, 1, 0);
            content.Controls.Add(lstRecommended, 1, 1);
            content.Controls.Add(lblRecent, 1, 2);
            content.Controls.Add(lstRecent, 1, 3);

            // Back button
            StyleButton(btnBack, "Back to Main Menu");
            btnBack.AutoSize = false;
            btnBack.Dock = DockStyle.Right;
            btnBack.Width = 180;
            btnBack.Height = 40;
            btnBack.BackColor = Color.DimGray;
            btnBack.Margin = new Padding(0, 10, 0, 0);
            btnBack.Click += btnBack_Click;

            // Outer layout
            layout.Dock = DockStyle.Fill;
            layout.Padding = new Padding(20);
            layout.ColumnCount = 1;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.RowCount = 5;
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.Controls.Add(lblTitle, 0, 0);
            layout.Controls.Add(lblAnnouncement, 0, 1);
            layout.Controls.Add(filterRow, 0, 2);
            layout.Controls.Add(content, 0, 3);
            layout.Controls.Add(btnBack, 0, 4);

            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AcceptButton = btnSearch;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(920, 640);
            MinimumSize = new Size(780, 540);
            Controls.Add(layout);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Local Events and Announcements";

            filterRow.ResumeLayout(false);
            filterRow.PerformLayout();
            content.ResumeLayout(false);
            content.PerformLayout();
            layout.ResumeLayout(false);
            layout.PerformLayout();
            ResumeLayout(false);
        }
    }
}