using System;
using System.Threading;
using System.Windows.Forms;
using Rowatch2.Forms;

namespace Rowatch2
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.ThreadException += Application_ThreadException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain(args));
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("Exception occured:\n" + e.Exception.ToString(), "Exception",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}