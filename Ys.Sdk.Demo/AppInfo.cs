using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ys.Sdk.Demo.Common;

namespace Ys.Sdk.Demo
{
	/// <summary>
	/// 程序相关信息
	/// </summary>
	public class AppInfo : ConstParams
	{
		static string m_UpdateUrl = AppConfiguration.GetItem("update");
		/// <summary>
		/// 检测更新url
		/// </summary>
		public static string UpdateUrl
		{
			get
			{
				if (!Uri.IsWellFormedUriString(m_UpdateUrl, UriKind.Absolute))
				{
					m_UpdateUrl = "http://dm.txooo.com/software/update/{0}";
				}
				return m_UpdateUrl;
			}
		}
		/// <summary>
		/// 皮肤文件根目录
		/// </summary>
		public static string SkinRootPath
		{
			get { return Path.Combine(Environment.CurrentDirectory, "SKin"); }
		}
		/// <summary>
		///  主窗体皮肤路径
		/// </summary>
		public static string SkinFormPath
		{
			get { return Path.Combine(SkinRootPath, "FormSkin"); }
		}

		#region 程序集特性访问器
		/// <summary>
		/// 标题
		/// </summary>
		public static string AssemblyTitle
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				if (attributes.Length > 0)
				{
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
					if (titleAttribute.Title != "")
					{
						return titleAttribute.Title;
					}
				}
				return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}
		/// <summary>
		/// 版本
		/// </summary>
		public static string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public static string AssemblyDescription
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyDescriptionAttribute)attributes[0]).Description;
			}
		}
		/// <summary>
		/// 产品名称
		/// </summary>
		public static string AssemblyProduct
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyProductAttribute)attributes[0]).Product;
			}
		}
		/// <summary>
		/// 版权
		/// </summary>
		public static string AssemblyCopyright
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}
		/// <summary>
		/// 公司
		/// </summary>
		public static string AssemblyCompany
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}
		#endregion
	}
	/// <summary>
	/// 常量
	/// </summary>
	public class ConstParams
	{
		#region Const
		public const string PlatFormName = "无人超市监控平台";

		/// <summary>
		/// 作者
		/// </summary>
		public const string AUTHOR = @"iwenli";

		/// <summary>
		/// 作者
		/// </summary>
		public const string APP_AUTHOR = @"iwenli";

		/// <summary>
		/// 默认头像
		/// </summary>
		public const string DEFAULT_HEAD_PIC = @"https://img.txooo.com/2016/04/18/43dddcd3fff51e5418c33dbeef55c001.png";
		#endregion
		#region 程序集特性访问器
		/// <summary>
		/// 标题
		/// </summary>
		public static string AssemblyTitle
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				if (attributes.Length > 0)
				{
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
					if (titleAttribute.Title != "")
					{
						return titleAttribute.Title;
					}
				}
				return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}
		/// <summary>
		/// 版本
		/// </summary>
		public static string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public static string AssemblyDescription
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyDescriptionAttribute)attributes[0]).Description;
			}
		}
		/// <summary>
		/// 产品名称
		/// </summary>
		public static string AssemblyProduct
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyProductAttribute)attributes[0]).Product;
			}
		}
		/// <summary>
		/// 版权
		/// </summary>
		public static string AssemblyCopyright
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}
		/// <summary>
		/// 公司
		/// </summary>
		public static string AssemblyCompany
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}
		#endregion
	}
}
