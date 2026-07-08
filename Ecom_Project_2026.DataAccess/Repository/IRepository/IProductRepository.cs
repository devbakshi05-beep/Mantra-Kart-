using Ecom_Project_2026.Data;
using Ecom_Project_2026.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Project_2026.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> GetAll();
    }


    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Product> GetAll()
        {
            return _context.Products;   // 👈 Access DbSet
        }

    }
}
