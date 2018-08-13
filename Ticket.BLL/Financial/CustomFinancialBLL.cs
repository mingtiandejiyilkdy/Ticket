using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Models.Financial;
using PWMIS.DataMap.Entity;
using PWMIS.Core.Extensions;
using Ticket.Common;
using Ticket.ViewModels.Contract;
using Ticket.Models.Contract;
using Ticket.Models.Custom;

namespace Ticket.BLL.Financial
{
    public class CustomFinancialBLL : BLLBase
    {
        #region 基础方法
        /// <summary>
        /// 获取列表（全部）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<CustomFinancialModel> GetAllModelList()
        {
            CustomFinancialModel model = new CustomFinancialModel();
            OQL q = OQL.From(model)
                .Select()
                .OrderBy(model.ID, "desc")
                .END;
            return q.ToList<CustomFinancialModel>();
        }

        /// <summary>
        /// 获取列表（分页）
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
                CustomName = custom.CustomName,
                CustomTypeName = customType.CustomTypeName,
                ContractNo = contract.ContractNo,
                //ContractAmount = contract.ContractAmount.ToString("N2"),
                //Deductions = contract.Deductions.ToString("N2"),
                //Balance = contract.Balance.ToString("N2"),
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
        public JsonRsp Add(CustomFinancialModel model)
        {
            int returnvalue = Add<CustomFinancialModel>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 查 根据Id获取详情，如果没有则返回空对象
        /// </summary>
        /// <param name="TicketerID"></param>
        /// <returns></returns>
        public CustomFinancialModel GetModelById(int Id)
        {
            return GetModel<CustomFinancialModel>(Id);
        }


        /// <summary>
        /// save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Save(CustomFinancialModel model)
        {
            return Add(model);
        }

        #endregion

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public CustomFinancialModel GetCustomFinancialByCustomId(long customId)
        {
            List<CustomFinancialModel> list = GetAllModelList().FindAll(o => o.CustomId == customId);
            if (list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<CustomFinancialModel> GetList(long customId, int moneyType)
        {
            JsonRsp<CustomFinancialModel> rsp = new JsonRsp<CustomFinancialModel>();
            rsp.data = GetAllModelList().FindAll(o => o.CustomId == customId);
            rsp.success = true;
            return rsp;
        }
    }
}
