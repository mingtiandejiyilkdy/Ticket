using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Common;
using Ticket.ViewModels;
using Ticket.Models;
using PWMIS.DataMap.Entity;
using PWMIS.Core.Extensions;

namespace Ticket.BLL
{

    public class AdminAccountBLL : BLLBase
    {
        #region  判断用户是否重复
        public JsonRsp CheckAccount(string accountName,long accountId=0)
        { 
            OQLCompareFunc<AdminAccount> cmpResult = (cmp, c) =>                       (
                         cmp.Comparer(c.TenantId, OQLCompare.CompareType.Equal, TenantId) & 
                         cmp.Comparer(c.AccountName, OQLCompare.CompareType.Equal, accountName)
                       );
            var model = GetModel<AdminAccount>(cmpResult);
            return new JsonRsp { success = (model==null) };

        }
        #endregion

        #region 基础方法
        /// <summary>
        /// 获取管理员列表（全部）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<AccountViewModel> GetAllList()
        {
            JsonRsp<AccountViewModel> rsp = new JsonRsp<AccountViewModel>();
            AdminAccount model = new AdminAccount();
            OQL q = OQL.From(model)
                .Select()
                .OrderBy(model.ID, "asc")
                .END;
            List<AdminAccount> list = q.ToList<AdminAccount>();//使用OQL扩展
            rsp.data = list.ConvertAll<AccountViewModel>(o =>
            {
                return new AccountViewModel()
                {
                    ID = o.ID,
                    AccountName = o.AccountName,
                    TrueName = o.TrueName,
                    Status = o.Status,
                    LoginTime = o.LoginTime,
                    LastLoginTime = o.LastLoginTime,
                    LoginCount = o.LoginCount,
                    CreateTime = o.CreateTIme,
                };
            }
            );
            rsp.success = true;
            rsp.code = 0;
            return rsp;
        }

        /// <summary>
        /// 获取管理员列表（分页）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<AccountViewModel> GetPageList(int pageIndex, int pageSize)
        {
            JsonRsp<AccountViewModel> rsp = new JsonRsp<AccountViewModel>();

            AdminAccount m = new AdminAccount();
            OQL q = OQL.From(m)
                .Select(m.ID, m.AccountName, m.TrueName, m.Status, m.LoginTime, m.LoginCount, m.LastLoginTime, m.CreateTIme)
                .OrderBy(m.ID, "asc")
                .END;
            //分页
            q.Limit(pageSize, pageIndex, true);
            //q.PageWithAllRecordCount = allCount;
            //List<Employee> list= EntityQuery<Employee>.QueryList(q);
            List<AdminAccount> list = q.ToList<AdminAccount>();//使用OQL扩展
            rsp.data = list.ConvertAll<AccountViewModel>(o =>
            {
                return new AccountViewModel()
                {
                    ID = o.ID,
                    AccountName = o.AccountName,
                    TrueName = o.TrueName,
                    Status = o.Status,
                    LoginTime = o.LoginTime,
                    LastLoginTime = o.LastLoginTime,
                    LoginCount = o.LoginCount,
                    CreateTime = o.CreateTIme,
                };
            }
            );
            rsp.success = true;
            rsp.code = 0;
            rsp.count = q.PageWithAllRecordCount;
            return rsp;
        }

        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Add(AdminAccount model)
        {
            JsonRsp json = CheckAccount(model.AccountName);
            if (!json.success) {
                json.retmsg = "该用户名已存在";
                return json;   
            }
            if (string.IsNullOrEmpty(model.AccountPwd)) {
                model.AccountPwd = "888888";
            }

            //salt
            string strSalt = Guid.NewGuid().ToString();
            model.Salt = strSalt;
            model.AccountPwd = EncryptHelper.MD5Encoding(model.AccountPwd, strSalt);
            int returnvalue = Add<AdminAccount>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Remove(AdminAccount model)
        {
            bool success = Delete<AdminAccount>(model);
            return new JsonRsp { success = success };
        }
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Update(AdminAccount model)
        {
            JsonRsp json = CheckAccount(model.AccountName, model.ID);
            if (!json.success)
            {
                json.retmsg = "该用户名已存在";
                return json;
            }
            if (string.IsNullOrEmpty(model.AccountPwd))
            {
                model.AccountPwd = "888888";
            }

            int returnvalue = Update<AdminAccount>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }

        /// <summary>
        /// 查 根据Id获取详情，如果没有则返回空对象
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public AdminAccount GetModelById(long Id)
        {
            return GetModel<AdminAccount>(Id);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp DeleteById(long Id)
        {
            return Remove(GetModelById(Id));
        }


        /// <summary>
        /// save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Save(AdminAccount model)
        {
            if (model.ID == 0)
            {
                return Add(model);
            }
            else
            {
                return Update(model);
            }
        }

        #region 启用/禁用/删除
        /// <summary>
        ///  启用/禁用/删除
        /// </summary>
        /// <param name="accountStatus"></param>
        /// <returns></returns>
        public JsonRsp SetStatus(int accountId, int accountStatus)
        {
            AdminAccount dbAccount = GetModelById(accountId);
            dbAccount.Status = accountStatus;
            return Update(dbAccount);
        }
        #endregion 

         /// <summary>
        ///  启用/禁用/删除
        /// </summary>
        /// <param name="accountStatus"></param>
        /// <returns></returns>
        public JsonRsp SetStatus(long[] Ids, int status)
        { 
            if (Ids == null)
            {
                return new JsonRsp { success = false, retmsg = "请选择要操作的数据" };
            }
            AdminAccount model = new AdminAccount();
            model.Status = status; 
            OQL q = OQL.From(model)
               .Update(model.Status)
                          .Where(cmp => cmp.Comparer(model.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的
                       .END;
            int returnvalue = EntityQuery<AdminAccount>.Instance.ExecuteOql(q);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        #endregion



        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public JsonRsp Login(string accountName, string accountPwd)
        {
            JsonRsp json = new JsonRsp();

            AdminAccount model = new AdminAccount();
            model.AccountName = accountName;
            OQL q = OQL.From(model)
                .Select()
                .Where(model.AccountName) //以用户名和密码来验证登录
            .END;

            model = q.ToEntity<AdminAccount>();//ToEntity，OQL扩展方法 
            if (model == null)
            {

                json.success = false;
                json.retmsg = "账号或密码不匹配";
            }
            else
            {
                if (model.AccountPwd == EncryptHelper.MD5Encoding(accountPwd, model.Salt))
                {
                    json.success = true;
                    json.code = 0;
                    json.returnObj = model;
                }
                else
                {
                    json.success = false;
                    json.retmsg = "账号或密码不匹配";
                }
            }
            return json;
        }
    }
}
