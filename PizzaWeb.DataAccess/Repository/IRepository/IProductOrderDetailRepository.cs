using PizzaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaWeb.DataAccess.Repository.IRepository
{
    public interface IProductOrderDetailRepository : IRepository<ProductOrderDetail>
    {
        //void GetAll();
        void Update(ProductOrderDetail obj);
    }
}
