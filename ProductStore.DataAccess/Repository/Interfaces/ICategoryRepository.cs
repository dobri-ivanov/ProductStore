using ProductStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore.DataAcess.Repository.Interfaces
{
	public interface ICategoryRepository : IRepository<Category>
	{
		void Update(Category category);
	}
}
