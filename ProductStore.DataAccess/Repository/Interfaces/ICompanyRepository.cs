using ProductStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore.DataAcess.Repository.Interfaces
{
	public interface ICompanyRepository : IRepository<Company>
	{
		void Update(Company category);
	}
}
