using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            EntityService.ServiceClient client = new EntityService.ServiceClient();

            //Adding primary categories
            client.Add("Clothing","CLOTH","All types of clothing",0,string.Empty,string.Empty,"PC");
            client.Add("Food and beverages", "F&B", "All food and related products", 0, string.Empty, string.Empty, "PC");
            client.Add("Electronics", "ELECTRONICS", "All electronic items", 0, string.Empty, string.Empty, "PC");

            //Adding secondary categories
            client.Add("Men Shirts", "SHIRT-MEN", "Shirts for men", 0, "CLOTH", string.Empty, "SC");
            client.Add("Womnen Shirts", "SHIRT-WOMEN", "Shirts for women", 0, "CLOTH", string.Empty, "SC");
            client.Add("Rice", "RICE", "Rice and its items", 0, "F&B", string.Empty, "SC");
            client.Add("Mobiles", "MOB", "Mobiles", 0, "ELECTRONICS", string.Empty, "SC");

            //Adding products
             client.Add("Allen Solly Shirts", "AS-SHIRT", "Allen Solly Shirts", 0, string.Empty, "SHIRT-MEN", "PR");
             client.Add("Sona Masuri Rice", "SM-RICE", "Different varieties of Sona masuri Rice", 0, string.Empty, "RICE", "PR");
             client.Add("Smart Phones", "SM-MOB", "Different smart phones", 0, string.Empty, "MOB", "PR");

            //Adding Schemes
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now.AddDays(2);
            client.AddScheme("Heavy Discount on rice", "DC-RICE", "Buy 8 kg and get 1 kg free", fromDate, toDate, false, 12.5m, 8, 0, string.Empty, string.Empty, "SM-RICE", string.Empty);

            Console.WriteLine("Primary categories are added");
            Console.WriteLine("Secondary categories are added");
            Console.WriteLine("Products are added");
            

            //get scheme 
            
            var Schemelist = client.GetScheme(string.Empty, string.Empty, "SM-RICE");

            EntityService.ProductsAndQty  obj = new EntityService.ProductsAndQty();
            obj.orderedQty= 13;
            obj.productCode="SM-RICE";

            EntityService.ProductsAndQty[] objList = new EntityService.ProductsAndQty[1];
            objList[0] = obj;
            var SchemeListForProducts = client.GetSchemeForProducts(objList);

            Console.ReadLine();





        }
    }
}
