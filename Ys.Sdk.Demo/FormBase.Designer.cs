namespace Ys.Sdk.Demo
{
	partial class FormBase
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
			this.SuspendLayout();
			// 
			// FormBase
			// 
			this.ClientSize = new System.Drawing.Size(238, 144);
			this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormBase";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = AppInfo.PlatFormName;
			this.ResumeLayout(false);

		}

		#endregion
	}
}