using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ys.Sdk.Demo.Core;
using Ys.Sdk.Demo.Properties;

namespace Ys.Sdk.Demo.Forms
{
	public partial class FullCamera : FormBase
	{
		private readonly string cameraId;
		private readonly IntPtr sessionId;
		private readonly int level;
		private readonly string safeKey;

		public FullCamera(string cameraId, IntPtr sessionId, int level, string safeKey)
		{
			InitializeComponent();

			this.cameraId = cameraId;
			this.sessionId = sessionId;
			this.level = level;
			this.safeKey = safeKey;
			var pictureBox = new PictureBox();
			pictureBox.BackColor = Color.Black;
			pictureBox.Name = "picFull";
			pictureBox.Dock = DockStyle.Fill;
			pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox.MouseDoubleClick += (s, e) =>
			{
				Closed();
			};
			Controls.Add(pictureBox);

			var play = YsAction.Play(pictureBox.Handle, sessionId, cameraId, level, safeKey);
			if (!play)
			{
				SM("全屏播放失败！");
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			int WM_KEYDOWN = 256;
			int WM_SYSKEYDOWN = 260;
			if (msg.Msg == WM_KEYDOWN | msg.Msg == WM_SYSKEYDOWN)
			{
				switch (keyData)
				{
					case Keys.Escape:
						Closed();//esc关闭窗体
						break;
				}
			}
			return false;
		}

		void Closed()
		{
			YsAction.Stop(sessionId);
			Close();
		}
	}
}
