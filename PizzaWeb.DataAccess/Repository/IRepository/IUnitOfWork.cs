﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaWeb.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ICompanyRepository Company { get; }
        IShoppingCartRepository ShoppingCart{ get; }
        IApplicationUserRepository ApplicationUser{ get; }
        IOrderHeaderRepository OrderHeaderRepository { get; }


      //  IApplicationRepository ApplicationUser { get; }

        void Save();
    }
}
