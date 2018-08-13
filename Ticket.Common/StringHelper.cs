/** 1. 功能：用于字符串处理的公共操作类
 *  2. 作者：何平 
 *  3. 创建日期：2008-9-8
 *  4. 最后修改日期：2008-9-12
**/
#region 命名空间引用
using System;
using System.Data;
using System.Configuration;
using System.Text;
#endregion

namespace Ticket.Common
{
    /// <summary>
    /// 用于字符串处理的公共操作类
    /// </summary>
    public sealed class StringHelper
    {
        #region 对脚本中的消息进行过滤处理
        /// <summary>
        /// 对脚本中的消息进行过滤处理
        /// </summary>
        /// <param name="msg">脚本中的消息字符串,但不包括脚本函数.
        /// 范例：alert('ab\n\rc'),只传入ab\n\rc,不要把alert('')传进来。</param>
        public static string GetValidScriptMsg( string msg )
        {
            StringBuilder validMsg = new StringBuilder( msg );
            validMsg.Replace( "\\", @"\\" );
            validMsg.Replace( "\n", "\\n" );
            validMsg.Replace( "\t", "\\t" );
            validMsg.Replace( "\r", "\\r" );
            validMsg.Replace( "'", "\\'" );

            //返回有效的字符串
            return validMsg.ToString();
        }
        #endregion

        #region 去除字符串最后的逗号
        /// <summary>
        /// 去除字符串最后的逗号,返回新的字符串
        /// </summary>
        /// <param name="text">原始字符串</param>
        public static string RemoveLastComma( ref string text )
        {
            //检测字符串最后是否存在逗号
            if ( text.EndsWith( "," ) )
            {
                //去除逗号
                text = text.Remove( text.Length - 1, 1 );                
            }

            //返回新的字符串
            return text;
        }

        /// <summary>
        /// 去除字符串最后的逗号,返回新的字符串
        /// </summary>
        /// <param name="text">原始字符串</param>
        public static string RemoveLastComma( StringBuilder text )
        {
            //获取string类型
            string temp = text.ToString();

            //检测字符串最后是否存在逗号
            if ( temp.EndsWith( "," ) )
            {
                //去除逗号
                text.Remove( text.Length - 1, 1 );
            }

            //返回新的字符串
            return text.ToString();
        }
        #endregion

        #region 清空字符串
        /// <summary>
        /// 清空字符串
        /// </summary>
        /// <param name="text">原始字符串</param>
        public static void Clear( StringBuilder text )
        {
            text.Remove( 0, text.Length );
        }
        #endregion

        #region 获取字符串的最后一个字符
        /// <summary>
        /// 获取字符串的最后一个字符
        /// </summary>
        /// <param name="text">原始字符串</param>        
        public static string GetLastChar( string text )
        {
            //如果字符串为空，则返回
            if ( ValidationHelper.IsNullOrEmpty( text ) )
            {
                return "";
            }

            return text.Substring( text.Length - 1, 1 );
        }
        #endregion

        #region 用逗号拼接数组

        #region 重载方法1
        /// <summary>
        /// 获取用逗号拼接数组元素的字符串。返回字符串的数组元素默认不带引号，如果需要引号请使用重载方法。
        /// 范例: 数组为:string[] a = new string[] { "1","2","3" };返回值:1,2,3
        /// </summary>
        /// <param name="stringArray">原始字符串数组</param>
        public static string GetCommaString( params string[] stringArray )
        {
            return GetCommaString( false, stringArray );
        } 
        #endregion

        #region 重载方法2
        /// <summary>
        /// 获取用逗号拼接数组元素的字符串。
        /// 范例: 数组为:string[] a = new string[] { "1","2","3" };返回值:1,2,3
        /// </summary>
        /// <param name="isAddQuotationMarks">是否添加单引号,如果传入true，则返回值为'1','2','3'</param>
        /// <param name="stringArray">原始字符串数组</param>
        public static string GetCommaString( bool isAddQuotationMarks, params string[] stringArray )
        {
            //临时字符串
            StringBuilder list = new StringBuilder();

            //循环字符串数组，添加到临时字符串中
            foreach ( string text in stringArray )
            {
                if ( isAddQuotationMarks )
                {
                    list.AppendFormat( "'{0}',", text );
                }
                else
                {
                    list.AppendFormat( "{0},", text );
                }
            }

            //返回字符串
            return RemoveLastComma( list );
        } 
        #endregion

