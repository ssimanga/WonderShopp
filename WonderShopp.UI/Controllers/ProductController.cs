using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WonderShopp.Core.Contracts;
using WonderShopp.Core.Models;
using WonderShopp.Core.ViewModels;

namespace WonderShopp.UI.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        public ProductController(IRepository<Product> productContext, 
            IRepository<ProductCategory> categoryContext)
        {
            context = productContext;
            productCategories = categoryContext;
        }
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }
        public ActionResult Create() 
        {
            ProductVM viewModel = new ProductVM();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if(file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages" + product.Image));
                }
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
                return HttpNotFound();
            else
            {
                ProductVM viewModel = new ProductVM();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();
                return View(viewModel);
            }        
            
        }
        [HttpPost]
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)
        {
            Product p = context.Find(Id);
            if(p == null)
                return HttpNotFound();
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages" + product.Image));
                }
                p.Category = product.Category;
                p.Description = product.Description;
                p.Name = product.Name;
                p.Price = product.Price;
                context.Commit();
                return RedirectToAction("index");
                 
            }
        }
        public ActionResult Delete(string Id)
        {
            Product p = context.Find(Id);
            if (p == null)
                return HttpNotFound();
            else
                return View(p);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product p = context.Find(Id);
            if (p == null)
                return HttpNotFound();
            else
                context.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}