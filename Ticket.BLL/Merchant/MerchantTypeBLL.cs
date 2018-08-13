using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel; 
using PWMIS.DataMap.Entity; 
using PWMIS.Core.Extensions;
using Ticket.Common;
using Ticket.Models.Merchant;
using Ticket.ViewModels.Merchant;

namespace Ticket.BLL.Merchant
{
    public class MerchantTypeBLL : BLLBase
    {
        #region 基础方法
        /// <summary>
        /// 获取管理员列表（全部）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<MerchantTypeModel> GetAllModelList()
        { 
            MerchantTypeModel model = new MerchantTypeModel();
            OQL q = OQL.From(model)
                .Select()
                .OrderBy(model.ID, "asc")
                .END;
            return q.ToList<MerchantTypeModel>();//使用OQL扩展 
        }

        /// <summary>
        /// 获取管理员列表（分页）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<MerchantTypeViewModel> GetPageList(int pageIndex, int pageSize)
        {
            JsonRsp<MerchantTypeViewModel> rsp = new JsonRsp<MerchantTypeViewModel>();

            MerchantTypeModel m = new MerchantTypeModel();
            OQL q = OQL.From(m)
                .Select()
                .OrderBy(m.ID, "asc")
                .END;
            //分页
            q.Limit(pageSize, pageIndex, true);
            //q.PageWithAllRecordCount = allCount;
            //List<Employee> list= EntityQuery<Employee>.QueryList(q);
            List<MerchantTypeModel> list = q.ToList<MerchantTypeModel>();//使用OQL扩展
            rsp.data = list.ConvertAll<MerchantTypeViewModel>(o =>
            {
                return new MerchantTypeViewModel()
                {
                    ID = o.ID,
                    MerchantTypeName = o.MerchantTypeName,
                    CreateId = o.CreateId,
                    CreateUser = o.CreateUser,
                    CreateIP = o.CreateIP,
                    CreateTime = o.CreateTime,
                    Sort = o.Sort,
                    Status = o.Status,
                    UpdateBy = o.UpdateUser,
                    UpdateIP = o.UpdateIP,
                    UpdateTime = o.UpdateTime, 
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
        public JsonRsp Add(MerchantTypeModel model)
        { 
            int returnvalue = Add<MerchantTypeModel>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Remove(MerchantTypeModel model)
        {
            int returnvalue = EntityQuery<MerchantTypeModel>.Instance.Delete(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Update(MerchantTypeModel model)
        {
            int returnvalue = Update<MerchantTypeModel>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }

        /// <summary>
        /// 查 根据Id获取详情，如果没有则返回空对象
        /// </summary>
        /// <param name="MerchanterID"></param>
        /// <returns></returns>
        public MerchantTypeModel GetModelById(int accountId)
        {
            MerchantTypeModel model = new MerchantTypeModel() { ID = accountId };
            if (EntityQuery<MerchantTypeModel>.Fill(model))
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
            MerchantTypeModel user = new MerchantTypeModel(); 
             
            OQL deleteQ = OQL.From(user)
                            .Delete()
                            .Where(cmp => cmp.Comparer(user.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的
                         .END;
            int returnvalue = EntityQuery<MerchantTypeModel>.Instance.ExecuteOql(deleteQ);

            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }


        /// <summary>
        /// save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Save(MerchantTypeModel model)
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
                return new JsonRsp { success = false, retmsg="请选择要操作的数据" };
            }
            MerchantTypeModel model = new MerchantTypeModel(); 
            model.Status = status;
            model.UpdateId = AdminId;
            model.UpdateUser = AdminName;
            model.UpdateIP = Util.GetLocalIP;
            model.UpdateTime = DateTime.Now;
            OQL q = OQL.From(model)
               .Update(model.Status, model.UpdateId, model.UpdateUser, model.UpdateIP, model.UpdateIP)
                          .Where(cmp => cmp.Comparer(model.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的 
                       .END;
            int returnvalue = EntityQuery<MerchantTypeModel>.Instance.ExecuteOql(q);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        #endregion


        /// <summary>
        /// 获取列表（全部）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<MerchantTypeViewModel> GetAllList()
        {
            JsonRsp<MerchantTypeViewModel> rsp = new JsonRsp<MerchantTypeViewModel>();
            List<MerchantTypeModel> list = GetAllModelList();
            rsp.data = list.ConvertAll<MerchantTypeViewModel>(o =>
            {
                return new MerchantTypeViewModel()
                {
                    ID = o.ID,
                    MerchantTypeName = o.MerchantTypeName,
                    CreateId = o.CreateId,
                    CreateUser = o.CreateUser,
                    CreateIP = o.CreateIP,
                    CreateTime = o.CreateTime,
                    Sort = o.Sort,
                    Status = o.Status,
                    UpdateBy = o.UpdateUser,
                    UpdateIP = o.UpdateIP,
                    UpdateTime = o.UpdateTime,
                };
            }
            );
            rsp.success = true;
            rsp.code = 0;
            return rsp;
        }

        #region  获取客户类型SelectTree
        public List<TreeSelect> GetSelectTrees()
        {
            List<TreeSelect> treeSelects = new List<TreeSelect>();
            foreach (var item in GetAllModelList())
            {
                treeSelects.Add(new TreeSelect
                {
                    id = item.ID,
                    name = item.MerchantTypeName,
                    value = item.ID,
                });
            }
            return treeSelects;
        }
        #endregion
    }
}
