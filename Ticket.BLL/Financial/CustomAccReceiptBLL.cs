using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Models.Financial;
using PWMIS.DataMap.Entity;
using PWMIS.Core.Extensions;
using Ticket.Common;
using Ticket.ViewModels.Financial;
using Ticket.Models.Financial;
using Ticket.Models.Custom; 

namespace Ticket.BLL.Financial
{
    public class CustomAccReceiptBLL : BLLBase
    {
        #region 基础方法  

        /// <summary>
        /// 获取列表（分页）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<CustomAccReceiptViewModel> GetPageList(int pageIndex, int pageSize)
        {
            JsonRsp<CustomAccReceiptViewModel> rsp = new JsonRsp<CustomAccReceiptViewModel>(); 
            CustomAccReceiptModel CustomAccReceipt = new CustomAccReceiptModel();
            CustomModel custom = new CustomModel(); 

            //Select 方法不指定具体要选择的实体类属性，可以推迟到EntityContainer类的MapToList 方法上指定
            OQL joinQ = OQL.From(CustomAccReceipt)
                .Join(custom).On(CustomAccReceipt.CustomId, custom.ID) 
                .Select()
                .OrderBy(CustomAccReceipt.CreateTime, "asc")
                .END;

            joinQ.Limit(pageSize, pageIndex, true);

            PWMIS.DataProvider.Data.AdoHelper db = PWMIS.DataProvider.Adapter.MyDB.GetDBHelper();
            EntityContainer ec = new EntityContainer(joinQ, db);

            rsp.data = (List<CustomAccReceiptViewModel>)ec.MapToList<CustomAccReceiptViewModel>(() => new CustomAccReceiptViewModel()
            {
                ID = CustomAccReceipt.ID,
                CustomId = CustomAccReceipt.CustomId,
                CustomName = custom.CustomName,
                ChargeCardNo = CustomAccReceipt.ChargeCardNo,
                CurrentAmount = CustomAccReceipt.CurrentAmount,
                Remark = CustomAccReceipt.Remark,
                Status = CustomAccReceipt.Status,
                CreateId = CustomAccReceipt.CreateId,
                CreateUser = CustomAccReceipt.CreateUser,
                CreateIP = CustomAccReceipt.CreateIP,
                CreateTime = CustomAccReceipt.CreateTime,
            });
            rsp.success = true;
            rsp.code = 0;
            rsp.count = joinQ.PageWithAllRecordCount;
            return rsp;
        }
        

        /// <summary>
        /// 查 根据Id获取详情，如果没有则返回空对象
        /// </summary>
        /// <param name="TicketerID"></param>
        /// <returns></returns>
        public CustomAccReceiptModel GetModelById(int Id)
        {
            return GetModel<CustomAccReceiptModel>(Id);
        } 
        #endregion
         
    }
}
