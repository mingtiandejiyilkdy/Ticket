using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using PWMIS.DataMap.Entity;
using PWMIS.Core.Extensions;
using Ticket.Common;
using Ticket.Enum;
using Ticket.Models.Contract;
using Ticket.ViewModels.Contract;  
using Ticket.BLL;
using Ticket.Models.Custom;
using Ticket.Models.Financial;

namespace Ticket.BLL.Contract
{
    public class ContractBLL : BLLBase
    {
        #region 基础方法
        /// <summary>
        /// 获取列表（全部）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<ContractModel> GetAllModelList()
        { 
            ContractModel model = new ContractModel();
            OQL q = OQL.From(model)
                .Select()
                .OrderBy(model.ID, "asc")
                .END;
            return q.ToList<ContractModel>(); 
        }

        /// <summary>
        /// 获取管理员列表（分页）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<ContractViewModel> GetPageList(int pageIndex, int pageSize)
        {
            JsonRsp<ContractViewModel> rsp = new JsonRsp<ContractViewModel>();


            ContractModel contract = new ContractModel();
            CustomModel custom = new CustomModel();
            CustomTypeModel customType = new CustomTypeModel();

            //Select 方法不指定具体要选择的实体类属性，可以推迟到EntityContainer类的MapToList 方法上指定
            OQL joinQ = OQL.From(contract)
                .Join(custom).On(contract.CustomId, custom.ID)
                .Join(customType).On(custom.CustomTypeId, customType.ID)
                .Select()
                .OrderBy(contract.Sort, "desc")
                .END;

            joinQ.Limit(pageSize, pageIndex, true);

            PWMIS.DataProvider.Data.AdoHelper db = PWMIS.DataProvider.Adapter.MyDB.GetDBHelper();
            EntityContainer ec = new EntityContainer(joinQ, db);

            rsp.data = (List<ContractViewModel>)ec.MapToList<ContractViewModel>(() => new ContractViewModel()
            {
                ID = contract.ID,
                CustomId = contract.CustomId,
                CustomName=custom.CustomName,
                CustomTypeName=customType.CustomTypeName,
                ContractNo = contract.ContractNo,
                ContractAmount = contract.ContractAmount,
                Deductions = contract.Deductions,
                Balance = contract.Balance,
                Remark = contract.Remark,
                Sort = contract.Sort,
                Status = contract.Status,
                CreateId = contract.CreateId,
                CreateUser = contract.CreateUser,
                CreateIP = contract.CreateIP,
                CreateTime = contract.CreateTime,
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
        public JsonRsp Add(ContractModel model)
        {
            model.Deductions = 0;
            model.Balance = model.ContractAmount;
            model.Salt = Guid.NewGuid().ToString();
            //合同信息
            string salt = model.Salt;
            string signStr = model.ContractAmount.ToString() + model.Deductions.ToString() + model.Balance + salt;
            model.BalanceKey = EncryptHelper.MD5Encoding(signStr, salt);
            model.ContractNo = "H" + DateTime.Now.ToString("yyyyMMddHHmmss"); 
            int returnvalue = Add<ContractModel>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Remove(ContractModel model)
        {
            int returnvalue = EntityQuery<ContractModel>.Instance.Delete(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Update(ContractModel model)
        {
            int returnvalue = Update<ContractModel>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }

        /// <summary>
        /// 查 根据Id获取详情，如果没有则返回空对象
        /// </summary>
        /// <param name="TicketerID"></param>
        /// <returns></returns>
        public ContractModel GetModelById(long id)
        {
            ContractModel model = new ContractModel() { ID = id };
            if (EntityQuery<ContractModel>.Fill(model))
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
            ContractModel user = new ContractModel();

            OQL deleteQ = OQL.From(user)
                            .Delete()
                            .Where(cmp => cmp.Comparer(user.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的
                         .END;
            int returnvalue = EntityQuery<ContractModel>.Instance.ExecuteOql(deleteQ);

            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }


        /// <summary>
        /// save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Save(ContractModel model)
        {
            model.TenantId = TenantId;
            if (model.ID == 0)
            {
                return Add(model);
            }
            else
            {
                if (model.TenantId != TenantId)
                {
                    return new JsonRsp { success = false, code = -1, retmsg = "数据验证失败" };
                } 
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
            ContractModel user = new ContractModel();
            user.Status = status;
            OQL q = OQL.From(user)
               .Update(user.Status)
                          .Where(cmp => cmp.Comparer(user.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的
                       .END;
            int returnvalue = EntityQuery<ContractModel>.Instance.ExecuteOql(q);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        #endregion

         /// <summary>
        /// 审核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Audit(long[] Ids, int status)
        { 
            #region 
            if (Ids == null)
            {
                return new JsonRsp { success = false, retmsg = "请选择要操作的数据" };
            }

            int returnvalue = 0;
            PWMIS.DataProvider.Data.AdoHelper db = PWMIS.DataProvider.Adapter.MyDB.GetDBHelper();
            try
            {
                //开始事务
                db.BeginTransaction();

                //更新状态
                ContractModel model = new ContractModel();
                OQL qList = OQL.From(model)
                    .Select()
                    .OrderBy(model.Sort, "asc")
                    .END;
                List<ContractModel> items = qList.ToList<ContractModel>();
                foreach (ContractModel item in items)
                {
                    if (item.Status != 0)
                    {
                        return new JsonRsp { success = false, retmsg = "只能审核待审核合同，该合同当前状态为：" + Util.getStatus(item.Status, typeof(BaseEnum.ProtocolTypeEnum)) };
                    }
                    //更新合同状态
                    item.Status = status;
                    returnvalue += Update<ContractModel>(item, db, false);

                    string remark = "合同/协议号：" + item.ContractNo + "审核通过";
                    //获取当前客户财务信息 
                    CustomFinancialModel financialModel = OQL.FromObject<CustomFinancialModel>().Select().Where((cmp, m) => cmp.Comparer(m.CustomId, "=", item.CustomId) & cmp.Comparer(m.TenantId, "=", item.TenantId)).END.ToObject(db);
                    if (financialModel == null)
                    {
                        financialModel = new CustomFinancialModel();
                        //客户财务信息初始化
                        financialModel.CustomId = item.CustomId;
                        financialModel.MoneyTypeOneAmount = item.MoneyTypeOneAmount;
                        financialModel.MoneyTypeOneTotalAmount = item.MoneyTypeOneAmount;
                        financialModel.MoneyTypeTwoAmount = item.MoneyTypeTwoAmount;
                        financialModel.MoneyTypeTwoTotalAmount = item.MoneyTypeTwoAmount;
                        financialModel.MoneyTypeThreeAmount = item.MoneyTypeThreeAmount;
                        financialModel.MoneyTypeThreeTotalAmount = item.MoneyTypeThreeAmount;
                        financialModel.Status = 1;
                        returnvalue += Add<CustomFinancialModel>(financialModel, db);
                    }
                    else
                    {
                        //客户财务信息修改
                        financialModel.CustomId = item.CustomId;
                        financialModel.MoneyTypeOneAmount += item.MoneyTypeOneAmount;
                        financialModel.MoneyTypeOneTotalAmount += item.MoneyTypeOneAmount;
                        financialModel.MoneyTypeTwoAmount += item.MoneyTypeTwoAmount;
                        financialModel.MoneyTypeTwoTotalAmount += item.MoneyTypeTwoAmount;
                        financialModel.MoneyTypeThreeAmount += item.MoneyTypeThreeAmount;
                        financialModel.MoneyTypeThreeTotalAmount += item.MoneyTypeThreeAmount;
                        financialModel.Status = 1;
                        returnvalue += Update<CustomFinancialModel>(financialModel, db);
                    }

                    //新增客户财务信息日志 
                    //应收明细
                    CustomFinancialDetailModel financialDetailOne = new CustomFinancialDetailModel();
                    financialDetailOne.CustomFinancialId = financialModel.ID;
                    financialDetailOne.FinanciaOpeType = (int)FinanciaOpeTypeEnum.增加;
                    financialDetailOne.CurrentAmount += item.MoneyTypeOneAmount;
                    financialDetailOne.Balance += item.MoneyTypeOneAmount;
                    financialDetailOne.Remark = remark;
                    financialDetailOne.Status = 1;
                    financialDetailOne.MoneyType = (int)BaseEnum.MoneyTypeEnum.应收;
                    returnvalue += Add<CustomFinancialDetailModel>(financialDetailOne, db);
                    //赠送明细     
                    CustomFinancialDetailModel financialDetailTwo = new CustomFinancialDetailModel();
                    financialDetailTwo.CustomFinancialId = financialModel.ID;
                    financialDetailTwo.FinanciaOpeType = (int)FinanciaOpeTypeEnum.增加;
                    financialDetailTwo.CurrentAmount += item.MoneyTypeTwoAmount;
                    financialDetailTwo.Balance += item.MoneyTypeTwoAmount;
                    financialDetailTwo.Remark = remark;
                    financialDetailTwo.Status = 1;
                    financialDetailTwo.MoneyType = (int)BaseEnum.MoneyTypeEnum.赠送;
                    returnvalue += Add<CustomFinancialDetailModel>(financialDetailTwo, db);
                    //置换明细
                    CustomFinancialDetailModel financialDetailThree = new CustomFinancialDetailModel();
                    financialDetailThree.CustomFinancialId = financialModel.ID;
                    financialDetailThree.FinanciaOpeType = (int)FinanciaOpeTypeEnum.增加;
                    financialDetailThree.CurrentAmount += item.MoneyTypeThreeAmount;
                    financialDetailThree.Balance += item.MoneyTypeThreeAmount;
                    financialDetailThree.Remark = remark;
                    financialDetailThree.Status = 1;
                    financialDetailThree.MoneyType = (int)BaseEnum.MoneyTypeEnum.置换;
                    returnvalue += Add<CustomFinancialDetailModel>(financialDetailThree);
                    //事务提交
                    if (returnvalue == 5)
                    {
                        db.Commit();
                    }
                    else
                    {
                        returnvalue = 0;
                        db.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                //事务回滚
                db.Rollback();
                return new JsonRsp { success = false, retmsg = ex.Message.ToString() };
            }
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
            #endregion
        }

        /// <summary>
        /// 查 根据Id获取详情，如果没有则返回空对象
        /// </summary>
        /// <param name="TicketerID"></param>
        /// <returns></returns>
        public ContractViewModel GetViewModelById(long contractId)
        {
            ContractModel contract = GetModelById(contractId);
            if (contract != null)
            {
                CustomModel customModel = new CustomModel() { ID = contract.CustomId };
                if (EntityQuery<CustomModel>.Fill(customModel))
                {
                    return new ContractViewModel()
                    {
                        ID = contract.ID,
                        CustomId = contract.CustomId,
                        CustomName = customModel.CustomName,
                        ContractNo = contract.ContractNo,
                        ContractAmount = contract.ContractAmount,
                        Deductions = contract.Deductions,
                        Balance = contract.Balance,
                        Remark = contract.Remark,
                        Sort = contract.Sort,
                        Status = contract.Status,
                        CreateId = contract.CreateId,
                        CreateUser = contract.CreateUser,
                        CreateIP = contract.CreateIP,
                        CreateTime = contract.CreateTime,
                    };
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }

        #region  获取合同SelectTree
        public JsonRsp<TreeSelect> GetSelectTrees(long? customId)
        {
            JsonRsp<TreeSelect> rsp = new JsonRsp<TreeSelect>();
            List<ContractModel> contractList = GetAllModelList().FindAll(o=>o.Balance>0);
            if(customId>0){
                contractList = contractList.FindAll(o=>o.CustomId==customId);
            };
            foreach (var item in GetAllModelList())
            {
                rsp.data.Add(new TreeSelect
                {
                    id = item.ID,
                    name = item.ContractNo,
                    value = item.ID,
                });
            }
            rsp.success=true;
            return rsp;
        }
        #endregion

    }
}
