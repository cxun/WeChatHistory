using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WechatHistory
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "出错啦！");
            }
        }
    }
}
