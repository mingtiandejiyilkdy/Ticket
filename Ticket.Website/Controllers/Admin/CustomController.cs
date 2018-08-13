using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;  
using Ticket.BLL.Custom; 
using Ticket.Models.Custom;

namespace Ticket.Website.Controllers.Admin
{

    public class CustomController : BaseController
    {
        protected readonly CustomBLL bll = new CustomBLL();
        protected readonly CustomTypeBLL customTypeBLL = new CustomTypeBLL();
        // 列表页面 分页
        // GET: /Admin/Custom/PageList
        [HttpGet]
        public ActionResult PageList()
        {
            return View("Index");
        }
         
        // 列表分页 方法
        // GET: /Admin/Custom/PageList
        [HttpPost]
        public JsonResult PageList(string name, int page, int limit)
        {
            return Json(bll.GetPageList(name,page, limit), JsonRequestBehavior.AllowGet);
        }

        // 待添加
        // GET: /Admin/Custom/Add
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.selectTrees = customTypeBLL.GetSelectTrees();
            return View("Show");
        }

        // 添加
        // GET: /Admin/Custom/Add
        [HttpPost]
        public ActionResult Add(CustomModel model)
        {
            model.ID = 0;
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }

        // 待编辑
        // GET: /Admin/Custom/Edit
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            ViewBag.selectTrees = customTypeBLL.GetSelectTrees();
            ViewBag.model = bll.GetModelById(Id);
            return View("Show");
        }

        // 编辑
        // GET: /Admin/Custom/Edit
        [HttpPost]
        public ActionResult Edit(CustomModel model)
        {
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        } 

        //
        // GET: //Admin/Custom/DeleteById/Ids
        [HttpPost]
        public ActionResult DeleteById(long[] Ids)
        {
            return Json(bll.DeleteById(Ids), JsonRequestBehavior.AllowGet);
        } 

        // 删除（逻辑删除，状态修改为-1）
        // GET: /Admin/Custom/Delete/Ids
        [HttpPost]
        public ActionResult Delete(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, -1), JsonRequestBehavior.AllowGet);
        }

        // 启用
        // GET: /Admin/Custom/SetEnable
        [HttpPost]
        public ActionResult SetEnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 1), JsonRequestBehavior.AllowGet);
        }

        //禁用
        // GET: /Admin/Custom/SetUnable
        [HttpPost]
        public ActionResult SetUnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 0), JsonRequestBehavior.AllowGet);
        }  

    }
}
