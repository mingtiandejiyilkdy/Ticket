using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel; 
using PWMIS.DataMap.Entity; 
using PWMIS.Core.Extensions;
using Ticket.Common;
using Ticket.Models.Financial;
using Ticket.ViewModels.Financial;

namespace Ticket.BLL.Financial
{
    public class CustomReceiptedBLL : BLLBase
    {
        #region 基础方法
        /// <summary>
        /// 获取管理员列表（全部）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<CustomReceiptedModel> GetAllModelList()
        {
            JsonRsp<CustomReceiptedViewModel> rsp = new JsonRsp<CustomReceiptedViewModel>();
            CustomReceiptedModel model = new CustomReceiptedModel();
            OQL q = OQL.From(model)
                .Select()
                .OrderBy(model.ID, "desc")
                .END;
            return q.ToList<CustomReceiptedModel>();  
        }

        /// <summary>
        /// 获取管理员列表（分页）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<CustomReceiptedViewModel> GetPageList(int pageIndex, int pageSize)
        {
            JsonRsp<CustomReceiptedViewModel> rsp = new JsonRsp<CustomReceiptedViewModel>();

            CustomReceiptedModel m = new CustomReceiptedModel();
            OQL q = OQL.From(m)
                .Select()
                .OrderBy(m.ID, "asc")
                .END;
            //分页
            q.Limit(pageSize, pageIndex, true);
            //q.PageWithAllRecordCount = allCount;
            //List<Employee> list= EntityQuery<Employee>.QueryList(q);
            List<CustomReceiptedModel> list = q.ToList<CustomReceiptedModel>();//使用OQL扩展
            rsp.data = list.ConvertAll<CustomReceiptedViewModel>(o =>
            {
                return new CustomReceiptedViewModel()
                {
                    ID = o.ID, 
                    CustomAccReceiptId = o.CustomAccReceiptId, 
                    BankAccountId = o.BankAccountId,
                    CurrentAmount = o.CurrentAmount,
                    BankSerialNumber = o.BankSerialNumber,
                    DateOfEntry = o.DateOfEntry,
                    CreateId = o.CreateId,
                    CreateUser = o.CreateUser,
                    CreateIP = o.CreateIP,
                    CreateTime = o.CreateTime, 
                    Status = o.Status, 
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
        public JsonRsp Add(CustomReceiptedModel model)
        {
            model.CustomReceiptedNo = "S" + model.CustomId + DateTime.Now.ToString("yyyyMMddHHmmss");
            int returnvalue = Add<CustomReceiptedModel>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Remove(CustomReceiptedModel model)
        {
            int returnvalue = EntityQuery<CustomReceiptedModel>.Instance.Delete(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Update(CustomReceiptedModel model)
        {
            int returnvalue = Update<CustomReceiptedModel>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }

        /// <summary>
        /// 查 根据Id获取详情，如果没有则返回空对象
        /// </summary>
        /// <param name="CustomReceiptederID"></param>
        /// <returns></returns>
        public CustomReceiptedModel GetModelById(int accountId)
        {
            CustomReceiptedModel model = new CustomReceiptedModel() { ID = accountId };
            if (EntityQuery<CustomReceiptedModel>.Fill(model))
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
            CustomReceiptedModel user = new CustomReceiptedModel(); 
             
            OQL deleteQ = OQL.From(user)
                            .Delete()
                            .Where(cmp => cmp.Comparer(user.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的
                         .END;
            int returnvalue = EntityQuery<CustomReceiptedModel>.Instance.ExecuteOql(deleteQ);

            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }


        /// <summary>
        /// save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Save(CustomReceiptedModel model)
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
            CustomReceiptedModel user = new CustomReceiptedModel();
            user.Status = status;
            OQL q = OQL.From(user)
               .Update(user.Status)
                          .Where(cmp => cmp.Comparer(user.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的
                       .END;
            int returnvalue = EntityQuery<CustomReceiptedModel>.Instance.ExecuteOql(q);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        #endregion

        
        #region ViewModel

        #region 获取列表（全部）
        /// <summary>
        /// 获取管理员列表（全部）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<CustomReceiptedViewModel> GetAllList()
        {
            JsonRsp<CustomReceiptedViewModel> rsp = new JsonRsp<CustomReceiptedViewModel>();

            rsp.data = GetAllModelList().ConvertAll<CustomReceiptedViewModel>(o =>
            {
                return new CustomReceiptedViewModel()
                {
                    ID = o.ID,
                    CustomAccReceiptId = o.CustomAccReceiptId, 
                    BankAccountId = o.BankAccountId,
                    CurrentAmount = o.CurrentAmount,
                    BankSerialNumber = o.BankSerialNumber,
                    DateOfEntry = o.DateOfEntry,
                    CreateId = o.CreateId,
                    CreateUser = o.CreateUser,
                    CreateIP = o.CreateIP,
                    CreateTime = o.CreateTime,
                    Status = o.Status,
                };
            }
            );
            rsp.success = true;
            rsp.code = 0;
            return rsp;
        }
        #endregion
         

        #endregion
    }
}
