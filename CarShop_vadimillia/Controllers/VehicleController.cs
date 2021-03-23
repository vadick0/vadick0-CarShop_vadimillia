using CarShop_vadimillia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace CarShop_vadimillia.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ApplicationContext _db;
        IWebHostEnvironment _appEnvironment;
        public VehicleController(ApplicationContext db, IWebHostEnvironment appEnvironment)
        {
            _db = db;
            _appEnvironment = appEnvironment;
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult AddVehicle()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AddVehicle(string Mark, int BuildYear, string Description, int Price, IFormFile formFile)
        {
            try
            {
                if (formFile != null)
                {
                    string formfile = "/Files/" + formFile.FileName;
                    using (var filestream = new FileStream(_appEnvironment.WebRootPath + formfile, FileMode.Create))
                    {
                        formFile.CopyTo(filestream);
                    }
                }
                Vehicle veh = new Vehicle();
                veh.Mark = Mark;
                veh.BuildYear = BuildYear;
                veh.Description = Description;
                veh.Price = Price;
                veh.Img = "/Files/" + formFile.FileName;
                _db.Vehicles.Add(veh);
                _db.SaveChanges();
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("Error");
            }
        }
        public IActionResult Index(string mark = "", /*int year1 = 1000, int year2 = 2025,*/ string desc = "", int price1 = 100, int price2 = 500000000,bool order = true)
        {
            try
            {
                if (order)
                {
                    ViewBag.Cars = _db.Vehicles.Where(x => x.Mark.Contains(mark) && /*x.BuildYear >= year1 && x.BuildYear <= year2 &&*/ x.Description.Contains(desc) && x.Price >= price1 && x.Price <= price2).OrderByDescending(x => x.Id).ToList();
                }
                else
                {
                    ViewBag.Cars = _db.Vehicles.Where(x => x.Mark.Contains(mark) && /*x.BuildYear >= year1 && x.BuildYear <= year2 &&*/ x.Description.Contains(desc) && x.Price >= price1 && x.Price <= price2).OrderBy(x => x.Id).ToList();
                }
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
            
        }
        //public IActionResult Index()
        //{
        //    ViewBag.AllCars = _db.Vehicles.ToList();
        //    return View();
        //}

    }
}
