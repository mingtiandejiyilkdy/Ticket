using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PWMIS.Core.Extensions;
using System.Web;
using System.Web.Security;
using System.Web.Script.Serialization;
using PWMIS.DataMap.Entity;
using PWMIS.DataProvider.Data;
using PWMIS.DataProvider.Adapter;
using System.Runtime.Caching;
using Ticket.Interface;
using Ticket.Common;
using Ticket.ViewModels;
using System.Data;
using System.Reflection;

namespace Ticket.BLL
{
    public class BLLBase
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Add<T>(T model) where T : EntityBase, ILongID, new()
        {
            System.Reflection.PropertyInfo[] ps = model.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo pi in ps)
            {
                //Name 为属性名称,GetValue 得到属性值(参数this 为对象本身,null)
                string name = pi.Name.ToLower();
                if (name == "TenantId".ToLower())
                {
                    pi.SetValue(model, TenantId, null);
                }
                else if (name == "CreateId".ToLower())
                {
                    pi.SetValue(model, AdminId, null);
                }
                else if (name == "CreateUser".ToLower())
                {
                    pi.SetValue(model, AdminName, null);
                }
                else if (name == "CreateIP".ToLower())
                {
                    pi.SetValue(model, Util.GetLocalIP, null);
                }
                else if (name == "CreateTime".ToLower())
                {
                    pi.SetValue(model, DateTime.Now, null);
                }
            }
            EntityQuery<T> q = new EntityQuery<T>();
            return q.Insert(model);
        }
        #endregion

        public static int Add<T>(T model, AdoHelper ado) where T : EntityBase, ILongID, new()
        {
            System.Reflection.PropertyInfo[] ps = model.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo pi in ps)
            {
                //Name 为属性名称,GetValue 得到属性值(参数this 为对象本身,null)
                string name = pi.Name.ToLower();
                if (name == "TenantId".ToLower())
                {
                    pi.SetValue(model, TenantId, null);
                }
                else if (name == "CreateId".ToLower())
                {
                    pi.SetValue(model, AdminId, null);
                }
                else if (name == "CreateUser".ToLower())
                {
                    pi.SetValue(model, AdminName, null);
                }
                else if (name == "CreateIP".ToLower())
                {
                    pi.SetValue(model, Util.GetLocalIP, null);
                }
                else if (name == "CreateTime".ToLower())
                {
                    pi.SetValue(model, DateTime.Now, null);
                }
            }
            EntityQuery<T> q = new EntityQuery<T>(ado);
            return q.Insert(model);
        }

        #region 获取列表数据 OQLCompare
        /// <summary>
        /// 获取列表数据 OQLCompare
        /// </summary>
        /// <param name="cp">where条件，没有可传人null</param>
        /// <param name="orders">排序字段对象</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public static List<T> GetList<T>(OQLCompare cp, OQLOrder orders, int pageSize, int pageIndex) where T : EntityBase, ILongID, new()
        {
            T m = new T();
            OQL q = new OQL(m);
            if ((object)cp != null)
            {
                if ((object)orders != null)
                {
                    q.Select().Where(cp).OrderBy(orders);
                }
                else
                {
                    q.Select().Where(cp);
                }
            }
            else
            {
                if ((object)orders != null)
                {
                    q.Select().OrderBy(orders);
                }
                else
                {
                    q.Select();
                }
            }
            q.PageWithAllRecordCount = GetRecordCounts<T>(cp);
            if (pageSize > 0 && pageIndex > 0 && q.PageWithAllRecordCount > 0)
            {
                q.Limit(pageSize, pageIndex);
            }

            return EntityQuery<T>.QueryList(q);
        }
        #endregion

        #region 获取列表数据 OQLCompareFunc
        /// <summary>
        /// 获取列表数据 OQLCompareFunc
        /// </summary>
        /// <param name="cp">where条件，没有可传人null</param>
        /// <param name="orders">排序字段对象</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public static List<T> GetList<T>(OQLCompareFunc<T> cp, OQLOrder orders, int pageSize, int pageIndex) where T : EntityBase, ILongID, new()
        {
            T m = new T();
            OQL q = new OQL(m);
            if ((object)cp != null)
            {
                if ((object)orders != null)
                {
                    q.Select().Where(cp).OrderBy(orders);
                }
                else
                {
                    q.Select().Where(cp);
                }
            }
            else
            {
                if ((object)orders != null)
                {
                    q.Select().OrderBy(orders);
                }
                else
                {
                    q.Select();
                }
            }
            q.PageWithAllRecordCount = GetRecordCounts<T>(cp);
            if (pageSize > 0 && pageIndex > 0 && q.PageWithAllRecordCount > 0)
            {
                q.Limit(pageSize, pageIndex);
            }
            string s = q.PrintParameterInfo();
            return EntityQuery<T>.QueryList(q);
        }
        #endregion

        #region 取得记录总数 OQLCompare
        /// <summary>
        /// 取得记录总数 OQLCompare
        /// </summary>
        /// <param name="cp">where条件</param>
        /// <returns></returns>
        public static int GetRecordCounts<T>(OQLCompare cp) where T : EntityBase, ILongID, new()
        {
            T m = new T();
            OQL q = new OQL(m);
            if ((object)cp != null)
            {
                q.Select().Count(m.ID, "Counts").Where(cp);
            }
            else
            {
                q.Select().Count(m.ID, "Counts");
            }

            T mCounts = EntityQuery<T>.QueryObject(q);

            return Convert.ToInt32(mCounts.PropertyList("Counts"));

        }
        #endregion

        #region 取得记录总数 GetRecordCounts
        /// <summary>
        /// 取得记录总数 GetRecordCounts
        /// </summary>
        /// <param name="cp">where条件</param>
        /// <returns></returns>
        public static int GetRecordCounts<T>(OQLCompareFunc<T> cp) where T : EntityBase, ILongID, new()
        {
            T m = new T();
            OQL q = new OQL(m);
            if ((object)cp != null)
            {
                q.Select().Count(m.ID, "Counts").Where(cp);
            }
            else
            {
                q.Select().Count(m.ID, "Counts");
            }

            T mCounts = EntityQuery<T>.QueryObject(q);

            return Convert.ToInt32(mCounts.PropertyList("Counts"));

        }
        #endregion

        #region 取得记录总数 AdoHelper
        /// <summary>
        /// 取得记录总数 AdoHelper
        /// </summary>
        /// <param name="cp">where条件</param>
        /// <returns></returns>
        public static int GetRecordCounts<T>(OQLCompareFunc<T> cp, AdoHelper ado) where T : EntityBase, ILongID, new()
        {
            T m = new T();
            OQL q = new OQL(m);
            if ((object)cp != null)
            {
                q.Select().Count(m.ID, "Counts").Where(cp);
            }
            else
            {
                q.Select().Count(m.ID, "Counts");
            }

            T mCounts = EntityQuery<T>.QueryObject(q, ado);

            return Convert.ToInt32(mCounts.PropertyList("Counts"));

        }
        #endregion

        #region 根据id获取实体对象
        /// <summary>
        /// 根据id获取实体对象
        /// </summary>
        /// <param name="ID">对象id</param>
        /// <returns></returns>
        public static T GetModel<T>(long ID, bool IsUseCache = false) where T : EntityBase, ILongID, new()
        {
            T model = new T();
            model.ID = ID;

            //UpdateCacheEventSource es = new UpdateCacheEventSource();

            //model.Subscribe(es);

            //es.RaiseEvent("GetModel");

            if (RedisHelper.IsUseCache && IsUseCache && (model is ICacheEvent))
            {
                MemoryCache mc = MemoryCache.Default;
                ICacheEvent im = model as ICacheEvent;

                string rule = im.GetCacheKeyRule();

                string key = string.Format(rule, model.GetIdentityName(), model.ID);
                CacheItemPolicy cip = new CacheItemPolicy();
                cip.AbsoluteExpiration = DateTimeOffset.Now.AddHours(6);

                var cache = mc.Get(key);

                if (cache != null)
                {
                    model = cache as T;
                }
                else
                {
                    model = RedisHelper.GetStringKey<T>(key);

                    if (model == null)
                    {
                        model = OQL.FromObject<T>().Select().Where((cmp, m) => cmp.Comparer(m.ID, "=", ID)).END.ToObject();

                        if (model != null)
                        {
                            RedisHelper.SetStringKey<T>(key, model, DateTime.Now.AddDays(7) - DateTime.Now);
                        }
                    }
                    if (model != null)
                    {
                        //更新本地缓存
                        mc.Set(key, model, cip);
                    }
                }
            }
            else
            {
                model = OQL.FromObject<T>().Select().Where((cmp, m) => cmp.Comparer(m.ID, "=", ID)).END.ToObject();
            }

            return model;
        }
        #endregion

        #region 根据id获取实体对象 AdoHelper
        /// <summary>
        /// 根据id获取实体对象 AdoHelper
        /// </summary>
        /// <param name="ID">对象id</param>
        /// <returns></returns>
        public static T GetModel<T>(int ID, AdoHelper ado, bool IsUseCache = false) where T : EntityBase, ILongID, new()
        {
            T model = new T();
            model.ID = ID;

            if (RedisHelper.IsUseCache && IsUseCache && (model is ICacheEvent))
            {
                MemoryCache mc = MemoryCache.Default;
                ICacheEvent im = model as ICacheEvent;

                string rule = im.GetCacheKeyRule();

                string key = string.Format(rule, model.GetIdentityName(), model.ID);
                CacheItemPolicy cip = new CacheItemPolicy();
                cip.AbsoluteExpiration = DateTimeOffset.Now.AddHours(6);

                var cache = mc.Get(key);

                if (cache != null)
                {
                    model = cache as T;
                }
                else
                {
                    model = RedisHelper.GetStringKey<T>(key);

                    if (model == null)
                    {
                        model = OQL.FromObject<T>().Select().Where((cmp, m) => cmp.Comparer(m.ID, "=", ID)).END.ToObject(ado);

                        if (model != null)
                        {
                            RedisHelper.SetStringKey<T>(key, model, DateTime.Now.AddDays(7) - DateTime.Now);
                        }
                    }
                    if (model != null)
                    {
                        //更新本地缓存
                        mc.Set(key, model, cip);
                    }
                }
            }
            else
            {
                model = OQL.FromObject<T>().Select().Where((cmp, m) => cmp.Comparer(m.ID, "=", ID)).END.ToObject(ado);
            }

            return model;
        }
        #endregion

        #region 获取符合条件的实体对象列表 OQLCompareFunc 
        /// <summary>
        /// 获取符合条件的实体对象列表 OQLCompareFunc 
        /// </summary>
        /// <param name="cp">where条件，没有可传人null</param>
        /// <param name="orders">排序字段对象</param> 
        /// <returns></returns>
        public static List<T> GetAllList<T>(OQLCompareFunc<T> cp, OQLOrder orders) where T : EntityBase, ILongID, new()
        {
            T m = new T();
            OQL q = new OQL(m);
            if ((object)cp != null)
            {
                if ((object)orders != null)
                {
                    q.Select().Where(cp).OrderBy(orders);
                }
                else
                {
                    q.Select().Where(cp);
                }
            }
            else
            {
                if ((object)orders != null)
                {
                    q.Select().OrderBy(orders);
                }
                else
                {
                    q.Select();
                }
            }
            q.PageWithAllRecordCount = GetRecordCounts<T>(cp); 
            string s = q.PrintParameterInfo();
            return EntityQuery<T>.QueryList(q);
        } 
        #endregion

        #region 根据id获取实体对象
        /// <summary>
        /// 根据id获取实体对象
        /// </summary>
        /// <param name="ID">对象id</param>
        /// <returns></returns>
        public static T GetModel<T>(OQLCompareFunc<T> cp, bool IsUseCache = false) where T : EntityBase, ILongID, new()
        {
            T model = new T();              
            model = OQL.FromObject<T>().Select().Where(cp).END.ToObject();          
            return model;
        }
        #endregion

        #region 更新
        /// <summary>
        /// 注意：更新方法的调用要确保model不是缓存得到的，否则缓存数据会覆盖掉数据库数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="IsUseCache"></param>
        /// <returns></returns>
        public static int Update<T>(T model, bool IsUseCache = false) where T : EntityBase, new()
        {
            System.Reflection.PropertyInfo[] ps = model.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo pi in ps)
            {
                //Name 为属性名称,GetValue 得到属性值(参数this 为对象本身,null)
                string name = pi.Name.ToLower();
                if (name == "TenantId".ToLower())
                {
                    pi.SetValue(model, TenantId, null);
                }
                else if (name == "UpdateId".ToLower())
                {
                    pi.SetValue(model, AdminId, null);
                }
                else if (name == "UpdateUser".ToLower())
                {
                    pi.SetValue(model, AdminName, null);
                }
                else if (name == "UpdateIP".ToLower())
                {
                    pi.SetValue(model, Util.GetLocalIP, null);
                }
                else if (name == "UpdateTime".ToLower())
                {
                    pi.SetValue(model, DateTime.Now, null);
                }
            }
            EntityQuery<T> q = new EntityQuery<T>();
            int count = q.Update(model);

            if (RedisHelper.IsUseCache && IsUseCache && (model is ICacheEvent))
            {
                ICacheEvent im = model as ICacheEvent;

                string[] cks = im.GetCacheKeys();
                string rule = im.GetCacheKeyRule();

                MemoryCache mc = MemoryCache.Default;

                CacheItemPolicy cip = new CacheItemPolicy();
                cip.AbsoluteExpiration = DateTimeOffset.Now.AddHours(6);

                foreach (var item in cks)
                {
                    string key = string.Format(rule, item, model[item]);

                    mc.Set(key, model, cip);

                    RedisHelper.SetStringKey<T>(key, model, DateTime.Now.AddDays(7) - DateTime.Now);
                }
            }

            return count ;
        }
        #endregion

        #region 更新 AdoHelper
        /// <summary>
        /// 注意：更新方法的调用要确保model不是缓存得到的，否则缓存数据会覆盖掉数据库数据，另外有事务的问题，如果ado有开事务，更新缓存之后如果事务回滚，缓存数据就有问题。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="ado"></param>
        /// <param name="IsUseCache"></param>
        /// <returns></returns>
        public static int Update<T>(T model, AdoHelper ado, bool IsUseCache = false) where T : EntityBase, new()
        {
            EntityQuery<T> q = new EntityQuery<T>(ado);
            int count = q.Update(model);

            if (RedisHelper.IsUseCache && IsUseCache && (model is ICacheEvent))
            {
                ICacheEvent im = model as ICacheEvent;

                string[] cks = im.GetCacheKeys();
                string rule = im.GetCacheKeyRule();

                MemoryCache mc = MemoryCache.Default;

                CacheItemPolicy cip = new CacheItemPolicy();
                cip.AbsoluteExpiration = DateTimeOffset.Now.AddHours(6);

                foreach (var item in cks)
                {
                    string key = string.Format(rule, item, model[item]);

                    mc.Set(key, model, cip);

                    RedisHelper.SetStringKey<T>(key, model, DateTime.Now.AddDays(7) - DateTime.Now);
                }
            }

            return count ;
        }
        #endregion

        #region 批量删除 string IDs
        public static int SetDelete<T>(string IDs) where T : EntityBase, ILongID, IIsDelete, new()
        {
            string[] strs = IDs.Split(new char[] { ',' });
            T t = new T() { IsDelete = true };

            OQL.From(t).Select(t.ID, t.IsDelete);

            OQL q = OQL.From(t).Update(t.IsDelete).Where<T>((cmp1, user) => cmp1.Comparer(user.ID.ToString(), "in", strs)).END;
            try
            {
                return EntityQuery<T>.ExecuteOql(q, MyDB.GetDBHelper());
            }
            catch
            {
                return 0;
            }

        }
        #endregion

        #region 批量删除 AdoHelper string IDs
        public static int SetDelete<T>(string IDs, AdoHelper ado) where T : EntityBase, IIntID, IIsDelete, new()
        {
            string[] strs = IDs.Split(new char[] { ',' });
            T t = new T() { IsDelete = true };

            OQL.From(t).Select(t.ID, t.IsDelete);

            OQL q = OQL.From(t).Update(t.IsDelete).Where<T>((cmp1, user) => cmp1.Comparer(user.ID.ToString(), "in", strs)).END;
            try
            {
                return EntityQuery<T>.ExecuteOql(q, ado);
            }
            catch
            {
                return 0;
            }

        }
        #endregion

        #region 删除
        public static bool Delete<T>(T model) where T : EntityBase, new()
        {
            EntityQuery<T> q = new EntityQuery<T>();
            int count = q.Delete(model);
            return count > 0;
        }
        #endregion

        #region 删除 AdoHelper
        public static bool Delete<T>(T model, AdoHelper ado) where T : EntityBase, new()
        {
            EntityQuery<T> q = new EntityQuery<T>(ado);
            int count = q.Delete(model);
            return count > 0;
        }
        #endregion

        #region 删除记录
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="IDs">注意：必须是,号分割的整形数字格式</param>
        /// <returns></returns>
        public static bool Delete<T>(string IDs) where T : EntityBase, ILongID, new()
        {

            string[] strs = IDs.Split(new char[] { ',' });
            T t = new T();
            OQL q = OQL.From(t).Delete().Where<T>((cmp1, user) => cmp1.Comparer(user.ID.ToString(), "in", strs)).END;

            try
            {
                return EntityQuery<T>.ExecuteOql(q, MyDB.GetDBHelper()) > 0;
            }
            catch
            {
                return false;
            }
        }
        #endregion  
         

        #region cookies方法
        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        public static AccountViewModel AdminUser
        {
            get
            {
                //1.登录状态获取用户信息（自定义保存的用户）
                var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                //2.使用 FormsAuthentication 解密用户凭据
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                AccountViewModel model = new AccountViewModel();

                //3. 直接解析到用户模型里去，有没有很神奇
                model = new JavaScriptSerializer().Deserialize<AccountViewModel>(ticket.UserData);

                return model;
            }
        }
        //商户Id
        public static long TenantId
        {
            get
            {
                return AdminUser.TenantId;
            }
        }
        //登录用户Id
        public static long AdminId
        {
            get
            {
                return AdminUser.ID;
            }
        }
        //登录用户名称
        public static string AdminName
        {
            get
            {
                return AdminUser.AccountName;
            }
        }
        #endregion
    }
}
