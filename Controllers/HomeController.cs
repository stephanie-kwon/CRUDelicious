using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private YourContext dbContext;
        public HomeController(YourContext context)
        {
            dbContext = context;
        }
        public ViewResult Index()
        {
            System.Console.WriteLine("first point");

            List <Dishes> ReturnedDishes = dbContext.Dishes.ToList();
            ViewBag.AllRecipes = ReturnedDishes;

            return View();
        }


        [HttpGet]
        [Route("NewRecipe")]
        public ViewResult DishForm()
        {
            System.Console.WriteLine("second");
            return View("NewRecipe");
        }

        [HttpPost]
        [Route("addnew")]
        public IActionResult Addnew (Dishes dish)
        {
            if(ModelState.IsValid)
            {
                Dishes newDish = new Dishes
                {
                    Name = dish.Name,
                    Chef = dish.Chef,
                    Tastiness = dish.Tastiness,
                    Calories = dish.Calories,
                    Description = dish.Description
                
                

                };
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                System.Console.WriteLine("third");
                return RedirectToAction("Index");
            }
            else
            {
                System.Console.WriteLine("fourth");
                return View("NewRecipe");
            }
        }
        
        [HttpGet]
        [Route("{id}")]
        public ViewResult dish_id(int id)
        {

            List <Dishes> ReturnedDish = dbContext.Dishes.Where(dish => dish.id == id).ToList();
            ViewBag.AllRecipes = ReturnedDish;
            ViewBag.ID = id; 
            return View("ViewRecipe");
        }

        [HttpGet]
        [Route("{id}/EditRecipe")]
        public ViewResult ShowRecipe(int id,Dishes editdish)
        {
            Dishes RetrievedDish = dbContext.Dishes.FirstOrDefault(dish => dish.id == id);
            ViewBag.OneDish = RetrievedDish;

            return View("EditRecipe");
        }

        [HttpPost]
        [Route("{id}/EditRecipe")]
        public IActionResult EditRecipe(int id, Dishes editdish)
        {

            Dishes RetrievedDish = dbContext.Dishes.FirstOrDefault(dish => dish.id == id);
            ViewBag.OneDish = RetrievedDish;
            if(ModelState.IsValid)

            {
                RetrievedDish.Name = editdish.Name;
                RetrievedDish.Chef = editdish.Chef;
                RetrievedDish.Calories = editdish.Calories;
                RetrievedDish.Tastiness = editdish.Tastiness;
                RetrievedDish.Description = editdish.Description;
            
                dbContext.SaveChanges();
                System.Console.WriteLine("its hittingggggg");
                return RedirectToAction("Index");
            }
            else 
            {
                System.Console.WriteLine("hittting hereee!");
                return View("EditRecipe");
            }

        }
        [HttpGet]
        [Route("{id}/delete")]
        public IActionResult delete (int id, Dishes deletedish)
        {
            Dishes RetrievedDish = dbContext.Dishes.SingleOrDefault(dish => dish.id == id);
            dbContext.Dishes.Remove(RetrievedDish);
            dbContext.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}

