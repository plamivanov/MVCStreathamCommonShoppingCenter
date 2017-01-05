using OnlineShoppingStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingStore.Domain.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Entities.Product> Products { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int id);
    }
}
