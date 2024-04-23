using Microsoft.EntityFrameworkCore;
using Orleans.Domain.Entity.UserAggregate;

namespace Orleans.Infrastructure;

public class OrleansDbContext : DbContext
{
    public OrleansDbContext(DbContextOptions<OrleansDbContext> options) : base(options)
    {


    }

    public DbSet<SysUser> SysUser { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {



    }
}

