using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Txooo.Extension;
using Txooo.Extension.Extension;
using Ys.Sdk.Demo.Core;
using Ys.Sdk.Demo.Forms;
using Ys.Sdk.Demo.Properties;
using Ys.Sdk.Demo.Service;

namespace Ys.Sdk.Demo
{
	public partial class MainForm : FormBase
	{
		/// <summary>
		/// 记录用户是否正在播放状态，0为未播放，1为正在播放
		/// </summary>
		int a = 0;
		/// <summary>
		/// 表示用户选择的是几个窗口播放，默认为1   
		/// </summary>
		int c = 1;

		public MainForm()
		{
			InitializeComponent();
			//try
			//{
			//	M();
			//}
			//catch (Exception ex)
			//{
			//	Log(ex.Message);
			//}
			Load += MainForm_Load;
		}

		private async void MainForm_Load(object sender, EventArgs e)
		{
			await InitServiceContext();
			this.Text = ConstParams.AssemblyTitle + string.Format(" V{0}    PowerBy:{1}"
				, ConstParams.AssemblyVersion.Substring(0, ConstParams.AssemblyVersion.LastIndexOf('.'))
				, ConstParams.APP_AUTHOR);
			InitToolbar();
			InitStatusBar();
			InitFormControl();
			LogService.ShowLog += (msg) =>
			{
				AppendLogWarning(txtLog, msg);
			};
		}

		/// <summary>
		/// 主要业务绑定
		/// </summary>
		private void InitFormControl()
		{
			ResetPalyBox();
			flpPlay.Resize += (s, e) => { ResetPalyBox(); };
			YsAction.OnSubscribe += (e, s) =>
			{
				Log(s.ToJson());
			};
		}

		#region 服务接入

