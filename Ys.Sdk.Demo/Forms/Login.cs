using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Txooo.Extension;
using Txooo.Extension.Extension;
using Ys.Sdk.Demo.Common;
using Ys.Sdk.Demo.Service;

namespace Ys.Sdk.Demo.Forms
{
	partial class Login : FormBase
	{
		const int TotalCount = 60; //倒计时总秒数

		/// <summary>
		/// 服务端验证码
		/// </summary>
		string _serverVerityCode = string.Empty;
		//验证码倒计时
		Timer _timer = new Timer();
		int leftTime = TotalCount;

		public Login(ServiceContext context,RichTextBox richTextBox) : base(context)
		{
			InitializeComponent();
			RichTextBox = richTextBox;
			Text = "登录 " + ConstParams.AssemblyTitle;
			Init();
		}

		/// <summary>
		/// 初始化
		/// </summary>
		void Init()
		{
			txtMobile.SetHintText("手机号");
			txtMobileCode.SetHintText("验证码");
			btnOk.Enabled = false;
			btnOk.Click += BtnClick;
			btnSendCode.Click += BtnClick;
			var _brandId = 499006;  // 497863;  //498451
			txtMobile.Text = "11111" + _brandId;
			txtMobileCode.Text = _brandId.ToString();

			_timer.Interval = 1000;
			_timer.Tick += (s, e) =>
			{
				if (leftTime <= 1)
				{
					_timer.Stop();
					_timer.Enabled = false;
					leftTime = TotalCount;
					btnSendCode.Text = "发送验证码(&S)";
					btnSendCode.Enabled = true;
				}
				else
				{
					leftTime--;
					btnSendCode.Enabled = false;
					btnSendCode.Text = $"有效期({leftTime}S)";
				}
			};
		}

		private async void BtnClick(object sender, EventArgs e)
		{
			var button = sender as Button;
			var mobile = txtMobile.Text;
			if (!mobile.IsMoble())
			{
				IS("请输入正确的手机号");
				button.Focus();
			}
			else
			{
				if (button == btnOk)
				{
					//登陆
					var mobileCode = txtMobileCode.Text.Trim();
					if (!mobileCode.IsIntege() || mobileCode.Length != 6)
					{
						IS("输入正确的验证码");
						button.Focus();
					}
					else
					{
						button.Enabled = false;
						btnOk.Text = "正在登陆.";

						var result = await _context.Session.LoginAsync(_context, mobile, _serverVerityCode, mobileCode);
						if (result == null)
						{
							this.DialogResult = DialogResult.OK;
							Close();
							return;
						}
						btnOk.Text = "登录(&O)";
						EM("登录失败：" + result.Message);
						button.Enabled = true;
					}
				}
				else if (button == btnSendCode)
				{
					//发送验证码
					btnSendCode.Enabled = false;
					try
					{
						var checkResult = await _context.PassportService.MobileCanLogin(mobile);
						if (!checkResult.IsSuccess)
						{
							IS(checkResult.Msg);
							btnSendCode.Enabled = true;
						}
						else
						{
							SendMobileCodeAsync(mobile);
						}
					}
					catch (Exception ex)
					{
						EM(ex.Message);
						btnSendCode.Enabled = true;
					}
				}
			}
		}

		/// <summary>
		/// 发送验证码
		/// </summary>
		/// <param name="mobile"></param>
		private async void SendMobileCodeAsync(string mobile)
		{
			if (!_timer.Enabled)
			{
				try
				{
					var sendResult = await _context.PassportService.SendMobileCode(mobile);
					if (!sendResult.IsSuccess)
					{
						IS(sendResult.Msg);
						btnSendCode.Enabled = true;
					}
					else
					{
						_serverVerityCode = sendResult.MobileVerify;
						btnOk.Enabled = true;
						_timer.Enabled = true;
						_timer.Start();
					}
				}
				catch (Exception ex)
				{
					EM(ex.GetInnerException().Message);
					btnSendCode.Enabled = true;
				}
			}
		}
	}
}
