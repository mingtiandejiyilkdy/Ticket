using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket.BLL;
using System.Web.Security;
using System.Web.Script.Serialization;
using Ticket.Models;
using Ticket.Common;

namespace Ticket.Website.Controllers.Admin
{
    public class LoginController : Controller
    {
        protected readonly LoginBLL loginBLL = new LoginBLL();
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LoginIn(string username, string password, string rememberMe, string returnUrl)
        {
            JsonRsp json = new JsonRsp { success = false };
            if (!ModelState.IsValid)
            {
                return Json(json, JsonRequestBehavior.AllowGet);
            }

            bool status = Request.IsAuthenticated;


            json = loginBLL.CheckPassWord(username, password);
            if (!json.success)
            {
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            AdminAccount account = (AdminAccount)json.returnObj;

            //string host = HttpContext.Request.Url.Host;
            //long tenantId = 0;
            //TenantModel tenant = tenantBLL.GetAllModelList().Find(o => o.TenantDomain.ToLower() == host.ToLower());
            //if (tenant != null)
            //{
            //    tenantId = tenant.ID;
            //    account.TenantId = tenantId;
            //}


            ////4. 用户描述用户基本信息
            //AccountViewModel userInfo = new AccountViewModel()
            //{
            //    ID = json.code,
            //    AccountName = json.retmsg, 
            //};

            //2.用它来序列化要对象
            JavaScriptSerializer serial = new JavaScriptSerializer();
            //5. 生成初始化凭据
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1,
                username,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                false,
                serial.Serialize(account)
                );
            //6. 加密
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            //7. 响应到客户端
            System.Web.HttpCookie authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
            //8. 返回首页， 也可以跳回 RetureUrl

            //获取用户权限

            //获取用户配置信息
            //租户ID
            //TenantId 

            CacheHelper.CacheAddObject("AdminId",account.ID);
            

            if (!String.IsNullOrEmpty(returnUrl))
                json.retmsg = returnUrl;
            else
                json.retmsg = "/Admin/Home/";
            return RedirectToAction("Home", "Admin");
            //return Json(json, JsonRequestBehavior.AllowGet);
            //if (!String.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);
            //else return RedirectToAction("Home", "Admin"); 
        }

        /// <summary>
        /// 注销方法,退出登录
        /// </summary>
        public void LogOut()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

    }
}
