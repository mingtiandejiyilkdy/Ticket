/** 1. 功能：加密操作类
 *  2. 作者：何平 
 *  3. 创建日期：2008-3-2
 *  4. 最后修改日期：2008-10-6
**/
#region 命名空间引用
using System;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
#endregion 命名空间引用

namespace Ticket.Common
{
    /// <summary>
    /// 加密操作类
    /// </summary>
    public class EncryptHelper
    {
        #region MD5加密
        /// <summary>
        /// 128位MD5算法加密字符串
        /// </summary>
        /// <param name="text">要加密的字符串</param>    
        public static string GetMD5( string text )
        {
            //如果字符串为空，则返回
            if ( ValidationHelper.IsNullOrEmpty<string>( text ) )
            {
                return "";
            }

            //将传入的字符串转换成Byte数组
            byte[] data = ConvertHelper.StringToBytes( text,Encoding.UTF8 );

            //返回MD5值的字符串表示
            return GetMD5(data);
        }

        /// <summary>
        /// 128位MD5算法加密Byte数组
        /// </summary>
        /// <param name="data">要加密的Byte数组</param>    
        public static string GetMD5(byte[] data)
        {
            //如果Byte数组为空，则返回
            if ( ValidationHelper.IsNullOrEmpty<byte[]>( data ) )
            {
                return "";
            }
             
                //创建MD5密码服务提供程序
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

                //计算传入的字节数组的哈希值
                byte[] result = md5.ComputeHash( data );

                //释放资源
                md5.Clear();

                //返回MD5值的字符串表示
                return Convert.ToBase64String( result );
             
        }
        #endregion

        #region Base64加密
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="text">要加密的字符串</param>
        /// <returns></returns>
        public static string EncodeBase64( string text )
        {
            //如果字符串为空，则返回
            if ( ValidationHelper.IsNullOrEmpty<string>( text ) )
            {
                return "";
            }

            try
            {
                char[] Base64Code = new char[]{'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T',
											'U','V','W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l','m','n',
											'o','p','q','r','s','t','u','v','w','x','y','z','0','1','2','3','4','5','6','7',
											'8','9','+','/','='};
                byte empty = (byte)0;
                ArrayList byteMessage = new ArrayList( Encoding.Default.GetBytes( text ) );
                StringBuilder outmessage;
                int messageLen = byteMessage.Count;
                int page = messageLen / 3;
                int use = 0;
                if ( ( use = messageLen % 3 ) > 0 )
                {
                    for ( int i = 0; i < 3 - use; i++ )
                        byteMessage.Add( empty );
                    page++;
                }
                outmessage = new System.Text.StringBuilder( page * 4 );
                for ( int i = 0; i < page; i++ )
                {
                    byte[] instr = new byte[3];
                    instr[0] = (byte)byteMessage[i * 3];
                    instr[1] = (byte)byteMessage[i * 3 + 1];
                    instr[2] = (byte)byteMessage[i * 3 + 2];
                    int[] outstr = new int[4];
                    outstr[0] = instr[0] >> 2;
                    outstr[1] = ( ( instr[0] & 0x03 ) << 4 ) ^ ( instr[1] >> 4 );
                    if ( !instr[1].Equals( empty ) )
                        outstr[2] = ( ( instr[1] & 0x0f ) << 2 ) ^ ( instr[2] >> 6 );
                    else
                        outstr[2] = 64;
                    if ( !instr[2].Equals( empty ) )
                        outstr[3] = ( instr[2] & 0x3f );
                    else
                        outstr[3] = 64;
                    outmessage.Append( Base64Code[outstr[0]] );
                    outmessage.Append( Base64Code[outstr[1]] );
                    outmessage.Append( Base64Code[outstr[2]] );
                    outmessage.Append( Base64Code[outstr[3]] );
                }
                return outmessage.ToString();
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }
        #endregion

        #region Base64解密
        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="text">要解密的字符串</param>
        public static string DecodeBase64( string text )
        {
            //如果字符串为空，则返回
            if ( ValidationHelper.IsNullOrEmpty<string>( text ) )
            {
                return "";
            }

            //将空格替换为加号
            text = text.Replace( " ", "+" );

            try
            {
                if ( ( text.Length % 4 ) != 0 )
                {
                    return "包含不正确的BASE64编码";
                }
                if ( !Regex.IsMatch( text, "^[A-Z0-9/+=]*$", RegexOptions.IgnoreCase ) )
                {
                    return "包含不正确的BASE64编码";
                }
                string Base64Code = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
                int page = text.Length / 4;
                ArrayList outMessage = new ArrayList( page * 3 );
                char[] message = text.ToCharArray();
                for ( int i = 0; i < page; i++ )
                {
                    byte[] instr = new byte[4];
                    instr[0] = (byte)Base64Code.IndexOf( message[i * 4] );
                    instr[1] = (byte)Base64Code.IndexOf( message[i * 4 + 1] );
                    instr[2] = (byte)Base64Code.IndexOf( message[i * 4 + 2] );
                    instr[3] = (byte)Base64Code.IndexOf( message[i * 4 + 3] );
                    byte[] outstr = new byte[3];
                    outstr[0] = (byte)( ( instr[0] << 2 ) ^ ( ( instr[1] & 0x30 ) >> 4 ) );
                    if ( instr[2] != 64 )
                    {
                        outstr[1] = (byte)( ( instr[1] << 4 ) ^ ( ( instr[2] & 0x3c ) >> 2 ) );
                    }
                    else
                    {
                        outstr[2] = 0;
                    }
                    if ( instr[3] != 64 )
                    {
                        outstr[2] = (byte)( ( instr[2] << 6 ) ^ instr[3] );
                    }
                    else
                    {
                        outstr[2] = 0;
                    }
                    outMessage.Add( outstr[0] );
                    if ( outstr[1] != 0 )
                        outMessage.Add( outstr[1] );
                    if ( outstr[2] != 0 )
                        outMessage.Add( outstr[2] );
                }
                byte[] outbyte = (byte[])outMessage.ToArray( Type.GetType( "System.Byte" ) );
                return Encoding.Default.GetString( outbyte );
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }
        #endregion        

        /// <summary> 
        /// MD5 加密字符串 
        /// </summary> 
        /// <param name="rawPass">源字符串</param> 
        /// <returns>加密后字符串</returns> 
        private static string MD5Encoding(string rawPass)
        {
            // 创建MD5类的默认实例：MD5CryptoServiceProvider 
            MD5 md5 = MD5.Create();
            byte[] bs = Encoding.UTF8.GetBytes(rawPass);
            byte[] hs = md5.ComputeHash(bs);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hs)
            {
                // 以十六进制格式格式化 
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary> 
        /// MD5盐值加密 
        /// </summary> 
        /// <param name="rawPass">源字符串</param> 
        /// <param name="salt">盐值</param> 
        /// <returns>加密后字符串</returns> 
        public static string MD5Encoding(string rawPass, object salt)
        {
            if (salt == null) return rawPass;
            return MD5Encoding(rawPass + "{" + salt.ToString() + "}");
        } 
    }
}
