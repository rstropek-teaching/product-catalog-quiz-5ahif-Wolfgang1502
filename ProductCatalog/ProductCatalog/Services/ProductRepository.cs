using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalog.Model;
using RestSharp;
using Newtonsoft.Json;

namespace ProductCatalog.Services
{
    public class ProductRepository : IProductRepository
    {

        public void Add(Product prod)
        {
            var client = new RestClient("https://productcatalog-00e4.restdb.io/rest/product");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", "ab8e13d603d34f1c9bbe08ade17ce9580cf75");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"Name\":\"" + prod.Name + "\",\"Description\":\"" + prod.Description + "\",\"Price\":\"" + prod.Price + "\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

        }

        public void Delete(int id)
        {

            var clientGet = new RestClient("https://productcatalog-00e4.restdb.io/rest/product");
            var requestGet = new RestRequest(Method.GET);
            requestGet.AddHeader("cache-control", "no-cache");
            requestGet.AddHeader("x-apikey", "ab8e13d603d34f1c9bbe08ade17ce9580cf75");
            requestGet.AddHeader("content-type", "application/json");

            IRestResponse response = clientGet.Execute(requestGet);
            string json = response.Content.ToString();
            string docid = "";

            string[] arr = json.Split("}");
            foreach (string item in arr)
            {
                if (item.Contains("\"ProductID\":" + id))
                {
                    string[] tmp = item.Split(",");
                    foreach (string str in tmp)
                    {
                        if (str.Contains("_id"))
                        {
                            string[] idstr = str.Split(":");
                            docid = idstr[1].Trim().Replace("\"", "");
                        }
                    }
                }
            }

            var client = new RestClient("https://productcatalog-00e4.restdb.io/rest/product/"+docid);
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", "ab8e13d603d34f1c9bbe08ade17ce9580cf75");
            request.AddHeader("content-type", "application/json");
            IRestResponse responsedelete = client.Execute(request);
        }

        public IEnumerable<Product> GetAll()
        {

            var client = new RestClient("https://productcatalog-00e4.restdb.io/rest/product");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", "ab8e13d603d34f1c9bbe08ade17ce9580cf75");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);

            string tmp = response.Content.ToString();
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(tmp);
            return products.ToArray();
        }

        public Product GetById(int id)
        {
            IEnumerable<Product> products = this.GetAll();
            return products.FirstOrDefault(product => product.ProductID == id);
        }

        public IEnumerable<Product> GetByName(string name)
        {
            if (string.IsNullOrEmpty(name) == false)
            {
                IEnumerable<Product> products = this.GetAll();
                var filter = new List<Product>();

                foreach (var item in products)
                {
                    if (item.Name.Trim().ToUpper().Contains(name.Trim().ToUpper()))
                    {
                        filter.Add(item);
                    }
                }
                return filter.ToArray();
            }
            else
            {
                return null;
            }
        }
    }
}
