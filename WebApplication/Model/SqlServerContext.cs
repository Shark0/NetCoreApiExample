using Microsoft.EntityFrameworkCore;
using WebApplication.Model.Member;

namespace WebApplication.Model
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options)
            : base(options)
        {
        }

        public DbSet<MemberDo> Member { get; set; }
    }
}