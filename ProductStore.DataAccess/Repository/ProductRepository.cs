 using Microsoft.EntityFrameworkCore;
using ProductStore.DataAcess.Data;
using ProductStore.DataAcess.Repository.Interfaces;
using ProductStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore.DataAcess.Repository
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private ProductStoreDbContext _db;
		public ProductRepository(ProductStoreDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(Product product)
		{
			var objFromDb = _db.Products.FirstOrDefault(u => u.Id == product.Id);
			if (objFromDb != null)
			{
				objFromDb.Title = product.Title;
				objFromDb.ISBN = product.ISBN;
				objFromDb.Price = product.Price;
				objFromDb.Price50 = product.Price50;
				objFromDb.Price100 = product.Price100;
				objFromDb.ListPrice = product.ListPrice;
				objFromDb.Description = product.Description;
				objFromDb.CategoryId = product.CategoryId;
				objFromDb.Author = product.Author;
				if (product.ImageUrl != null)
				{
					objFromDb.ImageUrl = product.ImageUrl;
				}

			}
		}
	}
}
