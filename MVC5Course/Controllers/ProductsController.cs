using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();
        ProductRepository repo = RepositoryHelper.GetProductRepository();

        // GET: Products
        public ActionResult Index(bool Active = true)
        {
            var data = repo.GetProduct列表頁所有資料(Active, showAll:false);

            ViewData.Model = data;
            ViewData["ppp"] = data;
            ViewBag.ppp = data;
            return View();
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Get單筆資料ByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                repo.Add(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Get單筆資料ByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                repo.Update(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Get單筆資料ByProductId(id.Value); //db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Product product = repo.Get單筆資料ByProductId(id);
                repo.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
                repo.Delete(product);

                //var repoOrderLines = RepositoryHelper.GetOrderLineRepository(repo.UnitOfWork);
                //foreach (var item in product.OrderLine) {
                //    repoOrderLines.Delete(item);
                //}

                repo.UnitOfWork.Commit();
            }
            catch (DbEntityValidationException err) {
                throw err;
            }
            return RedirectToAction("Index");
        }

        public ActionResult ListProducts(QueryProductsVM queryVM)
        {
            var data = repo.GetProduct列表頁所有資料(true);

            if (ModelState.IsValid) {
                if (!string.IsNullOrEmpty(queryVM.ProductName))
                {
                    data = data.Where(p => p.ProductName.Contains(queryVM.ProductName));
                }

                data = data.Where(p => p.Stock > queryVM.Stock_S && p.Stock < queryVM.Stock_E);
            }

            ViewData.Model = data.Select(p => new ProductLiteVM()
                            {
                                ProductId = p.ProductId,
                                ProductName = p.ProductName,
                                Price = p.Price,
                                Stock = p.Stock
                            });

            return View();
        }

        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductLiteVM data)
        {
            if (ModelState.IsValid) {
                //TODO 將資料寫進資料庫
                #region 新增資料
                TempData["__Temp__"] = "CreateProduct";
                #endregion
                return RedirectToAction("ListProducts");
            }
            return View();
        }
    }
}
