using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.Sdk.Demo.Service.Entities
{
	/// <summary>
	/// 播放配置
	/// </summary>
	public class PlayConfig
	{
		private int playBoxCount = 1;
		/// <summary>
		/// 监控窗体数
		/// </summary>
		public int PlayBoxCount
		{
			get
			{
				return playBoxCount;
			}
			set
			{
				if (value == 1 || value == 4 || value == 9)
				{
					playBoxCount = value;
				}
			}
		}

		private int level;
		/// <summary>
		/// 清晰度，0-流畅，1-均衡，2-高清，3-超清
		/// </summary>
		public int Level
		{
			get { return level; }
			set
			{
				if (value >= 0 && level <= 3)
				{
					level = value;
				}
			}
		}

	}
}
