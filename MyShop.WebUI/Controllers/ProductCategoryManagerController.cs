using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShope.Core.Models;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Contracts;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        // GET: ProductCategoryManager
        IRepository<ProductCategory> context;

        public ProductCategoryManagerController(IRepository<ProductCategory> pc)
        {
            this.context = pc;

        }

        // GET: ProductManager
        public ActionResult Index()
        {

            List<ProductCategory> productsCa = context.Collection().ToList();
            return View(productsCa);
        }

        [HttpGet]
        [Route("ProductCategory/GetCategory")]
        public List<ProductCategory> GetCategory()
        {
            List<ProductCategory> productsCa = context.Collection().ToList();
            return productsCa;
        }

        public ActionResult Create()
        {
            ProductCategory productCa = new ProductCategory();
            return View(productCa);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory pro)
        {

            if (!ModelState.IsValid)
            {
                return View(pro);
            }
            else
            {
                context.Insert(pro);
                context.Commit();

                return RedirectToAction("Index");

            }

        }



        public ActionResult Delete(string Id)
        {
            ProductCategory productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }


        [Route("ProductCategory/Editar")]
        public ActionResult Edit(string Id)
        {
            ProductCategory product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        [HttpPost]

        public ActionResult Edit(ProductCategory productCa, string Id)
        {
            ProductCategory productToEdit = context.Find(Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {

                if (!ModelState.IsValid)
                {
                    return View(productCa);
                }
       
                productToEdit.Category = productCa.Category;
             

                context.Commit();
                return RedirectToAction("Index");

            }
        }
    }
}