namespace CustomServer
{
    partial class FormMain
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtServerPath = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.labPrintInfo = new System.Windows.Forms.Label();
            this.txtHtml = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1746, 250);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 60);
            this.button1.TabIndex = 0;
            this.button1.Text = "浏览";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(166, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(249, 77);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(390, 42);
            this.txtIP.TabIndex = 2;
            this.txtIP.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 30);
            this.label2.TabIndex = 3;
            this.label2.Text = "端口号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 274);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 30);
            this.label3.TabIndex = 4;
            this.label3.Text = "服务器目录";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(249, 166);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(390, 42);
            this.txtPort.TabIndex = 5;
            this.txtPort.Text = "11000";
            // 
            // txtServerPath
            // 
            this.txtServerPath.Location = new System.Drawing.Point(249, 262);
            this.txtServerPath.Name = "txtServerPath";
            this.txtServerPath.Size = new System.Drawing.Size(1448, 42);
            this.txtServerPath.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1746, 344);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(174, 65);
            this.button2.TabIndex = 7;
            this.button2.Text = "开启服务";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OpenServer_Click);
            // 
            // labPrintInfo
            // 
            this.labPrintInfo.AutoSize = true;
            this.labPrintInfo.Location = new System.Drawing.Point(244, 361);
            this.labPrintInfo.Name = "labPrintInfo";
            this.labPrintInfo.Size = new System.Drawing.Size(28, 30);
            this.labPrintInfo.TabIndex = 8;
            this.labPrintInfo.Text = ".";
            // 
            // txtHtml
            // 
            this.txtHtml.Location = new System.Drawing.Point(142, 456);
            this.txtHtml.Multiline = true;
            this.txtHtml.Name = "txtHtml";
            this.txtHtml.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtHtml.Size = new System.Drawing.Size(1460, 755);
            this.txtHtml.TabIndex = 9;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(171, 455);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1323, 760);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2694, 1545);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtHtml);
            this.Controls.Add(this.labPrintInfo);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtServerPath);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "FormMain";
            this.Text = "自定义服务器";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtServerPath;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labPrintInfo;
        private System.Windows.Forms.TextBox txtHtml;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

