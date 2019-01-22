using Ys.Sdk.Demo.Properties;

namespace Ys.Sdk.Demo
{
	partial class MainForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.flpCameraList = new System.Windows.Forms.FlowLayoutPanel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.rightMenuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiCameraCount = new System.Windows.Forms.ToolStripMenuItem();
			this.单画面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.四画面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.九画面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiLevel = new System.Windows.Forms.ToolStripMenuItem();
			this.流畅ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.标准ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.高清ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.超清ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.flpPlay = new System.Windows.Forms.FlowLayoutPanel();
			this.gbLog = new System.Windows.Forms.GroupBox();
			this.txtLog = new System.Windows.Forms.RichTextBox();
			this.ts = new System.Windows.Forms.ToolStrip();
			this.tsExit = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsLogout = new System.Windows.Forms.ToolStripButton();
			this.tsLogin = new System.Windows.Forms.ToolStripButton();
			this.st = new System.Windows.Forms.StatusStrip();
			this.stStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.stProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.rightMenuContext.SuspendLayout();
			this.gbLog.SuspendLayout();
			this.ts.SuspendLayout();
			this.st.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.splitContainer1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 25);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1095, 604);
			this.panel1.TabIndex = 1;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.gbLog);
			this.splitContainer1.Size = new System.Drawing.Size(1095, 604);
			this.splitContainer1.SplitterDistance = 916;
			this.splitContainer1.TabIndex = 0;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
			this.splitContainer2.Size = new System.Drawing.Size(916, 604);
			this.splitContainer2.SplitterDistance = 132;
			this.splitContainer2.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.flpCameraList);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(132, 604);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "设备";
			// 
			// flpCameraList
			// 
			this.flpCameraList.AutoScroll = true;
			this.flpCameraList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flpCameraList.Location = new System.Drawing.Point(3, 17);
			this.flpCameraList.Name = "flpCameraList";
			this.flpCameraList.Size = new System.Drawing.Size(126, 584);
			this.flpCameraList.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.Color.Transparent;
			this.groupBox2.ContextMenuStrip = this.rightMenuContext;
			this.groupBox2.Controls.Add(this.flpPlay);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(780, 604);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "监控预览";
			// 
			// rightMenuContext
			// 
			this.rightMenuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCameraCount,
            this.tsmiLevel});
			this.rightMenuContext.Name = "rightMenuContext";
			this.rightMenuContext.Size = new System.Drawing.Size(181, 70);
			// 
			// tsmiCameraCount
			// 
			this.tsmiCameraCount.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.单画面ToolStripMenuItem,
            this.四画面ToolStripMenuItem,
            this.九画面ToolStripMenuItem});
			this.tsmiCameraCount.Name = "tsmiCameraCount";
			this.tsmiCameraCount.Size = new System.Drawing.Size(180, 22);
			this.tsmiCameraCount.Text = "画  面(&S)";
			// 
			// 单画面ToolStripMenuItem
			// 
			this.单画面ToolStripMenuItem.Name = "单画面ToolStripMenuItem";
			this.单画面ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.单画面ToolStripMenuItem.Tag = "1";
			this.单画面ToolStripMenuItem.Text = "单画面";
			// 
			// 四画面ToolStripMenuItem
			// 
			this.四画面ToolStripMenuItem.Name = "四画面ToolStripMenuItem";
			this.四画面ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.四画面ToolStripMenuItem.Tag = "4";
			this.四画面ToolStripMenuItem.Text = "四画面";
			// 
			// 九画面ToolStripMenuItem
			// 
			this.九画面ToolStripMenuItem.Name = "九画面ToolStripMenuItem";
			this.九画面ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.九画面ToolStripMenuItem.Tag = "9";
			this.九画面ToolStripMenuItem.Text = "九画面";
			// 
			// tsmiLevel
			// 
			this.tsmiLevel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.流畅ToolStripMenuItem,
            this.标准ToolStripMenuItem,
            this.高清ToolStripMenuItem,
            this.超清ToolStripMenuItem});
			this.tsmiLevel.Name = "tsmiLevel";
			this.tsmiLevel.Size = new System.Drawing.Size(180, 22);
			this.tsmiLevel.Text = "清晰度(&L)";
			// 
			// 流畅ToolStripMenuItem
			// 
			this.流畅ToolStripMenuItem.Name = "流畅ToolStripMenuItem";
			this.流畅ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.流畅ToolStripMenuItem.Tag = "0";
			this.流畅ToolStripMenuItem.Text = "流畅";
			// 
			// 标准ToolStripMenuItem
			// 
			this.标准ToolStripMenuItem.Name = "标准ToolStripMenuItem";
			this.标准ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.标准ToolStripMenuItem.Tag = "1";
			this.标准ToolStripMenuItem.Text = "标准";
			// 
			// 高清ToolStripMenuItem
			// 
			this.高清ToolStripMenuItem.Name = "高清ToolStripMenuItem";
			this.高清ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.高清ToolStripMenuItem.Tag = "2";
			this.高清ToolStripMenuItem.Text = "高清";
			// 
			// 超清ToolStripMenuItem
			// 
			this.超清ToolStripMenuItem.Name = "超清ToolStripMenuItem";
			this.超清ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.超清ToolStripMenuItem.Tag = "3";
			this.超清ToolStripMenuItem.Text = "超清";
			// 
			// flpPlay
			// 
			this.flpPlay.BackColor = System.Drawing.Color.Transparent;
			this.flpPlay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flpPlay.Location = new System.Drawing.Point(3, 17);
			this.flpPlay.Name = "flpPlay";
			this.flpPlay.Size = new System.Drawing.Size(774, 584);
			this.flpPlay.TabIndex = 0;
			// 
			// gbLog
			// 
			this.gbLog.Controls.Add(this.txtLog);
			this.gbLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbLog.Location = new System.Drawing.Point(0, 0);
			this.gbLog.Name = "gbLog";
			this.gbLog.Size = new System.Drawing.Size(175, 604);
			this.gbLog.TabIndex = 0;
			this.gbLog.TabStop = false;
			this.gbLog.Text = "日志";
			// 
			// txtLog
			// 
			this.txtLog.BackColor = System.Drawing.SystemColors.Control;
			this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtLog.Location = new System.Drawing.Point(3, 17);
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.Size = new System.Drawing.Size(169, 584);
			this.txtLog.TabIndex = 0;
			this.txtLog.Text = "";
			// 
			// ts
			// 
			this.ts.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.ts.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsExit,
            this.toolStripSeparator1,
            this.tsLogout,
            this.tsLogin});
			this.ts.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.ts.Location = new System.Drawing.Point(0, 0);
			this.ts.Name = "ts";
			this.ts.Size = new System.Drawing.Size(1095, 25);
			this.ts.TabIndex = 3;
			this.ts.Text = "toolStrip1";
			// 
			// tsExit
			// 
			this.tsExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsExit.Image = global::Ys.Sdk.Demo.Properties.Resources.block_16;
			this.tsExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tsExit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsExit.Name = "tsExit";
			this.tsExit.Size = new System.Drawing.Size(68, 22);
			this.tsExit.Text = "退出(&X)";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsLogout
			// 
			this.tsLogout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsLogout.Enabled = false;
			this.tsLogout.Image = global::Ys.Sdk.Demo.Properties.Resources.left_16;
			this.tsLogout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tsLogout.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsLogout.Name = "tsLogout";
			this.tsLogout.Size = new System.Drawing.Size(66, 22);
			this.tsLogout.Text = "注销(&L)";
			// 
			// tsLogin
			// 
			this.tsLogin.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsLogin.Image = global::Ys.Sdk.Demo.Properties.Resources.user_16;
			this.tsLogin.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tsLogin.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsLogin.Name = "tsLogin";
			this.tsLogin.Size = new System.Drawing.Size(64, 22);
			this.tsLogin.Text = "登录(&I)";
			// 
			// st
			// 
			this.st.AutoSize = false;
			this.st.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.st.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stStatus,
            this.stProgress,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel5});
			this.st.Location = new System.Drawing.Point(0, 629);
			this.st.Name = "st";
			this.st.Size = new System.Drawing.Size(1095, 25);
			this.st.TabIndex = 4;
			this.st.Text = "statusStrip1";
			// 
			// stStatus
			// 
			this.stStatus.AutoSize = false;
			this.stStatus.Image = global::Ys.Sdk.Demo.Properties.Resources.info_16;
			this.stStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.stStatus.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.stStatus.Name = "stStatus";
			this.stStatus.Size = new System.Drawing.Size(300, 20);
			this.stStatus.Text = "就绪";
			this.stStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// stProgress
			// 
			this.stProgress.Name = "stProgress";
			this.stProgress.Size = new System.Drawing.Size(400, 19);
			this.stProgress.Visible = false;
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(628, 20);
			this.toolStripStatusLabel2.Spring = true;
			// 
			// toolStripStatusLabel3
			// 
			this.toolStripStatusLabel3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripStatusLabel3.IsLink = true;
			this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
			this.toolStripStatusLabel3.Size = new System.Drawing.Size(32, 20);
			this.toolStripStatusLabel3.Tag = "https://github.com/iwenli";
			this.toolStripStatusLabel3.Text = "开源";
			// 
			// toolStripStatusLabel4
			// 
			this.toolStripStatusLabel4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripStatusLabel4.IsLink = true;
			this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
			this.toolStripStatusLabel4.Size = new System.Drawing.Size(32, 20);
			this.toolStripStatusLabel4.Tag = "http://blog.iwenli.org";
			this.toolStripStatusLabel4.Text = "博客";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripStatusLabel1.IsLink = true;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(32, 20);
			this.toolStripStatusLabel1.Tag = "http://iwenli.org";
			this.toolStripStatusLabel1.Text = "主页";
			// 
			// toolStripStatusLabel5
			// 
			this.toolStripStatusLabel5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripStatusLabel5.IsLink = true;
			this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
			this.toolStripStatusLabel5.Size = new System.Drawing.Size(56, 20);
			this.toolStripStatusLabel5.Tag = "http://wpa.qq.com/msgrd?v=3&uin=234486036&site=qq&menu=yes";
			this.toolStripStatusLabel5.Text = "给我反馈";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1095, 654);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.ts);
			this.Controls.Add(this.st);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = global::Ys.Sdk.Demo.Properties.Resources.favicon;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.rightMenuContext.ResumeLayout(false);
			this.gbLog.ResumeLayout(false);
			this.ts.ResumeLayout(false);
			this.ts.PerformLayout();
			this.st.ResumeLayout(false);
			this.st.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStrip ts;
		private System.Windows.Forms.ToolStripButton tsExit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsLogout;
		private System.Windows.Forms.ToolStripButton tsLogin;
		private System.Windows.Forms.StatusStrip st;
		private System.Windows.Forms.ToolStripStatusLabel stStatus;
		private System.Windows.Forms.ToolStripProgressBar stProgress;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox gbLog;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.RichTextBox txtLog;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.FlowLayoutPanel flpCameraList;
		private System.Windows.Forms.FlowLayoutPanel flpPlay;
		private System.Windows.Forms.ContextMenuStrip rightMenuContext;
		private System.Windows.Forms.ToolStripMenuItem tsmiCameraCount;
		private System.Windows.Forms.ToolStripMenuItem tsmiLevel;
		private System.Windows.Forms.ToolStripMenuItem 单画面ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 四画面ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 九画面ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 流畅ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 标准ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 高清ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 超清ToolStripMenuItem;
	}
}