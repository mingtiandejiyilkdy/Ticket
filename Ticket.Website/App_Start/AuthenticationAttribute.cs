using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ticket.ViewModels;
using System.Web.Script.Serialization;
using Ticket.Models.Tenant;
using Ticket.BLL.Tenant;
using System.Web.Http.Controllers;
using Ticket.Common;

namespace Ticket.Website.App_Start
{
    /// <summary>
    /// 登录身份验证
    /// </summary>
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        protected readonly TenantBLL tenantBLL = new TenantBLL();
        /// <summary>
        /// 是否记录日志
        /// </summary>
        protected bool _log;
        /// <summary>
        /// 备注信息
        /// </summary>
        protected string _message;
        /// <summary>
        /// 控制器名称
        /// </summary>
        protected string controllername;
        /// <summary>
        /// 操作名称
        /// </summary>
        protected string actionname;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //获取当前域名
            string host = filterContext.HttpContext.Request.Url.Host;
            long tenantId = 0;
            TenantModel tenant = tenantBLL.GetAllModelList().Find(o => o.TenantDomain.ToLower() == host.ToLower());
            if (tenant != null)
            {
                tenantId = tenant.ID;
                //CacheHelper.SetCookie("tenantId", "tenantId");
            }            

            //获取当前控制器信息
            string controllername = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            string actionname = filterContext.ActionDescriptor.ActionName.ToLower();
            string allowActions = string.Empty;


            if (!filterContext.RequestContext.HttpContext.Request.IsAuthenticated)
            {
                //未登录的时候，此处加了一个判断，判断同步请求还是一部请求
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    //异步请求，返回JSON数据
                    filterContext.Result = new JsonResult
                    {
                        Data = new
                        {
                            Status = -1,
                            Message = "登录已过期，请刷新页面后操作!"
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    //非异步请求，则跳转登录页
                    FormsAuthentication.RedirectToLoginPage();//重定向会登录页
                }
            }
            else
            {
                //1.登录状态获取用户信息（自定义保存的用户）
                var cookie = filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

                //2.使用 FormsAuthentication 解密用户凭据
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                AccountViewModel loginUser = new AccountViewModel();

                //3. 直接解析到用户模型里去，有没有很神奇
                loginUser = new JavaScriptSerializer().Deserialize<AccountViewModel>(ticket.UserData);

                //4. 将要使用的数据放到ViewData 里，方便页面使用
                filterContext.Controller.ViewData["UserName"] = loginUser.AccountName;
                //filterContext.Controller.ViewData["Portrait"] = loginUser.Portrait;
                filterContext.Controller.ViewData["UserID"] = loginUser.ID;

                //var actionParameters = filterContext.ActionDescriptor.GetParameters();
                //foreach (var p in actionParameters)
                //{
                //    if (p.ParameterType == typeof(string))
                //    {
                //        if (filterContext.ActionParameters[p.ParameterName] != null)
                //        {
                //            filterContext.ActionParameters[p.ParameterName] = StringHelper.GetValidScriptMsg(filterContext.ActionParameters[p.ParameterName].ToString());
                //        }
                //    }
                //}


                //////对参数的过滤，例如处理一些比较敏感的信息
                ////string parameterName = "AdminName";
                //////s1: 获取参数信息 ，得到参数的个数以及每个参数的类别
                ////filterContext.ActionDescriptor.GetParameters();
                //////s2: 获取参数值,返回一个字典
                //////parameterValue 参数值 ， parameterName 参数名
                ////var parameterValue = filterContext.ActionParameters[parameterName];
                //////s3: 敏感信息过滤
                ////// 过滤算法

                ////// 过滤后赋值
                //////filterContext.ActionParameters[parameterName] = newParameterValue;

                ////追加参数
                //var parameters = filterContext.ActionParameters;
                //parameters.Add("TenantId", loginUser.TenantId);
                //parameters.Add("CreateId", loginUser.ID);
                //parameters.Add("AdminName", loginUser.AccountName);
                //parameters.Add("CreateIP", Util.GetLocalIP);
                //parameters.Add("CreateTime", DateTime.Now);
                //parameters.Add("UpdateId", loginUser.ID);
                //parameters.Add("UpdateUser", loginUser.AccountName);
                //parameters.Add("UpdateIP", Util.GetLocalIP);
                //parameters.Add("UpdateTime", DateTime.Now);
            }
            // 别忘了这一句。
            base.OnActionExecuting(filterContext);

            

            //if (parameters.Keys.Contains("qxun_userkey"))
            //{
            //    parameters["qxun_userkey"] as string;
            //}
        } 

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            //filterContext.HttpContext.Response.Write("Action执行之后" + Message + "<br />");
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
            //filterContext.HttpContext.Response.Write("返回Result之前" + Message + "<br />");
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            //filterContext.HttpContext.Response.Write("返回Result之后" + Message + "<br />");
        }
    }
}