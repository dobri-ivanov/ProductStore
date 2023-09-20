using ProductStore.DataAcess.Data;
using ProductStore.DataAcess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore.DataAcess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private ProductStoreDbContext _db;
		public ICategoryRepository Category {  get; private set; }
		public IProductRepository Product {  get; private set; }

        public UnitOfWork(ProductStoreDbContext db)
        {
            _db = db;
			Category= new CategoryRepository(db);
			Product = new ProductRepository(db);
        }
        public void Save()
		{
			_db.SaveChanges();
		}
	}
}
