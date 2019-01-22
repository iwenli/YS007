namespace Ys.Sdk.Demo.Controls
{
	partial class PlayControl
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
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.picBoxPlay = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.picBoxPlay)).BeginInit();
			this.SuspendLayout();
			// 
			// picBoxPlay
			// 
			this.picBoxPlay.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.picBoxPlay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picBoxPlay.Location = new System.Drawing.Point(0, 0);
			this.picBoxPlay.Name = "picBoxPlay";
			this.picBoxPlay.Size = new System.Drawing.Size(586, 507);
			this.picBoxPlay.TabIndex = 1;
			this.picBoxPlay.TabStop = false;
			// 
			// PlayControl
			// 
			this.Controls.Add(this.picBoxPlay);
			this.Name = "PlayControl";
			this.Size = new System.Drawing.Size(586, 507);
			((System.ComponentModel.ISupportInitialize)(this.picBoxPlay)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox picBoxPlay;
	}
}
