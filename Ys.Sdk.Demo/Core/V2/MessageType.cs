using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Core.V2
{
	/// <summary>
	/// 消息类型定义
	/// </summary>
	public enum MessageType
	{
		/// <summary>
		/// 播放异常，通常是设备断线或网络异常造成
		/// </summary>
		INS_PLAY_EXCEPTION = 0,
		/// <summary>
		/// 重连，实时流播放时内部会自动重连
		/// </summary>
		INS_PLAY_RECONNECT = 1,
		/// <summary>
		/// 重连异常
		/// </summary>
		INS_PLAY_RECONNECT_EXCEPTION = 2,
		/// <summary>
		/// 播放开始
		/// </summary>
		INS_PLAY_START = 3,
		/// <summary>
		/// 播放终止
		/// </summary>
		INS_PLAY_STOP = 4,
		/// <summary>
		/// 播放结束，回放结束时会有此消息
		/// </summary>
		INS_PLAY_ARCHIVE_END = 5,
		/// <summary>
		/// 语音对讲开始
		/// </summary>
		INS_VOICETALK_START = 16,
		/// <summary>
		/// 语音对讲停止
		/// </summary>
		INS_VOICETALK_STOP = 17,
		/// <summary>
		/// 语音对讲异常
		/// </summary>
		INS_VOICETALK_EXCEPTION = 18,
		/// <summary>
		/// 云台控制异常
		/// </summary>
		INS_PTZ_EXCEPTION = 19,
		/// <summary>
		/// 查询的录像文件(录像搜索结果)
		/// </summary>
		INS_RECORD_FILE = 20,
		/// <summary>
		/// 录像查询结束（暂不使用）
		/// </summary>
		INS_RECORD_SEARCH_END = 21,
		/// <summary>
		/// 录像查询失败
		/// </summary>
		INS_RECORD_SEARCH_FAILED = 22,
		/// <summary>
		/// 布防成功
		/// </summary>
		INS_DEFENSE_SUCCESS = 23,
		/// <summary>
		/// 布防失败
		/// </summary>
		INS_DEFENSE_FAILED = 24,
		/// <summary>
		/// 回放异常结束，可能是接收数据超时
		/// </summary>
		INS_PLAY_ARCHIVE_EXCEPTION = 28,
		/// <summary>
		/// 云台控制命令发送成功
		/// </summary>
		INS_PTZCTRL_SUCCESS = 46,
		/// <summary>
		/// 云台控制失败
		/// </summary>
		INS_PTZCTRL_FAILED = 47
	}
}
