using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShope.Core.Models;
using MyShop.DataAccess.InMemory;
using MyShope.Core.ViewModels;
using MyShop.Core.Contracts;
using System.IO;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {

       IRepository<Product> context;
       IRepository<ProductCategory> context2;

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategory)
        {
            context = productContext;
            context2 = productCategory;
        }

        // GET: ProductManager
        public ActionResult Index()
        {

            List<Product> products = context.Collection().ToList();
            return View(products);
        }

    
        public ActionResult Crear()
        {

            ProductManagerViewModel productView = new ProductManagerViewModel();

            productView.Product = new Product();
            productView.ProductCategories = context2.Collection();


            return View(productView);
        }

        [HttpPost]

        public ActionResult Crear(ProductManagerViewModel pro, HttpPostedFileBase file)
        {

            if (!ModelState.IsValid)
            {

                return View(pro);
            }
            else
            {

                if(file != null)
                {
                    pro.Product.Image = pro.Product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + pro.Product.Image);
                }

                context.Insert(pro.Product);
                context.Commit();

                return RedirectToAction("Index");

            }

        }
        



        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
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
            Product productToDelete = context.Find(Id);
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



        public ActionResult Edit( string Id)
        {
            Product product = context.Find(Id);
            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel productCategory = new ProductManagerViewModel();
                productCategory.Product = product;
                productCategory.ProductCategories = context2.Collection();
                return View(productCategory);
            }
        }

        [HttpPost]

        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)
        {
            Product productToEdit = context.Find(Id);

            if ( productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {

                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImage//") + product.Image);
                }



                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Price = product.Price;
                productToEdit.Category = product.Category;
                productToEdit.Name = product.Name;

                context.Commit();
                return RedirectToAction("Index");

            }
        }

    }
}