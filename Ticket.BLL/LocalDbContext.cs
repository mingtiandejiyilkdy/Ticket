using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PWMIS.Core.Extensions;
using Ticket.Models;
using Ticket.Models.Tenant;
using Ticket.Models.Custom;
using Ticket.Models.Ticket;
using Ticket.Models.Contract;
using Ticket.Models.Merchant;
using Ticket.Models.Product;
using Ticket.Models.Financial;
using Ticket.Models.Bank; 

namespace Ticket.BLL
{
    /// <summary>
    /// 用来测试的本地SqlServer 数据库上下文类
    /// </summary>
    public class LocalDbContext : DbContext
    {
        public LocalDbContext()
            : base("local")
        {
            //local 是连接字符串名字
        }

        #region 父类抽象方法的实现

        protected override bool CheckAllTableExists()
        {
            //创建租户表
            CheckTableExists<TenantModel>();
            //创建用户表
            CheckTableExists<AdminAccount>();
            //创建角色表
            CheckTableExists<AdminRole>();
            //创建菜单表
            CheckTableExists<AdminMenu>();
            //创建菜单操作表
            CheckTableExists<AdminAction>();
            //创建用户角色表
            CheckTableExists<AdminAccountRole>();
            //创建客户类型表
            CheckTableExists<CustomTypeModel>();
            //创建客户表
            CheckTableExists<CustomModel>();
            //创建票据类型表
            CheckTableExists<TicketTypeModel>();
            //创建票据批次表
            CheckTableExists<TicketBatchModel>();
            //创建票据信息表
            CheckTableExists<TicketInfo>();
            //创建合同信息表
            CheckTableExists<ContractModel>();
            //创建商家类型表
            CheckTableExists<MerchantTypeModel>();
            //创建商家表
            CheckTableExists<MerchantModel>();
            //创建产品类别表
            CheckTableExists<ProductTypeModel>();
            //创建产品类别表
            CheckTableExists<CustomFinancialModel>();
            //创建产品类别表
            CheckTableExists<CustomFinancialDetailModel>();
            //创建银行表
            CheckTableExists<BankModel>();
            //创建银行账号表
            CheckTableExists<BankAccountModel>();
            //创建客户应收款表
            CheckTableExists<CustomAccReceiptModel>();
            //创建客户实收款表
            CheckTableExists<CustomReceiptedModel>(); 
            return true;
        }

        #endregion
    }
}
