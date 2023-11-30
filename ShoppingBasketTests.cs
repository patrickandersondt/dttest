
using NUnit.Framework;
using ShoppingBasket;
using System;

namespace ShoppingBasketTests
{
    [TestFixture]
    public class BasketTests
    {
        private Product butter;
        private Product milk;
        private Product bread;

        [SetUp]
        public void Setup()
        {
            // Initialize common products
            butter = new Product("Butter", 0.80m);
            milk = new Product("Milk", 1.15m);
            bread = new Product("Bread", 1.00m);
        }

        [Test]
        public void Total_NoOffers_ShouldEqualSumOfProducts()
        {
            var basket = new Basket();
            basket.AddProduct(bread);
            basket.AddProduct(butter);
            basket.AddProduct(milk);

            decimal total = basket.CalculateTotal();
            Assert.AreEqual(2.95m, total);
        }

        [Test]
        public void Total_ButterBreadOffer_ShouldApplyDiscount()
        {
            var basket = new Basket();
            basket.AddProduct(butter);
            basket.AddProduct(butter);
            basket.AddProduct(bread);
            basket.AddProduct(bread);
            basket.ApplyOffer(new ButterBreadOffer());

            decimal total = basket.CalculateTotal();
            Assert.AreEqual(3.10m, total);
        }

        [Test]
        public void Total_MilkOffer_ShouldApplyDiscount()
        {
            var basket = new Basket();
            for (int i = 0; i < 4; i++)
            {
                basket.AddProduct(milk);
            }
            basket.ApplyOffer(new MilkOffer());

            decimal total = basket.CalculateTotal();
            Assert.AreEqual(3.45m, total);
        }

        [Test]
        public void Total_MultipleOffers_ShouldApplyAllDiscounts()
        {
            var basket = new Basket();
            basket.AddProduct(butter);
            basket.AddProduct(butter);
            basket.AddProduct(bread);
            for (int i = 0; i < 8; i++)
            {
                basket.AddProduct(milk);
            }
            basket.ApplyOffer(new ButterBreadOffer());
            basket.ApplyOffer(new MilkOffer());

            decimal total = basket.CalculateTotal();
            Assert.AreEqual(9.00m, total);
        }
    }
}
