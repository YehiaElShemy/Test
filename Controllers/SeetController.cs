using projectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projectMVC.viewModel;
using projectMVC.enums;
using System.Data.Entity;

namespace projectMVC.Controllers
{
    public class SeetController : Controller
    {
        private projectDbContext con = new projectDbContext();
        private Admin owner;
        // GET: Seet
        public ActionResult Index()
        {
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            var data=  con.Seets.Where(s => s.Code == owner.Code );
            List<SeetsViewModel> seets = new List<SeetsViewModel>();
            foreach (var item in data)
            {
                if (item.SeetType == SeetType.SingleSeet)
                {
                    seets.Add(new SeetsViewModel
                    {

                        ID = item.Id,
                        
                        Number = item.Number,
                        SeetType = item.SeetType,
                        Cost = item.Cost
                  

                    }) ;
                }
                else
                {
                    seets.Add(new SeetsViewModel
                    {

                        ID = item.Id,
                        Number = item.Number,
                        SeetType = item.SeetType,
                        Cost = item.Cost,
                   

                    });
                }
            }
            ViewBag.type = owner.BussinesType;

            return View(seets);
        }

        public ActionResult Create()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Create(SeetsViewModel SeetForm)
        {
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            if(!ModelState.IsValid)
            {
                return Content(" not valied");
            }
            if(SeetForm.SeetType == SeetType.SingleSeet)
            {
                Seet seet = new Seet
                {
                    Admin_Id = owner.Id,
                    Code = owner.Code,
                    SeetType = SeetForm.SeetType,
                    Number = SeetForm.Number,
                    Cost = SeetForm.Cost,
                    IsEmpty = false,
                };
                con.Seets.Add(seet);
                con.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Seet seet = new Seet
                {
                    Admin_Id = owner.Id,
                    Code = owner.Code,
                    SeetType = SeetType.MultiSeet,
                    Number = SeetForm.Number,
                    Cost = SeetForm.Cost
                };
                con.Seets.Add(seet);
                con.SaveChanges();
             
                return RedirectToAction("Index");
            }

      

        }

        public ActionResult Edit(int id)
        {
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            var data = con.Seets.Where(s => s.Code == owner.Code && s.Id == id).SingleOrDefault();
            var seet = new SeetsViewModel
            {

                ID = data.Id,

                Number = data.Number,
                SeetType = data.SeetType,
                Cost = data.Cost


            };
            return View(seet);
        }
        [HttpPost]
        public ActionResult Edit(SeetsViewModel seetForm)
        {
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            if(!ModelState.IsValid)
            {
                return View();
            }    
            var data = con.Seets.Where(s => s.Code == owner.Code && s.Id == seetForm.ID).SingleOrDefault();
            data.Cost = seetForm.Cost;
            data.SeetType = seetForm.SeetType;
            data.Number = seetForm.Number;
            con.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            var data = con.Seets.Where(s => s.Code == owner.Code && s.Id == id).SingleOrDefault();
            var seet = new SeetsViewModel
            {

                ID = data.Id,

                Number = data.Number,
                SeetType = data.SeetType,
                Cost = data.Cost


            };
            return View(seet);
        }
        [HttpPost]
        public ActionResult Delete(SeetsViewModel seetForm)
        {
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            if (!ModelState.IsValid)
            {
                return View();
            }
            var data = con.Seets.Where(s => s.Code == owner.Code && s.Id == seetForm.ID).SingleOrDefault();
            con.Seets.Remove(data);
            con.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}