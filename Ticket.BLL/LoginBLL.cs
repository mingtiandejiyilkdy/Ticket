using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Models;
using Ticket.Common;
using PWMIS.DataMap.Entity;
using PWMIS.Core.Extensions;

namespace Ticket.BLL
{
    public class LoginBLL : BLLBase
    { 
        #region 验证密码是否正确 
        /// <summary>
        /// 验证密码是否正确
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public JsonRsp CheckPassWord(string accountName, string accountPwd)
        {
            JsonRsp json = new JsonRsp();
            AdminAccount model = new AdminAccount();
             
            model.AccountName = accountName;
            OQL q = OQL.From(model)
                .Select()
                .Where(model.AccountName) //以用户名和密码来验证登录
            .END;

            var list = q.ToList<AdminAccount>();//ToEntity，OQL扩展方法  

            if (list.Count == 0){
                json.success = false;
                json.retmsg = "用户不存在";
                return json;
            }
            else if (list.Count == 1)
            {
                model = list[0];
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
                    return json;
                }
            }
            else
            {
                model = new AdminAccount();
            }
            return json;
        }
        #endregion
    }
}
