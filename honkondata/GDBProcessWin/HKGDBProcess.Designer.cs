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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.gdbFileTxt = new System.Windows.Forms.TextBox();
            this.targerFeatures = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.originFeature = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.startBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.HLevel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buffer = new System.Windows.Forms.TextBox();
            this.processResult = new System.Windows.Forms.Label();
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
            this.gdbFileTxt.Location = new System.Drawing.Point(17, 36);
            this.gdbFileTxt.Name = "gdbFileTxt";
            this.gdbFileTxt.Size = new System.Drawing.Size(500, 21);
            this.gdbFileTxt.TabIndex = 1;
            this.gdbFileTxt.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FindGDBFile);
            // 
            // targerFeatures
            // 
            this.targerFeatures.FormattingEnabled = true;
            this.targerFeatures.Location = new System.Drawing.Point(17, 87);
            this.targerFeatures.Name = "targerFeatures";
            this.targerFeatures.Size = new System.Drawing.Size(500, 148);
            this.targerFeatures.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.Location = new System.Drawing.Point(17, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "选择转换目标：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label3.Location = new System.Drawing.Point(17, 242);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "数据源：";
            // 
            // originFeature
            // 
            this.originFeature.Location = new System.Drawing.Point(17, 267);
            this.originFeature.Name = "originFeature";
            this.originFeature.Size = new System.Drawing.Size(500, 21);
            this.originFeature.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label4.Location = new System.Drawing.Point(21, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "GDB数据库位置：";
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(447, 538);
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
            this.label5.Location = new System.Drawing.Point(17, 295);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(238, 21);
            this.label5.TabIndex = 8;
            this.label5.Text = "HLevel（多个HLevel请写多行）";
            // 
            // HLevel
            // 
            this.HLevel.Location = new System.Drawing.Point(17, 320);
            this.HLevel.Multiline = true;
            this.HLevel.Name = "HLevel";
            this.HLevel.Size = new System.Drawing.Size(500, 62);
            this.HLevel.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label6.Location = new System.Drawing.Point(21, 389);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(186, 21);
            this.label6.TabIndex = 10;
            this.label6.Text = "缓冲区大小（单位：米）";
            // 
            // buffer
            // 
            this.buffer.Location = new System.Drawing.Point(17, 414);
            this.buffer.Name = "buffer";
            this.buffer.Size = new System.Drawing.Size(500, 21);
            this.buffer.TabIndex = 11;
            // 
            // processResult
            // 
            this.processResult.AutoSize = true;
            this.processResult.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.processResult.Location = new System.Drawing.Point(23, 489);
            this.processResult.Name = "processResult";
            this.processResult.Size = new System.Drawing.Size(0, 21);
            this.processResult.TabIndex = 12;
            // 
            // HKDataProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 573);
            this.Controls.Add(this.processResult);
            this.Controls.Add(this.buffer);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.HLevel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.originFeature);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.targerFeatures);
            this.Controls.Add(this.gdbFileTxt);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HKDataProcessForm";
            this.Text = "香港三维数据处理程序";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox gdbFileTxt;
        private System.Windows.Forms.CheckedListBox targerFeatures;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox originFeature;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox HLevel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox buffer;
        private System.Windows.Forms.Label processResult;
    }
}

