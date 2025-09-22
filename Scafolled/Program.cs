using Microsoft.EntityFrameworkCore;
using Scafolled.Data;
using Scafolled.Models;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Scafolled
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new ApplicationDbContext();

            // 1- List all customers' first and last names along with their email addresses.

            var customers = context.Customers
                .Select(c => new
                {
                    FullName = c.FirstName + " " + c.LastName,
                    c.Email
                })
                .ToList();

            Console.WriteLine("1) Customers with Email:");
            foreach (var c in customers)
                Console.WriteLine($"{c.FullName} - {c.Email}");
            Console.WriteLine("------------------------------------");



            // 2- Retrieve all orders processed by a specific staff member (e.g., staff_id = 3).

            var staffOrders = context.Orders
                .Where(o => o.StaffId == 3)
                .ToList();

            Console.WriteLine("2) Orders processed by staff #3:");
            foreach (var o in staffOrders)
                Console.WriteLine($"OrderId: {o.OrderId}, CustomerId: {o.CustomerId}");
            Console.WriteLine("------------------------------------");



            // 3- Get all products that belong to a category named "Mountain Bikes".

            var mountainBikes = context.Products
                .Where(p => p.Category.CategoryName == "Mountain Bikes")
                .ToList();

            Console.WriteLine("3) Mountain Bikes:");
            foreach (var p in mountainBikes)
                Console.WriteLine(p.ProductName);
            Console.WriteLine("------------------------------------");



            // 4- Count the total number of orders per store.

            var ordersPerStore = context.Orders
                .GroupBy(o => o.StoreId)
                .Select(g => new { StoreId = g.Key, TotalOrders = g.Count() })
                .ToList();

            Console.WriteLine("4) Orders per Store:");
            foreach (var s in ordersPerStore)
                Console.WriteLine($"Store {s.StoreId} -> Orders: {s.TotalOrders}");
            Console.WriteLine("------------------------------------");



            // 5- List all orders that have not been shipped yet.

            var unshippedOrders = context.Orders
                .Where(o => o.ShippedDate == null)
                .ToList();

            Console.WriteLine("5) Unshipped Orders:");
            foreach (var o in unshippedOrders)
                Console.WriteLine($"OrderId: {o.OrderId}");
            Console.WriteLine("------------------------------------");



            // 6- Display each customer’s full name and the number of orders they have placed.

            var customerOrders = context.Customers
                .Select(c => new
                {
                    FullName = c.FirstName + " " + c.LastName,
                    OrdersCount = c.Orders.Count()
                })
                .ToList();

            Console.WriteLine("6) Customers & Number of Orders:");
            foreach (var c in customerOrders)
                Console.WriteLine($"{c.FullName} -> {c.OrdersCount}");
            Console.WriteLine("------------------------------------");



            // 7- List all products that have never been ordered.

            var productsNotOrdered = context.Products
                .Where(p => !p.OrderItems.Any())
                .ToList();

            Console.WriteLine("7) Products never ordered:");
            foreach (var p in productsNotOrdered)
                Console.WriteLine(p.ProductName);
            Console.WriteLine("------------------------------------");



            // 8- Display products that have a quantity of less than 5 in any store stock.

            var lowStockProducts = context.Stocks
                .Where(s => s.Quantity < 5)
                .Select(s => s.Product)
                .Distinct()
                .ToList();

            Console.WriteLine("8) Products with stock < 5:");
            foreach (var p in lowStockProducts)
                Console.WriteLine(p.ProductName);
            Console.WriteLine("------------------------------------");



            // 9- Retrieve the first product from the products table.

            var firstProduct = context.Products.FirstOrDefault();
            Console.WriteLine($"9) First Product: {firstProduct?.ProductName}");
            Console.WriteLine("------------------------------------");



            // 10- Retrieve all products from the products table with a certain model year.

            int modelYear = 2023;
            var productsByYear = context.Products
                .Where(p => p.ModelYear == modelYear)
                .ToList();

            Console.WriteLine($"10) Products with Model Year {modelYear}:");
            foreach (var p in productsByYear)
                Console.WriteLine(p.ProductName);
            Console.WriteLine("------------------------------------");



            // 11- Display each product with the number of times it was ordered.

            var productOrdersCount = context.Products
                .Select(p => new
                {
                    p.ProductName,
                    OrdersCount = p.OrderItems.Count()
                })
                .ToList();

            Console.WriteLine("11) Product Orders Count:");
            foreach (var p in productOrdersCount)
                Console.WriteLine($"{p.ProductName} -> {p.OrdersCount}");
            Console.WriteLine("------------------------------------");



            // 12- Count the number of products in a specific category.

            int categoryId = 3;
            var productCountInCategory = context.Products
                .Count(p => p.CategoryId == categoryId);

            Console.WriteLine($"12) Products Count in Category {categoryId}: {productCountInCategory}");
            Console.WriteLine("------------------------------------");



            // 13- Calculate the average list price of products.

            var avgPrice = context.Products.Average(p => p.ListPrice);
            Console.WriteLine($"13) Average List Price: {avgPrice}");
            Console.WriteLine("------------------------------------");



            // 14- Retrieve a specific product from the products table by ID.

            int productId = 10;
            var specificProduct = context.Products.FirstOrDefault(p => p.ProductId == productId);
            Console.WriteLine($"14) Product #{productId}: {specificProduct?.ProductName}");
            Console.WriteLine("------------------------------------");



            // 15- List all products that were ordered with a quantity greater than 3 in any order.

            var productsOrderedWithQuantityGreaterThan3 = context.OrderItems
                .Where(oi => oi.Quantity > 3)
                .Select(oi => oi.Product)
                .Distinct()
                .ToList();

            Console.WriteLine("15) Products ordered with quantity > 3:");
            foreach (var p in productsOrderedWithQuantityGreaterThan3)
                Console.WriteLine(p.ProductName);
            Console.WriteLine("------------------------------------");



            // 16- Display each staff member’s name and how many orders they processed.

            var staffOrdersCount = context.Staffs
                .Select(s => new
                {
                    FullName = s.FirstName + " " + s.LastName,
                    OrdersProcessed = s.Orders.Count()
                })
                .ToList();

            Console.WriteLine("16) Staff & Orders Processed:");
            foreach (var s in staffOrdersCount)
                Console.WriteLine($"{s.FullName} -> {s.OrdersProcessed}");
            Console.WriteLine("------------------------------------");



            // 17- List active staff members only (active = true) along with their phone numbers.

            var activeStaff = context.Staffs
                .Where(s => s.Active == 1)
                .Select(s => new
                {
                    FullName = s.FirstName + " " + s.LastName,
                    s.Phone
                })
                .ToList();

            Console.WriteLine("17) Active Staff Members:");
            foreach (var s in activeStaff)
                Console.WriteLine($"{s.FullName} - {s.Phone}");
            Console.WriteLine("------------------------------------");



            // 18- List all products with their brand name and category name.

            var productsWithBrandAndCategory = context.Products
                .Select(p => new
                {
                    p.ProductName,
                    BrandName = p.Brand.BrandName,
                    CategoryName = p.Category.CategoryName
                })
                .ToList();

            Console.WriteLine("18) Products with Brand & Category:");
            foreach (var p in productsWithBrandAndCategory)
                Console.WriteLine($"{p.ProductName} - {p.BrandName} - {p.CategoryName}");
            Console.WriteLine("------------------------------------");
            //var completedOrders = context.Orders
            //    .Where(o => o.OrderStatus == "Completed")


             //===============================================
             //19 - Retrieve orders that are completed.
             //===============================================
            var completedOrders = context.Orders
                //completed = 1
                .Where(o => o.OrderStatus == 1)
                .ToList();
            Console.WriteLine("Completed Orders:");
            foreach (var o in completedOrders)
                Console.WriteLine($"OrderId: {o.OrderId}, CustomerId: {o.CustomerId}");



            // 20- List each product with the total quantity sold.

            var productsQuantitySold = context.Products
                .Select(p => new
                {
                    p.ProductName,
                    TotalQuantitySold = p.OrderItems.Sum(oi => (int?)oi.Quantity) ?? 0
                })
                .ToList();

            Console.WriteLine("20) Products with Total Quantity Sold:");
            foreach (var p in productsQuantitySold)
                Console.WriteLine($"{p.ProductName} -> {p.TotalQuantitySold}");
            Console.WriteLine("------------------------------------");






        }
    }
}
