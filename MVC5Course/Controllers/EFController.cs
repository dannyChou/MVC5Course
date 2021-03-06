﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    public class EFController : BaseController
    {
        // GET: EF
        public ActionResult Index()
        {
            var all = db.Product.AsQueryable();
            var data = all.Where(p => 
                p.Is刪除 ==false && p.Active == true && p.ProductName.Contains("Black")
                ).OrderByDescending( p=>p.ProductId).Take(10);
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid) {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Edit(int id)
        {
            var item = db.Product.Find(id);

            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id,Product product)
        {
            if (ModelState.IsValid) {
                var item = db.Product.Find(id);
                item.ProductName = product.ProductName;
                item.Price = product.Price;
                item.Active = product.Active;
                item.Stock = product.Stock;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Delete(int id)
        {
            var item = db.Product.Find(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Delete(int id, Product product)
        {
            var item = db.Product.Find(id);

            //foreach (var oderLine in item.OrderLine) {
            //    db.OrderLine.Remove(oderLine);
            //}

            //db.OrderLine.RemoveRange(item.OrderLine);

            //db.Product.Remove(item);
            item.Is刪除 = true;

            try {
                db.SaveChanges();
            }
            catch (DbEntityValidationException err) {
                throw err;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            //var item = db.Product.Find(id);
            var item = db.Database.SqlQuery<Product>(
                "Select * from dbo.Product where ProductId=@p0",id).FirstOrDefault();

            return View(item);
        }

        public void RemoveAll() {
            //db.Product.RemoveRange(db.Product);
            //db.SaveChanges();
            db.Database.ExecuteSqlCommand("Delete From dbo.Product");

        }
    }
}