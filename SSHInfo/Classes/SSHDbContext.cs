using Microsoft.EntityFrameworkCore;
using System;

namespace SSHInfo.Classes
{
    public class SSHDbContext:DbContext
    {
        public SSHDbContext(DbContextOptions<SSHDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=AS-TASKIN63;Database=DenemeSSH;User Id=sa;Password=1q2w3e4r+!;TrustServerCertificate=True");

        }

        public DbSet<SSHInfo> SSHInfos { get; set; }
    }
}
