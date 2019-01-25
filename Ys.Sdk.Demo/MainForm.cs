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
using Ys.Sdk.Demo.Controls;
using Ys.Sdk.Demo.Core;
using Ys.Sdk.Demo.Core.V2;
using Ys.Sdk.Demo.Forms;
using Ys.Sdk.Demo.Properties;
using Ys.Sdk.Demo.Service;

namespace Ys.Sdk.Demo
{
	public partial class MainForm : FormBase
	{
		List<PlayControl> playControls = new List<PlayControl>();

		public MainForm()
		{
			InitializeComponent();
			RichTextBox = txtLog;

			//try
			//{
			//	#region 萤石云SKD测试
			//	var _list = SDKAdapter.GetDeviceList();
			//	var _list1 = SDKAdapter.GetDeviceList(true);
			//	var _device = SDKAdapter.GetDevice("C16354862");
			//	//T("DisposeSdk", YsAction.DisposeSdk());

			//	void T(string msg = "", bool result = true)
			//	{
			//		AppendLogInfo($"{msg} { (result ? "成功" : "失败")}");
			//	}
			//	#endregion
			//}
			//catch (Exception ex)
			//{
			//	AppendLogError(ex.Message);
			//}


			Load += MainForm_Load;
			FormClosed += async (s, e) => { await Task.Run(() => { SDKAdapter.Dispose(); }); };
		}

		private async void MainForm_Load(object sender, EventArgs e)
		{
			await InitServiceContext();
			Text = ConstParams.AssemblyTitle + string.Format(" V{0}    PowerBy:{1}"
				, ConstParams.AssemblyVersion.Substring(0, ConstParams.AssemblyVersion.LastIndexOf('.'))
				, ConstParams.APP_AUTHOR);
			InitToolbar();
			InitStatusBar();
		}

		/// <summary>
		/// 主要业务绑定
		/// </summary>
		/// 
		async Task InitFormControlAsync()
		{
			InitContextMenu();
			await InitPalyBoxAsync();
			flpPlay.Resize += async (s, e) => { await InitPalyBoxAsync(false); };
		}

		/// <summary>
		/// 右键菜单相关事件
		/// </summary>
		private void InitContextMenu(bool isBindEvent = true)
		{
			foreach (var subItem in rightMenuContext.Items)
			{
				if (subItem is ToolStripMenuItem)
				{
					var _subItem = subItem as ToolStripMenuItem;
					if (_subItem.Name == nameof(tsmiCameraCount))
					{
						//监控数量
						foreach (ToolStripMenuItem item in _subItem.DropDownItems)
						{
							item.Checked = item.Tag.ToString() == _context.CacheContext.Data.PlayConfig.PlayBoxCount.ToString();
							if (isBindEvent)
							{
								item.Click += (s, e) =>
								{
									//更新
									var _value = Convert.ToInt32((s as ToolStripMenuItem).Tag);
									_context.CacheContext.Data.PlayConfig.PlayBoxCount = _value;
									_context.CacheContext.Save();
									InitContextMenu(false);
									InitPalyBoxAsync();
								};
							}
						}
					}
					else if (_subItem.Name == nameof(tsmiLevel))
					{
						//清晰度
						foreach (ToolStripMenuItem item in _subItem.DropDownItems)
						{
							item.Checked = item.Tag.ToString() == _context.CacheContext.Data.PlayConfig.Level.ToString();
							if (isBindEvent)
							{
								item.Click += (s, e) =>
								{
									//更新
									var _value = Convert.ToInt32((s as ToolStripMenuItem).Tag);
									_context.CacheContext.Data.PlayConfig.Level = _value;
									_context.CacheContext.Save();
									InitContextMenu(false);
								};
							}
						}
					}
				}
			}
		}


		#region 服务接入

