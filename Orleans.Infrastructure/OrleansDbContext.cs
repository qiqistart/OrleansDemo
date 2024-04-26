using Microsoft.EntityFrameworkCore;
using Orleans.Domain.Entity.UserAggregate;

namespace Orleans.Infrastructure;

public class OrleansDbContext : DbContext
{
    public OrleansDbContext(DbContextOptions<OrleansDbContext> options) : base(options)
    {


    }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<SysUser> SysUser { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DbSet<SysUserConfig> SysUserConfig { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {



    }
}

