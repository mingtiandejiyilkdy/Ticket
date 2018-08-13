/** 1. 功能：加密随机数操作类
 *  2. 作者：何平 
 *  3. 创建日期：2008-3-8
 *  4. 最后修改日期：2008-8-1
**/
#region 命名空间引用
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
#endregion

namespace Ticket.Common
{
    /// <summary>
    /// 使用RNGCryptoServiceProvider类生成加密随机数。
    /// </summary>
    public class CryptRandomHelper
    {
        //随机数对象
        private RNGCryptoServiceProvider _rng;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public CryptRandomHelper()
        {
            //为随机数对象赋值
            this._rng = new RNGCryptoServiceProvider();
        }
        #endregion

        #region 生成一个指定范围的随机整数
        /// <summary>
        /// 生成一个范围从1到指定值的随机整数,包括1和最大值
        /// </summary>        
        /// <param name="maxNum">最大值</param>
        public int GetRandomInt( int maxNum )
        {
            //接收随机数的字节数组
            byte[] bytes = new byte[1];

            //填充字节数组
            this._rng.GetBytes( bytes );

            //返回范围从1到最大值的整数
            return (int)( (decimal)bytes[0] / 256 * maxNum ) + 1;
        }
        #endregion
    }
}
