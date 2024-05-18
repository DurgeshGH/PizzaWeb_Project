using PizzaWeb.DataAccess.Data;
using PizzaWeb.DataAccess.Repository.IRepository;
using PizzaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaWeb.DataAccess.Repository
{
    public class ProductOrderDetailRepository : Repository<ProductOrderDetail>, IProductOrderDetailRepository
    {
        private ApplicationDbContext _db;
        public ProductOrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(ProductOrderDetail obj)
        {
            _db.ProductOrderDetails.Update(obj);
        }
    }
}
