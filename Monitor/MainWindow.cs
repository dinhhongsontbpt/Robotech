using CefSharp;
using CefSharp.WinForms;
using CefSharp.WinForms.Host;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelDownload
{
    public partial class MainWindow : KryptonForm
    {
        private ChromiumWebBrowser chromeBrowser1 = new ChromiumWebBrowser();
        private ChromiumWebBrowser chromeBrowser2 = new ChromiumWebBrowser();
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }
        #region Initial
        private void Init()
        {
            InitObject();
            InitDisplay();
            PerformDelayedMinimizeMaximize();
        }
        private string filePath = "links.txt";
        private void InitObject()
        {
            CheckAndCreateFile();
            try
            {
                string[] links = File.ReadAllLines(filePath);
                if (links.Length >= 2)
                {
                    chromiumHostControl1.Controls.Add(chromeBrowser1);
                    chromiumHostControl2.Controls.Add(chromeBrowser2);
                    chromeBrowser1.Load(links[0]);
                    chromeBrowser1.Load(links[1]);
                    lbCam1.Text = "Cam Left: " + links[0];
                    lbCam2.Text = "Cam Right: " + links[1];
                }
                else
                {
                    MessageBox.Show("File links.txt phải chứa ít nhất 2 link.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
        private void InitDisplay()
        {
            ucDateTime.IsRun = true;
            labelVersion.Text = "Version: " + Application.ProductVersion;
            labelBuildDate.Text = "Built: " + File.GetLastWriteTime(Application.ProductName + ".exe").ToString("yyyy/MM/dd");
            chromeBrowser1.Dock = DockStyle.Fill;
            chromeBrowser2.Dock = DockStyle.Fill;
            this.WindowState = FormWindowState.Maximized;
        }
        #endregion
        private void CheckAndCreateFile()
        {
            if (!File.Exists(filePath))
            {
                try
                {
                    string[] defaultLinks = {
                        "http://192.168.3.61/pages/hmi",
                        "http://192.168.3.62/pages/hmi"
                    };
                    File.WriteAllLines(filePath, defaultLinks);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            chromeBrowser1.Reload();
            chromeBrowser2.Reload();
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        private async void PerformDelayedMinimizeMaximize()
        {
            await Task.Delay(5000);
            this.WindowState = FormWindowState.Minimized;

            await Task.Delay(1000);
            this.WindowState = FormWindowState.Maximized;
        }

        private async void Zoom(ChromiumWebBrowser chromeBrowser, bool zoomIn)
        {
            if (zoomIn)
            {
                double currentZoomLevel = await chromeBrowser.GetZoomLevelAsync();
                double zoomLevel = currentZoomLevel + 0.1;
                chromeBrowser.SetZoomLevel(zoomLevel);
            }
            else
            {
                double currentZoomLevel = await chromeBrowser.GetZoomLevelAsync();
                double zoomLevel = currentZoomLevel - 0.1;
                chromeBrowser.SetZoomLevel(zoomLevel);
            }
        }
    }
}