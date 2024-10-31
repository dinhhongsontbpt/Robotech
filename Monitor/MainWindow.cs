using CefSharp;
using CefSharp.WinForms;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace VisionMonitor
{
    public partial class MainWindow : KryptonForm
    {
        private ChromiumWebBrowser chromeBrowser1 = new ChromiumWebBrowser();
        private ChromiumWebBrowser chromeBrowser2 = new ChromiumWebBrowser();
        private System.Timers.Timer reloadTimer;
        private double reloadTime; //Time auto reload browser (minute)
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
            // Initialize and configure the timer
            reloadTimer = new System.Timers.Timer(reloadTime * 60 * 1000);
            reloadTimer.Elapsed += ReloadTimer_Elapsed;
            reloadTimer.AutoReset = true;
            reloadTimer.Start();
        }
        private string filePath = "SettingVisionMonitor.txt";
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
                    chromeBrowser2.Load(links[1]);
                    lbCam1.Text = "Cam Left: " + links[0];
                    lbCam2.Text = "Cam Right: " + links[1];
                    reloadTime = Convert.ToDouble(links[2]);
                }
                else
                {
                    MessageBox.Show("File SettingVisionMonitor.txt phải chứa ít nhất 2 link, 1 số");
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
        private void ReloadTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                chromeBrowser1.Reload();
                chromeBrowser2.Reload();
            });
        }
        private void CheckAndCreateFile()
        {
            if (!File.Exists(filePath))
            {
                try
                {
                    string[] defaultLinks = {
                        "http://192.168.3.61/pages/hmi/",
                        "http://192.168.3.62/pages/hmi/",
                        "10" //Time auto refresh browser
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