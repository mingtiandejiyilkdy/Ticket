using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ticket.Enum
{
   public class BaseEnum
    {

        /// <summary>
        /// 过期、激活、通卡通用状态
        /// </summary>
        public enum WhetherEnum
        {
            否 = 0,
            是 = 1
        }

       /// <summary>
       /// 凭据类型、院线、产品、客户类型、银行账号状态
       /// </summary>
       public enum CredentialTypeEnum
       {
           启用 = 0,
           禁用 = 1,
           删除 = 9
       }

       /// <summary>
       /// 凭据状态
       /// </summary>
       public enum CredentialEnum
       {
           未交付 = 0,
           正常 = 1,
           锁定 = 2,
           删除 = 9
       }

       /// <summary>
       /// 影院状态
       /// </summary>
       public enum CinemaEnum
       {
           营业 = 0,
           停业 = 1,
           关闭 = 2,
           删除 = 9
       }

       /// <summary>
       /// 客户状态
       /// </summary>
       public enum CustomerEnum
       {
           正常 = 0,
           冻结 = 1,
           解冻 = 2,
           删除 = 9
       }

       /// <summary>
       /// 协议类型
       /// </summary>
       public enum ProtocolTypeEnum
       {
           待确认 = 0,
           已确认 = 1,
           已取消 = 2,
           删除 = 9
       }


       /// <summary>
       /// 款项类型
       /// </summary>
       public enum MoneyTypeEnum
       {
           初始化 = 0,
           应收 = 1,
           赠送 = 2,
           置换 = 3
       }


       /// <summary>
       /// 消费级别
       /// </summary>
       public enum ConsumptionlevelEnum
       {
           初始化 = 0 ,
           一级 = 1,
           二级 = 2,
           三级 = 3
       }

       /// <summary>
       /// 交易订单类型
       /// </summary>
       public enum OrderEnum
       {
           未支付 = 0,
           已支付 = 1,
           未出票 = 2,
           已出票 = 3,
           已完成 = 4,
           已取消 = 5,
           删除 = 9

       }

       /// <summary>
       /// 交易类型
       /// </summary>
       public enum OrderTypeEnum
       {
           线下购票 = 0,
           在线购票 = 1,
           在线充值 = 2,
           在线消费 = 3
       }




       /// <summary>
       /// 结算状态
       /// </summary>
       public enum SettlementEnum
       {
           未结算 = 0,
           已结算 = 1
       }
       

     

 
       
    }
}
