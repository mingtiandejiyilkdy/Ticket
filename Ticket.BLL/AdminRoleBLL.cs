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
    public class AdminRoleBLL : BLLBase
    {

        #region 基础方法
        /// <summary>
        /// 获取列表（全部）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<AdminRoleViewModel> GetAllList()
        {
            JsonRsp<AdminRoleViewModel> rsp = new JsonRsp<AdminRoleViewModel>();
            AdminRole model = new AdminRole();
            OQL q = OQL.From(model)
                .Select()
                .OrderBy(model.ID, "asc")
                .END;
            List<AdminRole> list = q.ToList<AdminRole>();//使用OQL扩展
            rsp.data = list.ConvertAll<AdminRoleViewModel>(o =>
            {
                return new AdminRoleViewModel()
                {
                    ID = o.ID,
                    RoleName = o.RoleName,
                    CreateTime = o.CreateTime,
                };
            }
            );
            rsp.success = true;
            rsp.code = 0;
            return rsp;
        }

        /// <summary>
        /// 获取管理员列表（分页）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<AdminRoleViewModel> GetPageList(int pageIndex, int pageSize)
        {
            JsonRsp<AdminRoleViewModel> rsp = new JsonRsp<AdminRoleViewModel>();

            AdminRole m = new AdminRole();
            OQL q = OQL.From(m)
                .Select(m.ID, m.RoleName, m.Status, m.CreateTime)
                .OrderBy(m.ID, "asc")
                .END;
            //分页
            q.Limit(pageSize, pageIndex, true);
            //q.PageWithAllRecordCount = allCount;
            //List<Employee> list= EntityQuery<Employee>.QueryList(q);
            List<AdminRole> list = q.ToList<AdminRole>();//使用OQL扩展
            rsp.data = list.ConvertAll<AdminRoleViewModel>(o =>
            {
                return new AdminRoleViewModel()
                {
                    ID = o.ID,
                    RoleName = o.RoleName,
                    Status = o.Status,
                    CreateTime = o.CreateTime,
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
        public JsonRsp Add(AdminRole model)
        {
            int returnvalue = Add<AdminRole>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Remove(AdminRole model)
        {
            int returnvalue = EntityQuery<AdminRole>.Instance.Delete(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp Update(AdminRole model)
        {
            int returnvalue = Update<AdminRole>(model);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }

        /// <summary>
        /// 查 根据Id获取详情，如果没有则返回空对象
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public AdminRole GetModelById(long Id)
        {
            return GetModel<AdminRole>(Id);
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
        public JsonRsp Save(AdminRole model)
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
            AdminRole dbAccount = GetModelById(Id);
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
            if (Ids == null)
            {
                return new JsonRsp { success = false, retmsg = "请选择要操作的数据" };
            }
            AdminRole model = new AdminRole();
            model.Status = status;
            model.UpdateId = AdminId;
            model.UpdateUser = AdminName;
            model.UpdateIP = Util.GetLocalIP;
            model.UpdateTime = DateTime.Now;
            OQL q = OQL.From(model)
               .Update(model.Status, model.UpdateId, model.UpdateUser, model.UpdateIP, model.UpdateTime)
                          .Where(cmp => cmp.Comparer(model.ID, "IN", Ids)) //为了安全，不带Where条件是不会全部删除数据的
                       .END;
            int returnvalue = EntityQuery<AdminRole>.Instance.ExecuteOql(q);
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

        #region 获取角色下拉树

        public JsonRsp<TreeSelect> GetRoleTreeSelect(int accountId)
        {
            List<long> roleIds = GetRoles(accountId);
            JsonRsp<TreeSelect> rsp = new JsonRsp<TreeSelect>();
            AdminRole model = new AdminRole();
            OQL q = OQL.From(model)
                .Select()
                .OrderBy(model.ID, "asc")
                .END;
            List<AdminRole> list = q.ToList<AdminRole>();//使用OQL扩展
            List<TreeSelect> allList = list.ConvertAll<TreeSelect>(o =>
            {
                return new TreeSelect()
                {
                    value = o.ID,
                    name = o.RoleName,
                    isChecked = roleIds.Contains(o.ID),
                };
            }
            );
            rsp.data = allList;
            rsp.success = true;
            rsp.code = 0;
            return rsp;
        }

        #endregion

        #region 保存用户角色
        /// <summary>
        /// save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonRsp SaveAccountRole(AdminAccount model, int[] itemIds)
        {
            AdminAccountRole emp = new AdminAccountRole();
            emp.AccountID = model.ID;
            //删除原有的
            OQL q = OQL.From(emp)
                .Delete()
                .Where(emp.AccountID)
                .END;
            EntityQuery<AdminAccountRole>.Instance.ExecuteOql(q);

            List<AdminAccountRole> list = new List<AdminAccountRole>();
            foreach (int id in itemIds)
            {
                list.Add(new AdminAccountRole
                {
                    AccountID = model.ID,
                    RoleID = id,
                });
            }
            int returnvalue = EntityQuery<AdminAccountRole>.Instance.Insert(list);
            return new JsonRsp { success = returnvalue > 0, code = returnvalue };
        }
        #endregion

        #region 获取用户权限菜单
        /// <summary>
        /// 获取用户权限菜单
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonRsp<MenuViewModel> GetMenuJsonList(long accountId, long menuType)
        {
            JsonRsp<MenuViewModel> rsp = new JsonRsp<MenuViewModel>();
            rsp.data = GetMenuListByAccountId(accountId, menuType);
            rsp.success = true;
            rsp.code = 0;
            return rsp;
        }

       
        /// <summary>
        /// 获取用户权限菜单
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="menuType"></param>
        /// <returns></returns>
        public List<MenuViewModel> GetMenuListByAccountId(long accountId, long menuType)
        {
            AdminRole role = new AdminRole();
            AdminAccountRole ar = new AdminAccountRole();
            ar.AccountID = accountId;
            OQL joinQ = OQL.From(role)
              .Join(ar).On(role.ID, ar.RoleID)
              .Select(role.MenuIds)
              .Where(ar.AccountID)
              .OrderBy(role.Sort, "desc")
              .END;
            PWMIS.DataProvider.Data.AdoHelper db = PWMIS.DataProvider.Adapter.MyDB.GetDBHelper();
            EntityContainer ec = new EntityContainer(joinQ, db);
            List<AdminRole> roleList = (List<AdminRole>)ec.MapToList<AdminRole>(() => new AdminRole()
            {
                MenuIds=role.MenuIds,
            }); 
            
            StringBuilder sbMenuIds = new StringBuilder();
            foreach (AdminRole item in roleList)
            {
                sbMenuIds.Append(item.MenuIds);
            }

            //long[] roleArr = Array.ConvertAll(sbMenuIds.ToString().Split(','), long.Parse);


            JsonRsp<MenuViewModel> rsp = new JsonRsp<MenuViewModel>();
            AdminMenu model = new AdminMenu();
            OQL q = new OQL(model);
            if (menuType > 0)
            {
                q.Select()
                    .Where(q.Condition.AND(model.MenuType, "=", menuType).AND(model.Status, ">", -1))
                    .OrderBy(model.Sort, "desc");
            }
            else
            {
                q.Select()
                    .Where(q.Condition.AND(model.Status, ">", -1))
                    .OrderBy(model.Sort, "desc");
            }

            List<AdminMenu> list = q.ToList<AdminMenu>();//使用OQL扩展
            return list.ConvertAll<MenuViewModel>(o =>
            {
                return new MenuViewModel()
                {
                    ID = o.ID,
                    ParentID = o.ParentID,
                    MenuKey = o.MenuKey,
                    MenuName = o.MenuName,
                    MenuUrl = o.MenuUrl,
                    MenuType = o.MenuType, 
                    Sort = o.Sort,
                    Status = o.Status,
                    CreateTime = o.CreateTime,
                };
            }
            );
        }
        #endregion


    }
}
