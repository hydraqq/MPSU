using System;
using System.Collections.Generic;

namespace ShopApp
{
    public class Product
    {
        public string ArticleNumber { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public Product(string articleNumber, decimal price, int stockQuantity)
        {
            ArticleNumber = articleNumber;
            Price = price;
            StockQuantity = stockQuantity;
        }

        public bool Sell(int quantity)
        {
            if (quantity > StockQuantity)
            {
                Console.WriteLine($"Недостаточно товара на складе! Доступно: {StockQuantity}");
                return false;
            }

            StockQuantity -= quantity;
            return true;
        }

        public override string ToString()
        {
            return $"Артикул: {ArticleNumber}, Цена: {Price:C}, Остаток: {StockQuantity} шт.";
        }
    }

    public class Customer
    {
        public string Name { get; set; }
        public decimal TotalSpent { get; private set; }
        public List<Product> PurchasedProducts { get; private set; }

        public Customer(string name)
        {
            Name = name;
            TotalSpent = 0;
            PurchasedProducts = new List<Product>();
        }

        public void BuyProduct(Product product, int quantity)
        {
            if (product.Sell(quantity))
            {
                decimal cost = product.Price * quantity;
                TotalSpent += cost;
                PurchasedProducts.Add(product);
                Console.WriteLine($"{Name} купил(а) {quantity} шт. товара {product.ArticleNumber} на сумму {cost:C}");
            }
        }

        public override string ToString()
        {
            return $"Покупатель: {Name}, Потрачено: {TotalSpent:C}, Куплено товаров: {PurchasedProducts.Count}";
        }
    }

    public class Shop
    {
        public string Name { get; set; }
        public double Area { get; set; }
        public List<Product> Products { get; private set; }

        public Shop(string name, double area)
        {
            Name = name;
            Area = area;
            Products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
            Console.WriteLine($"Товар {product.ArticleNumber} добавлен в магазин {Name}");
        }

        public void ShowProducts()
        {
            Console.WriteLine($"\n=== Товары магазина '{Name}' ===");
            foreach (var product in Products)
            {
                Console.WriteLine(product);
            }
        }

        public override string ToString()
        {
            return $"Магазин: {Name}, Площадь: {Area} м², Товаров в ассортименте: {Products.Count}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Shop shop = new Shop("Пятёрочка", 150.5);
            Console.WriteLine(shop);
            Console.WriteLine();

            Product product1 = new Product("ART001", 99.90m, 50);
            Product product2 = new Product("ART002", 249.50m, 30);
            Product product3 = new Product("ART003", 1599.00m, 15);

            shop.AddProduct(product1);
            shop.AddProduct(product2);
            shop.AddProduct(product3);

            shop.ShowProducts();
            Console.WriteLine();

            Customer customer1 = new Customer("Иван");
            Customer customer2 = new Customer("Мария");

            Console.WriteLine("\n=== Покупки ===");

            customer1.BuyProduct(product1, 2);
            customer1.BuyProduct(product3, 1);

            customer2.BuyProduct(product2, 3);
            customer2.BuyProduct(product1, 5);

            Console.WriteLine("\n=== Информация о покупателях ===");
            Console.WriteLine(customer1);
            Console.WriteLine(customer2);

            shop.ShowProducts();

            Console.ReadKey();
        }
    }
}