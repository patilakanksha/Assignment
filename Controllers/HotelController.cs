using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment.Controllers
{
    public class HotelController : Controller
    { 
        MVCTrainingEntities hotelDbContext = new MVCTrainingEntities();
        // GET: Hotel
        public ActionResult Index()
        {
            try
            {
                if (TempData["ErrorMessage"] != null)
                {
                    ViewBag.ErrorMessage = TempData["ErrorMessage"];
                }
                List<Hotel> hotelList = hotelDbContext.Hotels.ToList();
                ViewData["TotalHotelCount"] = hotelList.Count;
                return View("List", hotelList);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving hotels";
                return View("Error");
            }
        }

        public ActionResult Add()
        {
            try
            {
                using (var dbContext = new MVCTrainingEntities())
                {
                    List<Category> categories = dbContext.Categories.ToList();
                    List<SelectListItem> categoryList = categories.Select(category => new SelectListItem
                    {
                        Text = category.Name,
                        Value = category.CategoryId.ToString(),
                    }).ToList();
                    ViewBag.CategoryId = categoryList;
                }
                return View();
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "An error occurred while loading categories for adding a hotel.";
                return View("Error");
            }
        }

        [HttpPost]
        [ActionName("Add")]
        public ActionResult AddHotel(Hotel hotel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Hotel> hotelList = hotelDbContext.Hotels.ToList();
                    hotel.HotelId = "H100" + (hotelList.Count + 1);
                    hotelDbContext.Hotels.Add(hotel);
                    hotelDbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "An error occurred while adding the hotel.";
                return View("Error");
            }
        }


        public ActionResult Update(string id)
        {
            try
            {
                if (id != null)
                {
                    using (var hotelDbContext = new MVCTrainingEntities())
                    {
                        var hotel = hotelDbContext.Hotels.Where(x => x.HotelId == id).FirstOrDefault();
                        List<Category> categories = hotelDbContext.Categories.ToList();
                        List<SelectListItem> categoryList = categories.Select(category => new SelectListItem
                        {
                            Text = category.Name,
                            Value = category.CategoryId.ToString() // Convert to string
                        }).ToList();

                        ViewBag.CategoryList = categoryList;
                        return View(hotel);
                    }
                } else
                {
                    ViewBag.ErrorMessage = "An error occurred while loading hotel data for update";
                    return View("Error404");
                }
                
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ViewBag.ErrorMessage = "An error occurred while adding the hotel.";
                return View("Error");
            }
        }

        [HttpPost]
        [ActionName("Update")]
        public ActionResult UpdateHotel(Hotel hotel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    hotelDbContext.Entry(hotel).State = System.Data.Entity.EntityState.Modified;
                    hotelDbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            } 
            catch (Exception exception)
            {
                TempData["ErrorMessage"] = "Fail to update the record.";
                return RedirectToAction("Index");
            }
            
        }


        public ActionResult Delete(string id)
        {
            if (id != null)
            {
                var hotel = hotelDbContext.Hotels.Where(x => x.HotelId == id).FirstOrDefault();
                return View(hotel);
            }
            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteHotel(string id)
        {
            if (id != null)
            {
                var hotel = hotelDbContext.Hotels.Where(x => x.HotelId == id).FirstOrDefault();
                hotelDbContext.Hotels.Remove(hotel);
                hotelDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            try
            {
                if (id != null)
                {
                    var hotel = hotelDbContext.Hotels.Where(x => x.HotelId == id).FirstOrDefault();
                    if (hotel != null)
                    {
                        return View(hotel);
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "This Hotel is not present";
                        // Return a 404 error view when the record is not found
                        return View("Error404");
                    }
                }
                return View("Error404");
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ViewBag.ErrorMessage = "An error occurred while retrieving hotel details.";
                return View("Error");
            }
        }


    }
}