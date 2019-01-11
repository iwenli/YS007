using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txooo.Extension.Extension;

namespace Ys.Sdk.Demo.Common
{
	public static class SerializeExtension
	{
		/// <summary>
		/// 序列化json字符串
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="ignoreNull"></param>
		/// <returns></returns>
		public static string ToJson(this object obj)
		{
			string result;
			if (obj.IsNull())
			{
				result = null;
			}
			else
			{
				result = JsonConvert.SerializeObject(obj);
			}
			return result;
		}
		/// <summary>
		/// 反序列化对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="jsonStr"></param>
		/// <returns></returns>
		public static T FromJson<T>(this string jsonStr)
		{
			return jsonStr.IsNullOrEmpty() ? default(T) : JsonConvert.DeserializeObject<T>(jsonStr);
		}
	}
}
