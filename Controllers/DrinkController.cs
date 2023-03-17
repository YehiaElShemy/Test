using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projectMVC.Models;
using projectMVC.viewModel;
using projectMVC.enums;

namespace projectMVC.Controllers
{
    public class DrinkController : Controller
    {
        private projectDbContext con = new projectDbContext();
        private Admin owner;
        // GET: Drink
        public ActionResult Index()
        {
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            var data = con.Drinkies.Where(e => e.Code == code).ToList();
            List<DrinkViewModel> drinks = new List<DrinkViewModel>();
            foreach (var item in data)
            {
                drinks.Add(new DrinkViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Buy = item.Buy,
                    Sale = item.Sale,
                    Profit = item.Profit,
                    Amonut = item.Amonut,


                });
            }
            ViewBag.type = owner.BussinesType;

            return View(drinks);
        }
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(DrinkViewModel d )
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            Drink drink = new Drink
            {
                Admin_Id = owner.Id,
                Name = d.Name,
                Buy = d.Buy,
                Sale = d.Sale,
                Profit = d.Profit,
                Amonut = d.Amonut,
                Code = owner.Code,
                Type = TypeOfFoodAndDrink.Drink,
                TotalAmonut = d.Amonut
                
            };
            con.Drinkies.Add(drink);
            con.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Edit(int id)
        {


            var data = con.Drinkies.SingleOrDefault(d => d.Id == id);
            DrinkViewModel drink = new DrinkViewModel
            {
                Id = data.Id,
                Name = data.Name,
                Buy = data.Buy,
                Sale = data.Sale,
                Profit = data.Profit,
                Amonut = data.Amonut,
               

            };
            return View(drink);
        }
        [HttpPost]
        public ActionResult Edit(DrinkViewModel d)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            var drinkDb = con.Drinkies.Where(dr => dr.Id == d.Id && dr.Code == code).FirstOrDefault();
            drinkDb.Admin_Id = owner.Id;
            drinkDb.Name = d.Name;
            drinkDb.Sale = d.Sale;
            drinkDb.Profit = d.Profit;
            drinkDb.Amonut = d.Amonut;
            drinkDb.Code = owner.Code;
            drinkDb.Type = TypeOfFoodAndDrink.Drink;
            drinkDb.TotalAmonut = d.Amonut;


            con.SaveChanges();

            return RedirectToAction("Index");

        }
        public ActionResult Delete(int id)
        {
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            var data = con.Drinkies.Where(em => em.Id == id && em.Code == code).FirstOrDefault();
            DrinkViewModel drink = new DrinkViewModel
            {
                Id = data.Id,
                Name = data.Name,
                Buy = data.Buy,
                Sale = data.Sale,
                Profit = data.Profit,
                Amonut = data.Amonut,
            };
            return View(drink);
        }
        [HttpPost]

        public ActionResult Delete(DrinkViewModel d)
        {
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            var data = con.Drinkies.Where(dr => dr.Id == d.Id && dr.Code == code).FirstOrDefault();
            con.Drinkies.Remove(data);
            con.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}