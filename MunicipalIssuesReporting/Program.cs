using MunicipalIssuesReporting;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MunicipalIssueReporting
{
    internal static class Program
    {
        // Shared in-memory store for reported issues
        public static List<Issue> Issues { get; } = new List<Issue>();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenuForm());
        }
    }
}