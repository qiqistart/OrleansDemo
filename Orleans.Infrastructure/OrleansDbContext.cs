using Microsoft.EntityFrameworkCore;
using Orleans.Domain.Entity.UserAggregate;

namespace Orleans.Infrastructure;

public class OrleansDbContext : DbContext
{
    public OrleansDbContext(DbContextOptions options) : base(options)
    {


    }

    public DbSet<SysUser> SysUser { get; set; }
}