        #region 重载方法3
        /// <summary>
        /// 获取用逗号拼接数组元素的字符串。返回字符串的数组元素默认不带引号，如果需要引号请使用重载方法。
        /// 范例: 数组为:int[] a = new int[] { 1,2,3 };返回值:1,2,3
        /// </summary>
        /// <param name="intArray">原始整型数组</param>
        public static string GetCommaString( params int[] intArray )
        {
            return GetCommaString( false, intArray );
        }
        #endregion

        #region 重载方法4
        /// <summary>
        /// 获取用逗号拼接数组元素的字符串。
        /// 范例: 数组为:int[] a = new int[] { 1,2,3 };返回值:1,2,3
        /// </summary>
        /// <param name="isAddQuotationMarks">是否添加单引号,如果传入true，则返回值为'1','2','3'</param>
        /// <param name="intArray">原始整型数组</param>
        public static string GetCommaString( bool isAddQuotationMarks, params int[] intArray )
        {
            //临时字符串
            StringBuilder list = new StringBuilder();

            //循环字符串数组，添加到临时字符串中
            foreach ( int text in intArray )
            {
                if ( isAddQuotationMarks )
                {
                    list.AppendFormat( "'{0}',", text );
                }
                else
                {
                    list.AppendFormat( "{0},", text );
                }
            }

            //返回字符串
            return RemoveLastComma( list );
        }
        #endregion

        #endregion

        #region 获取时间加随机数的字符串
        /// <summary>
        /// 获取时间加随机数的字符串
        /// </summary>
        public static string DateTimeRandomString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                //添加当前时间
                sb.AppendFormat( "{0}", DateTime.Now.ToString().Replace( " ", "_" ).Replace( ":", "_" ) );
                //添加随机数
                sb.AppendFormat( "{0}", new RandomHelper().GetRandomInt( 1, 9999999 ) );
                
                //返回字符串
                return sb.ToString();
            }
        }
        #endregion

        #region 随机创建一个ID
        /// <summary>
        /// 随机创建一个ID
        /// </summary>
        /// <remarks>
        /// 生成规则：共10位数
        /// 1. 取毫秒的最后一位。
        /// 2. 取两位数的秒，不足两位补足。
        /// 3. 取两位数的分，不足两位补足。
        /// 4. 取小时的最后一位。
        /// 5. 取当天日期的最后一位。
        /// 6. 取当月的最后一位。
        /// 7. 取1——99之间的两位随机数，不足两位补足。
        /// </remarks>
        public static string GenerateID()
        {
            //取毫秒的最后一位
            string ms = StringHelper.GetLastChar( DateTime.Now.Millisecond.ToString() );

            //取两位数的秒，不足两位补足
            string ss = ConvertHelper.RepairZero( DateTime.Now.Second.ToString(), 2 );

            //取两位数的分，不足两位补足。
            string mm = ConvertHelper.RepairZero( DateTime.Now.Minute.ToString(), 2 );

            //取小时的最后一位
            string h = StringHelper.GetLastChar( DateTime.Now.Hour.ToString() );

            //取当天日期的最后一位
            string d = StringHelper.GetLastChar( DateTime.Now.Date.ToString() );

            //取当月的最后一位
            string m = StringHelper.GetLastChar( DateTime.Now.Month.ToString() );

            //取1——99之间的两位随机数
            RandomHelper random = new RandomHelper();
            string randomNumber = random.GetRandomInt( 1, 99 ).ToString();
            //补足两位
            randomNumber = ConvertHelper.RepairZero( randomNumber, 2 );

            //返回随机ID
            return ms + ss + mm + h + d + m + randomNumber;
        }
        #endregion
    }
}
