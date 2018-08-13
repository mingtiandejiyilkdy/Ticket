/** 1. 功能：操作正则表达式的公共类
 *  2. 作者：何平
 *  3. 创建日期：2008-2-4
 *  4. 最后修改日期：2008-8-1
**/
#region 引用命名空间
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
#endregion
//51aspx.com 提供下载

namespace Ticket.Common
{
    /// <summary>
    /// 操作正则表达式的公共类
    /// </summary>    
    public class RegexHelper
    {
        #region 验证输入的字符串是否合法
        /// <summary>
        /// 验证输入的字符串是否合法，合法返回true,否则返回false。
        /// </summary>
        /// <param name="strInput">输入的字符串</param>
        /// <param name="strPattern">模式字符串</param>        
        public static bool Validate( string strInput , string strPattern )
        {
            return Regex.IsMatch( strInput , strPattern );
        }
        #endregion
    }
}
