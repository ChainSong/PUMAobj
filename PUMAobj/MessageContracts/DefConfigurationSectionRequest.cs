using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.MessageContracts
{
    public class DefConfigurationSectionRequest
    {
        /// <summary>
        /// 起始日期
        /// </summary>
        public string startTime { get { return _startTime; } set { _startTime = value; } }

        /// <summary>
        /// 终止日期
        /// </summary>
        public string endTime { get { return _endTime; } set { _endTime = value; } }

        /// <summary>
        /// 页码
        /// </summary>
        public int page { get { return _page; } set { _page = value; } }


        /// <summary>
        /// 页大小
        /// </summary>
        public int pageSize { get { return _pageSize; } set { _pageSize = value; } }

        /// <summary>
        /// 运行模式
        /// on_once:单子执行，已提供日期为准
        /// on_continuity:连续执行，已提供日期的间隔为准
        /// off:停止
        /// </summary>
        public string runningMode { get { return _runningMode; } set { _runningMode = value; } }

        public int pageMax { get { return _pageMax; } set { _pageMax = value; } }

        private string _startTime;
        private string _endTime;
        private int _page;
        private int _pageSize;
        private string _runningMode;
        private int _pageMax;
        public DefConfigurationSectionRequest(string _startTime, string _endTime, int _page, int _pageSize, string _runningMode, int _pageMax)
        {
            this._startTime = _startTime;
            this._endTime = _endTime;
            this._page = _page;
            this._pageSize = _pageSize;
            this._runningMode = _runningMode;
            this._pageMax = _pageMax;
        }
    }
}
