using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication2.Models;
using WebApplication2.Repository;

namespace WebApplication2.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task< IActionResult> Index()
        {
           DataRepository dataRepository = new DataRepository();
            var User_list=dataRepository.GetUsers();
            if(User_list != null ) return View( User_list );
            else
            {
                return View();  
            }
            //return View(users);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            DataRepository repo=new DataRepository();
            int rp= repo.AddUser(user);
            if (rp >=1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public IActionResult File()
        {
            return View();
        }
        #region json
        public JsonResult GetCustomerDetails(int id)
        {
            string connectionString = _configuration.GetConnectionString("constr");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Get_CustomerDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        var customerDetails = new
                        {
                            region = reader["Region"].ToString(),
                            zone = reader["Zone"].ToString(),
                            groupComp = reader["Group_comp"].ToString(),
                            planType = reader["Plan_type"].ToString(),
                            custType = reader["Cust_type"].ToString(),
                            busVer = reader["Bus_ver"].ToString(),
                            entType = reader["Ent_type"].ToString(),
                            tMId = reader["tm_id"].ToString(),
                            pEId = reader["pe_id"].ToString(),
                            namePerson = reader["name_per"].ToString(),
                            sAPCode = reader["sap_code"].ToString(),
                            geoCode = reader["geo_code"].ToString(),
                            accManager = reader["acc_manage"].ToString(),
                            salesPerson = reader["sale_p"].ToString(),
                            company = reader["company"].ToString(),
                            custAcc = reader["Cust_acc"].ToString(),
                            channel = reader["Channel"].ToString(),
                            chargingMode = reader["Charging"].ToString()
                        };

                       
                        return Json(customerDetails);
                    }
                    return Json(null);
                }
            }
        }

        public JsonResult GetContacts(int id)
        {
            List<object> contacts = new List<object>();

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("constr")))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Get_Contacts", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure; 
                    cmd.Parameters.AddWithValue("@id", id); 

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            contacts.Add(new
                            {
                                function = reader["fun_type"].ToString(),
                                contactPerson = reader["Contact_P"].ToString(),
                                contactNumber = reader["Con_num"].ToString(),
                                emailId = reader["Email_id"].ToString()
                            });
                        }
                    }
                }
            }

            return Json(contacts); 
        }
        #endregion
    }
}
