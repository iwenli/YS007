using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ys.Sdk.Demo.Forms;
using Ys.Sdk.Demo.Service;

namespace Ys.Sdk.Demo
{
	public partial class MainForm : FormBase
	{
		public MainForm()
		{
			InitializeComponent();
			Load += MainForm_Load;
		}

		private async void MainForm_Load(object sender, EventArgs e)
		{
			await InitServiceContext();
			this.Text = AppInfo.AssemblyTitle + string.Format(" V{0}    PowerBy:{1}"
				, AppInfo.AssemblyVersion.Substring(0, ConstParams.AssemblyVersion.LastIndexOf('.'))
				, AppInfo.APP_AUTHOR);
			InitToolbar();
			InitStatusBar();
			InitFormControl();
		}

		/// <summary>
		/// 主要业务绑定
		/// </summary>
		private void InitFormControl()
		{
		}

		#region 服务接入

		/// <summary>
		/// 初始化服务状态
		/// </summary>
		async Task InitServiceContext()
		{
			_context = new ServiceContext();
			BeginOperation("正在初始化配置信息...", 0, true);
			await Task.Delay(1000);
			EndOperation();
		}

		#endregion

		#region 登陆 注销
		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Login(object sender, EventArgs e)
		{
			AppendLog(txtLog, "登录中...");
			if (new Login(_context).ShowDialog(this) != DialogResult.OK)
			{
				AppendLog(txtLog, "登录取消...");
			}
		}
		/// <summary>
		/// 注销
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Logout(object sender, EventArgs e)
		{
			//清空数据
			//foreach (Control ctl in gbArea.Controls)
			//{
			//    if (ctl is ComboBox)
			//    {
			//        var combobox = ctl as ComboBox;
			//        if (combobox.DataSource != null)
			//        {
			//            combobox.DataSource = null;
			//        }
			//    }
			//}
			//foreach (Control ctl in gbClass.Controls)
			//{
			//    if (ctl is ComboBox)
			//    {
			//        var combobox = ctl as ComboBox;
			//        if (combobox.DataSource != null)
			//        {
			//            combobox.DataSource = null;
			//        }
			//    }
			//}
			//txtLog.Text = string.Empty;
			_context.CacheContext.Save();
			_context.Session.Logout();
			AppendLogWarning(txtLog, "退出登录成功！");
		}
		/// <summary>
		/// 登录状态变化
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async Task LoginedChanged()
		{
			tsLogin.Enabled = !(splitContainer2.Enabled = tsLogout.Enabled = _context.Session.IsLogined);
			tsLogin.Text = _context.Session.IsLogined ? string.Format("已登录为【{0} ({1})】", _context.Session.LoginInfo.Mobile, _context.Session.LoginInfo.UserId) : "登录(&I)";
			if (_context.Session.IsLogined)
			{
				AppendLog(txtLog, "登录成功...");
				AppendLog(txtLog, tsLogin.Text);
				try
				{
					BeginOperation("开始更新缓存数据...");
					await _context.CacheContext.UpdateAsync();
					//TODO:渲染设备列表
					var _companyList = await _context.PassportService.GetCompanyList();
					if (_companyList.IsSuccess)
					{
						_context.Session.LoginInfo.BrandList.AddRange(_companyList.Data);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				finally
				{
					EndOperation("缓存更新完成...");
				}
			}
		}
		#endregion

		#region  初始化工具栏状态栏 
		/// <summary>
		/// 初始化工具栏
		/// </summary>
		void InitToolbar()
		{
			tsExit.Click += (s, e) => Close();
			tsLogin.Enabled = !(splitContainer2.Enabled = tsLogout.Enabled = _context.Session.IsLogined);
			tsLogin.Click += Login;
			tsLogout.Click += Logout;
			//捕捉登录状态变化事件，在登录状态发生变化的时候重设登录状态
			_context.Session.LoginStateChange += async (s, e) => await LoginedChanged();
		}

		/// <summary>
		/// 初始化状态栏
		/// </summary>
		void InitStatusBar()
		{
			//绑定链接处理
			foreach (var label in st.Items.OfType<ToolStripStatusLabel>().Where(s => s.IsLink && s.Tag != null))
			{
				label.Click += (s, e) =>
				{
					try
					{
						Process.Start((s as ToolStripStatusLabel).Tag.ToString());
					}
					catch (Exception ex)
					{
						MessageBox.Show(this, "错误：无法打开网址，错误信息：" + ex.Message + "。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				};
			}
		}
		#endregion

		#region 异步操作
		/// <summary>
		/// 表示开始一个操作
		/// </summary>
		/// <param name="opName">当前操作的名称</param>
		/// <param name="maxItemsCount">当前操作如果需要显示进度，那么提供任务总数；不提供则为跑马灯等待</param>
		/// <param name="disableForm">是否禁用当前窗口的操作</param>
		void BeginOperation(string opName = "正在操作，请稍等...", int maxItemsCount = 100, bool disableForm = false)
		{
			stStatus.Text = opName;
			AppendLog(txtLog, stStatus.Text);
			stProgress.Visible = true;
			stProgress.Maximum = maxItemsCount > 0 ? maxItemsCount : 100;
			stProgress.Style = maxItemsCount > 0 ? ProgressBarStyle.Blocks : ProgressBarStyle.Marquee;
		}

		/// <summary>
		/// 操作结束
		/// </summary>
		void EndOperation(string opName = "就绪.")
		{
			AppendLog(txtLog, opName);
			stStatus.Text = opName;
			stProgress.Visible = false;
		}

		#endregion
	}
}
