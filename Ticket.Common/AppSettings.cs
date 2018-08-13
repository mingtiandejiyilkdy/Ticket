using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Ticket.Common
{
    /// <summary>
    /// 应用配置
    /// </summary>
    public static class AppSettings
    {

        /// <summary>
        /// 获取配置文件节点数据
        /// </summary>
        /// <param name="name">节点名称</param>
        /// <returns></returns>
        public static string Get(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }

    }
}
