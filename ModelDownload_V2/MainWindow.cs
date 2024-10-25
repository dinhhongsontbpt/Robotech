using ComponentFactory.Krypton.Toolkit;
using ModelDownload_V2.Class;
using ModelDownload_V2.Common;
using ModelDownload_V2.MyUserControl;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace ModelDownload_V2
{
    public partial class MainWindow : KryptonForm
    {
        const string Filename = "Model.xlsx";
        const int TrayRow = 6;
        const int TrayColumn = 14;
        const int ModelNameLength = 20;
        List<UcTrayElement> ucTrayElements = new List<UcTrayElement>();
        List<TrayElement> trayElements = new List<TrayElement>();
        //Connect
        SerialPort serialPort = new SerialPort();
        private ManagementEventWatcher insertWatcher;
        private ManagementEventWatcher removeWatcher;
        private Thread DownloadAllThread;
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
        }
        private void InitObject()
        {
            for (int i = 0; i < TrayRow * TrayColumn; i++)
            {
                UcTrayElement ucTrayElement = new UcTrayElement();
                ucTrayElement.Dock = DockStyle.Fill;
                tbTray.Controls.Add(ucTrayElement);
                ucTrayElements.Add(ucTrayElement);
            }
            LoadCombobox();
            InitWatchers();
            DownloadAllThread = new Thread(DownloadAll);
            DownloadAllThread.IsBackground = true;
            DownloadAllThread.Start();

            string host = Properties.Settings.Default.Host;
            if(string.IsNullOrEmpty(host))
            {
                txtHost.Text = "http://192.168.0.93:80";
            }
            else
            {
                txtHost.Text = host;
            }
        }
        private void InitDisplay()
        {
            ucDateTime.IsRun = true;
            labelVersion.Text = "Version: " + Application.ProductVersion;
            labelBuildDate.Text = "Built: " + System.IO.File.GetLastWriteTime(Application.ProductName + ".exe").ToString("yyyy/MM/dd");
            //Table screws        
            tbScrews.ColumnHeadersDefaultCellStyle.BackColor = Color.Violet;
            tbScrews.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            tbScrews.ColumnHeadersDefaultCellStyle.Font = new Font(tbScrews.Font, FontStyle.Bold);
            tbScrews.EnableHeadersVisualStyles = false;
        }
        private void LoadCombobox()
        {
            cboComport.Items.Clear();
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'"))
            {
                var portnames = SerialPort.GetPortNames();
                var portstt = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString());

                var portList = portnames.Select(n => n + " - " + portstt.FirstOrDefault(s => s.Contains(n))).ToList();
                foreach (var i in portList)
                {
                    cboComport.Items.Add(i.Split('(')[1].Split(')')[0].Trim());
                }
            }
            if(cboComport.Items.Count > 0)
            {
                cboComport.SelectedIndex = 0;
            }
            else
            {
                cboComport.Text = string.Empty;
                cboComport.BackColor = Color.Red;
            }
        }
        private void InitWatchers()
        {
            // Watcher for device insertion
            insertWatcher = new ManagementEventWatcher();
            insertWatcher.EventArrived += new EventArrivedEventHandler(DeviceInsertedEvent);
            insertWatcher.Query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2");
            insertWatcher.Start();

            // Watcher for device removal
            removeWatcher = new ManagementEventWatcher();
            removeWatcher.EventArrived += new EventArrivedEventHandler(DeviceRemovedEvent);
            removeWatcher.Query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 3");
            removeWatcher.Start();
        }

        private void DeviceInsertedEvent(object sender, EventArrivedEventArgs e)
        {
            Invoke(new MethodInvoker(LoadCombobox));
        }

        private void DeviceRemovedEvent(object sender, EventArrivedEventArgs e)
        {
            Invoke(new MethodInvoker(LoadCombobox));
        }
        #endregion
        private void Connect(string portName, int baudRate = 9600)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }

            serialPort.PortName = portName;
            serialPort.BaudRate = baudRate;
            serialPort.StopBits = StopBits.One;
            serialPort.Parity = Parity.None;

            try
            {
                serialPort.Open();
                cboComport.BackColor = Color.Lime;
                //sMessage.Info($"Connected to {portName}");
            }
            catch (Exception ex)
            {
                sMessage.Error($"Error: {ex.Message}");
            }
        }
        private void OpenExcel()
        {
            LoadTray();
        }
        private bool LoadMaster()
        {
            if(tbScrews.InvokeRequired)
            {
                tbScrews.Invoke(new Action(() => LoadMaster()));
            }
            else
            {
                TrayElement.ScrewNames.Clear();
                tbScrews.Rows.Clear();
                trayElements.Clear();

                if (!System.IO.File.Exists(Filename))
                {
                    sMessage.Error("File does not exist");
                    return false;
                }

                try
                {
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(Filename)))
                    {
                        if (excelPackage.Workbook.Worksheets.Count == 0)
                        {
                            sMessage.Error("No worksheets in the Excel file");
                            return false;
                        }

                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];
                        if (worksheet == null)
                        {
                            sMessage.Error("Worksheet is null");
                            return false;
                        }

                        if (worksheet.Dimension == null || worksheet.Dimension.End.Row == 0)
                        {
                            sMessage.Error("No data in the worksheet");
                            return false;
                        }

                        for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                        {
                            string[] row = new string[] { worksheet.Cells[i, 1].Text, worksheet.Cells[i, 2].Text };
                            TrayElement.ScrewNames.Add(worksheet.Cells[i, 2].Text);
                            tbScrews.Rows.Add(row);
                        }
                    }
                }
                catch (Exception ex)
                {
                    sMessage.Error($"An error occurred while loading the file: {ex.Message}");
                    return false;
                }
            }
            return true;
        }
        private bool LoadTray()
        {
            if(LoadMaster())
            {
                try
                {
                    bool trayEmpty = true;
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(Filename)))
                    {
                        if (excelPackage.Workbook.Worksheets.Count == 0)
                        {
                            sMessage.Error("No worksheets in the Excel file");
                            return false;
                        }

                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];
                        if (worksheet == null)
                        {
                            sMessage.Error("Worksheet is null");
                            return false;
                        }

                        if (worksheet.Dimension == null || worksheet.Dimension.End.Row == 0)
                        {
                            sMessage.Error("No data in the worksheet");
                            return false;
                        }
                        /////////////////////////////////////////////////////
                        int modelRow = 2 + 13 * (int)txtModelNo.Value;

                        ClassCommon.InvokeControl.ControlTextInvoke(lbModelName, worksheet.Cells[modelRow, 2].Text);
                        for (int i = 0; i < TrayRow; i++)
                        {
                            for(int j = 0; j < TrayColumn; j++)
                            {
                                string screwName = worksheet.Cells[modelRow + 1 + 2*i, 4 + j].Text;
                                string glue = worksheet.Cells[modelRow + 2 + 2 * i, 4 + j].Text;
                                bool isGlue = glue.Trim().ToUpper() == "GLUE";
                                TrayElement element = new TrayElement(screwName, isGlue);
                                trayElements.Add(element);
                                if(!element.IsEmpty()) trayEmpty = false;
                            }
                        }
                    }
                    string dataWrite = ((int)txtModelNo.Value).ToString("D2") + TruncateOrPadString(lbModelName.Text, ModelNameLength);
                    for (int i = 0; i < trayElements.Count; i++)
                    {
                        dataWrite += trayElements[i].ToString();
                        ucTrayElements[i].Update(trayElements[i]);
                    }
                    if (!trayEmpty)
                    {
                        ClassCommon.InvokeControl.ControlTextInvoke(txtDataWrite, dataWrite);
                    }
                    else
                    {
                        ClassCommon.InvokeControl.ControlTextInvoke(txtDataWrite, "");
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    sMessage.Error($"An error occurred while loading the file: {ex.Message}");
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        private string TruncateOrPadString(string input, int length)
        {
            if (input.Length > length)
            {
                return input.Substring(0, length);
            }
            else if (input.Length < length)
            {
                return input.PadRight(length);
            }
            return input;
        }
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////
        /// </summary>
        private TrayModel GetModel(int modelNo)
        {
            try
            {
                TrayModel model = new TrayModel();
                bool trayEmpty = true;
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(Filename)))
                {
                    if (excelPackage.Workbook.Worksheets.Count == 0)
                    {
                        sMessage.Error("No worksheets in the Excel file");
                        return null;
                    }

                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];
                    if (worksheet == null)
                    {
                        sMessage.Error("Worksheet is null");
                        return null;
                    }

                    if (worksheet.Dimension == null || worksheet.Dimension.End.Row == 0)
                    {
                        sMessage.Error("No data in the worksheet");
                        return null;
                    }
                    /////////////////////////////////////////////////////
                    int modelRow = 2 + 13 * modelNo;
                    model.ModelNo = modelNo;
                    model.ModelName = worksheet.Cells[modelRow, 2].Text;

                    for (int i = 0; i < TrayRow; i++)
                    {
                        for (int j = 0; j < TrayColumn; j++)
                        {
                            string screwName = worksheet.Cells[modelRow + 1 + 2 * i, 4 + j].Text;
                            string glue = worksheet.Cells[modelRow + 2 + 2 * i, 4 + j].Text;
                            bool isGlue = glue.Trim().ToUpper() == "GLUE";
                            TrayElement element = new TrayElement(screwName, isGlue);
                            trayElements.Add(element);
                            if (!element.IsEmpty()) trayEmpty = false;
                            ////////////
                            model.ScrewType[TrayColumn * i + j] = TrayElement.ScrewNames.IndexOf(screwName) + 1;
                            if (isGlue) model.IsGlue[TrayColumn * i + j] = 1;
                        }
                    }
                }
                
                if (trayEmpty)
                {
                    return null;
                }

                return model;
            }
            catch (Exception ex)
            {
                sMessage.Error($"An error occurred while loading the file: {ex.Message}");
                return null;
            }
        }
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Excel Files",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "xlsx",
                Filter = "xlsx files (*.xlsx)|*.xlsx",
                FilterIndex = 2,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.File.Copy(openFileDialog.FileName, Filename, true);
                }
                catch (Exception ex)
                {
                    sMessage.Error($"An error occurred while copying the file: {ex.Message}");
                }
                OpenExcel();
            }
        }
        private bool SendString(string data)
        {
            if (serialPort.IsOpen)
            {
                if(data.Length != 358)
                {
                    sMessage.Error("Model is empty");
                    return false;
                }
                serialPort.Write(data + "@");
                return true;
            }
            else
            {
                sMessage.Error("Serial port is not open");
                return false;
            }
        }
        bool downloading = false;
        protected void DownloadAll()
        {
            while(true)
            {
                if (downloading)
                {
                    downloading = LoadTray();
                    if (downloading)
                    {
                        SendString(txtDataWrite.Text);
                        ClassCommon.InvokeControl.ControlTextInvoke(txtModelNo, (txtModelNo.Value + 1).ToString());
                    }
                    if (txtModelNo.Value >= 100)
                    {
                        btnDownloadAll.Text = "DOWNLOAD ALL";
                        downloading = false;
                        ClassCommon.InvokeControl.ControlTextInvoke(txtModelNo, "0");
                        sMessage.Info($"Download all model complete..!!");
                    }
                }
                Thread.Sleep(300);
            }
        }
        private void txtModelNo_ValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Filename))
            {
                LoadTray();
            }
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            TrayModel model = GetModel((int)txtModelNo.Value);
            if (model == null)
            {
                MessageBox.Show("Model Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string url = $"{txtHost.Text}/api/ModelDownload/WriteSingleModel";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonData = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        string statusCode = response.StatusCode.ToString();
                        MessageBox.Show($"Error: {statusCode}\nError code: {errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private async void btnDownloadAll_Click(object sender, EventArgs e)
        {
            string url = $"{txtHost.Text}/api/ModelDownload/WriteSingleModel";
            using (HttpClient client = new HttpClient())
            {
                List<int> failedModels = new List<int>();

                for (int i = 0; i < 100; i++)
                {
                    TrayModel model = GetModel(i);
                    if (model == null)
                    {
                        MessageBox.Show($"Model at index {i} is missing.", "Model Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        string jsonData = JsonConvert.SerializeObject(model);
                        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(url, content);

                        if (!response.IsSuccessStatusCode)
                        {
                            failedModels.Add(i);
                        }
                    }
                    catch (Exception ex)
                    {
                        failedModels.Add(i);
                        MessageBox.Show($"Error downloading model {i}. Details: {ex.Message}", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (failedModels.Count == 0)
                {
                    MessageBox.Show("All models downloaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string failedModelsList = string.Join(", ", failedModels);
                    MessageBox.Show($"Failed to download models: {failedModelsList}", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboComport.Text))
            {
                Connect(cboComport.Text);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Host = txtHost.Text;
            Properties.Settings.Default.Save();
            sMessage.Info("Save !");
        }
    }
}
