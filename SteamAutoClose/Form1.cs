using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamAutoClose
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Visible = false;
            ShowInTaskbar = false;
            ShowIcon = false;
            SizeGripStyle = SizeGripStyle.Hide;
            Opacity = 0.0;
            WindowState = FormWindowState.Minimized;
        }

        private void timer_SteamClose_Tick(object sender, EventArgs e)
        {
            bool closed = false;

            Process[] ps = Process.GetProcessesByName("Steam");
            for(int i = 0; i < ps.Length; i++)
            {
                Process p = ps[i];
                try
                {
                    IntPtr mainHandle = p.MainWindowHandle;

                    p.CloseMainWindow();
                }
                catch
                {
                }
                finally
                {
                    p.Dispose();
                    p = null;
                }
                closed = true;
            }
            if (closed)
            {
                timer_SteamClose.Stop();
                timer_MainClose.Stop();
                Application.Exit();
            }
        }

        private void timer_MainClose_Tick(object sender, EventArgs e)
        {
            timer_SteamClose.Stop();
            timer_MainClose.Stop();
            Application.Exit();
        }
    }
}
