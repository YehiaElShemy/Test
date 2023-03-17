using projectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projectMVC.viewModel;
using System.Data.Entity;

namespace projectMVC.Controllers
{
    public class CoffieCasherController : Controller
    {
        // GET: CoffieCasher

        private projectDbContext con = new projectDbContext();
        public ActionResult Index()
        {
          
            if(Session["UserNameCasher"]== null || Session["PasswordCasher"]==null || Session["Code"] == null)
            {
                return RedirectToAction("Index", "HomePage");

            }
            var code = Session["Code"].ToString();
            var foods = con.Foodies.Where(f => f.Code == code && f.Amonut > 0).ToList();
            var drinks = con.Drinkies.Where(d => d.Code == code && d.Amonut > 0).ToList();

            FoodAndDrinksCasher casherdata = new FoodAndDrinksCasher
            {
                foods = foods,
                drinks = drinks
            };
            return View(casherdata);
        }

        public ActionResult Logout()
        {
            Session["UserNameCasher"] = null;
            Session["PasswordCasher"] = null;

            Session["Code"] = null;
            return RedirectToAction("Index", "HomePage");

        }



        [HttpPost]
        public JsonResult Sale(FDA data)
        {
            var bill = new FDA
            {
                Names = data.Names,
                Amounts = data.Amounts,
                Total = data.Total
            };

            string code = Session["Code"].ToString();
            var foodDB = con.Foodies.ToList();
            var drinksDB = con.Drinkies.ToList();
            var myBill = new Bills();
            myBill.Date = DateTime.Now.Date;
            myBill.Code = code;
            myBill.UserName = Session["UserNameCasher"].ToString();
            myBill.Names = "";
            myBill.Total = data.Total;
            for (int i=0; i< bill.Names.Count; i++)
            {
                string str = bill.Names[i].ToString().Trim();
                int a = bill.Amounts[i];
                var x =  con.Foodies.Where(f => f.Code == code && f.Name.Trim() == str).SingleOrDefault();
                if(x == null)
                {
                    var y = con.Drinkies.Where(f => f.Code == code && f.Name.Trim() == str).SingleOrDefault();
                    y.Amonut -= a;
                    con.SaveChanges();
                }
                else
                {
                    x.Amonut -= a;
                    con.SaveChanges();
                }
                myBill.Names += $"{ str } = {a} , ";
                

            }
            con.Bills.Add(new Bill {UserName=myBill.UserName , 
            Code = myBill.Code , Date= myBill.Date , Names =myBill.Names, Total = myBill.Total});
            con.SaveChanges();

            return Json(bill , JsonRequestBehavior.AllowGet);

        }
       
    }
}