namespace VisionMonitor.MyUserControl
{
    partial class UcTrayElement
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
            this.lbScrewName = new System.Windows.Forms.Label();
            this.lbGlue = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lbScrewName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbGlue, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(131, 88);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbScrewName
            // 
            this.lbScrewName.AutoSize = true;
            this.lbScrewName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbScrewName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbScrewName.Location = new System.Drawing.Point(3, 0);
            this.lbScrewName.Name = "lbScrewName";
            this.lbScrewName.Size = new System.Drawing.Size(125, 66);
            this.lbScrewName.TabIndex = 0;
            this.lbScrewName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbGlue
            // 
            this.lbGlue.AutoSize = true;
            this.lbGlue.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbGlue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbGlue.Location = new System.Drawing.Point(3, 66);
            this.lbGlue.Name = "lbGlue";
            this.lbGlue.Size = new System.Drawing.Size(125, 22);
            this.lbGlue.TabIndex = 1;
            // 
            // UcTrayElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UcTrayElement";
            this.Size = new System.Drawing.Size(131, 88);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbScrewName;
        private System.Windows.Forms.Label lbGlue;
    }
}