		/// <summary>
		/// 初始化服务状态
		/// </summary>
		async Task InitServiceContext()
		{
			BeginOperation("正在初始化配置信息...", 0, true);
			_context = new ServiceContext();
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
			Log("登录中...");
			if (new Login(_context).ShowDialog(this) != DialogResult.OK)
			{
				Log("登录取消...");
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
				Log("登录成功...");
				Log(tsLogin.Text);
				try
				{
					BeginOperation("开始更新缓存数据...", 0, true);
					await _context.CacheContext.UpdateAsync();
					EndOperation("缓存更新完成...");
					//TODO:渲染设备列表
					BeginOperation("开始渲染设备列表...", 0, true);
					await InitCameraAsync();
					EndOperation("设备列表渲染完成...");
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				finally
				{
					EndOperation();
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
			Log(stStatus.Text);
			stProgress.Visible = true;
			stProgress.Maximum = maxItemsCount > 0 ? maxItemsCount : 100;
			stProgress.Style = maxItemsCount > 0 ? ProgressBarStyle.Blocks : ProgressBarStyle.Marquee;
		}

		/// <summary>
		/// 操作结束
		/// </summary>
		void EndOperation(string opName = "就绪.")
		{
			Log(opName);
			stStatus.Text = opName;
			stProgress.Visible = false;
		}

		#endregion

		#region 萤石云SKD测试
		void M()
		{
			T("InitSdk", YsAction.InitSdk());
			T(YsAction.GetAccessToken());
			var _list = YsAction.GetCameraList();
			T("DisposeSdk", YsAction.DisposeSdk());
		}

		void T(string msg = "", bool result = true)
		{
			Log($"{msg} { (result ? "成功" : "失败")}");
		}
		#endregion

		void Log(string msg)
		{
			AppendLog(txtLog, msg);
		}

		async Task InitCameraAsync()
		{
			await Task.Run(() =>
			{
				var _serials = _context.CacheContext.Data.DeviceList.FindAll(m => m.DeviceType == 1).Select(m => m.Info).ToList();
				var _cameraList = _context.CacheContext.Data.CameraList.FindAll(m => _serials.Contains(m.DeviceSerial));
				foreach (var camera in _cameraList)
				{
					var lbl1 = new Label();
					lbl1.Text = $"{camera.DeviceName}({camera.DeviceSerial})-{camera.CameraName}"; //摄像头名字
					Label lbl2 = new Label();
					lbl2.Text = $"加密状态:{(camera.IsEncrypt == 1 ? "启用" : "未启用")}";
					Label lbl3 = new Label();
					lbl3.Text = $"在线状态:{(camera.Status == 1 ? "在线" : "不在线")}";
					var pic = new PictureBox();
					pic.Size = new Size(120, 120);
					pic.Tag = camera.CameraId;
					pic.Name = "pic_" + pic.Tag;
					pic.BackgroundImage = Resources.homeDevice;// Image.FromStream(WebRequest.Create(camera.PicUrl).GetResponse().GetResponseStream());//取网络图片
					pic.BackgroundImageLayout = ImageLayout.Stretch;//背景图自适应控件大小
					pic.MouseClick += new MouseEventHandler(picbox_MouseClick);//添加鼠标点击事件，方便后面确定点击的是哪个摄像头              
					pic.MouseHover += new System.EventHandler(picbox_MouseHover);
					pic.MouseLeave += new System.EventHandler(picbox_MouseLeave);

					if (flpCameraList.InvokeRequired)
					{
						flpCameraList.Invoke(new Action(() =>
						{
							flpCameraList.Controls.Add(pic);//添加控件picturebox
							flpCameraList.Controls.Add(lbl1);//添加控件label
							flpCameraList.Controls.Add(lbl2);//添加控件label
							flpCameraList.Controls.Add(lbl3);//添加控件label
						}));
					}
				}
			});
		}

		#region 单击该缩略图获取摄像头ID并播放
		string[] PlayCameraId = new string[9];
		IntPtr[] SessionId = new IntPtr[9];//存放申请的session
		PictureBox[] picbox = new PictureBox[9];//创建对象数组，存放picbox对象，最多9画面，所以最多9个
		static IntPtr[] handle = new IntPtr[9];//存放播放句柄
		int j = 0;
		int d = 0;
		private void picbox_MouseClick(object sender, MouseEventArgs e)
		{
			PictureBox picb1 = sender as PictureBox;//取出点击的控件sender
			if (picb1 == null)//点击则非空，否则为空
			{
				return;
			}
			var cameraId = picb1.Tag.ToString();
			if (c > 1)
			{
				if (PlayCameraId.Contains(cameraId))
				{
					IS("该视频已经在播放中，请勿重复点击！");
					return;
				}
			}
			else
			{

			}
			if (c == 1)//单画面
			{
				handle[0] = picbox[0].Handle;
				if (a == 1)
				{
					bool close = YsAction.Stop(SessionId[j]);
				}
				PlayCameraId[j] = cameraId;
				Play(c);
				j = 0;
			}
			//else if (c == 4)//4画面
			//{
			//	handle[j] = picbox[j].Handle;
			//	if (a == 1 && d == 1)
			//	{
			//		bool close = HkAction.Stop(SessionId[j]);
			//	}
			//	Start_Play4();
			//	if (j < 3)
			//	{
			//		j++;
			//	}
			//	else if (j == 3)
			//	{
			//		j = 0;
			//		d = 1;
			//	}
			//}
			//else if (c == 9)//9画面
			//{
			//	//picbox[j].Image = Image.FromFile("60.gif");//未成功播放前显示加载动态图
			//	//picbox[j].SizeMode = PictureBoxSizeMode.CenterImage;//图片居中
			//	handle[j] = picbox[j].Handle;//赋予句柄
			//	if (a == 1 && d == 1)//全部容器都在播放
			//	{
			//		bool close = HkAction.Stop(SessionId[j]);//关闭最开始打开的画面
			//	}
			//	Start_Play9();
			//	if (j < 8)
			//	{
			//		j++;
			//	}
			//	else if (j == 8)
			//	{
			//		j = 0;
			//		d = 1;
			//	}
			//}
		}

		/// <summary>
		/// 播放
		/// </summary>
		/// <param name="winCount"></param>
		void Play(int winCount = 1)
		{
			SessionId[j] = YsAction.AllocSession();//每次点击存放session
			if (SessionId[j] != null)//每次播放申请会话
			{
				bool play;
				try
				{
					play = YsAction.Play(handle[0], SessionId[j], PlayCameraId[j]);
					if (play == true)
					{
						a = 1;
					}
				}
				catch (Exception ex)
				{
					EM(ex.Message);
				}
			}
			else
			{
				IS("申请会话异常!");
			}
		}
		#region 创建播放容器并添加双击事件
		private void ResetPalyBox()
		{
			flpPlay.Controls.Clear();//清楚所有容器
			var _rolOrColCount = (int)Math.Sqrt(c);
			var height = (flpPlay.Height - _rolOrColCount) / _rolOrColCount;
			var width = (flpPlay.Width - _rolOrColCount) / _rolOrColCount;
			for (int i = 0; i < c; i++)
			{
				PictureBox pic = new PictureBox();
				pic.BackColor = Color.Black;
				pic.Size = new Size(width, height);//指定播放容器大小4画面
				pic.Name = "picBox" + i;
				flpPlay.Controls.Add(pic);//创建播放容器 
				picbox[i] = pic;
				handle[i] = picbox[0].Handle;
				pic.Margin = new Padding(1);
				//picbox[i].MouseDoubleClick += new MouseEventHandler(picbox_MouseDoubleClick);//添加鼠标双击击事件，用于全屏播放
			}
			//if (c == 1)//单画面只需创建一个容器即可
			//{

			//}
			//else//多画面时根据C的值创建容器
			//{
			//	for (int i = 0; i < c; i++)
			//	{
			//		PictureBox pic = new PictureBox();
			//		if (c == 4)
			//		{
			//			pic.Size = new Size(470, 330);//指定播放容器大小4画面
			//		}
			//		else
			//		{
			//			pic.Size = new Size(312, 218);//指定播放容器大小9画面
			//		}
			//		pic.BackColor = Color.Black;
			//		pic.Name = "picBox" + i.ToString();
			//		this.flowLayoutPanel1.Controls.Add(pic);//创建播放容器 
			//		picbox[i] = pic;
			//		handle[i] = picbox[i].Handle;
			//		pic.Margin = new System.Windows.Forms.Padding(1);
			//		picbox[i].MouseDoubleClick += new MouseEventHandler(picbox_MouseDoubleClick);
			//	}
			//}
		}
		#endregion
		#endregion
		#region 鼠标停在摄像头缩略图上显示播放图标
		private void picbox_MouseHover(object sender, EventArgs e)
		{
			PictureBox pictureBox = sender as PictureBox;
			if (pictureBox != null)//点击则非空，否则为空
			{
				pictureBox.Image = Resources._2;
				pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
			}
		}
		#endregion
		#region 鼠标离开时清除播放图片，重新显示缩略图
		private void picbox_MouseLeave(object sender, EventArgs e)
		{
			PictureBox pictureBox = sender as PictureBox;//取出点击的控件sender            
			if (pictureBox != null)//点击则非空，否则为空
			{
				pictureBox.Image = null;
			}
		}
		#endregion
	}
}
