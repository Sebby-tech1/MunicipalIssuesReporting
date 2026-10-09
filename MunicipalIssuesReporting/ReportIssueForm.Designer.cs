using System;
using System.Drawing;
using System.Windows.Forms;

namespace MunicipalIssueReporting
{
    partial class ReportIssueForm
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel layout, buttonRow;
        private FlowLayoutPanel attachRow;
        private Label lblLocation, lblCategory, lblDescription, lblAttachment, lblEngagement;
        private TextBox txtLocation;
        private ComboBox cmbCategory;
        private RichTextBox rtbDescription;
        private Button btnAttach, btnSubmit, btnBack;
        private ProgressBar progressBar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private static void StyleLabel(Label l, string text)
        {
            l.AutoSize = true;
            l.Text = text;
            l.Margin = new Padding(0, 10, 0, 3);
            l.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        private static void StyleButton(Button b, string text)
        {
            b.Text = text;
            b.FlatStyle = FlatStyle.Flat;
            b.BackColor = Color.SteelBlue;
            b.ForeColor = Color.White;
            b.UseVisualStyleBackColor = false;
        }

        private void InitializeComponent()
        {
            layout = new TableLayoutPanel();
            buttonRow = new TableLayoutPanel();
            attachRow = new FlowLayoutPanel();
            lblLocation = new Label();
            lblCategory = new Label();
            lblDescription = new Label();
            lblAttachment = new Label();
            lblEngagement = new Label();
            txtLocation = new TextBox();
            cmbCategory = new ComboBox();
            rtbDescription = new RichTextBox();
            btnAttach = new Button();
            btnSubmit = new Button();
            btnBack = new Button();
            progressBar = new ProgressBar();
            layout.SuspendLayout();
            buttonRow.SuspendLayout();
            attachRow.SuspendLayout();
            SuspendLayout();

            StyleLabel(lblLocation, "Location:");
            StyleLabel(lblCategory, "Category:");
            StyleLabel(lblDescription, "Description:");

            txtLocation.Dock = DockStyle.Fill;
            txtLocation.TextChanged += rtbDescription_TextChanged;

            cmbCategory.Dock = DockStyle.Fill;
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.SelectedIndexChanged += rtbDescription_TextChanged;

            rtbDescription.Dock = DockStyle.Fill;
            rtbDescription.MinimumSize = new Size(0, 80);
            rtbDescription.TextChanged += rtbDescription_TextChanged;

            StyleButton(btnAttach, "Attach File...");
            btnAttach.AutoSize = true;
            btnAttach.Click += btnAttach_Click;

            lblAttachment.AutoSize = true;
            lblAttachment.Text = "No file attached";
            lblAttachment.Margin = new Padding(10, 8, 0, 0);

            attachRow.Dock = DockStyle.Fill;
            attachRow.AutoSize = true;
            attachRow.Controls.Add(btnAttach);
            attachRow.Controls.Add(lblAttachment);

            lblEngagement.AutoSize = true;
            lblEngagement.ForeColor = Color.SeaGreen;
            lblEngagement.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblEngagement.Margin = new Padding(0, 15, 0, 3);
            lblEngagement.Text = "Let's get started: tell us where the problem is.";

            progressBar.Dock = DockStyle.Fill;

            StyleButton(btnSubmit, "Submit");
            btnSubmit.Dock = DockStyle.Fill;
            btnSubmit.Click += btnSubmit_Click;

            StyleButton(btnBack, "Back to Main Menu");
            btnBack.Dock = DockStyle.Fill;
            btnBack.BackColor = Color.DimGray;
            btnBack.Click += btnBack_Click;

            buttonRow.Dock = DockStyle.Fill;
            buttonRow.ColumnCount = 2;
            buttonRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonRow.RowCount = 1;
            buttonRow.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            buttonRow.Height = 55;
            buttonRow.Margin = new Padding(0, 15, 0, 0);
            buttonRow.Controls.Add(btnSubmit, 0, 0);
            buttonRow.Controls.Add(btnBack, 1, 0);

            layout.Dock = DockStyle.Fill;
            layout.Padding = new Padding(20);
            layout.ColumnCount = 1;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.RowCount = 10;
            for (int i = 0; i < 10; i++)
                layout.RowStyles.Add(i == 5 ? new RowStyle(SizeType.Percent, 100F)
                                            : new RowStyle(SizeType.AutoSize));
            layout.Controls.Add(lblLocation, 0, 0);
            layout.Controls.Add(txtLocation, 0, 1);
            layout.Controls.Add(lblCategory, 0, 2);
            layout.Controls.Add(cmbCategory, 0, 3);
            layout.Controls.Add(lblDescription, 0, 4);
            layout.Controls.Add(rtbDescription, 0, 5);
            layout.Controls.Add(attachRow, 0, 6);
            layout.Controls.Add(lblEngagement, 0, 7);
            layout.Controls.Add(progressBar, 0, 8);
            layout.Controls.Add(buttonRow, 0, 9);

            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(520, 560);
            MinimumSize = new Size(420, 520);
            Controls.Add(layout);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Report an Issue";
            attachRow.ResumeLayout(false);
            attachRow.PerformLayout();
            buttonRow.ResumeLayout(false);
            layout.ResumeLayout(false);
            layout.PerformLayout();
            ResumeLayout(false);
        }
    }
}