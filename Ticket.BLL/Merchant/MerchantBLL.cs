using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel; 
using PWMIS.DataMap.Entity; 
using PWMIS.Core.Extensions;
using Ticket.Common;
using Ticket.ViewModels.Merchant;
using Ticket.Models.Merchant;

namespace Ticket.BLL.Merchant
{
    public class MerchantBLL : BLLBase
    {
        #region 基础方法
        /// <summary>
        /// 获取管理员列表（全部）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<MerchantModel> GetAllModelList()
        {
            JsonRsp<MerchantViewModel> rsp = new JsonRsp<MerchantViewModel>();
            MerchantModel model = new MerchantModel(); 
            OQL q = OQL.From(model)
                .Select() 
                .Where(model.TenantId==TenantId)
                .OrderBy(model.Sort, "desc")
                .END;
            return q.ToList<MerchantModel>();  
        }

        /// <summary>
        /// 获取管理员列表（分页）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<MerchantViewModel> GetPageList(int pageIndex, int pageSize)
        {
            JsonRsp<MerchantViewModel> rsp = new JsonRsp<MerchantViewModel>();


            MerchantModel model = new MerchantModel();
            MerchantTypeModel MerchantType = new MerchantTypeModel();

            model.TenantId = TenantId;
            //Select 方法不指定具体要选择的实体类属性，可以推迟到EntityContainer类的MapToList 方法上指定
            OQL joinQ = OQL.From(model)
                .Join(MerchantType).On(model.MerchantTypeId, MerchantType.ID) 
                .Select()
                .OrderBy(model.Sort, "desc")
                .END;

            joinQ.Limit(pageSize, pageIndex, true);

            PWMIS.DataProvider.Data.AdoHelper db = PWMIS.DataProvider.Adapter.MyDB.GetDBHelper();
            EntityContainer ec = new EntityContainer(joinQ, db);

            rsp.data = (List<MerchantViewModel>)ec.MapToList<MerchantViewModel>(() => new MerchantViewModel()
            {
                ID = model.ID,
                MerchantTypeId = model.MerchantTypeId,
                MerchantTypeName = MerchantType.MerchantTypeName,
                MerchantName = model.MerchantName,
                LinkPhone = model.LinkPhone,
                LinkName = model.LinkName,
                LinkMobile = model.LinkMobile,
                MerchantArea = model.MerchantArea,
                MerchantAddress = model.MerchantAddress,
                CreateId = model.CreateId,
                CreateUser = model.CreateUser,
                CreateIP = model.CreateIP,
                CreateTime = model.CreateTime,
                Sort = model.Sort,
                Status = model.Status,
                UpdateBy = model.UpdateUser,
                UpdateIP = model.UpdateIP,
                UpdateTime = model.UpdateTime,
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
        public JsonRsp Add(MerchantModel model)
        {
            int returnvalue = EntityQuery<MerchantModel>.Instance.Insert(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Remove(MerchantModel model)
        { 
            int returnvalue = EntityQuery<MerchantModel>.Instance.Delete(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Update(MerchantModel model)
        {
            int returnvalue = EntityQuery<MerchantModel>.Instance.Update(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }

        /// <summary>
        /// 查 根据Id获取详情，如果没有则返回空对象
        /// </summary>
        /// <param name="MerchanterID"></param>
        /// <returns></returns>
        public MerchantModel GetModelById(int Id)
        {
            MerchantModel model = new MerchantModel() { TenantId=TenantId , ID = Id };
            if (EntityQuery<MerchantModel>.Fill(model))
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
            MerchantModel user = new MerchantModel(); 
             
            OQL deleteQ = OQL.From(user)
                            .Delete()
                            .Where(cmp => cmp.Comparer(user.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的
                         .END;
            int returnvalue = EntityQuery<MerchantModel>.Instance.ExecuteOql(deleteQ);

            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }


        /// <summary>
        /// save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Save(MerchantModel model)
        {
            model.TenantId = TenantId;
            if (model.ID == 0)
            {
                model.CreateId = AdminId;
                model.CreateUser = AdminName;
                model.CreateIP = Util.GetLocalIP;
                model.CreateTime = DateTime.Now; 
                return Add(model);
            }
            else
            {
                if (model.TenantId != TenantId)
                {
                    return new JsonRsp { success = false, code = -1, retmsg = "数据验证失败" };
                }
                model.UpdateId = AdminId;
                model.UpdateUser = AdminName;
                model.UpdateIP = Util.GetLocalIP;
                model.UpdateTime = DateTime.Now; 
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
            MerchantModel model = new MerchantModel();
            model.Status = status;
            model.UpdateId = AdminId;
            model.UpdateUser = AdminName;
            model.UpdateIP = Util.GetLocalIP;
            model.UpdateTime = DateTime.Now;
            OQL q = OQL.From(model)
               .Update(model.Status, model.UpdateId, model.UpdateUser, model.UpdateIP, model.UpdateIP)
                          .Where(cmp => cmp.Comparer(model.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的
                       .END;
            int returnvalue = EntityQuery<MerchantModel>.Instance.ExecuteOql(q);
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
        public JsonRsp<MerchantViewModel> GetAllList()
        {
            JsonRsp<MerchantViewModel> rsp = new JsonRsp<MerchantViewModel>();

            rsp.data = GetAllModelList().ConvertAll<MerchantViewModel>(o =>
            {
                return new MerchantViewModel()
                {
                    ID = o.ID,
                    MerchantTypeId = o.MerchantTypeId,
                    MerchantName = o.MerchantName,
                    LinkPhone = o.LinkPhone,
                    LinkName = o.LinkName,
                    LinkMobile = o.LinkMobile,
                    MerchantArea = o.MerchantArea,
                    MerchantAddress = o.MerchantAddress,
                    CreateId = o.CreateId,
                    CreateUser = o.CreateUser,
                    CreateIP = o.CreateIP,
                    CreateTime = o.CreateTime,
                    Sort = o.Sort,
                    Status = o.Status,
                    UpdateBy = o.UpdateUser,
                    UpdateIP = o.UpdateIP,
                    UpdateTime = o.UpdateTime ,
                };
            }
            );
            rsp.success = true;
            rsp.code = 0;
            return rsp;
        }
        #endregion

        #region  获取客户SelectTree
        public List<TreeSelect> GetSelectTrees() {
            List<TreeSelect> treeSelects = new List<TreeSelect>();
            foreach (var item in GetAllModelList()) {
                treeSelects.Add(new TreeSelect { 
                     id=item.ID,
                      name=item.MerchantName,
                        value=item.ID,
                });
            }
            return treeSelects;
        }
        #endregion

        #endregion
    }
}
