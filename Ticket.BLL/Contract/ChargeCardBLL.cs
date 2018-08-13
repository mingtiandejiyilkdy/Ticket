using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Models.Contract;
using Ticket.Common;
using PWMIS.DataMap.Entity;
using PWMIS.Core.Extensions;
using Ticket.ViewModels.Contract;
using Ticket.Models.Custom;
using Ticket.Models.Ticket;
using Ticket.Models.Financial;
using Ticket.Enum;

namespace Ticket.BLL.Contract
{
    public class ChargeCardBLL : BLLBase
    {
        #region 基础方法
        /// <summary>
        /// 获取列表（全部）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<ChargeCardsModel> GetAllModelList()
        {
            ChargeCardsModel model = new ChargeCardsModel();
            OQL q = OQL.From(model)
                .Select()
                .OrderBy(model.Sort, "desc")
                .OrderBy(model.ID, "asc")
                .END;
            return q.ToList<ChargeCardsModel>();//使用OQL扩展             
        }

        /// <summary>
        /// 获取列表（分页）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<ChargeCardViewModel> GetPageList(int pageIndex, int pageSize, bool limit = true)
        {
            JsonRsp<ChargeCardViewModel> rsp = new JsonRsp<ChargeCardViewModel>();

            ChargeCardsModel c = new ChargeCardsModel();
            CustomModel custom = new CustomModel();
            TicketTypeModel t = new TicketTypeModel();
            OQL joinQ = OQL.From(c)
               .Join(custom).On(c.CustomId, custom.ID)
                .LeftJoin(t).On(c.TicketTypeID, t.ID)
                .Select()
                .OrderBy(c.Sort, "desc")
                .END;
            //分页
            if (limit)
            {
                joinQ.Limit(pageSize, pageIndex, true);
            }
            PWMIS.DataProvider.Data.AdoHelper db = PWMIS.DataProvider.Adapter.MyDB.GetDBHelper();
            EntityContainer ec = new EntityContainer(joinQ, db);

            rsp.data = (List<ChargeCardViewModel>)ec.MapToList<ChargeCardViewModel>(() => new ChargeCardViewModel()
            {
                ID = c.ID,
                CustomId = c.CustomId,
                CustomName = custom.CustomName,
                TicketTypeID = c.TicketTypeID,
                TicketTypeName = t.TicketTypeName,
                CurrentCount = c.CurrentCount,
                FaceAmount = c.FaceAmount,
                CurrentAmount = c.CurrentAmount,
                ExpireDate = c.ExpireDate,
                TicketBatchId = c.TicketBatchId,
                TicketStart = c.TicketStart,
                TicketEnd = c.TicketEnd,
                Consumptionlevel = c.Consumptionlevel,
                IsCommonCard = c.IsCommonCard,
                Sort = c.Sort,
                CreateId = c.CreateId,
                CreateUser = c.CreateUser,
                CreateIP = c.CreateIP,
                CreateTime = c.CreateTime,
                UpdateBy = c.UpdateUser,
                UpdateIP = c.UpdateIP,
                UpdateTime = c.UpdateTime,
            });
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
        public JsonRsp Add(ChargeCardsModel model)
        {
            model.ChargeCardNo = "H" + model.CustomId + DateTime.Now.ToString("yyyyMMddHHmmss");
            //开启事务
            PWMIS.DataProvider.Data.AdoHelper db = PWMIS.DataProvider.Adapter.MyDB.GetDBHelper();

            int returnvalue = 0;
            try
            {
                db.BeginTransaction();
                //客户财务信息操作

                //获取当前客户财务信息 
                CustomFinancialModel financialModel = OQL.FromObject<CustomFinancialModel>().Select().Where((cmp, m) => cmp.Comparer(m.CustomId, "=", model.CustomId) & cmp.Comparer(m.TenantId, "=", TenantId)).END.ToObject(db);
                if (financialModel == null)
                {
                    db.Rollback();
                    return new JsonRsp { success = false, retmsg = "获取客户合同信息失败" };
                }
                decimal amount = model.CurrentAmount;
                string remark = "客户充卡";
                if (model.MoneyType == (int)MoneyTypeEnum.应收)
                {
                    if (financialModel.MoneyTypeOneAmount < amount) 
                    {
                        db.Rollback();
                        return new JsonRsp { success = false, retmsg = "余额只有" + financialModel.MoneyTypeOneAmount + "" };
                    }
                    financialModel.MoneyTypeOneAmount -= amount;
                    CustomFinancialDetailModel financialDetail = new CustomFinancialDetailModel();
                    financialDetail.CustomFinancialId = financialModel.ID;
                    financialDetail.FinanciaOpeType = (int)FinanciaOpeTypeEnum.减少;
                    financialDetail.Remark = remark;
                    financialDetail.MoneyType = (int)BaseEnum.MoneyTypeEnum.应收;
                    financialDetail.CurrentAmount = amount;
                    financialDetail.Balance = financialModel.MoneyTypeOneAmount;
                    returnvalue += Add<CustomFinancialDetailModel>(financialDetail, db);
                }
                else if (model.MoneyType == (int)BaseEnum.MoneyTypeEnum.赠送)
                {
                    if (financialModel.MoneyTypeTwoAmount < amount)
                    {
                        db.Rollback();
                        return new JsonRsp { success = false, retmsg = "余额只有" + financialModel.MoneyTypeTwoAmount + "" };
                    }
                    financialModel.MoneyTypeTwoAmount -= amount;
                    CustomFinancialDetailModel financialDetail = new CustomFinancialDetailModel();
                    financialDetail.CustomFinancialId = financialModel.ID;
                    financialDetail.FinanciaOpeType = (int)FinanciaOpeTypeEnum.减少;
                    financialDetail.Remark = remark;
                    financialDetail.MoneyType = (int)BaseEnum.MoneyTypeEnum.赠送;
                    financialDetail.CurrentAmount = amount;
                    financialDetail.Balance = financialModel.MoneyTypeTwoAmount; ;
                    returnvalue += Add<CustomFinancialDetailModel>(financialDetail, db);
                }
                else if (model.MoneyType == (int)BaseEnum.MoneyTypeEnum.置换)
                {
                    if (financialModel.MoneyTypeThreeAmount < amount)
                    {
                        db.Rollback();
                        return new JsonRsp { success = false, retmsg = "余额只有" + financialModel.MoneyTypeThreeAmount + "" };
                    }
                    financialModel.MoneyTypeThreeAmount -= amount; 
                    CustomFinancialDetailModel financialDetail = new CustomFinancialDetailModel();
                    financialDetail.CustomFinancialId = financialModel.ID;
                    financialDetail.FinanciaOpeType = (int)FinanciaOpeTypeEnum.减少;
                    financialDetail.Remark = remark;
                    financialDetail.MoneyType = (int)BaseEnum.MoneyTypeEnum.置换;
                    financialDetail.CurrentAmount = amount;
                    financialDetail.Balance = financialModel.MoneyTypeThreeAmount;
                    returnvalue += Add<CustomFinancialDetailModel>(financialDetail, db);
                }
                returnvalue += Update<CustomFinancialModel>(financialModel, db);
                returnvalue += Add<ChargeCardsModel>(model, db);

                ////新增客户应付
                //CustomAccReceiptModel customAR = new CustomAccReceiptModel();
                //customAR.CustomId = model.CustomId;
                //customAR.ChargeCardNo = model.ChargeCardNo;
                //customAR.ARAmount = model.CurrentAmount;
                //customAR.Status = (int)ARStatusEnum.已确认;
                //customAR.CreateUser = AdminName;
                //customAR.CreateIP = Util.GetLocalIP();
                //customAR.CreateTime = DateTime.Now;
                //returnvalue = EntityQuery<CustomAccReceiptModel>.Instance.Insert(customAR); 
                //事务提交
                if (returnvalue == 3)
                {
                    db.Commit();
                }
                else
                {
                    returnvalue = 0;
                    db.Rollback();
                }
            }
            catch (Exception ex)
            {
                //事务回滚
                db.Rollback();
                return new JsonRsp { success = false, retmsg = ex.Message.ToString() };
            }
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Remove(ChargeCardsModel model)
        {
            int returnvalue = EntityQuery<ChargeCardsModel>.Instance.Delete(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Update(ChargeCardsModel model)
        {
            int returnvalue = Update<ChargeCardsModel>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }

        /// <summary>
        /// 查 根据Id获取详情，如果没有则返回空对象
        /// </summary>
        /// <param name="TicketerID"></param>
        /// <returns></returns>
        public ChargeCardsModel GetModelById(int Id)
        {
            return GetModel<ChargeCardsModel>(Id);
        }
        /// <summary>
        /// save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp DeleteById(long[] Ids)
        {
            //删除 测试数据-----------------------------------------------------
            ChargeCardsModel user = new ChargeCardsModel();

            OQL deleteQ = OQL.From(user)
                            .Delete()
                            .Where(cmp => cmp.Comparer(user.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的
                         .END;
            int returnvalue = EntityQuery<ChargeCardsModel>.Instance.ExecuteOql(deleteQ);

            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }


        /// <summary>
        /// save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Save(ChargeCardsModel model)
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
            return new JsonRsp { success = false };
        }
        #endregion

        #region  获取充值编号SelectTree
        public List<TreeSelect> GetSelectTrees()
        {
            List<TreeSelect> treeSelects = new List<TreeSelect>();
            foreach (var item in GetAllModelList())
            {
                treeSelects.Add(new TreeSelect
                {
                    id = item.ID,
                    name = item.ChargeCardNo,
                    value = item.ID,
                });
            }
            return treeSelects;
        }
        #endregion

    }
}
