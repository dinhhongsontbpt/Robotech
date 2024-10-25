using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System;

namespace Cutreson_UserControl
{
    public partial class UspcInfor : UserControl
    {
        private bool isRun = false;
        private PCInfor pcInfo;
        private Thread updateThread;

        [Category("Appearance")]
        [Description("Gets or sets whether to display PC info")]
        public bool IsRun
        {
            get => isRun; set
            {
                isRun = value;
                if (value)
                {
                    StartUpdating();
                }
            }
        }

        public UspcInfor()
        {
            InitializeComponent();
            pcInfo = new PCInfor();
        }

        private void StartUpdating()
        {
            updateThread = new Thread(() =>
            {
                while (isRun)
                {
                    try
                    {
                        int spuUsage = pcInfo.GetPerformspU();
                        int ramUsage = pcInfo.GetPerformRAM();

                        BeginInvoke((Action)(() =>
                        {
                            progressBarspU.Value = spuUsage;
                            labelspU.Text = $"{spuUsage}%";
                            progressBarRAM.Value = ramUsage;
                            labelRAM.Text = $"{ramUsage}%";
                        }));
                    }
                    catch { }

                    Thread.Sleep(1000);
                }
            });

            updateThread.Name = "UspcInfo";
            updateThread.IsBackground = true;
            updateThread.Start();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            isRun = false;
            Thread.Sleep(1000);
            base.OnHandleDestroyed(e);
        }
    }

    public class PCInfor
    {
        protected PerformanceCounter spuCounter;

        protected PerformanceCounter ramCounter;

        protected PerformanceCounter ramAmount;

        public PCInfor()
        {
            InitData();
        }

