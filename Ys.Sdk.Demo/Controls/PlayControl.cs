using System;
using System.Drawing;
using System.Windows.Forms;
using Ys.Sdk.Demo.Core;
using Ys.Sdk.Demo.Core.Entities.V2;
using Ys.Sdk.Demo.Core.V2;
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
		public string SessionId => sessionId;
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
		private string sessionId;

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
			SDKAdapter.OnMessage += SDKAdapter_OnMessage;	
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

		private void SDKAdapter_OnMessage(object sender, Core.V2.Event.MessageEventArgs e)
		{
			switch (e.MessageType)
			{
				case MessageType.INS_PLAY_EXCEPTION:
					break;
				case MessageType.INS_PLAY_RECONNECT:
					break;
				case MessageType.INS_PLAY_RECONNECT_EXCEPTION:
					break;
				case MessageType.INS_PLAY_START:
					playState = 10;
					picBoxPlay.Image = null;
					break;
				case MessageType.INS_PLAY_STOP:
					playState = 11;
					break;
				case MessageType.INS_PLAY_ARCHIVE_END:
					playState = 12;
					break;
				case MessageType.INS_VOICETALK_START:
					break;
				case MessageType.INS_VOICETALK_STOP:
					break;
				case MessageType.INS_VOICETALK_EXCEPTION:
					break;
				case MessageType.INS_PTZ_EXCEPTION:
					break;
				case MessageType.INS_RECORD_FILE:
					break;
				case MessageType.INS_RECORD_SEARCH_END:
					break;
				case MessageType.INS_RECORD_SEARCH_FAILED:
					break;
				case MessageType.INS_DEFENSE_SUCCESS:
					break;
				case MessageType.INS_DEFENSE_FAILED:
					break;
				case MessageType.INS_PLAY_ARCHIVE_EXCEPTION:
					break;
				case MessageType.INS_PTZCTRL_SUCCESS:
					break;
				case MessageType.INS_PTZCTRL_FAILED:
					break;
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
				picBoxPlay.Image = Resources.load2;
				try
				{
					_playOk = SDKAdapter.Play(picBoxPlay.Handle, Camera, context.CacheContext.Data.SafeKye,out sessionId);
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
			var _stopOK = SDKAdapter.Stop(sessionId);
			return _stopOK;
		}
	}
}
