using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projectMVC.Controllers
{
    public class BillController : Controller
    {
        // GET: Bill
        private projectDbContext con = new projectDbContext();

        public ActionResult Index()
        {
            if (Session["UserNameAdmin"] == null || Session["PasswordAdmin"] == null || Session["CodeAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "HomePage");
            }
            string str = Session["CodeAdmin"].ToString();
            var dates = con.Bills.Where(b => b.Code == str).Select(s => s.Date).Distinct().ToList();
            ViewBag.dates = dates;

            ViewBag.bussinesType = con.Admines.Where(a => a.Code == str).SingleOrDefault().BussinesType; 
            return View();
        }
        public ActionResult GetBill(string date)
        {
            string str = Session["CodeAdmin"].ToString();
            var x = DateTime.Parse(date);
            var data = con.Bills.Where(b => b.Code == str && b.Date == x).ToList();
          
            var bills = new List<projectMVC.viewModel.Bills>();
            foreach (var item in data)
            {
                var bill = new projectMVC.viewModel.Bills();

                bill.Names = item.Names;
                bill.Total = item.Total;
                bill.UserName = item.UserName;
                bills.Add(bill);

            }
          
            return View(bills);
        }
    }
}