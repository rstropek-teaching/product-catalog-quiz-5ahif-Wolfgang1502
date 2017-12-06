using ProductCatalog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Services
{
    public interface IProductRepository
    {

        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetByName(string name);
        Product GetById(int id);
        void Add(Product prod);
        void Delete(int id);

    }
}
