using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel; 
using PWMIS.DataMap.Entity; 
using PWMIS.Core.Extensions;
using Ticket.Common;
using Ticket.ViewModels.Bank;
using Ticket.Models.Bank;  

namespace Ticket.BLL.Bank
{
    public class BankAccountBLL : BLLBase
    {
        #region 基础方法
        /// <summary>
        /// 获取列表（全部）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<BankAccountModel> GetAllModelList()
        { 
            BankAccountModel model = new BankAccountModel();
            OQL q = OQL.From(model)
                .Select()
                .OrderBy(model.ID, "asc")
                .END;
            return q.ToList<BankAccountModel>();//使用OQL扩展 
        }

        /// <summary>
        /// 获取管理员列表（分页）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<BankAccountViewModel> GetPageList(int pageIndex, int pageSize,bool limit=true)
        {
            JsonRsp<BankAccountViewModel> rsp = new JsonRsp<BankAccountViewModel>();

            BankAccountModel ba = new BankAccountModel(); 
            BankModel b = new BankModel(); 
            OQL joinQ = OQL.From(ba)
               .Join(b).On(ba.BankId, b.ID) 
                .Select()
                .OrderBy(ba.Sort, "desc")
                .END;
            //分页
            if (limit)
            {
                joinQ.Limit(pageSize, pageIndex, true);
            }
            PWMIS.DataProvider.Data.AdoHelper db = PWMIS.DataProvider.Adapter.MyDB.GetDBHelper();
            EntityContainer ec = new EntityContainer(joinQ, db);

            rsp.data = (List<BankAccountViewModel>)ec.MapToList<BankAccountViewModel>(() => new BankAccountViewModel()
            {
                ID = ba.ID,
                BankAccountName = ba.BankAccountName,
                BankName = b.BankName,
                BankId = ba.BankId,
                BankAccountCode = ba.BankAccountCode,
                CreateId = ba.CreateId,
                CreateUser = ba.CreateUser,
                CreateIP = ba.CreateIP,
                CreateTime = ba.CreateTime,
                Sort = ba.Sort,
                Status = ba.Status,
            }) ;             
            rsp.success = true;
            rsp.code = 0;
            rsp.count = joinQ.PageWithAllRecordCount;
            return rsp;
        }

        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Add(BankAccountModel model)
        {
            int returnvalue = Add<BankAccountModel>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Remove(BankAccountModel model)
        {
            int returnvalue = EntityQuery<BankAccountModel>.Instance.Delete(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Update(BankAccountModel model)
        {
            int returnvalue = Update<BankAccountModel>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }

        /// <summary>
        /// 查 根据Id获取详情，如果没有则返回空对象
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public BankAccountModel GetModelById(int Id)
        {
            return GetModel<BankAccountModel>(Id);
        }
        /// <summary>
        /// save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp DeleteById(long[] Ids)
        {
            //删除 测试数据-----------------------------------------------------
            BankAccountModel user = new BankAccountModel(); 
             
            OQL deleteQ = OQL.From(user)
                            .Delete()
                            .Where(cmp => cmp.Comparer(user.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的
                         .END;
            int returnvalue = EntityQuery<BankAccountModel>.Instance.ExecuteOql(deleteQ);

            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }


        /// <summary>
        /// save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Save(BankAccountModel model)
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
            BankAccountModel user = new BankAccountModel();
            user.Status = status;
            OQL q = OQL.From(user)
               .Update(user.Status)
                          .Where(cmp => cmp.Comparer(user.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的
                       .END;
            int returnvalue = EntityQuery<BankModel>.Instance.ExecuteOql(q);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        #endregion

        /// <summary>
        /// 获取列表（全部）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<BankAccountViewModel> GetAllList()
        {
            return GetPageList(0,0,false);
        }

        #region  获取银行账号列表SelectTree
        public List<TreeSelect> GetSelectTrees()
        {
            List<TreeSelect> treeSelects = new List<TreeSelect>();
            foreach (var item in GetAllModelList())
            {
                treeSelects.Add(new TreeSelect
                {
                    id = item.ID,
                    name = item.BankAccountName,
                    value = item.ID,
                });
            }
            return treeSelects;
        }
        #endregion

    }
}
