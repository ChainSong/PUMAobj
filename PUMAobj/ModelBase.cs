using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj
{

    /// <summary>
    /// 建立(目标)模型基类
    /// </summary>
    public abstract class ModelBase
    {
        //定义委托
        public delegate bool SubEventHandler();

        //定义事件
        public event SubEventHandler SubEvent;

        //定义方法
        protected bool StartAbutment()
        {
            if (this.SubEvent != null)
            {
                //调用方法
                return this.SubEvent();
            }
            return false;
        }
    }
}
