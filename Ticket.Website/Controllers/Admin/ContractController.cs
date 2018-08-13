using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;   
using Ticket.Models.Contract;
using Ticket.BLL.Contract;
using Ticket.BLL.Custom; 

namespace Ticket.Website.Controllers.Admin
{

    public class ContractController : BaseController
    {
        protected readonly ContractBLL bll = new ContractBLL();
        protected readonly CustomBLL customBLL = new CustomBLL();
        // 列表页面（分页）
        // GET: /Admin/Contract/PageList
        [HttpGet]
        public ActionResult PageList()
        {
            return View("Index");
        } 

        // 列表方法
        // GET: /Admin/Contract/PageList
        [HttpPost]
        public JsonResult PageList(int page, int limit)
        {
            return Json(bll.GetPageList(page, limit), JsonRequestBehavior.AllowGet);
        }


        // 待添加
        // GET: /Admin/Contract/Add
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.selectTrees = customBLL.GetSelectTrees();
            return View("Show");
        }

        // 添加
        // GET: /Admin/Contract/Add
        [HttpPost]
        public ActionResult Add(ContractModel model)
        {
            model.ID = 0;
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }

        // 待编辑
        // GET: /Admin/Contract/Edit
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            ViewBag.selectTrees = customBLL.GetSelectTrees();
            ViewBag.model = bll.GetModelById(Id);
            return View("Show");
        }

        // 编辑
        // GET: /Admin/Contract/Edit
        [HttpPost]
        public ActionResult Edit(ContractModel model)
        {
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }
                
        // 删除（逻辑删除，状态修改为-1）
        // GET: //Admin/Contract/Delete/Ids
        [HttpPost]
        public ActionResult Delete(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, -1), JsonRequestBehavior.AllowGet);
        }
        // 启用
        // GET: /Admin/Contract/SetEnable
        [HttpPost]
        public ActionResult SetEnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids,1), JsonRequestBehavior.AllowGet);
        }
        // 禁用
        // GET: /Admin/Contract/SetUnable
        [HttpPost]
        public ActionResult SetUnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 0), JsonRequestBehavior.AllowGet);
        }
        // 审核
        // GET: /Admin/Contract/Audit
        [HttpPost]
        public ActionResult Audit(long[] Ids)
        {
            return Json(bll.Audit(Ids, 1), JsonRequestBehavior.AllowGet);
        }

    }
}
