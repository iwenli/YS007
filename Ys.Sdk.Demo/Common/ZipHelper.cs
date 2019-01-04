using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Common
{
	/// <summary>
	/// 压缩解压帮助类
	/// </summary>
	public class ZipHelper
	{
		/// <summary>
		/// 将传入字符串以GZip算法压缩后，返回Base64编码字符
		/// </summary>
		/// <param name="rawString">需要压缩的字符串</param>
		/// <returns>压缩后的Base64编码的字符串</returns>
		public static string GZipCompressString(string rawString)
		{
			if (string.IsNullOrEmpty(rawString) || rawString.Length == 0)
			{
				return "";
			}
			else
			{
				byte[] _rawData = Encoding.UTF8.GetBytes(rawString.ToString());
				byte[] _zippedData = Compress(_rawData);
				return (string)(Convert.ToBase64String(_zippedData));
			}
		}
		/// <summary>
		/// GZip压缩
		/// </summary>
		/// <param name="rawData"></param>
		/// <returns></returns>
		public static byte[] Compress(byte[] rawData)
		{
			MemoryStream _ms = new MemoryStream();
			GZipStream _compressedzipStream = new GZipStream(_ms, CompressionMode.Compress, true);
			_compressedzipStream.Write(rawData, 0, rawData.Length);
			_compressedzipStream.Close();
			return _ms.ToArray();
		}

		/// <summary>
		/// 将传入的二进制字符串资料以GZip算法解压缩
		/// </summary>
		/// <param name="zippedString">经GZip压缩后的二进制字符串</param>
		/// <returns>原始未压缩字符串</returns>
		public static string GZipDecompressString(string zippedString)
		{
			if (string.IsNullOrEmpty(zippedString) || zippedString.Length == 0)
			{
				return "";
			}
			else
			{
				byte[] _zippedData = Convert.FromBase64String(zippedString.ToString());
				return (string)(Encoding.UTF8.GetString(Decompress(_zippedData)));
			}
		}
		/// <summary>
		/// ZIP解压
		/// </summary>
		/// <param name="zippedData"></param>
		/// <returns></returns>
		public static byte[] Decompress(byte[] zippedData)
		{
			MemoryStream _ms = new MemoryStream(zippedData);
			GZipStream _compressedzipStream = new GZipStream(_ms, CompressionMode.Decompress);
			MemoryStream _outBuffer = new MemoryStream();
			byte[] _block = new byte[1024];
			while (true)
			{
				int _bytesRead = _compressedzipStream.Read(_block, 0, _block.Length);
				if (_bytesRead <= 0)
					break;
				else
					_outBuffer.Write(_block, 0, _bytesRead);
			}
			_compressedzipStream.Close();
			return _outBuffer.ToArray();
		}
	}
}
