using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ticket.Common
{
    /// <summary>
    /// 下拉树选择
    /// </summary>
    public class TreeSelect
    {
        /// <summary>
        /// 显示内容
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public long value { get; set; }
        /// <summary>
        /// 上级ID
        /// </summary>
        public long ParentID { get; set; } 
        /// <summary>
        /// 是否已勾选
        /// </summary>
        public bool isChecked { get; set; }
        /// <summary>
        /// 嵌套结构
        /// </summary>
        public List<TreeSelect> children { get; set; }
    }
}
