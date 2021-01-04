using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PUMAobj.MessageContracts
{
      /// <summary>
      /// 处理ConfigSection
      /// </summary>
    public class DefConfigurationSectionHandler : IConfigurationSectionHandler
    {

        public object Create(object parent, object configContext, XmlNode section)
        {
            Dictionary<string, DefConfigurationSectionRequest> names = new Dictionary<string, DefConfigurationSectionRequest>();

            string key = string.Empty;
            string startTime = string.Empty;
            string endTime = string.Empty;
            int page = 0;
            int pageSize = 0;
            string runningMode = "";
            int pageMax = 0;
            foreach (XmlNode childNode in section.ChildNodes)
            {
                if (childNode.Attributes["key"] != null)
                {
                    key = childNode.Attributes["key"].Value;

                    if (childNode.Attributes["startTime"] != null)
                    {
                        startTime = childNode.Attributes["startTime"].Value;
                    }
                    if (childNode.Attributes["endTime"] != null)
                    {
                        endTime = childNode.Attributes["endTime"].Value;
                    }
                    if (childNode.Attributes["page"] != null)
                    {
                        page = Convert.ToInt32(childNode.Attributes["page"].Value);
                    }
                    if (childNode.Attributes["pageSize"] != null)
                    {
                        pageSize = Convert.ToInt32(childNode.Attributes["pageSize"].Value);
                    }
                    if (childNode.Attributes["runningMode"] != null)
                    {
                        runningMode = childNode.Attributes["runningMode"].Value;
                    }
                    if (childNode.Attributes["pageMax"] != null)
                    {
                        pageMax = Convert.ToInt32(childNode.Attributes["pageMax"].Value);
                    }

                    names.Add(key, new DefConfigurationSectionRequest(startTime, endTime, page, pageSize, runningMode, pageMax));
                }
            }
            return names;
        }

    }
}
