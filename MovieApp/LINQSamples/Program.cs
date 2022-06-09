using LINQSamples.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQSamples
{
    class CustomerModel
    {
        public CustomerModel()
        {
            this.Orders = new List<OrderModel>();
        }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int OrderCount { get; set; }
        public List<OrderModel> Orders { get; set; }

    }
    class OrderModel
    {
        public int OrderId { get; set; }
        public decimal Total { get; set; }
        public List<ProductModel> Products { get; set; }

    }


    public class ProductModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        //public int Quantity { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //using (var db = new CustomNorthWindContext())
            //{
            //    //var sonuc = db.Database.ExecuteSqlRaw("delete from products where productId=81");
            //    //var sonuc = db.Database.ExecuteSqlRaw("update products set unitprice=unitprice*1.2 where categoryId=4");
            //    //var query = "4";

            //    //var products = db.Products.FromSqlRaw($"select * from products where categoryId={query}").ToList();


            //    //var products = db.ProductModels.FromSqlRaw("select ProductId,ProductName,UnitPrice from Products").ToList();

            //    //foreach (var item in products)
            //    //{
            //    //    Console.WriteLine(item.Name + " => " + item.Price);
            //    //}


            //}
        }

        private static void Ders240BirdenFazlaTabloIleCalısma(NorthwindContext db)
        {
            //var customers = db.Customers
            //    .Where(cus => cus.CustomerId == "PERIC")
            //    .Select(cus => new CustomerModel
            //    {
            //        CustomerId = cus.CustomerId,
            //        CustomerName = cus.ContactName,
            //        OrderCount = cus.Orders.Count,
            //        Orders = cus.Orders.Select(order => new OrderModel
            //        {
            //            OrderId = order.OrderId,
            //            Total = order.OrderDetails.Sum(od => od.Quantity * od.UnitPrice),
            //            Products = order.OrderDetails.Select(od => new ProductModel
            //            {
            //                ProductId = od.ProductId,
            //                Name = od.Product.ProductName,
            //                Price = od.UnitPrice,
            //                //Quantity = od.Quantity
            //            }).ToList()
            //        }).ToList()
            //    })
            //    .OrderBy(i => i.OrderCount)
            //    .ToList();


            //foreach (var customer in customers)
            //{
            //    Console.WriteLine(customer.CustomerId + " => " + customer.CustomerName + " => " + customer.OrderCount);
            //    Console.WriteLine("Siparişler");
            //    foreach (var order in customer.Orders)
            //    {
            //        Console.WriteLine("***************");
            //        Console.WriteLine(order.OrderId + "=>" + order.Total);
            //        foreach (var product in order.Products)
            //        {
            //            Console.WriteLine(product.ProductId + "=>" + product.Name + "=>" + product.Price + "=>");
            //        }

            //    }
            //}
        }
        private static void Ders239BirdenFazlaTabloIleCalısma()
        {
            using (var db = new NorthwindContext())
            {
                ///*var products = db.Products.Include(p=>p.Category).Where(p => p.Category.CategoryName == "Beverages").ToList();*///navigation property ınclude
                //var products = db.Products
                //    .Where(p => p.Category.CategoryName == "Beverages")
                //    .Select(p=> new {name = p.ProductName, id=p.CategoryId,categoryname=p.Category.CategoryName})

                //foreach (var p in products)
                //{
                //    Console.WriteLine(p.name + " " + p.id + " " + p.categoryname);
                //}

                //var categories = db.Categories.Where(c => c.Products.Count() == 0).ToList();
                //var categories = db.Categories.Where(c => c.Products.Any()).ToList();

                //var products = db.Products.Select(p =>  //LEFT JOIN OPARATION
                //new {
                //    companyName = p.Supplier.CompanyName, 
                //    contactName = p.Supplier.ContactName,
                //    p.ProductName
                //}).ToList();
                //extension methods bu zamana kadar gördüklerimiz
                //query expressions göreceğimiz

                //var products = (from p in db.Products
                //                where p.UnitPrice>10
                //                select p).ToList();//same 
                //db.Products.Where(p=>p.Unitprice>10).ToList();//same

                var products = (from p in db.Products
                                join s in db.Suppliers on p.SupplierId equals s.SupplierId //INNER JOIN OPARATION
                                select new
                                {
                                    p.ProductName,
                                    contactName = s.ContactName,
                                    companyName = s.CompanyName

                                }).ToList();


                foreach (var item in products)
                {
                    Console.WriteLine(item.ProductName + " " + item.companyName + " " + item.contactName);
                }


            }

            Console.ReadLine();
        }
        private static void Ders238Silme2(NorthwindContext db)
        {
            /*var p = new Product() { ProductId = 86 };*/ // cache tracking method , üzerinde dur

            var p1 = new Product() { ProductId = 85 };
            var p2 = new Product() { ProductId = 84 };

            var products = new List<Product>() { p1, p2 };




            //db.Entry(p).State = EntityState.Deleted;
            db.Products.RemoveRange(products);
            db.SaveChanges();
        }
        private static void Ders238Sılme(NorthwindContext db)
        {
            var p = db.Products.Find(88);

            if (p != null)
            {
                db.Products.Remove(p);
                db.SaveChanges();
            }
        }
        private static void DersGuncelleme3(NorthwindContext db)
        {
            var product = db.Products.Find(1);

            Console.WriteLine(product.Category.CategoryName);

            if (product != null)
            {
                product.UnitPrice = 28;
            }
            db.Update(product);
            db.SaveChanges();
        }
        private static void Dersguncelleme2(NorthwindContext db)
        {
            var p = new Product() { ProductId = 1 };
            db.Products.Attach(p);

            p.UnitsInStock += 50;
            db.SaveChanges();
        }
        private static void Ders237Guncelleme(NorthwindContext db)
        {
            //    //change tracking
            //    var product = db.Products
            //        //.AsNoTracking()
            //        .FirstOrDefault(p => p.ProductId == 1);

            //    if (product != null)
            //    {
            //        product.UnitsInStock += 10;
            //    }
            //    db.SaveChanges();
            //    Console.WriteLine("Veri Güncellendi");
            //}
        }
        private static void Ders236KayıtEkleme(NorthwindContext db)
        {
            //var category = db.Categories.Where(i => i.CategoryName == "Beverages").FirstOrDefault();
            //var p1 = new Product() { ProductName = "Yeni ürün 11" };
            //var p2 = new Product() { ProductName = "Yeni ürün 12 " };
            //var p1 = new Product() { ProductName = "Yeni ürün 7",Category=new Category() { CategoryName="Yeni Kategori1" } };
            //var p2 = new Product() { ProductName = "Yeni ürün 8 " ,Category= new Category() { CategoryName = "Yeni Kategori2" } };

            //var products = new List<Product>()
            //{
            //    p1,p2
            //};

            //category.Products.Add(p1);
            //category.Products.Add(p2);


            //db.Products.AddRange(products);*/
            //db.SaveChanges();

            //Console.WriteLine("Veriler eklendi");
            //Console.WriteLine(p1.ProductId);
            //Console.WriteLine(p2.ProductId);
        }
        private static void Ders235uygulamasıralamahesaplamasorgu(NorthwindContext db)
        {
            //var result = db.Products.Count();
            //var result = db.Products.Count(i=>i.UnitPrice>10 && i.UnitPrice<30);
            //var results = db.Products.Count(i => !i.Discontinued);
            //var result = db.Products.Min(p => p.UnitPrice);
            //var result = db.Products.Where(p=>p.CategoryId==2).Max(p => p.UnitPrice);
            //var result = db.Products.Where(p=>!p.Discontinued).Average(p => p.UnitPrice);//discontinued satışta olmayan demek ünlemle olanı alrıız
            //var result = db.Products.Where(p => !p.Discontinued).Sum(p => p.UnitPrice);

            //var result = db.Products.OrderBy(p=>p.UnitPrice).ToList();
            //var result = db.Products.OrderByDescending(p => p.UnitPrice).LastOrDefault();

            //Console.WriteLine(result.ProductName + "  " + result.UnitPrice);


            //foreach (var p in result)
            //{
            //    Console.WriteLine(p.ProductName +" "+ p.UnitPrice);
            //}

        }
        private static void Ders234uygulamasecmesorgu(NorthwindContext db)
        {
            //var customers = db.Customers.Select(c => new { c.CustomerId, c.ContactName }).ToList();

            //foreach (var c in customers)
            //{
            //    Console.WriteLine(c.CustomerId +" " + c.ContactName);

            //}

            //var customers = db.Customers.Select(c => new { c.ContactName, c.Country }).Where(c => c.Country == "Germany").ToList();
            //foreach (var c in customers)
            //{
            //    Console.WriteLine(c.ContactName + " " + c.Country);

            //}

            //var customer = db.Customers.Select((c => new { c.ContactName, c.Country })).Where(c => c.ContactName == "Diego Roel").FirstOrDefault();
            //Console.WriteLine(customer.ContactName+ "  " + customer.Country);

            //var unit = db.Products.Select(u => new { u.ProductName, u.UnitsInStock }).Where(u => u.UnitsInStock == 0).ToList();

            //foreach (var u in unit)
            //{
            //    Console.WriteLine(u.ProductName + " " + u.UnitsInStock);
            //}

            //var employees = db.Employees.Select(e => new {FullName=e.FirstName+ " " + e.LastName }).ToList();

            //foreach (var emp in employees)
            //{
            //    Console.WriteLine(emp.FullName);
            //}

            //var products = db.Products.Take(5).ToList();

            //foreach (var p in products)
            //{
            //    Console.WriteLine(p.ProductName + " " + p.ProductId);
            //}

            //var products = db.Products.Skip(5).Take(5).ToList();

            //foreach (var p in products)
            //{
            //    Console.WriteLine(p.ProductName + " " + p.ProductId);
            //}

        }
        private static void Ders233filtrelemesorgu(NorthwindContext db)
        {
            //var products = db.Products.Where(p => p.UnitPrice > 18).ToList();
            //var products = db.Products.Select(p=> new {p.ProductName,p.UnitPrice }).Where(p => p.UnitPrice > 18).ToList();
            //var products = db.Products.Where(p => p.UnitPrice > 18 && p.UnitPrice<30).ToList();
            //var products = db.Products.Where(p => p.CategoryId >= 1 && p.CategoryId<=5).ToList();
            //var products = db.Products.Where(p => p.CategoryId == 1 || p.CategoryId == 5).ToList();
            //var products = db.Products.Where(p => p.CategoryId==1).Select(p => new { p.ProductName, p.UnitPrice }).ToList();
            //var products = db.Products.Where(i => i.ProductName == "Chai").ToList();
            //var products = db.Products.Where(i => i.ProductName.Contains("Ch")).ToList();

            //foreach (var p in products)
            //{
            //    Console.WriteLine(p.ProductName + " " + p.UnitPrice);
            //}
        }
        private static void Ders232secmesorgu(NorthwindContext db)
        {
            //var products = db.Products.ToList();
            //var products = db.Products.Select(p=>p.ProductName).ToList();
            //var products = db.Products.Select(p => new { p.ProductName, p.UnitPrice }).ToList();
            //var products = db.Products.Select(p => 
            //new ProductModel
            //{ 
            //   Name= p.ProductName,
            //   Price= p.UnitPrice 
            //}).ToList();

            //var product = db.Products.First();
            //var product = db.Products.Select(p => new { p.ProductName, p.UnitPrice }).FirstOrDefault();

            //Console.WriteLine(product.ProductName + " " + product.UnitPrice);


            //foreach (var p in products)
            //{
            //    Console.WriteLine(p.Name +" " + p.Price);
            //}
        }
    }
}
