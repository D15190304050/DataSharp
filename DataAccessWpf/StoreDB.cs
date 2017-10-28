using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.ObjectModel;

namespace DataAccessWpf
{
    /// <summary>
    /// The StoreDB class provides data that will be presented in the WPF window.
    /// </summary>
    public class StoreDB
    {
        // String literal for easy programming.
        private const string Products = "Products";
        private const string ModelNumber = "ModelNumber";
        private const string ModelName = "ModelName";
        private const string UnitCost = "UnitCost";
        private const string Description = "Description";
        private const string CategoryID = "CategoryID";
        private const string CategoryName = "CategoryName";
        private const string ProductImage = "ProductImage";

        /// <summary>
        /// Gets the product that has the specified ID.
        /// </summary>
        /// <param name="ID">The ID of the product.</param>
        /// <returns>The product that has the specified ID.</returns>
        public Product GetProduct(int ID)
        {
            // Get the dataset.
            DataSet ds = StoreDbDataSet.ReadDataSet();

            // Select a row in the Product table with specified ID.
            // The Select() method will return an array of DataRow[].
            // There is only 1 DataRow object in the returned array, so [0] represents the object here.
            DataRow productRow = ds.Tables[Products].Select("ProductID = " + ID)[0];

            // Initialize and return an instance of Product using the info extracted from dataset.
            Product product = new Product((string)productRow[ModelNumber],
                                          (string)productRow[ModelName],
                                          decimal.Parse(productRow[UnitCost].ToString()),
                                          (string)productRow[Description],
                                          int.Parse(productRow[CategoryID].ToString()),
                                          (string)productRow[CategoryName],
                                          (string)productRow[ProductImage]);
            return product;
        }

        /// <summary>
        /// Returns the collection that contains all the products.
        /// </summary>
        /// <returns>The collection that contains all the products.</returns>
        public ICollection<Product> GetProducts()
        {
            // Get the dataset.
            DataSet ds = StoreDbDataSet.ReadDataSet();

            // Fill the collection of products.
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            foreach (DataRow productRow in ds.Tables[Products].Rows)
            {
                products.Add(new Product((string)productRow[ModelNumber],
                                         (string)productRow[ModelName],
                                         decimal.Parse(productRow[UnitCost].ToString()),
                                         (string)productRow[Description],
                                         int.Parse(productRow[CategoryID].ToString()),
                                         (string)productRow[CategoryName],
                                         (string)productRow[ProductImage]));
            }
            return products;
        }

        /// <summary>
        /// Returns a collection of products in which the unit cost of the product is lager than or equal to the minimum cost.
        /// </summary>
        /// <param name="minCost">The minimum cost</param>
        /// <returns>A collection of products in which the unit cost of the product is lager than or equal to the minimum cost.</returns>
        /// <remarks>
        /// This method is implemented using LINQ.
        /// </remarks>
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

        public ICollection<Product> GetProductsSlow()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            return GetProducts();
        }
    }
}