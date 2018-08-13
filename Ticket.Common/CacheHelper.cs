using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections;
using System.Web.SessionState;

namespace Ticket.Common
{

    public class CacheHelper
    {
        #region CacheHelper


        internal static volatile System.Web.Caching.Cache webCache = System.Web.HttpRuntime.Cache;


        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="obj">缓存的对象</param>
        public static void CacheAddObject(string objId, object obj)
        {
            CacheAddObject(objId, obj, 0);
        }

        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="obj">缓存的对象</param>
        /// <param name="obj">到期时间,单位:秒</param>
        public static void CacheAddObject(string objId, object obj, int expire)
        {
            if (objId == null || objId.Length == 0 || obj == null)
            {
                return;
            }
            //表示永不过期
            if (expire == 0)
            {
                webCache.Insert(objId, obj, null, DateTime.MaxValue, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
            }
            else
            {
                webCache.Insert(objId, obj, null, DateTime.Now.AddSeconds(expire), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, null);
            }
        }


        /// <summary>
        /// 删除缓存对象
        /// </summary>
        /// <param name="objId">对象的关键字</param>
        public static void CacheRemoveObject(string objId)
        {
            if (objId == null || objId.Length == 0)
            {
                return;
            }
            webCache.Remove(objId);
        }


        /// <summary>
        /// 返回一个指定的对象
        /// </summary>
        /// <param name="objId">对象的关键字</param>
        /// <returns>对象</returns>
        public static object CacheRetrieveObject(string objId)
        {
            if (objId == null || objId.Length == 0)
            {
                return null;
            }
            return webCache.Get(objId);
        }


        /// <summary>
        /// 清空的有缓存数据
        /// </summary>
        public virtual void CacheFlushAll()
        {
            IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                webCache.Remove(CacheEnum.Key.ToString());
            }
        }

        #endregion

        #region cookie



        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void SetCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);
        }


        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        public static void SetCookie(string strName, object strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue.ToString();
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();
            return null;
        }



        /// <summary>
        /// 清空Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        public static void ClearCookies(string cookieName)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            DateTime now = DateTime.Now;
            cookie.Expires = now.AddDays(-2);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        #endregion

        #region Session


        /// <summary>
        /// 读Session值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetSession(string key)
        {
            HttpSessionState session = HttpContext.Current.Session;
            if (session != null)
                return session[key];
            return null;
        }


        /// <summary>
        /// 设Session值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public static void SetSession(string key, object obj)
        {
            HttpSessionState session = HttpContext.Current.Session;
            session.Timeout = 30;
            session[key] = obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="app"></param>
        public static void SetSession(string key, object obj, HttpApplication app)
        {
            //HttpContext context = app.Context;
            HttpSessionState session = app.Context.Session;
            session[key] = obj;
        }

        #endregion
    }
}
