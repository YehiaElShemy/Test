using projectMVC.Models;
using projectMVC.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace projectMVC.Controllers
{
    public class AdminCoffieController : Controller
    {
        private projectDbContext con = new projectDbContext();
        private Admin AdminOfCoffie;
        // GET: AdminCoffie
        public ActionResult Index()
        {
            if(Session["UserNameAdmin"]==null || Session["PasswordAdmin"] == null || Session["CodeAdmin"]==null)
            {
                return RedirectToAction("LoginAdmin", "HomePage");
            }
            string Code = Session["CodeAdmin"].ToString();
            var bay = 0.0m ;
            var sale = 0.0m;
            var totalEmp = 0.0m;
            var totalRed= 0.0m;
            var sum = 0.0m;

            if (con.Employies.Where(e => e.Code == Code).Count() > 0)
            {
                totalEmp = con.Employies.Where(e => e.Code == Code).Sum(e => e.Salary);
                ViewBag.TotalEmpSalary = totalEmp;
            }
            if (con.Foodies.Where(e => e.Code == Code).Count() > 0)
            {
                bay = con.Foodies.Where(f => f.Code == Code).Sum(f => f.Buy * f.Amonut);
                ViewBag.TotalFoodBayment = bay;
                 sale=   con.Foodies.Where(f => f.Code == Code).Sum(f => f.Sale * f.Amonut);
                ViewBag.TotalFoodSale = sale;
                sum = sum + (sale - bay);
                ViewBag.FoodProfit = sale - bay;

            }
            if (con.Drinkies.Where(e => e.Code == Code).Count() > 0)
            {
                bay = con.Drinkies.Where(d => d.Code == Code).Sum(d => d.Buy * d.Amonut);
                ViewBag.TotalDrinkBayment = bay;
                sale = con.Drinkies.Where(d => d.Code == Code).Sum(d => d.Sale * d.Amonut);
                ViewBag.TotalDrinkSale = sale;
                sum = sum + (sale - bay);

                ViewBag.DrinksProfit = sale - bay;

            }
            var reqDb = con.Requirement.Where(r => r.Code == Code).SingleOrDefault();
            if(reqDb != null)
            {
                ViewBag.Rent = reqDb.Rent;
                ViewBag.WaterBill = reqDb.WaterBill;
                ViewBag.ElecBill = reqDb.ElectricityBill;
                totalRed =(decimal) (reqDb.ElectricityBill + reqDb.WaterBill + reqDb.Rent);
                ViewBag.totalReq = totalRed;
            }



            var TotalProfit = sum - (totalEmp + totalRed);
            ViewBag.TotalBussinesProfit = TotalProfit;
            return View();
        }
        public ActionResult Employ()
        {
          return  RedirectToAction("Index","Employ");
        }
        public ActionResult Drinks()
        {
            return RedirectToAction("Index", "Drink");

        }
        public ActionResult Foods()
        {
            return RedirectToAction("Index", "Food");

        }
        public ActionResult Casher()
        {
            return RedirectToAction("Index", "Casher");

        }
        public ActionResult logOut()
        {
            Session["UserNameAdmin"] = null;  Session["PasswordAdmin"] = null; Session["CodeAdmin"] = null;
            return RedirectToAction("Index", "HomePage");
        }

        [HttpGet]
        public ActionResult SetReq()
        {
            string Code = Session["CodeAdmin"].ToString();

           var reqdb= con.Requirement.Where(r => r.Code == Code).SingleOrDefault();
            var req = new Req()
            {
                Rent = reqdb.Rent,
                WaterBill = reqdb.WaterBill,
                ElectricityBill = reqdb.ElectricityBill
            };

            return View(req);
        }

        [HttpPost]
        public ActionResult SetReq(Req R)
        {
            string Code = Session["CodeAdmin"].ToString();
            var reqDb = con.Requirement.Where( r => r.Code == Code).SingleOrDefault();
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (reqDb != null)
            {
                reqDb.Rent = R.Rent;
                reqDb.WaterBill = R.WaterBill;
                reqDb.ElectricityBill = R.ElectricityBill;
                con.SaveChanges();
            }
            return RedirectToAction("Index", "AdminCoffie");
        }


    }
}