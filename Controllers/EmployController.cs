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
    public class EmployController : Controller
    {
        private projectDbContext con = new projectDbContext();
        private Admin owner ;
        public EmployController()
        {
      

        }
        // GET: Employ
        public ActionResult Index()
        {
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a=> a.Code == code).First();
            var data = con.Employies.Where(e => e.Code == code).ToList();
            List<EmployeViewModel> emps = new List<EmployeViewModel>();
            foreach (var item in data)
            {
                emps.Add(new EmployeViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Phone = item.Phone,
                    Salary = item.Salary,
                    Job = item.Job,
                    Adress = item.Adress,


                });
            }
            ViewBag.type = owner.BussinesType;

            return View(emps);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create( EmployeViewModel emp)
        { 
            if(! ModelState.IsValid)
            {
                return View();
            }
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            Employe e = new Employe
            {
                Admin_Id = owner.Id,
                Name = emp.Name,
                Phone = emp.Phone,
                Salary = emp.Salary,
                Job = emp.Job,
                Adress = emp.Adress,
                Code = owner.Code,
                EmployeType = EmployType.Employee,
             
            };
            con.Employies.Add(e);
            con.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
           

            var data = con.Employies.SingleOrDefault(e => e.Id == id);
            EmployeViewModel emp = new EmployeViewModel
            {
                Id = data.Id,
                Name = data.Name,
                Adress = data.Adress,
                Job = data.Job,
                Salary = data.Salary,
                Phone = data.Phone
            };
            return View(emp);
        }
        [HttpPost]
        public ActionResult Edit(EmployeViewModel e)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            var empDb = con.Employies.Where(em => em.Id == e.Id && em.Code == code).FirstOrDefault();
            empDb.Admin_Id = owner.Id;
            empDb.Name = e.Name;
            empDb.Phone = e.Phone;
            empDb.Salary = e.Salary;
            empDb.Job = e.Job;
            empDb.Adress = e.Adress;
            empDb.Code = owner.Code;
            empDb.EmployeType = EmployType.Employee;
         
            con.SaveChanges();

            return RedirectToAction("Index");

        }
        public ActionResult Delete (int id)
        {
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            var data = con.Employies.Where(em => em.Id == id && em.Code == code).FirstOrDefault();
            EmployeViewModel emp = new EmployeViewModel
            {
                Id = data.Id,
                Name = data.Name,
                Adress = data.Adress,
                Job = data.Job,
                Salary = data.Salary,
                Phone = data.Phone
            };
            return View(emp);
        }
        [HttpPost]

        public ActionResult Delete(EmployeViewModel e)
        {
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            var data = con.Employies.Where(em => em.Id == e.Id && em.Code == code).FirstOrDefault();
            con.Employies.Remove(data);
            con.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}