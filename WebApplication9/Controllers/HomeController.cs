using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using WebApplication9.Models;

namespace WebApplication9.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public List<User> Index()
        {
            var users = new List<User>();
            //{
            //    new User
            //    {
            //        Id=1,
            //        Name="Hristo",
            //        Email="hristo@adslkfj.com",
            //        Phone="02398423",
            //        UserName="Hristo.a",
            //        Website = "fsdsfd.com",
            //        Address = new Address
            //        {
            //            City = "fdsa",
            //            Street = "asdf",
            //            Suite = "6",
            //            Zipcode = "4000",
            //            Geo = new Geo
            //            {
            //                Lat = "54545445",
            //                Lng = "5445422"
            //            }
            //        },
            //        Company = new Company
            //        {
            //            Name = "dasf",
            //            CatchPhrase = "fsdfsdfs",
            //            Bs = "fsdfsd"
            //        }
            //    }, new User
            //    {
            //        Id=1,
            //        Name="Zirgo",
            //        Email="hristo@fasd.com",
            //        Phone="02398423",
            //        UserName="fads.a",
            //        Website = "fsdsfd.com",
            //        Address = new Address
            //        {
            //            City = "fdas",
            //            Street = "fads",
            //            Suite = "6",
            //            Zipcode = "4000",
            //            Geo = new Geo
            //            {
            //                Lat = "54545445",
            //                Lng = "5445422"
            //            }
            //        },
            //        Company = new Company
            //        {
            //            Name = "fdasf",
            //            CatchPhrase = "fsdfsdfs",
            //            Bs = "fsdfsd"
            //        }
            //    }
            //};

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("select * from Users", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var currentUser = new User
                            {
                                Id = int.Parse(reader["UserId"].ToString()),
                                Name = reader["Name"].ToString()
                            };

                            users.Add(currentUser);
                        }
                    }
                }
            }

            return users;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
