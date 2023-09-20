using ProductStore.Models;

namespace ProductStore.DataAcess.Repository.Interfaces
{
	public interface IProductRepository : IRepository<Product>
	{
		void Update(Product product);
	}
}
