using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDbFirstApproach.Models;

namespace EFDbFirstApproach.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        // to retrive all the products
        public ActionResult Index(string search="")
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            //List<Product> products = db.Products.ToList(); //to retrive all the list of db
            // List<Product> products = db.Products.Where(temp=>temp.CategoryID==1 && temp.Price >= 50000).ToList(); // conditional sorting
            List<Product> products = db.Products.Where(temp => temp.ProductName.Contains(search)).ToList();
            ViewBag.search = search;
           
            return View(products);
        }

        //to retrive single row

        public ActionResult Details(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product p = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            return View(p);
        }

        // to create products
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product p)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            db.Products.Add(p);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Edit(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product p = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            return View(p);
        }

        [HttpPost]
        public ActionResult Edit(Product p)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product ExistingProduct =  db.Products.Where(temp => temp.ProductID == p.ProductID).FirstOrDefault();
            
            ExistingProduct.ProductName = p.ProductName;
            ExistingProduct.Price = p.Price;
            ExistingProduct.DateOfPurchase = p.DateOfPurchase;
            ExistingProduct.CategoryID = p.CategoryID;
            ExistingProduct.Brand = p.Brand;
            ExistingProduct.AvailabilityStatus = p.AvailabilityStatus;
            ExistingProduct.Active = p.Active;
            db.SaveChanges();

            return RedirectToAction("index");
            
        }

        public ActionResult Delete(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product p = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            return View(p);
        }

        [HttpPost]
        public ActionResult Delete(long id,Product p)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product removingProduct = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            db.Products.Remove(removingProduct);
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}