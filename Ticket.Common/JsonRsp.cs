using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Ticket.Common
{

    /// <summary>
    /// 通用JSON输出结构
    /// </summary>
    [Serializable]
    public class JsonRsp 
    {
        public JsonRsp() : base() { }

        private bool _success = false;
        /// <summary>
        /// 输出代码
        /// </summary>
        public bool success
        {
            set { _success = value; }
            get { return _success; }
        }

        private int _code = -1;
        /// <summary>
        /// 输出代码
        /// </summary>
        public int code
        {
            set { _code = value; }
            get { return _code; }
        }


        private object _returnvalue ;
        /// <summary>
        /// 返回值
        /// </summary>
        public object returnObj
        {
            set { _returnvalue = value; }
            get { return _returnvalue; }
        }

        private string _retmsg = string.Empty;
        /// <summary>
        /// 输出消息
        /// </summary>
        public string retmsg
        {
            set { _retmsg = value; }
            get { return _retmsg; }
        }

        private string _depict = string.Empty;
        /// <summary>
        /// 附加信息
        /// </summary>
        public string depict
        {
            set { _depict = value; }
            get { return _depict; }
        }

        private int _count = 0;
        /// <summary>
        /// 搜索到符合条件的结果总数
        /// </summary>
        public int count
        {
            set { _count = value; }
            get
            {
                return _count;
            }
        }

        // <summary>
        /// 转换对象成 JSON 字符串之后可以方便在 javascript 访问对象的属性 ，如 UserInfo 有属性 Name， 在 javascript 中可以用 var obj = eval('(jsonString)'); obj.Name 得到其值
        /// </summary>
        /// <returns></returns>
        public virtual string ToJSONString()
        {
            return ToJSONString(this);
        }
        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="o">this</param>
        /// <returns></returns>
        protected string ToJSONString(object o)
        {
            if (o == null) //如果对象为空直接返回
            {
                return "{}";
            }

            System.Reflection.PropertyInfo[] ps = o.GetType().GetProperties();
            StringBuilder jsonSB = new StringBuilder("{");
            int index = 1;
            foreach (System.Reflection.PropertyInfo pi in ps)
            {
                //Name 为属性名称,GetValue 得到属性值(参数this 为对象本身,null)
                string name = pi.Name;

                if (pi == null)
                {
                    continue;
                }

                if (pi.PropertyType == typeof(int) || pi.PropertyType == typeof(long) || pi.PropertyType == typeof(string) || pi.PropertyType == typeof(DateTime) || pi.PropertyType == typeof(bool) || pi.PropertyType == typeof(double))
                {
                    string value = Convert.ToString(pi.GetValue(o, null));
                    //把 javascript 中的特殊字符转成转义字符
                    value = value.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\"", "\\\"");

                    if (pi.PropertyType == typeof(int) || pi.PropertyType == typeof(long) || pi.PropertyType == typeof(double))//是否为字符串和日期类型
                    {
                        jsonSB.Append("\"" + name + "\"" + ":" + value + "");
                    }
                    else
                    {
                        jsonSB.Append("\"" + name + "\"" + ":\"" + value + "\"");
                    }
                }
                else if (pi.PropertyType.IsGenericType == true)//是否为泛型类
                {
                    //泛型类 // 2010-04-14 取消原方法 使用新增 自动遍历 泛型对象
                    //json += "\"" + name + "\"" + ":[]";

                    //自动 遍历泛型对象  210-04-14 启用
                    jsonSB.Append("\"" + name + "\":");
                    jsonSB.Append(ToJSONString<object>((IList)pi.GetValue(o, null)));

                    /*
                    foreach (Type piType in pi.PropertyType.GetGenericArguments())
                    {
                        piType.Name;//可以获取 泛型对象的类型名 -- 但仍然无法递归调用 暂时继续无法解决 2010-04-13
                    }
                     */
                }
                else if (pi.PropertyType.IsArray)//数组 代码暂略 未实化 数组代码 成员遍历
                {
                    jsonSB.Append("\"" + name + "\"" + ":[]");
                }
                else //其它自定义类对象
                {
                    //json += "\t\"" + name + "\"" + ":";//deug 模式代码

                    jsonSB.Append("\"" + name + "\"" + ":");
                    //回调参数
                    jsonSB.Append(ToJSONString(pi.GetValue(o, null)));//Convert.ToString(pi.GetValue(o, null));
                    //json += ",\r"; //debug 代码
                }

                if (index != ps.Length)
                {
                    //json += "," + "\r\n";//debug 代码
                    jsonSB.Append(",");
                }

                index++;
            }
            jsonSB.Append("}");
            return jsonSB.ToString();
        }
        /// <summary>
        /// 泛型对象集合 接口类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        protected string ToJSONString<T>(IList list)
        {
            T obj = (T)Activator.CreateInstance(typeof(T));
            StringBuilder jsonSB = new StringBuilder();
            jsonSB.Append("[");
            //jsonSB.Append("List.Count=" + list.Count.ToString());
            foreach (T o in list)
            {
                jsonSB.Append(ToJSONString(o) + ",");
            }
            jsonSB.Append("]");
            jsonSB = jsonSB.Replace(",]", "]");
            return jsonSB.ToString();
        }

        /// <summary>
        /// 泛型对象集合 指定对象转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        protected string ToJSONString<T>(List<T> list)
        {
            T obj = (T)Activator.CreateInstance(typeof(T));
            StringBuilder jsonSB = new StringBuilder();
            jsonSB.Append("[");
            foreach (T o in list)
            {
                jsonSB.Append(ToJSONString(o) + ",");
            }
            jsonSB.Append("]");
            jsonSB = jsonSB.Replace(",]", "]");
            return "";
        }
    }
    /// <summary>
    /// 通用JSON输出结构 泛型 （带输出 JSON ）
    /// </summary>
    [Serializable]
    public class JsonRsp<T> 
    {

        private bool _success = false;
        /// <summary>
        /// 输出代码
        /// </summary>
        public bool success
        {
            set { _success = value; }
            get { return _success; }
        }

        private int _code = 0;
        /// <summary>
        /// 输出代码
        /// </summary>
        public int code
        {
            set { _code = value; }
            get { return _code; }
        }


        private int _returnvalue = 0;
        /// <summary>
        /// 存储过程返回值
        /// </summary>
        public int returnvalue
        {
            set { _returnvalue = value; }
            get { return _returnvalue; }
        }

        private string _retmsg = string.Empty;
        /// <summary>
        /// 输出消息
        /// </summary>
        public string retmsg
        {
            set { _retmsg = value; }
            get { return _retmsg; }
        }

        private string _depict = string.Empty;
        /// <summary>
        /// 附加信息
        /// </summary>
        public string depict
        {
            set { _depict = value; }
            get { return _depict; }
        }

        private List<T> _data = new List<T>();
        /// <summary>
        /// 列表
        /// </summary>
        public List<T> data
        {
            set { _data = value; }
            get { return _data; }
        }

        private int _count = 0;
        /// <summary>
        /// 搜索到符合条件的结果总数
        /// </summary>
        public int count
        {
            set { _count = value; }
            get
            {
                return _count;
            }
        }

        private bool _has_next = false;
        /// <summary>
        /// 是否存在下一页
        /// </summary>
        public bool has_next
        {
            set { _has_next = value; }
            get { return _has_next; }
        }
    }


}
