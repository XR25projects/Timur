using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }

    public Product(string name, double price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
    }
}

class ShoppingCartItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }

    public ShoppingCartItem(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }
}

class Program
{
    static List<Product> products = new List<Product>
    {
        new Product("Product 1", 10.0, 5),
        new Product("Product 2", 15.0, 10),
        new Product("Product 3", 20.0, 3),
    };

    static List<ShoppingCartItem> shoppingCart = new List<ShoppingCartItem>();

    static double userCurrency = 100.0;

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Available Products:");
            ListAvailableProducts();

            Console.WriteLine("\n1. Purchase Product");
            Console.WriteLine("2. View Shopping Cart");
            Console.WriteLine("3. Get Product Info");
            Console.WriteLine("4. Exit");

            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PurchaseProduct();
                    break;
                case "2":
                    ViewShoppingCart();
                    break;
                case "3":
                    GetProductInfo();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void ListAvailableProducts()
    {
        foreach (var product in products)
        {
            Console.WriteLine($"{product.Name} - Price: ${product.Price} - Stock: {product.Stock}");
        }
    }

    static void PurchaseProduct()
    {
        Console.Write("Enter the name of the product you want to purchase: ");
        string productName = Console.ReadLine();
        Product product = products.Find(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));

        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        Console.Write("Enter the quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        if (product.Stock < quantity)
        {
            Console.WriteLine("Insufficient stock.");
            return;
        }

        double totalPrice = product.Price * quantity;

        if (totalPrice > userCurrency)
        {
            Console.WriteLine("Not enough currency to purchase.");
            return;
        }

        userCurrency -= totalPrice;
        product.Stock -= quantity;

        ShoppingCartItem cartItem = shoppingCart.Find(item => item.Product.Name.Equals(product.Name));
        if (cartItem != null)
        {
            cartItem.Quantity += quantity;
        }
        else
        {
            shoppingCart.Add(new ShoppingCartItem(product, quantity));
        }

        Console.WriteLine($"Successfully purchased {quantity} {product.Name}(s) for ${totalPrice}. Remaining currency: ${userCurrency}");
    }

    static void ViewShoppingCart()
    {
        Console.WriteLine("Shopping Cart:");
        foreach (var item in shoppingCart)
        {
            Console.WriteLine($"{item.Product.Name} - Quantity: {item.Quantity}");
        }
    }

    static void GetProductInfo()
    {
        Console.Write("Enter the name of the product you want information about: ");
        string productName = Console.ReadLine();
        Product product = products.Find(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));

        if (product != null)
        {
            Console.WriteLine($"Name: {product.Name}");
            Console.WriteLine($"Price: ${product.Price}");
            Console.WriteLine($"Stock: {product.Stock}");
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }
}