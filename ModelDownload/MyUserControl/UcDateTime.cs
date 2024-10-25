using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using ModelDownload.Common;

namespace ModelDownload.MyUserControl
{
    public partial class UcDateTime : UserControl
    {
        private bool isRun = false;
        [Category("Appearance")]
        [Description("Gets or sets Datetime")]
        public bool IsRun
        {
            get => isRun; set
            {
                isRun = value;
                if (value)
                {
                    Loading();
                }
            }
        }

        public UcDateTime()
        {
            InitializeComponent();
        }
        private void Loading()
        {
            Thread run = new Thread(() =>
            {
                while (isRun)
                {
                    ClassCommon.InvokeControl.ControlTextInvoke(lbDate, DateTime.Now.ToString("yyyy-MM-dd"));
                    ClassCommon.InvokeControl.ControlTextInvoke(lbDay, DateTime.Now.ToString("dddd").ToUpper());
                    ClassCommon.InvokeControl.ControlTextInvoke(lbTime, DateTime.Now.ToString("HH:mm:ss"));
                    Thread.Sleep(1000);
                }
            });
            run.Name = "UcDateTime";
            run.IsBackground = true;
            run.Start();
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            isRun = false;
            Thread.Sleep(1000);
            base.OnHandleDestroyed(e);
        }
    }
    partial class UcDateTime
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelSaperator = new System.Windows.Forms.Panel();
            this.lbTime = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbDay = new System.Windows.Forms.Label();
            this.lbDate = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panelSaperator, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbTime, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(350, 60);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelSaperator
            // 
            this.panelSaperator.BackColor = System.Drawing.Color.White;
            this.panelSaperator.Location = new System.Drawing.Point(175, 3);
            this.panelSaperator.Name = "panelSaperator";
            this.panelSaperator.Size = new System.Drawing.Size(1, 54);
            this.panelSaperator.TabIndex = 0;
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTime.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTime.ForeColor = System.Drawing.Color.Transparent;
            this.lbTime.Location = new System.Drawing.Point(180, 0);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(167, 60);
            this.lbTime.TabIndex = 1;
            this.lbTime.Text = "01:02:03";
            this.lbTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lbDay, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbDate, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(166, 54);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // lbDay
            // 
            this.lbDay.AutoSize = true;
            this.lbDay.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbDay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDay.ForeColor = System.Drawing.Color.Transparent;
            this.lbDay.Location = new System.Drawing.Point(3, 27);
            this.lbDay.Name = "lbDay";
            this.lbDay.Size = new System.Drawing.Size(160, 23);
            this.lbDay.TabIndex = 3;
            this.lbDay.Text = "[MONDAY]";
            this.lbDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDate
            // 
            this.lbDate.AutoSize = true;
            this.lbDate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbDate.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDate.ForeColor = System.Drawing.Color.Transparent;
            this.lbDate.Location = new System.Drawing.Point(3, 4);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(160, 23);
            this.lbDate.TabIndex = 2;
            this.lbDate.Text = "2023-01-01";
            this.lbDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UcDateTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UcDateTime";
            this.Size = new System.Drawing.Size(350, 60);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelSaperator;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lbDay;
        private System.Windows.Forms.Label lbDate;
    }
}
