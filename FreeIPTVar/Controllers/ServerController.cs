using FreeIPTVar.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeIPTVar.Controllers
{
    public class ServerController : Controller
    {
        FreeIPTVarEntities db = new FreeIPTVarEntities();

        // GET: Server
        public ActionResult Index()
        {
            //Get Data
            List<Server> items = db.Servers.ToList();

            //Return Data To View
            return View(items);
        }

        [HttpGet]//Create Server
        public ActionResult Create()
        {
            ServerModel model = new ServerModel();
            model.Categories = db.Categories.ToList();

            return View(model);
        }

        [HttpPost]//Create Item
        public ActionResult Create(ServerModel model, HttpPostedFileBase ServerImage)
        {
            if (ModelState.IsValid)
            {
                string Image_Name = model.ServerImage;
                if (ServerImage != null)
                {
                    //Save Img on project
                    Image_Name = ServerImage.FileName;
                    string File_Name_WithFullPath = Path.Combine(Server.MapPath("~/Content/img"), Image_Name);
                    ServerImage.SaveAs(File_Name_WithFullPath);
                }
                else
                {
                    
                    Image_Name = "no photo";
                    
                }

                
                

                //Insert Data
                Server NewItem = new Server();
                NewItem.ServerTitle = model.ServerTitle;
                NewItem.ServerDescription = model.ServerDescription;
                NewItem.ServerImage = Image_Name;
                NewItem.CategoryID = model.CategoryID;

                if (model.ServerLink == null)
                {
                    NewItem.ServerLink = "null";
                }
                else
                {
                    NewItem.ServerLink = model.ServerLink;
                }
                
                db.Servers.Add(NewItem);
                db.SaveChanges();
                

                return RedirectToAction("Index");

            }


            model.Categories = db.Categories.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int ServerID)
        {
            ServerModel model = new ServerModel();

            Server SelectedItem = db.Servers.Where(x => x.ServerID == ServerID).FirstOrDefault();

            model.ServerID = SelectedItem.ServerID;
            model.ServerTitle = SelectedItem.ServerTitle;
            model.CategoryID = SelectedItem.CategoryID;
            model.ServerDescription = SelectedItem.ServerDescription;
            model.ServerImage = SelectedItem.ServerImage;
            model.ServerLink = SelectedItem.ServerLink;

            model.Categories = db.Categories.ToList();

            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(ServerModel model, HttpPostedFileBase ServerImage)
        {

            if (ModelState.IsValid)
            {
                string Image_Name = model.ServerImage;
                if (ServerImage != null)
                {
                    //Save Img on project
                    Image_Name = ServerImage.FileName;
                    string File_Name_WithFullPath = Path.Combine(Server.MapPath("~/Content/img"), Image_Name);
                    ServerImage.SaveAs(File_Name_WithFullPath);
                }


                //Edit
                Server DBRecord = db.Servers.Where(x => x.ServerID == model.ServerID).SingleOrDefault();//Get record from DB

                DBRecord.ServerTitle = model.ServerTitle;
                DBRecord.ServerDescription = model.ServerDescription;
                DBRecord.ServerImage = Image_Name;
                DBRecord.CategoryID = model.CategoryID;
                if (model.ServerLink == null)
                {
                    DBRecord.ServerLink = "null";
                }
                else
                {
                    DBRecord.ServerLink = model.ServerLink;
                }
                
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            model.Categories = db.Categories.ToList();
            return View(model);
        }

        public ActionResult Delete(int ServerID)
        {

            //Delete , Remove
            Server DeleteRecoed = db.Servers.Where(x => x.ServerID == ServerID).SingleOrDefault();

            if (DeleteRecoed != null)
            {
                db.Servers.Remove(DeleteRecoed);
                db.SaveChanges();
            }

            return RedirectToAction("Index");


        }

    }
    }