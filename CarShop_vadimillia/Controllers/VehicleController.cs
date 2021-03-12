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
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return RedirectToAction("Error");
            }
        }

        //public IActionResult Index(int year, int? year2)
        //{
        //    try
        //    {
        //        if (year2 != null)
        //        {
        //            ViewBag.ByYears = _db.Vehicles.Where(x => x.BuildYear >= year && x.BuildYear <= year2);
        //        }
        //        else
        //        {
        //            ViewBag.ByYear = _db.Vehicles.Where(x => x.BuildYear >= year2 && x.BuildYear <= year);
        //        }
        //        return View();
        //    }
        //    catch (Exception)
        //    {

        //        return RedirectToAction("Index");
        //    }
        //}

        //public IActionResult Index(string description)
        //{
        //    try
        //    {
        //        if (description != null)
        //        {
        //            ViewBag.OrderByTitleAndDescription = _db.Vehicles.Where(x => x.Mark.Contains(description) || x.Description.Contains(description));
        //            return View();
        //        }
        //        else
        //        {
        //            return View(false);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        return RedirectToAction("Error");
        //    }
        //}
        public IActionResult Index()
        {
            ViewBag.AllCars = _db.Vehicles.ToList();
            return View();
        }

    }
}
