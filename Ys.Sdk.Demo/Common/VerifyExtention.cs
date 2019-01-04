using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Common
{
	/// <summary>
	/// 验证
	/// </summary>
	public static class VerifyExtention
	{
		/// <summary>
		/// 是否整数
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static bool IsIntege(this object obj)
		{
			return Check(obj, @"^-?[1-9]\d*$");
		}

		/// <summary>
		/// 是否为手机号
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static bool IsMoble(this object obj)
		{
			return Check(obj, @"^1[0-9]{10}$");
		}

		/// <summary>
		/// 检查数据是否匹配
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="reg"></param>
		/// <returns></returns>
		static bool Check(object obj, string reg)
		{
			return Regex.IsMatch(obj.ToString(),
				reg, RegexOptions.IgnoreCase | RegexOptions.Singleline);
		}
	}
}
