using System;
using System.Drawing;
using System.Windows.Forms;

namespace MunicipalIssueReporting
{
    partial class MainMenuForm
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel layout;
        private Label lblTitle;
        private Button btnReportIssues;
        private Button btnLocalEvents;
        private Button btnServiceStatus;
        private Button btnExit;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private static void StyleButton(Button b)
        {
            b.Dock = DockStyle.Fill;
            b.Margin = new Padding(40, 8, 40, 8);
            b.FlatStyle = FlatStyle.Flat;
            b.BackColor = Color.SteelBlue;
            b.ForeColor = Color.White;
            b.Font = new Font("Segoe UI", 11F);
            b.UseVisualStyleBackColor = false;
        }

        private void InitializeComponent()
        {
            layout = new TableLayoutPanel();
            lblTitle = new Label();
            btnReportIssues = new Button();
            btnLocalEvents = new Button();
            btnServiceStatus = new Button();
            btnExit = new Button();
            layout.SuspendLayout();
            SuspendLayout();

            layout.Dock = DockStyle.Fill;
            layout.ColumnCount = 1;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.RowCount = 5;
            for (int i = 0; i < 5; i++)
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            layout.Padding = new Padding(10);
            layout.Controls.Add(lblTitle, 0, 0);
            layout.Controls.Add(btnReportIssues, 0, 1);
            layout.Controls.Add(btnLocalEvents, 0, 2);
            layout.Controls.Add(btnServiceStatus, 0, 3);
            layout.Controls.Add(btnExit, 0, 4);

            lblTitle.Dock = DockStyle.Fill;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.SteelBlue;
            lblTitle.Text = "Municipal Services";

            btnReportIssues.Text = "Report Issues";
            btnReportIssues.Click += btnReportIssues_Click;

            btnLocalEvents.Text = "Local Events and Announcements";
            btnLocalEvents.Click += btnLocalEvents_Click;

            btnServiceStatus.Text = "Service Request Status";
            btnServiceStatus.Enabled = false;

            btnExit.Text = "Exit";
            btnExit.Click += btnExit_Click;

            StyleButton(btnReportIssues);
            StyleButton(btnLocalEvents);
            StyleButton(btnServiceStatus);
            StyleButton(btnExit);
            btnExit.BackColor = Color.DimGray;

            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(400, 450);
            MinimumSize = new Size(360, 420);
            Controls.Add(layout);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Main Menu";
            layout.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}