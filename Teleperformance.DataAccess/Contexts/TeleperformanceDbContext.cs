using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Teleperformance.Core.Entities;
using Teleperformance.Entities.Concrete;

namespace Teleperformance.DataAccess.Contexts
{
    public class TeleperformanceDbContext : IdentityDbContext<User, Role, int>
    {
        public TeleperformanceDbContext(DbContextOptions<TeleperformanceDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                switch (data.State)
                {
                    case EntityState.Modified:
                        data.Entity.ModifiedDate = DateTime.Now;
                        break;
                    case EntityState.Added:
                        data.Entity.CreatedDate = DateTime.Now;
                        data.Entity.ModifiedDate = DateTime.Now;
                        break;
                    default:
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
