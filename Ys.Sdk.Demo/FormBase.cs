﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
		/// 追加警告
		/// </summary>
		/// <param name="txtLog"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		protected void AppendLogWarning(RichTextBox txtLog, string message, params object[] args)
		{
			if (txtLog.InvokeRequired)
			{
				txtLog.Invoke(new Action(() =>
				{
					AppendLog(txtLog, Color.Violet, message, args);
				}));
				return;
			}
			AppendLog(txtLog, Color.Violet, message, args);
		}
		/// <summary>
		/// 追加错误信息
		/// </summary>
		/// <param name="txtLog"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		protected void AppendLogError(RichTextBox txtLog, string message, params object[] args)
		{
			if (txtLog.InvokeRequired)
			{
				txtLog.Invoke(new Action(() =>
				{
					AppendLog(txtLog, Color.Red, message, args);
				}));
				return;
			}
			AppendLog(txtLog, Color.Red, message, args);
		}
		/// <summary>
		/// 添加日志 定义颜色
		/// </summary>
		/// <param name="txtLog"></param>
		/// <param name="fontColor"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		void AppendLog(RichTextBox txtLog, Color fontColor, string message, params object[] args)
		{
			if (txtLog.InvokeRequired)
			{
				txtLog.Invoke(new Action(() =>
				{
					AppendLog(txtLog, fontColor, message, args);
				}));
				return;
			}
			txtLog.SelectionColor = fontColor;
			AppendLog(txtLog, message, args);
		}
		/// <summary>
		/// 添加日志
		/// </summary>
		/// <param name="message"></param>
		/// <param name="args"></param>
		protected void AppendLog(RichTextBox txtLog, string message, params object[] args)
		{
			if (txtLog.InvokeRequired)
			{
				txtLog.Invoke(new Action(() =>
				{
					AppendLog(txtLog, message, args);
				}));
				return;
			}
			string timeL = DateTime.Now.ToString("HH:mm:ss");
			txtLog.AppendText(timeL + "=>");
			if (args == null || args.Length == 0)
			{
				txtLog.AppendText(message);
			}
			else
			{
				txtLog.AppendText(string.Format(message, args));
			}
			txtLog.AppendText(Environment.NewLine);
			txtLog.ScrollToCaret();
		}

		/// <summary>
		/// 显示提示
		/// </summary>
		/// <param name="msg">提示内容</param>
		protected void SM(string msg)
		{
			MessageBox.Show(msg, ConstParams.AssemblyTitle);
		}
		/// <summary>
		/// 显示错误内容
		/// </summary>
		/// <param name="msg">错误内容</param>
		protected void EM(string msg)
		{
			MessageBox.Show(msg, ConstParams.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		/// <summary>
		/// 显示信息提示
		/// </summary>
		/// <param name="msg">错误内容</param>
		protected void IS(string msg)
		{
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
