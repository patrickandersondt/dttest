
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }

        public Product(string name, decimal cost)
        {
            Name = name;
            Cost = cost;
        }
    }

    public interface IOffer
    {
        decimal CalculateDiscount(Basket basket);
    }

    public class Basket
    {
        private List<Product> products = new List<Product>();
        private List<IOffer> offers = new List<IOffer>();

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public void ApplyOffer(IOffer offer)
        {
            offers.Add(offer);
        }

        public decimal CalculateTotal()
        {
            decimal total = products.Sum(p => p.Cost);
            foreach (var offer in offers)
            {
                total -= offer.CalculateDiscount(this);
            }
            return total;
        }

        public int GetProductCount(string productName)
        {
            return products.Count(p => p.Name == productName);
        }
    }

    public class ButterBreadOffer : IOffer
    {
        public decimal CalculateDiscount(Basket basket)
        {
            int butterCount = basket.GetProductCount("Butter");
            int breadCount = basket.GetProductCount("Bread");

            int eligibleDiscounts = Math.Min(butterCount / 2, breadCount);

            return eligibleDiscounts * 0.50m; // 50% off bread
        }
    }

    public class MilkOffer : IOffer
    {
        public decimal CalculateDiscount(Basket basket)
        {
            int milkCount = basket.GetProductCount("Milk");
            return (milkCount / 4) * 1.15m; // One free milk for every 4 milks
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Scenario 1
            Basket basket1 = new Basket();
            basket1.AddProduct(new Product("Bread", 1.00m));
            basket1.AddProduct(new Product("Butter", 0.80m));
            basket1.AddProduct(new Product("Milk", 1.15m));
            Console.WriteLine($"Total for basket 1: £{basket1.CalculateTotal()}");

            // Scenario 2
            Basket basket2 = new Basket();
            basket2.AddProduct(new Product("Butter", 0.80m));
            basket2.AddProduct(new Product("Butter", 0.80m));
            basket2.AddProduct(new Product("Bread", 1.00m));
            basket2.AddProduct(new Product("Bread", 1.00m));
            basket2.ApplyOffer(new ButterBreadOffer());
            Console.WriteLine($"Total for basket 2: £{basket2.CalculateTotal()}");

            // Scenario 3
            Basket basket3 = new Basket();
            for (int i = 0; i < 4; i++)
            {
                basket3.AddProduct(new Product("Milk", 1.15m));
            }
            basket3.ApplyOffer(new MilkOffer());
            Console.WriteLine($"Total for basket 3: £{basket3.CalculateTotal()}");

            // Scenario 4
            Basket basket4 = new Basket();
            basket4.AddProduct(new Product("Butter", 0.80m));
            basket4.AddProduct(new Product("Butter", 0.80m));
            basket4.AddProduct(new Product("Bread", 1.00m));
            for (int i = 0; i < 8; i++)
            {
                basket4.AddProduct(new Product("Milk", 1.15m));
            }
            basket4.ApplyOffer(new ButterBreadOffer());
            basket4.ApplyOffer(new MilkOffer());
            Console.WriteLine($"Total for basket 4: £{basket4.CalculateTotal()}");
        }
    }
}
