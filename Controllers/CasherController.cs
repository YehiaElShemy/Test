using projectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projectMVC.viewModel;
using projectMVC.enums;

namespace projectMVC.Controllers
{
    public class CasherController : Controller
    {
        // GET: Casher
        private projectDbContext con = new projectDbContext();
        private Admin owner;
    
        // GET: Employ
       public ActionResult Index()
        {
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
         
            var data = con.Employies.Where(e => e.Code == code && e.EmployeType == EmployType.Casher).ToList();
            var c = con.Cashers.Where(x => x.Admin_Id == owner.Id).ToList();
            var result = c.Join(data, a1 => a1.Emp_Id, a2 => a2.Id, (a1, a2) =>


          new CasherViewModel
          {

              Id =a1.Id,
              Name = a2.Name,
              Phone = a2.Phone,
              Salary = a2.Salary,
              Adress = a2.Adress,
              CasherUserName = a1.CasherUserName,
              CasherPassword = a1.CasherPassword
          }
            ).ToList();
            List<CasherViewModel> Cashers = new List<CasherViewModel>();
            foreach (var item in result)
            {
                Cashers.Add(new CasherViewModel
                {
                Id = item.Id,
                    Name = item.Name,
                    Phone = item.Phone,
                    Salary = item.Salary,
                    Adress = item.Adress,
                    CasherUserName = item.CasherUserName ,
                    CasherPassword=  item.CasherPassword
                
                }) ;

            }
            ViewBag.type = owner.BussinesType;

            return View(Cashers);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CasherViewModel casher)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            try
            {
                Employe e = new Employe()
                {
                    Admin_Id = owner.Id,
                    Name = casher.Name,
                    Phone = casher.Phone,
                    Salary = casher.Salary,
                    Job = "Casher",
                    Adress = casher.Adress,
                    Code = owner.Code,
                    EmployeType = EmployType.Casher,

                };
                con.Employies.Add(e);


                con.SaveChanges();
                var employe = con.Employies.Where(x => x.Code == code && x.EmployeType == EmployType.Casher).FirstOrDefault();

                Casher c = new Casher
                {
                    Id = employe.Id,
                    Admin_Id = owner.Id,
                    Emp_Id = employe.Id,
                    CasherUserName = casher.CasherUserName,
                    CasherPassword = casher.CasherPassword

                };

                con.Cashers.Add(c);
                con.SaveChanges();
            }
            catch (Exception ex)
            {
                return Content(" <h1 class='text-denger'>UserName of casher should be uniqe please try anthor username </h1> ");

            }
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {

            var casher = con.Cashers.SingleOrDefault(c => c.Id == id);
            int casherId = casher.Emp_Id;
            var emp = con.Employies.SingleOrDefault(e =>e.Id == casherId);
            CasherViewModel data = new CasherViewModel
            {
                Name = emp.Name,
                Phone = emp.Phone,
                Salary = emp.Salary,
                Adress = emp.Adress,
                CasherUserName = casher.CasherUserName,
                CasherPassword = casher.CasherPassword
            };
     
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(CasherViewModel casherForm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                var casher = con.Cashers.SingleOrDefault(c => c.Id == casherForm.Id);
                casher.CasherUserName = casherForm.CasherUserName;
                casher.CasherPassword = casherForm.CasherPassword;
                con.SaveChanges();

                var emp = con.Employies.SingleOrDefault(e => e.Id == casher.Emp_Id);
                emp.Name = casherForm.Name;
                emp.Phone = casherForm.Phone;
                emp.Salary = casherForm.Salary;
                emp.Adress = casherForm.Adress;
                con.SaveChanges();
            }
            catch (Exception ex)
            {
                return Content(" <h1 class='text-denger'>UserName of casher should be uniqe please try anthor username </h1> ");

            }

            return RedirectToAction("Index");

        }
        public ActionResult Delete(int id)
        {
            var casher = con.Cashers.SingleOrDefault(c => c.Id == id);
            var emp = con.Employies.SingleOrDefault(e => e.Id == casher.Emp_Id);
            CasherViewModel data = new CasherViewModel
            {
                Name = emp.Name,
                Phone = emp.Phone,
                Salary = emp.Salary,
                Adress = emp.Adress,
                CasherUserName = casher.CasherUserName,
                CasherPassword = casher.CasherPassword
            };


            return View(data);
        }
        [HttpPost]

        public ActionResult Delete( CasherViewModel casherForm)
        {
            var casher = con.Cashers.SingleOrDefault(c => c.Id == casherForm.Id);
            var emp = con.Employies.SingleOrDefault(e => e.Id == casher.Emp_Id);
            con.Employies.Remove(emp);
            con.SaveChanges();
       
            
            return RedirectToAction("Index");
        }
    }
}