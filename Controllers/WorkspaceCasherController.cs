using projectMVC.Models;
using projectMVC.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projectMVC.Controllers
{
    public class WorkspaceCasherController : Controller
    {
        // GET: WorkspaceCasher
        private projectDbContext con = new projectDbContext();
        public ActionResult Index()
        {
            //Session["UserNameCasher"] = data.CasherUserName;
            //Session["PasswordCasher"] = data.CasherPassword;

            //Session["Code"] = owner.Code;

            if (Session["UserNameCasher"] == null || Session["PasswordCasher"] == null || Session["Code"] == null)
            {
                return RedirectToAction("Index", "HomePage");

            }
            var code = Session["Code"].ToString();
            //  var data = con.Admines.Include(a => a.foodies).Include(a => a.Drinkies).Where(a => a.Code == code).ToList();

            //var seets = con.Seets.Where(s => s.Code == code && s.IsEmpty == false).ToList();
            var seets = con.Seets.Where(s => s.Code == code).ToList();

            var drinks = con.Drinkies.Where(d => d.Code == code && d.Amonut > 0).ToList();

            DrinksAndSeetCasher casherdata = new DrinksAndSeetCasher
            {
                Seets = seets,
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
      
            var drinksDB = con.Drinkies.ToList();
            var myBill = new Bills();
            myBill.Date = DateTime.Now.Date;
            myBill.Code = code;
            myBill.UserName = Session["UserNameCasher"].ToString();
            myBill.Names = "";
            myBill.Total = data.Total;
            for (int i = 0; i < bill.Names.Count; i++)
            {
                string str = bill.Names[i].ToString().Trim();
                int a = bill.Amounts[i];
                var y = con.Drinkies.Where(f => f.Code == code && f.Name == str).SingleOrDefault();

                if (y != null)
                {
                    y.Amonut -= a;
                    con.SaveChanges();
                }
              
                myBill.Names += $"{ str } = {a} , ";


            }
            con.Bills.Add(new Bill
            {
                UserName = myBill.UserName,
                Code = myBill.Code,
                Date = myBill.Date,
                Names = myBill.Names,
                Total = myBill.Total
            });
            con.SaveChanges();
            return Json(bill, JsonRequestBehavior.AllowGet);

        }
    }
}