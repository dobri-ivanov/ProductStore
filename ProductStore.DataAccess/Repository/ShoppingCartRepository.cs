using ProductStore.DataAcess.Data;
using ProductStore.DataAcess.Repository.Interfaces;
using ProductStore.Models;

namespace ProductStore.DataAcess.Repository
{
	public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
	{
		private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(ShoppingCart cart)
		{
			_db.ShoppingCarts.Update(cart);
		}
	}
}
