using Repository;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Company_Employees.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var Builder = new DbContextOptionsBuilder<RepositoryContext>()
            .UseSqlServer(Configuration.GetConnectionString("sqlconnection"));
            return new RepositoryContext(Builder.Options);
        }
    }
}
