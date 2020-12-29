using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PP.EntityFramework
{
    public class PPDbContextFactory : IDesignTimeDbContextFactory<PPDbContext>
    {
        public PPDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<PPDbContext>();
            //options.UseSqlServer("Server=USER-PC; Database=ONLYOU; Trusted_Connection=True;");
            options.UseSqlServer("Server=192.168.96.37;Database=ONLYOU;User id=sa; password=olimpiasknitting");

            return new PPDbContext(options.Options);
        }
    }
}