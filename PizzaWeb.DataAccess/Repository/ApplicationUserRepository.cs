﻿using PizzaWeb.DataAccess.Data;
using PizzaWeb.DataAccess.Repository.IRepository;
using PizzaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaWeb.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        //public void Update(Category obj)
        //{
        //    _db.Categories.Update(obj);
        //}
    }
}
