using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Lisa
{
    public class Program : Form
    {
        [STAThread]
        public static void Main()
        {
            Application.Run(new Program());
        }

        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;

        public Program()
        {
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Exit", OnExit);

            trayIcon = new NotifyIcon();
            trayIcon.Text = "Lisa";
            trayIcon.Icon = new Icon(SystemIcons.Information, 40, 40);

            // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;

            var thr = new Thread(new ThreadStart(Lisa.Listen));
            thr.IsBackground = true;
            thr.Start();
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;

            base.OnLoad(e);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                trayIcon.Dispose();
            }

            base.Dispose(isDisposing);
        }
    }
}
