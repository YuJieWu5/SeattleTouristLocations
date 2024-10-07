using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
    /// <summary>
    /// The file service class that acts as an intermediary between the pages and data store
    /// </summary>
    
   public class JsonFileProductService
    {
        /// <summary>
        /// contructor for the JsonFileProductService class, sets the webhost environment
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        ///the web host environment of the website, provides information about the web
        ///host environment the application is running in
        
        public IWebHostEnvironment WebHostEnvironment { get; }

        ///the filename of the data store being used by the website
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        /// <summary>
        /// Obtains a list of all of the products from the data store, in a useable IEnumerable format
        /// </summary>
        /// <returns> IEnumerable<ProductModel> </returns>
        
        public IEnumerable<ProductModel> GetProducts()
        {
            using(var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<ProductModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {

                        PropertyNameCaseInsensitive = true

                    });

            }
        }

        /// <summary>
        /// Adds a user submitted rating to the current list of ratings for a product with a specified Id
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        
        public bool AddRating(string productId, int rating)
        {
            /// If the ProductID is invalid, return
            
            if (string.IsNullOrEmpty(productId))
            {
                return false;
            }

            var products = GetProducts();

            /// Look up the product, if it does not exist, return
            
            var data = products.FirstOrDefault(x => x.Id.Equals(productId));

            if (data == null)
            {
                return false;
            }

            /// Check Rating for boundaries, do not allow ratings below 0
            
            if (rating < 0)
            {
                return false;
            }

            /// Check Rating for boundaries, do not allow ratings above 5
            
            if (rating > 5)
            {
                return false;
            }

            /// Check to see if the rating exist, if there are none, then create the array
            
            if (data.Ratings == null)
            {
                data.Ratings = new int[] { };
            }

            /// Add the Rating to the Array
            
            var ratings = data.Ratings.ToList();
            ratings.Add(rating);
            data.Ratings = ratings.ToArray();

            /// Save the data back to the data store
            
            SaveData(products);

            return true;
        }

        /// <summary>
        /// Save all products data to storage
        /// /// <param name="products"></param>
        /// </summary>
        
        public void SaveData(IEnumerable<ProductModel> products)
        {
            
            using (var outputStream = File.Create(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<ProductModel>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    products
                );
            }
        }

        /// <summary>
        /// Update the esisting data with the new data, then save the new data
        /// </summary>
        /// <param name="revised_product"></param>
        /// <returns> boolean value, indicating operation was a success </returns>
        
        public bool UpdateData(ProductModel revised_product)
        {
            ///verify the item exist
            
            if (revised_product.Id == null)
                return false;

            ///obtain the list of products
            
            var products = GetProducts();

            ///from the list of products obtain the product that matches the passed in Id
            
            var original_product = products.First(x => x.Id == revised_product.Id);

            ///update any of the changed fields inside of that product
            ///currently no error checking has been implemented
            ///(it might make more sense to add a method inside of the productmodel class to overwrite all changeable atributes, then you could just call that here)
            
            original_product.Location = revised_product.Location;
            original_product.Image = revised_product.Image;
            original_product.Title = revised_product.Title;
            original_product.Description = revised_product.Description;
            original_product.ContactNumber = revised_product.ContactNumber;
            original_product.E_link = revised_product.E_link;
            original_product.LocationCategory = revised_product.LocationCategory;
            
            ///save the data after updating
            
            SaveData(products);
            
            return true;
        }

        /// <summary>
        /// Delete the selected data, then save the remaining data
        /// </summary>
        /// <param name="product"></param>
        /// <returns> boolean value, indicating operation was a success </returns>
        
        public bool DeleteData(ProductModel product)
        {
            ///obtain the list of products
            
            var products = GetProducts();

            ///check whether the id is not null
            
            if (product.Id == null)
            {
                return false;
            }

            var pd = products.FirstOrDefault(x => x.Id == product.Id);
            if (pd == null)
            {
                return false;
            }

            ///filter locations based on the selected location id

            var new_products = products.Where(x => x.Id != product.Id);
            
            ///after getting the new location  list, pass the list to SaveData function
            
            SaveData(new_products);
                
            return true;
        }

        /// <summary>
        /// Populate the new product model, which is then saved to the data store
        /// </summary>
        /// <param name="product"></param>
        /// <returns> boolean value, indicating operation was a success </returns>
        
        public bool CreateData(ProductModel product)
        {
            ///obtain the list of products
            
            var products = GetProducts();

            ///obtain the current highest Id value from the existing products
            
            int Highest_Id = 0;
            
            foreach (ProductModel Current_Product in products)
            {
                int Current_Id = int.Parse(Current_Product.Id);
                
                if (Current_Id > Highest_Id)
                {
                    Highest_Id = Current_Id;
                }
            }
            ///make the new Id one higher than the highest existing ID value
            
            int New_Id = ++Highest_Id;

            ///set any of the values that are not user defined on the front end
            
            product.Id = New_Id.ToString();
            
            ///LocationCategory (handled on FE)
            ///location (handled on FE)
            ///img (handled on FE)
            ///url (handled on FE)
            ///title (handled on FE)
            ///description (handled on FE)
            
            product.Ratings = null;
            
            ///add the completed product to the current list of products and save it back to the data store
            
            var new_products = products.Append(product);
            
            SaveData(new_products);

            ///signal the method completed without error
            
            return true;
        }

    }

}