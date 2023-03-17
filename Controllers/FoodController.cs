using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projectMVC.enums;
using projectMVC.Models;
using projectMVC.viewModel;

namespace projectMVC.Controllers
{
    public class FoodController : Controller
    {
        // GET: Food

        private projectDbContext con = new projectDbContext();
        private Admin owner;
        // GET: Drink
        public ActionResult Index()
        {
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            var data = con.Foodies.Where(e => e.Code == code).ToList();
            List<FoodViewModel> foods = new List<FoodViewModel>();
            foreach (var item in data)
            {
                foods.Add(new FoodViewModel
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

            return View(foods);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FoodViewModel f)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            Food food = new Food
            {
                Admin_Id = owner.Id,
                Name = f.Name,
                Buy = f.Buy,
                Sale = f.Sale,
                Profit = f.Profit,
                Amonut = f.Amonut,
                Code = owner.Code,
                Type = TypeOfFoodAndDrink.Food,
                TotalAmonut = f.Amonut

            };
            con.Foodies.Add(food);
            con.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Edit(int id)
        {


            var data = con.Foodies.SingleOrDefault(d => d.Id == id);
            FoodViewModel food = new FoodViewModel
            {
                Id = data.Id,
                Name = data.Name,
                Buy = data.Buy,
                Sale = data.Sale,
                Profit = data.Profit,
                Amonut = data.Amonut,

            };
            return View(food);
        }
        [HttpPost]
        public ActionResult Edit(FoodViewModel f)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            var FoodDb = con.Foodies.Where(x => x.Id == f.Id && x.Code == code).FirstOrDefault();
            FoodDb.Admin_Id = owner.Id;
            FoodDb.Name = f.Name;
            FoodDb.Sale = f.Sale;
            FoodDb.Profit = f.Profit;
            FoodDb.Amonut = f.Amonut;
            FoodDb.Code = owner.Code;
            FoodDb.Type = TypeOfFoodAndDrink.Food;
            FoodDb.TotalAmonut = f.Amonut;

            con.SaveChanges();

            return RedirectToAction("Index");

        }
        public ActionResult Delete(int id)
        {
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            var data = con.Foodies.Where(em => em.Id == id && em.Code == code).FirstOrDefault();
            FoodViewModel food = new FoodViewModel
            {
                Id = data.Id,
                Name = data.Name,
                Buy = data.Buy,
                Sale = data.Sale,
                Profit = data.Profit,
                Amonut = data.Amonut,

            };
            return View(food);
        }
        [HttpPost]

        public ActionResult Delete(FoodViewModel f)
        {
            var code = Session["CodeAdmin"].ToString();
            owner = con.Admines.Where(a => a.Code == code).First();
            var data = con.Foodies.Where(x => x.Id == f.Id && x.Code == code).FirstOrDefault();
            con.Foodies.Remove(data);
            con.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}