        public void InitData()
        {
            spuCounter = new PerformanceCounter();
            spuCounter.CategoryName = "Processor";
            spuCounter.CounterName = "% Processor Time";
            spuCounter.InstanceName = "_Total";
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public int GetPerformspU()
        {
            float num = 0f;
            try
            {
                if (spuCounter != null)
                {
                    num = spuCounter.NextValue();
                }
            }
            catch
            {
            }

            return (int)num;
        }

        private float GetRAM_Available()
        {
            float result = 0f;
            try
            {
                result = ramCounter.NextValue();
            }
            catch
            {
            }

            return result;
        }

        private float GetRAM_Amount()
        {
            float result = 8196f;
            try
            {
                result = PerformanceInfo.GetTotalMemoryInMiB();
            }
            catch
            {
            }

            return result;
        }

        public int GetPerformRAM()
        {
            float rAM_Amount = GetRAM_Amount();
            float rAM_Available = GetRAM_Available();
            float num = 0f;
            if (rAM_Amount != 0f)
            {
                num = (1f - rAM_Available / rAM_Amount) * 100f;
            }

            return (int)num;
        }

        private float GetMemoryTotalSize(string driveName)
        {
            float num = 0f;
            DriveInfo[] drives = DriveInfo.GetDrives();
            DriveInfo[] array = drives;
            foreach (DriveInfo driveInfo in array)
            {
                float result = 0f;
                try
                {
                    if (driveInfo.Name.Trim() == driveName.Trim())
                    {
                        float.TryParse(driveInfo.TotalSize.ToString(), out result);
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine("GetMemoryTotalSize: " + driveInfo.Name + "error. (" + ex.Message + ")");
                }

                num += result;
            }

            return num / 1048576f;
        }

        private float GetMemoryFreeSpace(string driveName)
        {
            float num = 0f;
            DriveInfo[] drives = DriveInfo.GetDrives();
            DriveInfo[] array = drives;
            foreach (DriveInfo driveInfo in array)
            {
                float result = 0f;
                try
                {
                    if (driveInfo.Name.Trim() == driveName)
                    {
                        float.TryParse(driveInfo.TotalFreeSpace.ToString(), out result);
                    }

                    num += result;
                }
                catch
                {
                }
            }

            return num / 1048576f;
        }

        public int GetPerformDrive(string driveName)
        {
            float memoryTotalSize = GetMemoryTotalSize(driveName);
            float memoryFreeSpace = GetMemoryFreeSpace(driveName);
            float num = 0f;
            if (memoryTotalSize != 0f)
            {
                num = (1f - memoryFreeSpace / memoryTotalSize) * 100f;
            }

            return (int)num;
        }
    }

    public static class PerformanceInfo
    {
        public struct PerformanceInformation
        {
            public int Size;

            public IntPtr CommitTotal;

            public IntPtr CommitLimit;

            public IntPtr CommitPeak;

            public IntPtr PhysicalTotal;

            public IntPtr PhysicalAvailable;

            public IntPtr SystemCache;

            public IntPtr KernelTotal;

            public IntPtr KernelPaged;

            public IntPtr KernelNonPaged;

            public IntPtr PageSize;

            public int HandlesCount;

            public int ProcessCount;

            public int ThreadCount;
        }

        [DllImport("psapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPerformanceInfo(out PerformanceInformation PerformanceInformation, [In] int Size);

        public static long GetPhysicalAvailableMemoryInMiB()
        {
            PerformanceInformation PerformanceInformation = default(PerformanceInformation);
            if (GetPerformanceInfo(out PerformanceInformation, Marshal.SizeOf(PerformanceInformation)))
            {
                return Convert.ToInt64(PerformanceInformation.PhysicalAvailable.ToInt64() * PerformanceInformation.PageSize.ToInt64() / 1048576);
            }

            return -1L;
        }

        public static long GetTotalMemoryInMiB()
        {
            PerformanceInformation PerformanceInformation = default(PerformanceInformation);
            if (GetPerformanceInfo(out PerformanceInformation, Marshal.SizeOf(PerformanceInformation)))
            {
                return Convert.ToInt64(PerformanceInformation.PhysicalTotal.ToInt64() * PerformanceInformation.PageSize.ToInt64() / 1048576);
            }

            return -1L;
        }
    }

    partial class UspcInfor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelspU = new System.Windows.Forms.Label();
            this.labelRAM = new System.Windows.Forms.Label();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.progressBarspU = new System.Windows.Forms.ProgressBar();
            this.progressBarRAM = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 50);
            this.label1.TabIndex = 4;
            this.label1.Text = "CPU";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 50);
            this.label2.TabIndex = 6;
            this.label2.Text = "RAM";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelspU
            // 
            this.labelspU.AutoSize = true;
            this.labelspU.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelspU.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelspU.ForeColor = System.Drawing.Color.Black;
            this.labelspU.Location = new System.Drawing.Point(153, 0);
            this.labelspU.Name = "labelspU";
            this.labelspU.Size = new System.Drawing.Size(44, 50);
            this.labelspU.TabIndex = 8;
            this.labelspU.Text = "50%";
            this.labelspU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelRAM
            // 
            this.labelRAM.AutoSize = true;
            this.labelRAM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelRAM.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRAM.ForeColor = System.Drawing.Color.Black;
            this.labelRAM.Location = new System.Drawing.Point(153, 50);
            this.labelRAM.Name = "labelRAM";
            this.labelRAM.Size = new System.Drawing.Size(44, 50);
            this.labelRAM.TabIndex = 9;
            this.labelRAM.Text = "50%";
            this.labelRAM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.labelspU, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.labelRAM, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.progressBarspU, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.progressBarRAM, 1, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel.TabIndex = 12;
            // 
            // progressBarspU
            // 
            this.progressBarspU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.progressBarspU.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBarspU.Location = new System.Drawing.Point(53, 12);
            this.progressBarspU.Margin = new System.Windows.Forms.Padding(3, 12, 3, 12);
            this.progressBarspU.Name = "progressBarspU";
            this.progressBarspU.Size = new System.Drawing.Size(94, 26);
            this.progressBarspU.TabIndex = 10;
            // 
            // progressBarRAM
            // 
            this.progressBarRAM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.progressBarRAM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBarRAM.Location = new System.Drawing.Point(53, 62);
            this.progressBarRAM.Margin = new System.Windows.Forms.Padding(3, 12, 3, 12);
            this.progressBarRAM.Name = "progressBarRAM";
            this.progressBarRAM.Size = new System.Drawing.Size(94, 26);
            this.progressBarRAM.TabIndex = 11;
            // 
            // UspcInfor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "UspcInfor";
            this.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelspU;
        private System.Windows.Forms.Label labelRAM;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.ProgressBar progressBarspU;
        private System.Windows.Forms.ProgressBar progressBarRAM;
    }
}
