using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Common;
using Ticket.ViewModels;
using Ticket.Models;
using PWMIS.DataMap.Entity;
using PWMIS.Core.Extensions; 

namespace Ticket.BLL
{
    public class AdminActionBLL : BLLBase
    {

        #region 基础方法
        /// <summary>
        /// 获取列表（全部）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<AdminAction> GetAllModelList()
        { 
            AdminAction model = new AdminAction();
            OQL q = OQL.From(model)
                .Select()
                .OrderBy(model.ID, "asc")
                .END;
            return q.ToList<AdminAction>();//使用OQL扩展            
        }

        /// <summary>
        /// 获取管理员列表（分页）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<AdminActionViewModel> GetPageList(int pageIndex, int pageSize)
        {
            JsonRsp<AdminActionViewModel> rsp = new JsonRsp<AdminActionViewModel>();

            AdminAction action = new AdminAction();
            AdminMenu menu = new AdminMenu();
            //Select 方法不指定具体要选择的实体类属性，可以推迟到EntityContainer类的MapToList 方法上指定
            OQL joinQ = OQL.From(action)
                .Join(menu).On(action.MenuId, menu.ID)
                .Select(action.ID,menu.MenuName,action.ActionKey,action.ActionName,action.Status,action.CreateTime)
                .OrderBy(action.Sort, "desc")
                .END;

            joinQ.Limit(pageSize, pageIndex, true);

            PWMIS.DataProvider.Data.AdoHelper db = PWMIS.DataProvider.Adapter.MyDB.GetDBHelper();
            EntityContainer ec = new EntityContainer(joinQ, db);

            rsp.data = (List<AdminActionViewModel>)ec.MapToList<AdminActionViewModel>(() => new AdminActionViewModel()
            {
                ID = action.ID,
                MenuId=menu.ID,
                MenuName=menu.MenuName,
                ActionKey=action.ActionKey,
                ActionName=action.ActionName,
                Status=action.Status,
                CreateTime=action.CreateTime, 
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
        public JsonRsp Add(AdminAction model)
        {
            int returnvalue = Add<AdminAction>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Remove(AdminAction model)
        {
            int returnvalue = EntityQuery<AdminAction>.Instance.Delete(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Update(AdminAction model)
        {
            int returnvalue = Update<AdminAction>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }

        /// <summary>
        /// 查 根据Id获取详情，如果没有则返回空对象
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public AdminAction GetModelById(long Id)
        {
            return GetModel<AdminAction>(Id);
        }
        /// <summary>
        /// save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp DeleteById(int Id)
        {
            return Remove(GetModelById(Id));
        }


        /// <summary>
        /// save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Save(AdminAction model)
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
        public JsonRsp SetStatus(long Id, int status)
        {
            AdminAction dbAccount = GetModelById(Id);
            dbAccount.Status = status;
            return Update(dbAccount);
        } 

         /// <summary>
        ///  启用/禁用
        /// </summary>
        /// <param name="accountStatus"></param>
        /// <returns></returns>
        public JsonRsp SetStatus(long[] Ids, int status)
        {
            List<MenuViewModel> list = new AdminRoleBLL().GetMenuListByAccountId(1, 0).FindAll(o => o.MenuType == 1 && o.ParentID > 0);
            int index = 99;
            foreach (var item in list)
            {
                AdminMenu menu = new AdminMenu();
                menu.ParentID = item.ID;
                menu.MenuKey = "Add";
                menu.MenuName = "添加";
                menu.MenuType = 2;
                menu.MenuUrl = "#";
                menu.Remark = item.MenuName + menu.MenuName;
                menu.Sort = 99;
                Add<AdminMenu>(menu);

                menu = new AdminMenu();
                menu.ParentID = item.ID;
                menu.MenuKey = "Edit";
                menu.MenuName = "修改";
                menu.MenuType = 2;
                menu.MenuUrl = "#";
                menu.Remark = item.MenuName + menu.MenuName;
                menu.Sort = 98;
                Add<AdminMenu>(menu);

                menu = new AdminMenu();
                menu.ParentID = item.ID;
                menu.MenuKey = "Delete";
                menu.MenuName = "删除";
                menu.MenuType = 2;
                menu.MenuUrl = "#";
                menu.Remark = item.MenuName + menu.MenuName;
                menu.Sort = 97;
                Add<AdminMenu>(menu);

                menu = new AdminMenu();
                menu.ParentID = item.ID;
                menu.MenuKey = "SetEnable";
                menu.MenuName = "启用";
                menu.MenuType = 2;
                menu.MenuUrl = "#";
                menu.Remark = item.MenuName + menu.MenuName;
                menu.Sort = 96;
                Add<AdminMenu>(menu);

                menu = new AdminMenu();
                menu.ParentID = item.ID;
                menu.MenuKey = "SetUnable";
                menu.MenuName = "禁用";
                menu.MenuType = 2;
                menu.MenuUrl = "#";
                menu.Remark = item.MenuName + menu.MenuName;
                menu.Sort = 95;
                Add<AdminMenu>(menu);


            }

            if (Ids == null)
            {
                return new JsonRsp { success = false, retmsg = "请选择要操作的数据" };
            }
            AdminAction model = new AdminAction();
            model.Status = status;
            model.UpdateId = AdminId;
            model.UpdateUser = AdminName;
            model.UpdateIP = Util.GetLocalIP;
            model.UpdateTime = DateTime.Now;
            OQL q = OQL.From(model)
               .Update(model.Status, model.UpdateId, model.UpdateUser, model.UpdateIP, model.UpdateTime)
                          .Where(cmp => cmp.Comparer(model.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的
                       .END;
            int returnvalue = EntityQuery<AdminAction>.Instance.ExecuteOql(q);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        #endregion


        /// <summary>
        /// 获取用户角色集合
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<AdminAccountRole> GetAccountRoles(int accountId)
        {
            AdminAccountRole model = new AdminAccountRole();
            model.AccountID = accountId;
            OQL q = OQL.From(model)
                .Select()
                .Where(model.AccountID)
                .OrderBy(model.ID, "asc")
                .END;
            return q.ToList<AdminAccountRole>();//使用OQL扩展 
        }
        /// <summary>
        /// 获取指定用户的角色Id集合
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<long> GetRoles(int accountId)
        {
            List<AdminAccountRole> list = GetAccountRoles(accountId);
            List<long> roleIds = new List<long>();
            foreach (AdminAccountRole role in list)
            {
                roleIds.Add(role.RoleID);
            }
            return roleIds;
        } 
         
    }
}
