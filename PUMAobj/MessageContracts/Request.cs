using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.MessageContracts
{
    public class Request
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


        private string _startTime;
        private string _endTime;
        private int _page;
        private int _pageSize;
        public Request(string _startTime, string _endTime, int _page, int _pageSize)
        {
            this._startTime = _startTime;
            this._endTime = _endTime;
            this._page = _page;
            this._pageSize = _pageSize;
        }

        public Request() { }
    }
}
