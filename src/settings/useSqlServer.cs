using BugStore.Data;
using Microsoft.EntityFrameworkCore;

namespace BugStore.settings
{
    public static class useSqlServer
    {
        public static void AddSqlServerDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
        }
    }
}
