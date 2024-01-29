using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SSHInfo.Classes;
using System.Data.SqlClient;

namespace SSHFRONTEND.Controllers

{
    public class DashboardController : Controller
    {
        private readonly SSHDbContext _context;

        private readonly IConfiguration _configuration;

        public DashboardController(SSHDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {

            ViewBag.v1 = GetSSHInfosCount();
            ViewBag.VirtualMachines = GetVirtualMachinesFromDatabase();
            return View();
        }
        public IActionResult Watcher()
        {
            return View();
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

        private List<string> GetVirtualMachinesFromDatabase()
        {
            List<string> virtualMachines = new List<string>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                string sql = "SELECT Host FROM SSHInfos";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
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
    }
}
