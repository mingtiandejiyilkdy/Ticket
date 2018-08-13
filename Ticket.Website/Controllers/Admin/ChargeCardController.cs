using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket.BLL.Contract;
using Ticket.BLL.Ticket;
using Ticket.ViewModels.Contract;
using Ticket.Models.Contract;
using Ticket.BLL.Financial;

namespace Ticket.Website.Controllers.Admin
{
    public class ChargeCardController : BaseController
    {
        protected readonly ChargeCardBLL bll = new ChargeCardBLL();
        protected readonly CustomFinancialBLL customFinancialBLL = new CustomFinancialBLL();
        protected readonly TicketTypeBLL ticketTypeBLL = new TicketTypeBLL();
        protected readonly ContractBLL contractBLL = new ContractBLL();
        protected readonly TicketBatchBLL ticketBatchBLL = new TicketBatchBLL();

        // GET: /Admin/ChargeCard/PageList
        [HttpGet]
        public ActionResult PageList()
        {
            return View("Index");
        }

        //
        // GET: /Admin/ChargeCard/PageList
        [HttpPost]
        public JsonResult PageList(int page, int limit)
        {
            return Json(bll.GetPageList(0, 0, false), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Admin/ChargeCard/AddFromContract
        public ActionResult AddFromContract(int contractId)
        {
            ContractViewModel contractViewModel = contractBLL.GetViewModelById(contractId);
            ViewBag.selectTrees = ticketTypeBLL.GetSelectTrees();
            ViewBag.ticketBatchs = ticketBatchBLL.GetAllList().data;
            ViewBag.contractViewModel = contractViewModel;
            return View();
        }

        //
        // GET: //Admin/ChargeCard/Add
        [HttpPost]
        public ActionResult Save(ChargeCardsModel model)
        {
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: //Admin/ChargeCard/Add
        [HttpPost]
        public ActionResult GetBlance(long customId, int moneyType)
        {
            return Json(customFinancialBLL.GetList(customId, moneyType), JsonRequestBehavior.AllowGet);
        }
    }
}
