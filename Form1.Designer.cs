namespace BangBang_GUI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.comboPorts = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numBase = new System.Windows.Forms.NumericUpDown();
            this.numDeadband = new System.Windows.Forms.NumericUpDown();
            this.numTurnBoost = new System.Windows.Forms.NumericUpDown();
            this.numThresh = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.apply = new System.Windows.Forms.Button();
            this.chkInvert = new System.Windows.Forms.CheckBox();
            this.btnEnable = new System.Windows.Forms.Button();
            this.btnDisable = new System.Windows.Forms.Button();
            this.timerPoll = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblIn1 = new System.Windows.Forms.Label();
            this.lblIn2 = new System.Windows.Forms.Label();
            this.lblOut1 = new System.Windows.Forms.Label();
            this.lblOut2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDeadband)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTurnBoost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThresh)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboPorts
            // 
            this.comboPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPorts.FormattingEnabled = true;
            this.comboPorts.Location = new System.Drawing.Point(12, 12);
            this.comboPorts.Name = "comboPorts";
            this.comboPorts.Size = new System.Drawing.Size(121, 21);
            this.comboPorts.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(139, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(60, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(205, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(70, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(281, 10);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(78, 23);
            this.btnDisconnect.TabIndex = 3;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(365, 15);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(67, 13);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Disconnected";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numBase);
            this.groupBox1.Controls.Add(this.numDeadband);
            this.groupBox1.Controls.Add(this.numTurnBoost);
            this.groupBox1.Controls.Add(this.numThresh);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.apply);
            this.groupBox1.Controls.Add(this.chkInvert);
            this.groupBox1.Controls.Add(this.btnEnable);
            this.groupBox1.Controls.Add(this.btnDisable);
            this.groupBox1.Location = new System.Drawing.Point(12, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 150);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controller";
            // 
            // numBase
            // 
            this.numBase.Location = new System.Drawing.Point(90, 22);
            this.numBase.Maximum = new decimal(new int[] {255,0,0,0});
            this.numBase.Name = "numBase";
            this.numBase.Size = new System.Drawing.Size(80, 20);
            this.numBase.TabIndex = 1;
            this.numBase.Value = new decimal(new int[] {150,0,0,0});
            // 
            // numDeadband
            // 
            this.numDeadband.Location = new System.Drawing.Point(90, 48);
            this.numDeadband.Maximum = new decimal(new int[] {255,0,0,0});
            this.numDeadband.Name = "numDeadband";
            this.numDeadband.Size = new System.Drawing.Size(80, 20);
            this.numDeadband.TabIndex = 2;
            this.numDeadband.Value = new decimal(new int[] {10,0,0,0});
            // 
            // numTurnBoost
            // 
            this.numTurnBoost.Location = new System.Drawing.Point(90, 74);
            this.numTurnBoost.Maximum = new decimal(new int[] {255,0,0,0});
            this.numTurnBoost.Name = "numTurnBoost";
            this.numTurnBoost.Size = new System.Drawing.Size(80, 20);
            this.numTurnBoost.TabIndex = 3;
            this.numTurnBoost.Value = new decimal(new int[] {80,0,0,0});
            // 
            // numThresh
            // 
            this.numThresh.Location = new System.Drawing.Point(90, 100);
            this.numThresh.Maximum = new decimal(new int[] {255,0,0,0});
            this.numThresh.Name = "numThresh";
            this.numThresh.Size = new System.Drawing.Size(80, 20);
            this.numThresh.TabIndex = 4;
            this.numThresh.Value = new decimal(new int[] {128,0,0,0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Base (0-255)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Deadband";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Turn Boost";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Threshold";
            // 
            // apply
            // 
            this.apply.Location = new System.Drawing.Point(190, 97);
            this.apply.Name = "apply";
            this.apply.Size = new System.Drawing.Size(75, 23);
            this.apply.TabIndex = 7;
            this.apply.Text = "Apply";
            this.apply.UseVisualStyleBackColor = true;
            this.apply.Click += new System.EventHandler(this.apply_Click);
            // 
            // chkInvert
            // 
            this.chkInvert.AutoSize = true;
            this.chkInvert.Location = new System.Drawing.Point(190, 50);
            this.chkInvert.Name = "chkInvert";
            this.chkInvert.Size = new System.Drawing.Size(87, 17);
            this.chkInvert.TabIndex = 6;
            this.chkInvert.Text = "Active-Low";
            this.chkInvert.UseVisualStyleBackColor = true;
            this.chkInvert.CheckedChanged += new System.EventHandler(this.chkInvert_CheckedChanged);
            // 
            // btnEnable
            // 
            this.btnEnable.Location = new System.Drawing.Point(190, 19);
            this.btnEnable.Name = "btnEnable";
            this.btnEnable.Size = new System.Drawing.Size(75, 23);
            this.btnEnable.TabIndex = 5;
            this.btnEnable.Text = "Start";
            this.btnEnable.UseVisualStyleBackColor = true;
            this.btnEnable.Click += new System.EventHandler(this.btnEnable_Click);
            // 
            // btnDisable
            // 
            this.btnDisable.Location = new System.Drawing.Point(271, 19);
            this.btnDisable.Name = "btnDisable";
            this.btnDisable.Size = new System.Drawing.Size(75, 23);
            this.btnDisable.TabIndex = 6;
            this.btnDisable.Text = "Stop";
            this.btnDisable.UseVisualStyleBackColor = true;
            this.btnDisable.Click += new System.EventHandler(this.btnDisable_Click);
            // 
            // timerPoll
            // 
            this.timerPoll.Enabled = true;
            this.timerPoll.Interval = 150;
            this.timerPoll.Tick += new System.EventHandler(this.timerPoll_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblIn1);
            this.groupBox2.Controls.Add(this.lblIn2);
            this.groupBox2.Controls.Add(this.lblOut1);
            this.groupBox2.Controls.Add(this.lblOut2);
            this.groupBox2.Location = new System.Drawing.Point(12, 203);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(420, 60);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Telemetry";
            // 
            // lblIn1
            // 
            this.lblIn1.AutoSize = true;
            this.lblIn1.Location = new System.Drawing.Point(16, 27);
            this.lblIn1.Name = "lblIn1";
            this.lblIn1.Size = new System.Drawing.Size(35, 13);
            this.lblIn1.TabIndex = 0;
            this.lblIn1.Text = "In1: -";
            // 
            // lblIn2
            // 
            this.lblIn2.AutoSize = true;
            this.lblIn2.Location = new System.Drawing.Point(110, 27);
            this.lblIn2.Name = "lblIn2";
            this.lblIn2.Size = new System.Drawing.Size(35, 13);
            this.lblIn2.TabIndex = 0;
            this.lblIn2.Text = "In2: -";
            // 
            // lblOut1
            // 
            this.lblOut1.AutoSize = true;
            this.lblOut1.Location = new System.Drawing.Point(200, 27);
            this.lblOut1.Name = "lblOut1";
            this.lblOut1.Size = new System.Drawing.Size(49, 13);
            this.lblOut1.TabIndex = 0;
            this.lblOut1.Text = "OutL: -";
            // 
            // lblOut2
            // 
            this.lblOut2.AutoSize = true;
            this.lblOut2.Location = new System.Drawing.Point(290, 27);
            this.lblOut2.Name = "lblOut2";
            this.lblOut2.Size = new System.Drawing.Size(51, 13);
            this.lblOut2.TabIndex = 0;
            this.lblOut2.Text = "OutR: -";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 275);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.comboPorts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Bang-Bang Line Follower";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDeadband)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTurnBoost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThresh)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboPorts;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numBase;
        private System.Windows.Forms.NumericUpDown numDeadband;
        private System.Windows.Forms.NumericUpDown numTurnBoost;
        private System.Windows.Forms.NumericUpDown numThresh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button apply;
        private System.Windows.Forms.CheckBox chkInvert;
        private System.Windows.Forms.Button btnEnable;
        private System.Windows.Forms.Button btnDisable;
        private System.Windows.Forms.Timer timerPoll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblIn1;
        private System.Windows.Forms.Label lblIn2;
        private System.Windows.Forms.Label lblOut1;
        private System.Windows.Forms.Label lblOut2;
    }
}
