using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.ObjectModel;

namespace DataAccessWpf
{
    public class StoreDB
    {
        private const string Products = "Products";
        private const string ModelNumber = "ModelNumber";
        private const string ModelName = "ModelName";
        private const string UnitCost = "UnitCost";
        private const string Description = "Description";
        private const string CategoryID = "CategoryID";
        private const string CategoryName = "CategoryName";
        private const string ProductImage = "ProductImage";

        public Product GetProduct(int ID)
        {
            DataSet ds = StoreDbDataSet.ReadDataSet();
            DataRow productRow = ds.Tables[0].Select("ProductID = " + ID)[0];

            Product product = new Product((string)productRow[ModelNumber],
                                          (string)productRow[ModelName],
                                          (decimal)productRow[UnitCost],
                                          (string)productRow[Description],
                                          (int)productRow[CategoryID],
                                          (string)productRow[CategoryName],
                                          (string)productRow[ProductImage]);
            return product;
        }

        public ICollection<Product> GetProducts()
        {
            DataSet ds = StoreDbDataSet.ReadDataSet();

            ObservableCollection<Product> products = new ObservableCollection<Product>();
            foreach (DataRow productRow in ds.Tables[0].Rows)
            {
                products.Add(new Product((string)productRow[ModelNumber],
                                         (string)productRow[ModelName],
                                         (decimal)productRow[UnitCost],
                                         (string)productRow[Description],
                                         (int)productRow[CategoryID],
                                         (string)productRow[CategoryName],
                                         (string)productRow[ProductImage]));
            }
            return products;
        }

        public ICollection<Product> GetProductsFilteredWithLinq(decimal minCost)
        {
            // Get the full list of products.
            ICollection<Product> products = GetProducts();

            // Create a second collection with matching products.
            IEnumerable<Product> matches =
                from product in products
                where product.UnitCost >= minCost
                select product;

            return new ObservableCollection<Product>(matches.ToList());
        }
    }
}
