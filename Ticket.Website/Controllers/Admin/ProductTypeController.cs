using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;  
using Ticket.BLL.Product;
using Ticket.Models.Product;
using Ticket.BLL.Merchant;

namespace Ticket.Website.Controllers.Admin
{

    public class ProductTypeController : BaseController
    {
        protected readonly ProductTypeBLL bll = new ProductTypeBLL();
        protected readonly MerchantTypeBLL merchantTypeBLL = new MerchantTypeBLL();
        // 列表页面 分页
        // GET: /Admin/ProductType/PageList
        [HttpGet]
        public ActionResult PageList()
        {
            return View("Index");
        }
         
        // 列表分页 方法
        // GET: /Admin/ProductType/PageList
        [HttpPost]
        public JsonResult PageList(string searchTxt, int page, int limit)
        {
            return Json(bll.GetPageList(searchTxt, page, limit), JsonRequestBehavior.AllowGet);
        }

        // 待添加
        // GET: /Admin/ProductType/Add
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.selectTrees = merchantTypeBLL.GetSelectTrees();
            return View("Show");
        }

        // 添加
        // GET: /Admin/ProductType/Add
        [HttpPost]
        public ActionResult Add(ProductTypeModel model)
        {
            model.ID = 0;
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }

        // 待编辑
        // GET: /Admin/ProductType/Edit
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            ViewBag.selectTrees = merchantTypeBLL.GetSelectTrees();
            ViewBag.model = bll.GetModelById(Id);
            return View("Show");
        }

        // 编辑
        // GET: /Admin/ProductType/Edit
        [HttpPost]
        public ActionResult Edit(ProductTypeModel model)
        {
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        } 

        //
        // GET: //Admin/ProductType/DeleteById/Ids
        [HttpPost]
        public ActionResult DeleteById(long[] Ids)
        {
            return Json(bll.DeleteById(Ids), JsonRequestBehavior.AllowGet);
        } 

        // 删除（逻辑删除，状态修改为-1）
        // GET: /Admin/ProductType/Delete/Ids
        [HttpPost]
        public ActionResult Delete(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, -1), JsonRequestBehavior.AllowGet);
        }

        // 启用
        // GET: /Admin/ProductType/SetEnable
        [HttpPost]
        public ActionResult SetEnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 1), JsonRequestBehavior.AllowGet);
        }

        //禁用
        // GET: /Admin/ProductType/SetUnable
        [HttpPost]
        public ActionResult SetUnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 0), JsonRequestBehavior.AllowGet);
        }  

    }
}
