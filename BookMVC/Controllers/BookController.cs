using BookMVC.Models;
using BookMVC.Models.DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookMVC.Controllers
{
    public class BookController : Controller
    {
        private BookContext context { get; set; }
        public BookController(BookContext ctx)
        {
            context = ctx;
        }

        
        [HttpGet]
        public IActionResult Index()
        {
            List<Mmabook> books=context.Mmabooks.ToList();
            return View(books);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Insert(Mmabook mmaBook)
        {
            if (ModelState.IsValid)
            {
                try {
                    context.Mmabooks.Add(mmaBook);
                    context.SaveChanges();
                    TempData["message"] = $"{mmaBook.ProductCode} inserted to a database.";
                    return RedirectToAction("Index");
                }
                catch(DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;
                    if (sqlException.Number == 2627)
                    {
                        Console.WriteLine("Data duplication");
                        ModelState.AddModelError("", $"Product Code {mmaBook.ProductCode} already exists in a database.");
                        return View(mmaBook);
                    }
                    return View(mmaBook);
                }
              
            }
            else
            {
                ModelState.AddModelError("", "There are errors in the form.");
                return View(mmaBook);
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            Mmabook book=context.Mmabooks.Find(id);
            return View(book);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(Mmabook model)
        {
            if (ModelState.IsValid)
            {
                context.Mmabooks.Update(model);
                context.SaveChanges();
                TempData["message"] = $"{model.ProductCode} successfully edited.";
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            Mmabook item=context.Mmabooks.Where(item=> item.ProductCode==id).FirstOrDefault();
            context.Mmabooks.Remove(item);
            context.SaveChanges(); // related data deleted if cascade delete on
            TempData["message"] = $"{id} deleted from a database.";
            return RedirectToAction("Index");
        }
    }
}
