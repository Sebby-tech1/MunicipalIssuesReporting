using System;
using System.Windows.Forms;

namespace MunicipalIssueReporting
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void btnReportIssues_Click(object sender, EventArgs e)
        {
            using (var form = new ReportIssueForm())
            {
                Hide();
                form.ShowDialog();
                Show();
            }
        }

        private void btnLocalEvents_Click(object sender, EventArgs e)
        {
            using (var form = new LocalEventsForm())
            {
                Hide();
                form.ShowDialog();
                Show();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}