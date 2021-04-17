using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeIPTVar.Controllers
{
    public class CategoryController : Controller
    {
        FreeIPTVarEntities db = new FreeIPTVarEntities();

        // GET: Category
        public ActionResult Index()
        {
            List<Category> MyList = db.Categories.ToList();
            return View(MyList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Category model = new Category();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
           if(ModelState.IsValid)
            {
                Category OldCategory = db.Categories.Where(x => x.CategoryName == model.CategoryName).FirstOrDefault();

                if(OldCategory == null)
                {
                    db.Categories.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Already Exist ";
                    return View(model);
                }
            }
            return View(model);


        }

        [HttpGet]
        public ActionResult Edit(int CategoryID)
        {
            Category EditRecord = db.Categories.Where(x => x.CategoryID == CategoryID).SingleOrDefault();

            return View(EditRecord);
        }

        [HttpPost]
        public ActionResult Edit(Category model)
        {
            Category oldRecord = db.Categories.Where(x => x.CategoryName == model.CategoryName).FirstOrDefault();

            if (ModelState.IsValid)
            {
                if (oldRecord == null)
                {
                    Category newupdate = db.Categories.Where(x => x.CategoryID == model.CategoryID).SingleOrDefault();

                    newupdate.CategoryName = model.CategoryName;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.errormessage = "Already Exist ";
                    return View(model);
                }

            }



            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int CategoryID)
        {
            //Delete , Remove
            Category DeleteRecoed = db.Categories.Where(x => x.CategoryID == CategoryID).SingleOrDefault();

            if (DeleteRecoed != null)
            {
                db.Categories.Remove(DeleteRecoed);
                db.SaveChanges();
            }

            return RedirectToAction("Index");

        }

    }
}