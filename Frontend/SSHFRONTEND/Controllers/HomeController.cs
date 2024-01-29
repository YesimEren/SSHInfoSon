using Microsoft.AspNetCore.Mvc;
using SSHFRONTEND.Models;
using SSHInfo.Classes;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SSHFRONTEND.Controllers
{
    public class HomeController : Controller
    {

        private readonly SSHDbContext _context;
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration, SSHDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.VirtualMachines = GetVirtualMachinesFromDatabase();
            ViewBag.v1 = _context.SSHInfos.Count();


            return View();


        }

        private List<string> GetVirtualMachinesFromDatabase()
        {
            List<string> virtualMachines = new List<string>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                string sql = "SELECT Host FROM SSHInfos";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            virtualMachines.Add(reader["Host"].ToString());
                        }
                    }
                    return virtualMachines;
                }
            }
        }

        private int GetSSHInfosCount()
        {
            int sshInfosCount = 0;

            try
            {
                sshInfosCount = _context.SSHInfos.Count();
            }
            catch (Exception ex)
            {
                // Hata durumunda uygun bir işlem yapabilirsiniz.
                Console.WriteLine($"Hata oluştu: {ex.Message}");
            }

            return sshInfosCount;
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
}