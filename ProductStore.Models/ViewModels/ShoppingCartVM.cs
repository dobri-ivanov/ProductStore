namespace ProductStore.Models.ViewModels
{
	public class ShoppingCartVM
	{
		public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
		public double OrderTotal
		{
			get
			{
				return ShoppingCartList.Sum(s => s.Product.Price * s.Count);
			}
			set
			{
				this.OrderTotal = value;
			}
		}
	}
}
