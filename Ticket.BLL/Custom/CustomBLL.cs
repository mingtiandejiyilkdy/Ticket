using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Common;
using PWMIS.DataMap.Entity;
using PWMIS.Core.Extensions;
using Ticket.Models.Custom;
using Ticket.ViewModels.Custom;

namespace Ticket.BLL.Custom
{
    public class CustomBLL : BLLBase
    {
        #region 基础方法
        /// <summary>
        /// 获取管理员列表（全部）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<CustomModel> GetAllModelList()
        {
            JsonRsp<CustomViewModel> rsp = new JsonRsp<CustomViewModel>();
            CustomModel model = new CustomModel();
            model.TenantId = TenantId;
            OQL q = OQL.From(model)
                .Select()
                .Where(model.TenantId)
                .OrderBy(model.Sort, "desc")
                .END;
            return q.ToList<CustomModel>();
        }

        OQLCompare CreateCondition(OQLCompare cmp, CustomModel custom)
        {
            OQLCompare cmpResult = null;
            if (custom.CustomName != "")
                cmpResult = cmp.Comparer(custom.CustomName, OQLCompare.CompareType.Like, "%" + custom.CustomName + "%");
            return cmpResult;
        }

        /// <summary>
        /// 获取管理员列表（分页）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<CustomModel> GetPageList(string searchTxt, int pageIndex, int pageSize)
        {

            CustomModel CustomModel = new CustomModel();
            JsonRsp<CustomModel> rsp = new JsonRsp<CustomModel>();

            OQLCompareFunc<CustomModel> cmpResult = (cmp, c) =>
                    (
                        //cmp.Property(c.CustomName) == "ABC" &
                      cmp.Comparer(c.CustomName, OQLCompare.CompareType.Like, "%" + searchTxt + "%")
                    );

            rsp.success = true;
            rsp.code = 0;
            rsp.data = GetList<CustomModel>(cmpResult, null, pageSize, pageIndex); 
            return rsp;
        }

        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Add(CustomModel model)
        {
            int returnvalue = Add<CustomModel>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Remove(CustomModel model)
        {
            int returnvalue = EntityQuery<CustomModel>.Instance.Delete(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Update(CustomModel model)
        {
            int returnvalue = Update<CustomModel>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }

        /// <summary>
        /// 查 根据Id获取详情，如果没有则返回空对象
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public CustomModel GetModelById(int Id)
        {
            return GetModel<CustomModel>(Id);
        }
        /// <summary>
        /// save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp DeleteById(long[] Ids)
        {
            //删除 测试数据-----------------------------------------------------
            CustomModel user = new CustomModel();

            OQL deleteQ = OQL.From(user)
                            .Delete()
                            .Where(cmp => cmp.Comparer(user.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的
                         .END;
            int returnvalue = EntityQuery<CustomModel>.Instance.ExecuteOql(deleteQ);

            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }


        /// <summary>
        /// save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Save(CustomModel model)
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
            CustomModel model = new CustomModel(); 
            OQL q = OQL.From(model)
               .Update(model.Status, model.UpdateId, model.UpdateUser, model.UpdateIP, model.UpdateIP)
                          .Where(cmp => cmp.Comparer(model.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的
                       .END;
            int returnvalue = EntityQuery<CustomModel>.Instance.ExecuteOql(q);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        #endregion


        #region ViewModel

        #region 获取列表（全部）
        /// <summary>
        /// 获取列表（全部）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<CustomViewModel> GetAllList()
        {
            JsonRsp<CustomViewModel> rsp = new JsonRsp<CustomViewModel>();

            rsp.data = GetAllModelList().ConvertAll<CustomViewModel>(o =>
            {
                return new CustomViewModel()
                {
                    ID = o.ID,
                    CustomTypeId = o.CustomTypeId,
                    CustomName = o.CustomName,
                    LinkPhone = o.LinkPhone,
                    LinkName = o.LinkName,
                    LinkMobile = o.LinkMobile,
                    CustomArea = o.CustomArea,
                    CustomAddress = o.CustomAddress,
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
        #endregion

        #region  获取客户SelectTree
        public List<TreeSelect> GetSelectTrees()
        {
            List<TreeSelect> treeSelects = new List<TreeSelect>();
            foreach (var item in GetAllModelList())
            {
                treeSelects.Add(new TreeSelect
                {
                    id = item.ID,
                    name = item.CustomName,
                    value = item.ID,
                });
            }
            return treeSelects;
        }
        #endregion

        #endregion
    }
}
