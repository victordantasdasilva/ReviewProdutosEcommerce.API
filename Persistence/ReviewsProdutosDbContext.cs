using Microsoft.EntityFrameworkCore;
using ReviewProdutosEcommerce.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewProdutosEcommerce.API.Persistence
{
    public class ReviewsProdutosDbContext : DbContext
    {
        public ReviewsProdutosDbContext(DbContextOptions<ReviewsProdutosDbContext> options) : base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(p =>
            {
                p.ToTable("tb_Product");
                p.HasKey(p => p.Id);
                
                p   
                    .HasMany(pp => pp.Reviews)
                    .WithOne()
                    .HasForeignKey(r => r.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductReview>(pr =>
            {
                pr.ToTable("tb_ProductReviews");
                pr.HasKey(p => p.Id);

                pr.Property(p => p.Author)
                    .HasMaxLength(50)
                    .IsRequired();
            });
        }
    }
}
