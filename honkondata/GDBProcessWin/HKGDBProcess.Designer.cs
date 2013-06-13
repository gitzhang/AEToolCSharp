namespace GDBProcessWin
{
    partial class HKDataProcessForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HKDataProcessForm));
            this.label1 = new System.Windows.Forms.Label();
            this.gdbFileTxt = new System.Windows.Forms.TextBox();
            this.targerFeatures = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.startBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.HLevel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buffer = new System.Windows.Forms.TextBox();
            this.processResult = new System.Windows.Forms.Label();
            this.updateProcessPercent = new System.Windows.Forms.ProgressBar();
            this.details = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.PointTypeCBX = new System.Windows.Forms.ComboBox();
            this.originFeature = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ProcessInfo = new System.Windows.Forms.Label();
            this.originFeatureCBX = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.JieBianBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.RSFilePath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.RSFeatures = new System.Windows.Forms.CheckedListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 21);
            this.label1.TabIndex = 0;
            // 
            // gdbFileTxt
            // 
            this.gdbFileTxt.Location = new System.Drawing.Point(12, 36);
            this.gdbFileTxt.Name = "gdbFileTxt";
            this.gdbFileTxt.Size = new System.Drawing.Size(498, 21);
            this.gdbFileTxt.TabIndex = 1;
            this.gdbFileTxt.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FindGDBFile);
            // 
            // targerFeatures
            // 
            this.targerFeatures.FormattingEnabled = true;
            this.targerFeatures.Location = new System.Drawing.Point(12, 132);
            this.targerFeatures.Name = "targerFeatures";
            this.targerFeatures.Size = new System.Drawing.Size(498, 148);
            this.targerFeatures.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.Location = new System.Drawing.Point(8, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "赋值目标：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label3.Location = new System.Drawing.Point(8, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "三维点云数据源及类型：";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label4.Location = new System.Drawing.Point(8, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "GDB数据库：";
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(438, 523);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 7;
            this.startBtn.Text = "开始";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label5.Location = new System.Drawing.Point(8, 283);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(238, 21);
            this.label5.TabIndex = 8;
            this.label5.Text = "HLevel（多个HLevel请写多行）";
            // 
            // HLevel
            // 
            this.HLevel.Location = new System.Drawing.Point(12, 307);
            this.HLevel.Multiline = true;
            this.HLevel.Name = "HLevel";
            this.HLevel.Size = new System.Drawing.Size(498, 62);
            this.HLevel.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label6.Location = new System.Drawing.Point(8, 372);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(186, 21);
            this.label6.TabIndex = 10;
            this.label6.Text = "缓冲区大小（单位：米）";
            // 
            // buffer
            // 
            this.buffer.Location = new System.Drawing.Point(12, 396);
            this.buffer.Name = "buffer";
            this.buffer.Size = new System.Drawing.Size(498, 21);
            this.buffer.TabIndex = 11;
            // 
            // processResult
            // 
            this.processResult.AutoSize = true;
            this.processResult.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.processResult.Location = new System.Drawing.Point(8, 476);
            this.processResult.Name = "processResult";
            this.processResult.Size = new System.Drawing.Size(0, 21);
            this.processResult.TabIndex = 12;
            // 
            // updateProcessPercent
            // 
            this.updateProcessPercent.Location = new System.Drawing.Point(12, 428);
            this.updateProcessPercent.Name = "updateProcessPercent";
            this.updateProcessPercent.Size = new System.Drawing.Size(498, 23);
            this.updateProcessPercent.TabIndex = 13;
            // 
            // details
            // 
            this.details.AutoSize = true;
            this.details.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.details.Location = new System.Drawing.Point(8, 517);
            this.details.Name = "details";
            this.details.Size = new System.Drawing.Size(0, 17);
            this.details.TabIndex = 14;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(527, 578);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.PointTypeCBX);
            this.tabPage1.Controls.Add(this.originFeature);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.processResult);
            this.tabPage1.Controls.Add(this.details);
            this.tabPage1.Controls.Add(this.gdbFileTxt);
            this.tabPage1.Controls.Add(this.updateProcessPercent);
            this.tabPage1.Controls.Add(this.startBtn);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.targerFeatures);
            this.tabPage1.Controls.Add(this.buffer);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.HLevel);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(519, 552);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "道路赋值";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // PointTypeCBX
            // 
            this.PointTypeCBX.FormattingEnabled = true;
            this.PointTypeCBX.Items.AddRange(new object[] {
            "Point ZM",
            "Multipoint ZM"});
            this.PointTypeCBX.Location = new System.Drawing.Point(389, 85);
            this.PointTypeCBX.Name = "PointTypeCBX";
            this.PointTypeCBX.Size = new System.Drawing.Size(121, 20);
            this.PointTypeCBX.TabIndex = 16;
            this.PointTypeCBX.Text = "Point ZM";
            // 
            // originFeature
            // 
            this.originFeature.FormattingEnabled = true;
            this.originFeature.Location = new System.Drawing.Point(12, 85);
            this.originFeature.Name = "originFeature";
            this.originFeature.Size = new System.Drawing.Size(371, 20);
            this.originFeature.TabIndex = 15;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ProcessInfo);
            this.tabPage2.Controls.Add(this.originFeatureCBX);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.JieBianBtn);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.RSFilePath);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.RSFeatures);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(519, 552);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "接边处理";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ProcessInfo
            // 
            this.ProcessInfo.AutoSize = true;
            this.ProcessInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ProcessInfo.Location = new System.Drawing.Point(12, 358);
            this.ProcessInfo.Name = "ProcessInfo";
            this.ProcessInfo.Size = new System.Drawing.Size(0, 16);
            this.ProcessInfo.TabIndex = 14;
            // 
            // originFeatureCBX
            // 
            this.originFeatureCBX.FormattingEnabled = true;
            this.originFeatureCBX.Location = new System.Drawing.Point(12, 85);
            this.originFeatureCBX.Name = "originFeatureCBX";
            this.originFeatureCBX.Size = new System.Drawing.Size(498, 20);
            this.originFeatureCBX.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label9.Location = new System.Drawing.Point(8, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 21);
            this.label9.TabIndex = 12;
            this.label9.Text = "基准要素：";
            // 
            // JieBianBtn
            // 
            this.JieBianBtn.Location = new System.Drawing.Point(438, 523);
            this.JieBianBtn.Name = "JieBianBtn";
            this.JieBianBtn.Size = new System.Drawing.Size(75, 23);
            this.JieBianBtn.TabIndex = 11;
            this.JieBianBtn.Text = "开始";
            this.JieBianBtn.UseVisualStyleBackColor = true;
            this.JieBianBtn.Click += new System.EventHandler(this.JieBianBtn_Click);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label7.Location = new System.Drawing.Point(8, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 21);
            this.label7.TabIndex = 10;
            this.label7.Text = "GDB数据库：";
            // 
            // RSFilePath
            // 
            this.RSFilePath.Location = new System.Drawing.Point(12, 36);
            this.RSFilePath.Name = "RSFilePath";
            this.RSFilePath.Size = new System.Drawing.Size(498, 21);
            this.RSFilePath.TabIndex = 7;
            this.RSFilePath.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RSFilePath_MouseClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label8.Location = new System.Drawing.Point(8, 108);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 21);
            this.label8.TabIndex = 9;
            this.label8.Text = "接边目标：";
            // 
            // RSFeatures
            // 
            this.RSFeatures.FormattingEnabled = true;
            this.RSFeatures.Location = new System.Drawing.Point(12, 132);
            this.RSFeatures.Name = "RSFeatures";
            this.RSFeatures.Size = new System.Drawing.Size(498, 148);
            this.RSFeatures.TabIndex = 8;
            // 
            // HKDataProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 589);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HKDataProcessForm";
            this.Text = "香港三维数据处理程序";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox gdbFileTxt;
        private System.Windows.Forms.CheckedListBox targerFeatures;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox HLevel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox buffer;
        private System.Windows.Forms.Label processResult;
        private System.Windows.Forms.ProgressBar updateProcessPercent;
        private System.Windows.Forms.Label details;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox RSFilePath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckedListBox RSFeatures;
        private System.Windows.Forms.Button JieBianBtn;
        private System.Windows.Forms.ComboBox originFeature;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox originFeatureCBX;
        private System.Windows.Forms.Label ProcessInfo;
        private System.Windows.Forms.ComboBox PointTypeCBX;
    }
}

