using System;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MunicipalIssueReporting
{
    public partial class ReportIssueForm : Form
    {
        private string _attachmentPath;

        public ReportIssueForm()
        {
            InitializeComponent();
            cmbCategory.Items.AddRange(new object[]
            {
                "Sanitation", "Roads", "Utilities", "Water", "Electricity", "Other"
            });
            cmbCategory.SelectedIndex = 0;
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Images and Documents|*.jpg;*.jpeg;*.png;*.pdf;*.docx|All files|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _attachmentPath = dialog.FileName;
                    lblAttachment.Text = Path.GetFileName(_attachmentPath);
                }
            }
        }

        private void rtbDescription_TextChanged(object sender, EventArgs e)
        {
            int progress = 0;
            if (!string.IsNullOrWhiteSpace(txtLocation.Text)) progress += 33;
            if (cmbCategory.SelectedIndex >= 0) progress += 33;
            if (!string.IsNullOrWhiteSpace(rtbDescription.Text)) progress += 34;
            progressBar.Value = Math.Min(progress, 100);

            string message;
            if (progress >= 100)
                message = "Thank you for helping improve our community. Ready to submit!";
            else if (progress >= 66)
                message = "Almost there! Add a description to finish your report.";
            else if (progress >= 33)
                message = "Great start! Keep going.";
            else
                message = "Let's get started: tell us where the problem is.";

            lblEngagement.Text = message;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLocation.Text) ||
                string.IsNullOrWhiteSpace(rtbDescription.Text))
            {
                MessageBox.Show("Please enter a location and description.",
                    "Missing information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var issue = new Issue
            {
                Id = Program.Issues.Count + 1,
                Location = txtLocation.Text.Trim(),
                Category = cmbCategory.SelectedItem?.ToString() ?? "Other",
                Description = rtbDescription.Text.Trim(),
                AttachmentPath = _attachmentPath
            };

            Program.Issues.Add(issue);

            MessageBox.Show("Issue #" + issue.Id + " submitted successfully. Thank you!",
                "Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}