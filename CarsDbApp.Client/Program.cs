using ConsoleTools;
using DYRQO6_HFT_2022231.Client;
using DYRQO6_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Numerics;

namespace DYRQO6_HFT_2021222.Client
{
    internal class Program
    {
        static RestService rest;
        #region CRUD
        static void Create(string entity)
        {
            if (entity == "Manager")
            {
                Console.Write("Enter manager name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter the manager's age");
                int age = int.Parse(Console.ReadLine());
                rest.Post(new Manager() { Name = name, Age = age }, "manager");
            }
            else if (entity == "Customer")
            {
                Console.Write("Enter customer name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter the customer's address");
                string address = Console.ReadLine();
                Console.WriteLine("Enter the customer' age: ");
                int age = int.Parse(Console.ReadLine());
                rest.Post(new Customer() { Name = name , Address = address, Age = age}, "customer");
            }
            else if (entity == "Cars")
            {
                Console.Write("Enter car brand: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter car color");
                string color = Console.ReadLine();
                Console.WriteLine("Enter car price: ");
                int price = int.Parse(Console.ReadLine());
                rest.Post(new Cars() { CarType = name , Price = price, CarColor = color}, "cars");
            }
            else if (entity == "CarShop")
            {
                Console.Write("Enter a shop name): ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter the shop's address: ");
                string address = Console.ReadLine();
                rest.Post(new CarShop() { Name = name, Address = address}, "shop");
            }
        }
        static void List(string entity)
        {
            if (entity == "Manager")
            {
                List<Manager> managers = rest.Get<Manager>("manager");
                foreach (var item in managers)
                {
                    ;
                    Console.WriteLine(item.ManagerId + " Name: " + item.Name + " Age: " + item.Age);
                }
            }
            Console.ReadLine();

            if (entity == "Customer")
            {
                List<Customer> customers = rest.Get<Customer>("customer");
                foreach (var item in customers)
                {
                    Console.WriteLine(item.CustomerId + " Name: " + item.Name + " Address: " + item.Address + " Age: " + item.Age);
                }
            }
            Console.ReadLine();

            if (entity == "Cars")
            {
                List<Cars> cars = rest.Get<Cars>("cars");
                foreach (var item in cars)
                {
                    Console.WriteLine(item.CarId + " Brand: " + item.CarType + " Price: " + item.Price + " Color: " + item.CarColor);
                }
            }
            Console.ReadLine();

            if (entity == "CarShop")
            {
                List<CarShop> shops = rest.Get<CarShop>("shop");
                foreach (var item in shops)
                {
                    Console.WriteLine(item.ShopId + " Name: " + item.Name + " Address: " + item.Address);
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            if (entity == "Manager")
            {
                Console.Write("Enter manager's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Manager one = rest.Get<Manager>(id, "manager");
                Console.Write($"New name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                Console.Write($"New age [old: {one.Age}]: ");
                int age = int.Parse(Console.ReadLine());
                one.Name = name;
                one.Age = age;
                ;
                rest.Put(one, "manager");
            }

            else if (entity == "Customer")
            {
                Console.Write("Enter customers's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Customer one = rest.Get<Customer>(id, "customer");
                Console.Write($"New name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                Console.Write($"New address [old: {one.Address}]: ");
                string address = Console.ReadLine();
                Console.Write($"New age [old: {one.Age}]: ");
                int age = int.Parse(Console.ReadLine());
                one.Name = name;
                one.Address = address;
                one.Age = age;
                ;
                rest.Put(one, "customer");
                ;
            }

            else if (entity == "Cars")
            {
                Console.Write("Enter the Id of the car you want to update: ");
                int id = int.Parse(Console.ReadLine());
                Cars one = rest.Get<Cars>(id, "cars");
                Console.Write($"New car brand [old: {one.CarType}]: ");
                string brand = Console.ReadLine();
                Console.Write($"New price [old: {one.Price}]: ");
                int price = int.Parse(Console.ReadLine());
                Console.Write($"New car coloer [old: {one.CarColor}]: ");
                string color = Console.ReadLine();
                one.CarType = brand;
                one.Price = price;
                one.CarColor = color;
                rest.Put(one, "cars");
            }

            else if (entity == "CarShop")
            {
                Console.Write("Enter the id of the shop you want to update: ");
                int id = int.Parse(Console.ReadLine());
                CarShop one = rest.Get<CarShop>(id, "shop");
                Console.Write($"New shop name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                Console.Write($"New address [old: {one.Address}]: ");
                string address = Console.ReadLine();
                one.Name = name;
                one.Address = address;
                rest.Put(one, "shop");
            }
            Console.WriteLine(entity + " sucessfully updated");
            Console.ReadLine();
        }
        static void Delete(string entity)
        {
            if (entity == "Manager")
            {
                Console.Write("Enter managers's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "manager");
            }
            else if (entity == "Customer")
            {
                Console.Write("Enter customer's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "customer");
            }
            else if (entity == "Cars")
            {
                Console.Write("Enter the id of the car you want to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "cars");
            }

            else if (entity == "CarShop")
            {
                Console.Write("Enter the id of the shop you want to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "shop");
            }
        }
        #endregion
        #region NONCRUD
        static void GetHighestPaidManager(string endpoint)
        {
            Console.WriteLine("The highest paid manager is:");
            var manager = rest.Get<object>(endpoint);
            foreach (var item in manager)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadKey();
        }
        static void GetCustomerWithMostExpensiveCar(string endpoint)
        {
            Console.WriteLine("The owner of the most expensive car is: ");
            var customer = rest.Get<object>(endpoint);
            foreach (var item in customer)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadKey();
        }
        static void GetCarPurchaseDate(string endpoint)
        {
            Console.WriteLine("Purchase date of the oldest car: ");
            var purchasedate = rest.Get<object>(endpoint);
            foreach (var item in purchasedate)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadKey();
        }

        static void GetYoungestWithCar(string endpoint)
        {
            Console.WriteLine("Youngest person with a car");
            var youngest = rest.Get<object>(endpoint);
            foreach (var item in youngest)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadKey();
        }

        static void ShopWithBmw(string endpoint)
        {
            Console.WriteLine("The shop which sells a BMW");
            var car = rest.Get<object>(endpoint);
            
            foreach (var item in car)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadKey();
        }
        #endregion
        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:18906/", "cars");
            #region Menu
            var managerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Manager"))
                .Add("Create", () => Create("Manager"))
                .Add("Delete", () => Delete("Manager"))
                .Add("Update", () => Update("Manager"))
                .Add("Exit", ConsoleMenu.Close);

            var shopSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("CarShop"))
                .Add("Create", () => Create("CarShop"))
                .Add("Delete", () => Delete("CarShop"))
                .Add("Update", () => Update("CarShop"))
                .Add("Exit", ConsoleMenu.Close);

            var carsSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Cars"))
                .Add("Create", () => Create("Cars"))
                .Add("Delete", () => Delete("Cars"))
                .Add("Update", () => Update("Cars"))
                .Add("Exit", ConsoleMenu.Close);

            var customerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Customer"))
                .Add("Create", () => Create("Customer"))
                .Add("Delete", () => Delete("Customer"))
                .Add("Update", () => Update("Customer"))
                .Add("Exit", ConsoleMenu.Close);

            var nonCrudSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Highest paid Manager", () => GetHighestPaidManager("stat/gethighestpaidmanager"))
                .Add("Customer of the most expensive car", () => GetCustomerWithMostExpensiveCar("stat/getcustomerwithmostexpensivecar"))
                .Add("Purchase date of specified car", () => GetCarPurchaseDate("stat/getcarpurchasedate"))
                .Add("Youngest car owner", () => GetYoungestWithCar("stat/getyoungestwithcar"))
                .Add("Most expensive car in the shop", () => ShopWithBmw("stat/shopwithbmw"))
                .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Customers", () => customerSubMenu.Show())
                .Add("Cars", () => carsSubMenu.Show())
                .Add("Managers", () => managerSubMenu.Show())
                .Add("Shops", () => shopSubMenu.Show())
                .Add("NonCrudMethods", () => nonCrudSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
            #endregion
        }
    }
}
