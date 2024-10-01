using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace WebApplication2.Controllers
{
    public class IndexController : Controller
    {
        
        private readonly IConfiguration _configuration;
        public IndexController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       

        public IActionResult Index()
        {
            string connectionString = _configuration.GetConnectionString("constr");

            var reviews = new List<(string Type, string CustomerName, string Review)>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("Get_Reviews", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reviews.Add((
                                Type: reader["Type"].ToString(),
                                CustomerName: reader["CustomerName"].ToString(),
                                Review: reader["Review"].ToString()
                            ));
                        }
                    }
                }
            }

            return View(reviews);
        }

    }
}
