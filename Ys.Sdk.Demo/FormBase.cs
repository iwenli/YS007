﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Txooo.Extension;
using Ys.Sdk.Demo.Common;
using Ys.Sdk.Demo.Properties;
using Ys.Sdk.Demo.Service;

namespace Ys.Sdk.Demo
{
	public partial class FormBase : Form
	{
		/// <summary>
		/// 获得当前的上下文
		/// </summary>
		protected ServiceContext _context;
		/// <summary>
		/// 当前日志显示控件
		/// </summary>
		protected RichTextBox RichTextBox { get; set; }

		public FormBase(ServiceContext serviceContext)
		{
			_context = serviceContext;
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		public FormBase()
		{

		}

		#region 内部函数
		protected async Task RunAsync(Action task)
		{
			await Task.Run(task);
		}
		/// <summary>
		/// 追加提示
		/// </summary>
		/// <param name="message"></param>
		protected void AppendLogInfo(string message, RichTextBox richTextBox = null)
		{
			AppendLog(message, Color.DarkGray, richTextBox);
		}
		/// <summary>
		/// 追加警告
		/// </summary>
		/// <param name="message"></param>
		protected void AppendLogWarning(string message, RichTextBox richTextBox = null)
		{
			AppendLog(message, Color.Violet, richTextBox);
		}
		/// <summary>
		/// 追加错误信息
		/// </summary>
		/// <param name="message"></param>
		protected void AppendLogError(string message, RichTextBox richTextBox = null)
		{
			AppendLog(message, Color.Red, richTextBox);
		}
		/// <summary>
		/// 追加日志
		/// </summary>
		/// <param name="message"></param>
		/// <param name="fontColor"></param>
		/// <param name="RichTextBox"></param>
		void AppendLog(string message, Color fontColor, RichTextBox richTextBox = null)
		{
			if (richTextBox != null)
			{
				RichTextBox = richTextBox;
			}
			if (RichTextBox != null)
			{
				if (RichTextBox.InvokeRequired)
				{
					RichTextBox.Invoke(new Action(() =>
					{
						AppendLog(message, fontColor, RichTextBox);
					}));
					return;
				}
				RichTextBox.SelectionColor = fontColor;
				RichTextBox.AppendText($"{DateTime.Now.ToString("HH:mm:ss")}=>{message}{Environment.NewLine}");
				RichTextBox.ScrollToCaret();
			}
			//记录文本日志
			this.AppendInfoLog(message);
		}

		/// <summary>
		/// 显示提示
		/// </summary>
		/// <param name="msg">提示内容</param>
		protected void SM(string msg)
		{
			AppendLogInfo(msg);
			MessageBox.Show(msg, ConstParams.AssemblyTitle);
		}
		/// <summary>
		/// 显示错误内容
		/// </summary>
		/// <param name="msg">错误内容</param>
		protected void EM(string msg)
		{
			AppendLogError(msg);
			MessageBox.Show(msg, ConstParams.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		/// <summary>
		/// 显示信息提示
		/// </summary>
		/// <param name="msg">错误内容</param>
		protected void IS(string msg)
		{
			AppendLogWarning(msg);
			MessageBox.Show(msg, ConstParams.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		/// <summary>
		/// 显示提示，带ye和no提示的
		/// </summary>
		/// <param name="msg">错误内容</param>
		protected DialogResult SMYN(string msg)
		{
			return MessageBox.Show(msg, ConstParams.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
		}
		#endregion
	}
}
