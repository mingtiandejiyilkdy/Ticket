using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Common;
using PWMIS.DataMap.Entity;
using PWMIS.Core.Extensions;
using Ticket.Models.Product;
using Ticket.ViewModels.Product;
using Ticket.Models.Merchant;

namespace Ticket.BLL.Product
{
    public class ProductTypeBLL : BLLBase
    {
        #region 基础方法
        /// <summary>
        /// 获取管理员列表（全部）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<ProductTypeModel> GetAllModelList()
        {
            JsonRsp<ProductTypeViewModel> rsp = new JsonRsp<ProductTypeViewModel>();
            ProductTypeModel model = new ProductTypeModel();
            model.TenantId = TenantId;
            OQL q = OQL.From(model)
                .Select()
                .Where(model.TenantId)
                .OrderBy(model.Sort, "desc")
                .END;
            return q.ToList<ProductTypeModel>();
        }

        OQLCompare CreateCondition(OQLCompare cmp, ProductTypeModel ProductType)
        {
            OQLCompare cmpResult = null;
            if (ProductType.ProductTypeName != "")
                cmpResult = cmp.Comparer(ProductType.ProductTypeName, OQLCompare.CompareType.Like, "%" + ProductType.ProductTypeName + "%");
            return cmpResult;
        }

        /// <summary>
        /// 获取管理员列表（分页）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<ProductTypeViewModel> GetPageList(string searchTxt, int pageIndex, int pageSize)
        {
            JsonRsp<ProductTypeViewModel> rsp = new JsonRsp<ProductTypeViewModel>();

            ProductTypeModel pt = new ProductTypeModel();
            MerchantTypeModel mt = new MerchantTypeModel();
            OQLCompareFunc<ProductTypeModel> cmpFun = (cmp, u) =>
            {
                OQLCompare cmpResult = null;
                //and 条件
                cmpResult = cmpResult & cmp.Comparer(pt.ProductTypeName, OQLCompare.CompareType.Like, "%" + searchTxt + "%");
                //or
                cmpResult = cmpResult | cmp.Comparer(pt.TenantId, OQLCompare.CompareType.Equal, TenantId);
                return cmpResult;
            };
            //Select 方法不指定具体要选择的实体类属性，可以推迟到EntityContainer类的MapToList 方法上指定
            OQL joinQ = OQL.From(pt)
                .Join(mt).On(pt.MerchantTypeId,mt.ID)
                .Select(pt.ID,pt.ProductTypeName,mt.MerchantTypeName,pt.Status,pt.CreateTime)
                .Where(cmpFun)
                .OrderBy(pt.Sort, "desc")
                .END;

            joinQ.Limit(pageSize, pageIndex, true);
            PWMIS.DataProvider.Data.AdoHelper db = PWMIS.DataProvider.Adapter.MyDB.GetDBHelper();
            EntityContainer ec = new EntityContainer(joinQ, db);
            rsp.data = (List<ProductTypeViewModel>)ec.MapToList<ProductTypeViewModel>(() => new ProductTypeViewModel()
            {
                ID = pt.ID,
                ProductTypeName = pt.ProductTypeName,
                MerchantTypeId = mt.ID,
                MerchantTypeName = mt.MerchantTypeName,
                CreateId = pt.CreateId,
                CreateUser = pt.CreateUser,
                CreateIP = pt.CreateIP,
                CreateTime = pt.CreateTime,
                Status = pt.Status,
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
        public JsonRsp Add(ProductTypeModel model)
        {
            int returnvalue = Add<ProductTypeModel>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Remove(ProductTypeModel model)
        {
            int returnvalue = EntityQuery<ProductTypeModel>.Instance.Delete(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Update(ProductTypeModel model)
        {
            int returnvalue = Update<ProductTypeModel>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }

        /// <summary>
        /// 查 根据Id获取详情，如果没有则返回空对象
        /// </summary>
        /// <param name="ProductTypeerID"></param>
        /// <returns></returns>
        public ProductTypeModel GetModelById(int Id)
        {
            return GetModel<ProductTypeModel>(Id);
        }
        /// <summary>
        /// save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp DeleteById(long[] Ids)
        {
            //删除 测试数据-----------------------------------------------------
            ProductTypeModel user = new ProductTypeModel();

            OQL deleteQ = OQL.From(user)
                            .Delete()
                            .Where(cmp => cmp.Comparer(user.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的
                         .END;
            int returnvalue = EntityQuery<ProductTypeModel>.Instance.ExecuteOql(deleteQ);

            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }


        /// <summary>
        /// save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Save(ProductTypeModel model)
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
            ProductTypeModel model = new ProductTypeModel(); 
            OQL q = OQL.From(model)
               .Update(model.Status, model.UpdateId, model.UpdateUser, model.UpdateIP, model.UpdateIP)
                          .Where(cmp => cmp.Comparer(model.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的
                       .END;
            int returnvalue = EntityQuery<ProductTypeModel>.Instance.ExecuteOql(q);
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
        public JsonRsp<ProductTypeViewModel> GetAllList()
        {
            JsonRsp<ProductTypeViewModel> rsp = new JsonRsp<ProductTypeViewModel>();

            rsp.data = GetAllModelList().ConvertAll<ProductTypeViewModel>(o =>
            {
                return new ProductTypeViewModel()
                {
                    ID = o.ID, 
                    ProductTypeName = o.ProductTypeName,
                    MerchantTypeId = o.MerchantTypeId,                    
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
                    name = item.ProductTypeName,
                    value = item.ID,
                });
            }
            return treeSelects;
        }
        #endregion

        #endregion
    }
}
