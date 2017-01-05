using OnlineShoppingStore.Domain.Abstract;
using OnlineShoppingStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;
        public AdminController(IProductRepository repo)
        {
            this.repository = repo;
        }
        // GET: Admin
        public ActionResult Index()
        {
           
            return View(repository.Products);
        }

        public ViewResult Edit(int? id)
        {
            Product product = this.repository.Products.FirstOrDefault(x => x.ProductId == id);

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                this.repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);

                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }

            return View(product);
        }

        public ViewResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                this.repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);

                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult Delete (int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if(deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedProduct.Name);
                
            }
            return RedirectToAction("Index");
        }

    }
}