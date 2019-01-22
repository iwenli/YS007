using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Txooo.Extension;
using Ys.Sdk.Demo.Core;
using Ys.Sdk.Demo.Core.Entities;
using Ys.Sdk.Demo.Forms;
using Ys.Sdk.Demo.Properties;
using Ys.Sdk.Demo.Service;

namespace Ys.Sdk.Demo.Controls
{
	public partial class PlayControl : MyUserControl
	{
		/// <summary>
		/// 当前播放的摄像机信息
		/// </summary>
		public CameraInfo Camera { set; get; }
		/// <summary>
		/// 当前播放会话
		/// </summary>
		public IntPtr Session => session;
		/// <summary>
		/// 是否可以全屏
		/// </summary>
		public bool CanFull { get; }

		/// <summary>
		/// 播放承载对象
		/// </summary>
		public PictureBox PlayBox => picBoxPlay;


		/// <summary>
		/// 播放状态  1:准备加载 2：加载中 3:加载失败  10：播放 11：暂停 12:回放结束
		/// </summary>
		private int playState = 1;
		/// <summary>
		/// 会话
		/// </summary>
		private IntPtr session;

		/// <summary>
		/// 构造器
		/// </summary>
		/// <param name="context"></param>
		/// <param name="canFull">是否可以全屏</param>
		public PlayControl(ServiceContext context, bool canFull = true, CameraInfo camera = null, RichTextBox richTextBox = null) : base(context, richTextBox)
		{
			InitializeComponent();
			BackColor = Color.Black;

			picBoxPlay.SizeMode = PictureBoxSizeMode.CenterImage;
			
			YsAction.OnSubscribe += YsAction_OnSubscribe;
			CanFull = canFull;
			if (canFull)
			{
				picBoxPlay.MouseDoubleClick += PicBoxPlay_MouseDoubleClick;
			}
			if (camera != null)
			{
				Camera = camera;
			}
		}

		/// <summary>
		/// 双击跳转到全屏显示窗口
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PicBoxPlay_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (Camera == null)
			{
				EM("无视频");
				return;
			}
			Stop();//关闭当前所有正在播放的该摄像头

			var frm = new FullCamera(new PlayControl(context, false, Camera, RichTextBox));
			frm.FormClosed += (s, args) =>
			{
				Play();
			};
			frm.ShowDialog();
		}

		private void YsAction_OnSubscribe(object sender, SubscribeEventArgs e)
		{
			switch (e.MsgType)
			{
				case 3:
					playState = 10;
					picBoxPlay.Image = null;
					break;
				case 4:
					playState = 11;
					break;
				case 5:
					playState = 12;
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// 播放
		/// </summary>
		/// <param name="camera"></param>
		/// <returns></returns>
		public bool Play(CameraInfo camera = null)
		{
			var _playOk = false;
			if (camera != null)
			{
				if (playState == 10)
				{
					Stop();
				}
				Camera = camera;
			}
			if (Camera != null)
			{
				session = YsAction.AllocSession();
				if (session == null)
				{
					throw new Exception("申请会话异常!");
				}
				picBoxPlay.Image = Resources.load2;
				try
				{
					_playOk = YsAction.Play(picBoxPlay.Handle, session, Camera.CameraId, context.CacheContext.Data.PlayConfig.Level, context.CacheContext.Data.SafeKye);
					if (!_playOk)
					{
						playState = 3;
					}
				}
				catch (Exception ex)
				{
					playState = 3;
					throw ex;
				}
			}
			return _playOk;
		}

		public bool Stop()
		{
			var _stopOK = YsAction.Stop(session);
			return _stopOK;
		}
	}
}
