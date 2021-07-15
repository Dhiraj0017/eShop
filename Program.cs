using System;
using System.Collections.Generic;
using System.Linq;

namespace eShop
{
    class Program
    {
        
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>
            {
                new Product{ID=1, Name="Cola - Cold",Type="Drink",Cost=0.50},
                new Product{ID=2, Name="Coffee - Hot",Type="Drink",Cost=1.00},
                new Product{ID=3, Name="Cheese Sandwich - Cold",Type="Food",Cost=2.00},
                new Product{ID=4, Name="Steak Sandwich - Hot ",Type="Food",Cost=4.50}
            };
            Console.WriteLine("Please Enter Your Order");
            List<Order> orders = new List<Order>();
            foreach(var product in products)
            {
                Order order = new Order();
                order.ProductID = product.ID;
                Console.Write($"{product.Name} :");
                var qunatity = Console.ReadLine();
                int iQunatity=0;
                if (string.IsNullOrEmpty(qunatity) || qunatity == "0" || !int.TryParse(qunatity, out iQunatity))
                    order.Quantity = 0;
                else
                    order.Quantity = iQunatity;
                order.Total_price = order.Quantity * product.Cost;
                if(order.Quantity > 0)
                    orders.Add(order);
            }

            var ServiceCharge = 0.00;
            var ProductsTotal = orders.Sum(s => s.Total_price);
            if (orders.Join(products, a => a.ProductID, b => b.ID,(a,b)=>new {a,b}).Any(s=>s.b.Type=="Food"))
            {
                ServiceCharge = Math.Round((ProductsTotal * 10) / 100,2);
            }

            Console.WriteLine("Bill: ");
            foreach(var order in orders)
            {
                Console.WriteLine(products.FirstOrDefault(s=>s.ID ==order.ProductID).Name +":$" +order.Total_price);
            }
            Console.WriteLine($"Service Charge: ${ServiceCharge} ");
            Console.WriteLine($"Total: ${ServiceCharge +ProductsTotal} ");
            Console.ReadLine();

        }

        
    }

    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Cost { get; set; }
    }
    public class Order
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public double Total_price {get;set;}

    }
}