		/// <summary>
		/// 初始化服务状态
		/// </summary>
		async Task InitServiceContext()
		{
			BeginOperation("正在初始化配置信息...", 0, true);
			_context = new ServiceContext();
			SDKAdapter.OnData += (s, e) =>
			 {
				 AppendLogInfo(e.ToString());
			 };
			SDKAdapter.OnMessage += (s, e) =>
			{
				AppendLogInfo(e.ToString());
			};
			SDKAdapter.OnPushMessage += (s, e) =>
			{
				AppendLogInfo(e.ToString());
			};
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
			//var _sessionId = YsAction.AllocSession();
			//var _searchResult = YsAction.Search(_sessionId, "C39424393", 1);
			AppendLogWarning("登录中...");
			if (new Login(_context, txtLog).ShowDialog(this) != DialogResult.OK)
			{
				AppendLogWarning("登录取消...");
			}
		}
		/// <summary>
		/// 注销
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Logout(object sender, EventArgs e)
		{
			txtLog.Clear();
			flpCameraList.Controls.Clear();
			flpPlay.Controls.Clear();
			playControls.Clear();

			_context.CacheContext.Save();
			_context.Session.Logout();
			AppendLogWarning("退出登录成功！");
		}
		/// <summary>
		/// 登录状态变化
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async Task LoginedChanged()
		{
			tsLogin.Enabled = !(splitContainer2.Enabled = tsLogout.Enabled = _context.Session.IsLogined);
			tsLogin.Text = _context.Session.IsLogined ? string.Format("已登录为【{0} ({1})】", _context.Session.LoginInfo.Mobile, _context.Session.LoginInfo.BrandList.FirstOrDefault().BrandName) : "登录(&I)";
			if (_context.Session.IsLogined)
			{
				AppendLogInfo("登录成功...");
				AppendLogInfo(tsLogin.Text);
				try
				{
					BeginOperation("开始更新缓存数据...", 0, true);
					await _context.CacheContext.UpdateAsync();
					EndOperation("缓存更新完成...");
					//TODO:渲染设备列表
					BeginOperation("开始渲染设备列表...", 0, true);
					await InitCameraListAsync();
					EndOperation("设备列表渲染完成...");
					//绑定主业务
					InitFormControlAsync();
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
		void BeginOperation(string opName = "正在操作，请稍等...", int maxItemsCount = 0, bool disableForm = true)
		{
			stStatus.Text = opName;
			AppendLogWarning(stStatus.Text);
			stProgress.Visible = true;
			stProgress.Maximum = maxItemsCount > 0 ? maxItemsCount : 100;
			stProgress.Style = maxItemsCount > 0 ? ProgressBarStyle.Blocks : ProgressBarStyle.Marquee;
		}

		/// <summary>
		/// 操作结束
		/// </summary>
		void EndOperation(string opName = "就绪.")
		{
			AppendLogInfo(opName);
			stStatus.Text = opName;
			stProgress.Visible = false;
		}

		#endregion

		/// <summary>
		/// 初始化监控列表
		/// </summary>
		/// <returns></returns>
		async Task InitCameraListAsync()
		{
			await Task.Run(() =>
			{
				var deviceList = _context.CacheContext.Data.DeviceList.FindAll(m => m.DeviceType == 1);

				foreach (var device in deviceList)
				{
					var cameraDevice = _context.CacheContext.Data.YSDeviceList.Find(m => m.DeviceSerial == device.Info); ;
					var store = _context.CacheContext.Data.StoreList.Find(m => m.StoreId == device.StoreId);
					foreach (var camera in cameraDevice.CameraInfo)
					{
						var lbl0 = new Label()
						{
							Text = $"{store.StoreName}",
							AutoSize = true
						};
						var lbl1 = new Label()
						{
							Text = $"通道：{camera.CameraNo}({camera.CameraName})",
							AutoSize = true
						};
						var lbl2 = new Label()
						{
							Text = $"加密状态:{(cameraDevice.IsEncrypt ? "启用" : "未启用")}"
						};
						var lbl3 = new Label()
						{
							Text = $"在线状态:{(cameraDevice.Status == 1 ? "在线" : "不在线")}"
						};
						var pic = new PictureBox();
						pic.Size = new Size(120, 120);
						pic.Tag = camera.Id;
						pic.Name = "pic_" + pic.Tag;
						pic.BackgroundImage = Resources.homeDevice;// Image.FromStream(WebRequest.Create(camera.PicUrl).GetResponse().GetResponseStream());//取网络图片
						pic.BackgroundImageLayout = ImageLayout.Stretch;//背景图自适应控件大小
						pic.MouseClick += new MouseEventHandler(picbox_MouseClick);//添加鼠标点击事件，方便后面确定点击的是哪个摄像头              
						pic.MouseHover += new EventHandler(picbox_MouseHover);
						pic.MouseLeave += new EventHandler(picbox_MouseLeave);

						if (flpCameraList.InvokeRequired)
						{
							flpCameraList.Invoke(new Action(() =>
							{
								flpCameraList.Controls.Add(pic);//添加控件picturebox
								flpCameraList.Controls.Add(lbl0);//添加控件label
								flpCameraList.Controls.Add(lbl1);//添加控件label
								flpCameraList.Controls.Add(lbl2);//添加控件label
								flpCameraList.Controls.Add(lbl3);//添加控件label
							}));
						}
					}
				}
			});
		}

		#region 单击该缩略图获取摄像头ID并播放
		private void picbox_MouseClick(object sender, MouseEventArgs e)
		{
			var pictureBox = sender as PictureBox;//取出点击的控件sender
			if (pictureBox == null)//点击则非空，否则为空
			{
				return;
			}
			var cameraId = pictureBox.Tag.ToString();
			var camera = _context.CacheContext.Data.YSDeviceList.SelectMany(m => m.CameraInfo).FirstOrDefault(m => m.Id == cameraId);
			var _device = _context.CacheContext.Data.YSDeviceList.Find(m => m.DeviceSerial == camera?.DeviceSerial);
			if (_device.IsNull())
			{
				EM("不存在的设备！");
				return;
			}
			if (_device?.Status != 1)
			{
				EM("设备不在线，无法播放！");
				return;
			}
			if (playControls.Any(m => m.Camera?.Id == cameraId))
			{
				IS("该视频已经在播放中，请勿重复点击！");
				return;
			}
			var _playControl = playControls.FirstOrDefault(m => m.Camera.IsNull());
			if (_playControl == null)
			{
				_playControl = playControls.FirstOrDefault();
			}
			_playControl.Play(camera);
		}
		#endregion

		#region 创建播放容器并添加双击事件
		async Task InitPalyBoxAsync(bool isRrset = true)
		{
			BeginOperation("重置播放容器开始");
			await RunAsync(() =>
			{
				InitPalyBox(isRrset);
			});
			EndOperation("重置播放容器完成");
		}
		void InitPalyBox(bool isRrset = true)
		{
			if (flpPlay.InvokeRequired)
			{
				flpPlay.Invoke(new Action(
					() =>
					{
						InitPalyBox(isRrset);
					}
				));
				return;
			}
			if (isRrset)
			{
				flpPlay.Controls.Clear();//清除所有容器
				playControls.Clear();
				for (int i = 0; i < _context.CacheContext.Data.PlayConfig.PlayBoxCount; i++)
				{
					var playControl = new PlayControl(_context, richTextBox: txtLog)
					{
						Name = "playBox" + i,
						Tag = i,
						Margin = new Padding(1)
					};
					flpPlay.Controls.Add(playControl);//创建播放容器 
					playControls.Add(playControl);
				}
			}
			var _rolOrColCount = (int)Math.Sqrt(_context.CacheContext.Data.PlayConfig.PlayBoxCount);
			var height = (flpPlay.Height - _rolOrColCount * 2) / _rolOrColCount;
			var width = (flpPlay.Width - _rolOrColCount * 2) / _rolOrColCount;
			foreach (var control in playControls)
			{
				control.Size = new Size(width, height);//指定播放容器大小
			}
		}
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
