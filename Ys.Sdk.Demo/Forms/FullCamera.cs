using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ys.Sdk.Demo.Controls;
using Ys.Sdk.Demo.Core;
using Ys.Sdk.Demo.Properties;

namespace Ys.Sdk.Demo.Forms
{
	public partial class FullCamera : FormBase
	{

		public FullCamera(PlayControl playControl)
		{
			InitializeComponent();
			PlayControl = playControl;

			PlayControl.Dock = DockStyle.Fill;
			Controls.Add(PlayControl);
			PlayControl.Play();
			PlayControl.PlayBox.MouseDoubleClick += (s, e) =>
			{
				Exit();
			};
		}

		public PlayControl PlayControl { get; }

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			int WM_KEYDOWN = 256;
			int WM_SYSKEYDOWN = 260;
			if (msg.Msg == WM_KEYDOWN | msg.Msg == WM_SYSKEYDOWN)
			{
				switch (keyData)
				{
					case Keys.Escape:
						Exit();//esc关闭窗体
						break;
				}
			}
			return false;
		}

		void Exit()
		{
			PlayControl.Stop();
			base.Close();
		}
	}
}
