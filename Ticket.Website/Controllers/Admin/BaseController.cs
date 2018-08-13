using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket.ViewModels;
using System.Web.Security;
using System.Web.Script.Serialization;
using Ticket.Website.App_Start;
using Ticket.BLL;
using Ticket.Common;

namespace Ticket.Website.Controllers.Admin
{
    [Authentication]
    public class BaseController : Controller
    {
        public LayoutViewModel layoutViewModel = new LayoutViewModel();

        //
        // GET: /Base/
        public BaseController()
        {
            long adminId = 1;
            //if (CacheHelper.CacheRetrieveObject("AdminId") != null)
            //{
                //adminId = Convert.ToInt64(CacheHelper.CacheRetrieveObject("AdminId"));
                layoutViewModel.menuList = new AdminRoleBLL().GetMenuListByAccountId(adminId, 0);
            //}
            ViewBag.Title = "主页";
            ViewBag.TopMenuModel = layoutViewModel;
        }


        #region cookies方法
        public AccountViewModel AdminUser
        {
            get
            {
                //1.登录状态获取用户信息（自定义保存的用户）
                var cookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

                //2.使用 FormsAuthentication 解密用户凭据
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                AccountViewModel model = new AccountViewModel();

                //3. 直接解析到用户模型里去，有没有很神奇
                model = new JavaScriptSerializer().Deserialize<AccountViewModel>(ticket.UserData);

                return model;
            }
        }
        //登录用户Id
        public long AdminId
        {
            get
            {
                return AdminUser.ID;
            }
        }
        //登录用户名称
        public string AdminName
        {
            get
            {
                return AdminUser.AccountName;
            }
        }
        #endregion

    }
}

