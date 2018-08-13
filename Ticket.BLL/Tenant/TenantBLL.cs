using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Models.Tenant;
using PWMIS.DataMap.Entity;
using PWMIS.Core.Extensions;
using Ticket.Common;
using Ticket.ViewModels.Tenant;

namespace Ticket.BLL.Tenant
{
    public class TenantBLL : BLLBase
    {
        #region 基础方法
        /// <summary>
        /// 获取列表（全部）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<TenantModel> GetAllModelList()
        {
            TenantModel model = new TenantModel();
            OQL q = OQL.From(model)
                .Select()
                .OrderBy(model.ID, "asc")
                .END;
            return q.ToList<TenantModel>();//使用OQL扩展 
        }

        /// <summary>
        /// 获取管理员列表（分页）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<TenantViewModel> GetPageList(int pageIndex, int pageSize)
        {
            JsonRsp<TenantViewModel> rsp = new JsonRsp<TenantViewModel>();

            TenantModel m = new TenantModel();
            OQL q = OQL.From(m)
                .Select()
                .OrderBy(m.ID, "asc")
                .END;
            //分页
            q.Limit(pageSize, pageIndex, true);
            //q.PageWithAllRecordCount = allCount;
            //List<Employee> list= EntityQuery<Employee>.QueryList(q);
            List<TenantModel> list = q.ToList<TenantModel>();//使用OQL扩展
            rsp.data = list.ConvertAll<TenantViewModel>(o =>
            {
                return new TenantViewModel()
                {
                    ID = o.ID,
                    TenantName = o.TenantName,
                    TenantDomain = o.TenantDomain,
                    Sort = o.Sort,
                    Status = o.Status,
                    CreateBy = o.CreateUser,
                    CreateIP = o.CreateIP,
                    CreateTime = o.CreateTime,
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
        public JsonRsp Add(TenantModel model)
        {
            //salt
            string strSalt = Guid.NewGuid().ToString();
            model.CreateUser = AdminName;
            model.CreateIP = Util.GetLocalIP;
            model.CreateTime = DateTime.Now;
            int returnvalue = EntityQuery<TenantModel>.Instance.Insert(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Remove(TenantModel model)
        {
            int returnvalue = EntityQuery<TenantModel>.Instance.Delete(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Update(TenantModel model)
        {
            int returnvalue = EntityQuery<TenantModel>.Instance.Update(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }

        /// <summary>
        /// 查 根据Id获取详情，如果没有则返回空对象
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public TenantModel GetModelById(int accountId)
        {
            TenantModel model = new TenantModel() { ID = accountId };
            if (EntityQuery<TenantModel>.Fill(model))
                return model;
            else
                return null;
        }
        /// <summary>
        /// save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp DeleteById(long[] Ids)
        {
            //删除 测试数据-----------------------------------------------------
            TenantModel user = new TenantModel();

            OQL deleteQ = OQL.From(user)
                            .Delete()
                            .Where(cmp => cmp.Comparer(user.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的
                         .END;
            int returnvalue = EntityQuery<TenantModel>.Instance.ExecuteOql(deleteQ);

            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }


        /// <summary>
        /// save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Save(TenantModel model)
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

        /// <summary>
        ///  启用/禁用
        /// </summary>
        /// <param name="accountStatus"></param>
        /// <returns></returns>
        public JsonRsp SetStatus(long[] Ids, int status)
        {
            if (Ids == null)
            {
                return new JsonRsp { success = false, retmsg = "请选择要操作的数据" };
            }
            TenantModel user = new TenantModel();
            user.Status = status;
            OQL q = OQL.From(user)
               .Update(user.Status)
                          .Where(cmp => cmp.Comparer(user.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的
                       .END;
            int returnvalue = EntityQuery<TenantModel>.Instance.ExecuteOql(q);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        #endregion

        #region 基础方法
        /// <summary>
        /// 获取列表（全部）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<TenantViewModel> GetAllList()
        {
            JsonRsp<TenantViewModel> rsp = new JsonRsp<TenantViewModel>();
            List<TenantModel> list = GetAllModelList();
            rsp.data = list.ConvertAll<TenantViewModel>(o =>
            {
                return new TenantViewModel()
                {
                    ID = o.ID,
                    TenantName = o.TenantName,
                    TenantDomain = o.TenantDomain,
                    Sort = o.Sort,
                    Status = o.Status,
                    CreateBy = o.CreateUser,
                    CreateIP = o.CreateIP,
                    CreateTime = o.CreateTime,
                };
            }
            );
            rsp.success = true;
            rsp.code = 0;
            return rsp;
        }
        #endregion
    }
}

