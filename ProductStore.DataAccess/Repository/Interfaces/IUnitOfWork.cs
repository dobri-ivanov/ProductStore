﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore.DataAcess.Repository.Interfaces
{
	public interface IUnitOfWork
	{
		ICategoryRepository CategoryRepository { get; }
		void Save();
	}
}
