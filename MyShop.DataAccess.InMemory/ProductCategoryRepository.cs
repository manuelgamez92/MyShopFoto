using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShope.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {

        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productsCategories;

        public ProductCategoryRepository()
        {
            productsCategories = cache["productsCategories"] as List<ProductCategory>;
            if (productsCategories == null)
            {
                productsCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productsCategories"] = productsCategories;
        }

        public void Insert(ProductCategory p)
        {
            productsCategories.Add(p);
        }
        public void Update(ProductCategory productCategory)
        {
            ProductCategory productToUpdate = productsCategories.Find(p => p.Id == productCategory.Id);

            if (productToUpdate != null)
            {
                productToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory product = productsCategories.Find(p => p.Id == Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productsCategories.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory productdeleted = productsCategories.Find(p => p.Id == Id);

            if (productdeleted != null)
            {
                productsCategories.Remove(productdeleted);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
