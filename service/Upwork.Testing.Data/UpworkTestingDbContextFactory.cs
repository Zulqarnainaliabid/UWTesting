using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Upwork.Testing.Data
{
    public class UpworkTestingDbContextFactory : IDesignTimeDbContextFactory<UpworkTestingDbContext>
    {
        public UpworkTestingDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UpworkTestingDbContext>();
            optionsBuilder
                .UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=UpworkTesting1;Data Source=ISBLT-7581;");
                
            return new UpworkTestingDbContext(optionsBuilder.Options);
        }
    }
}
