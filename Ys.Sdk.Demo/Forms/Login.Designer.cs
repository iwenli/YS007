namespace Ys.Sdk.Demo.Forms
{
	partial class Login
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtMobile = new System.Windows.Forms.TextBox();
			this.txtMobileCode = new System.Windows.Forms.TextBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnSendCode = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(22, 51);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "验证码";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(22, 21);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "手机号";
			// 
			// txtMobile
			// 
			this.txtMobile.Location = new System.Drawing.Point(69, 18);
			this.txtMobile.Name = "txtMobile";
			this.txtMobile.Size = new System.Drawing.Size(202, 21);
			this.txtMobile.TabIndex = 1;
			// 
			// txtMobileCode
			// 
			this.txtMobileCode.Location = new System.Drawing.Point(69, 45);
			this.txtMobileCode.Name = "txtMobileCode";
			this.txtMobileCode.Size = new System.Drawing.Size(202, 21);
			this.txtMobileCode.TabIndex = 2;
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(169, 81);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(102, 48);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "登录(&O)";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// btnSendCode
			// 
			this.btnSendCode.Location = new System.Drawing.Point(60, 81);
			this.btnSendCode.Name = "btnSendCode";
			this.btnSendCode.Size = new System.Drawing.Size(102, 48);
			this.btnSendCode.TabIndex = 3;
			this.btnSendCode.Text = "发送验证码(&S)";
			this.btnSendCode.UseVisualStyleBackColor = true;
			// 
			// Login
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(298, 148);
			this.Controls.Add(this.btnSendCode);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.txtMobileCode);
			this.Controls.Add(this.txtMobile);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Login";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtMobile;
		private System.Windows.Forms.TextBox txtMobileCode;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnSendCode;
	}
}