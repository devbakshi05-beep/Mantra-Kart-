using Ecom_Project_2026.Data;
using Ecom_Project_2026.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Project_2026.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProductRepository Products { get; private set; }   // 👈 Implementation

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            CoverType = new CoverTypeRepository(_context);
            SP_Call = new SP_Call(context);
            Product = new ProductRepository(_context);  
            Company = new CompanyRepository(_context);
            Products = new ProductRepository(_context);   // 👈 Initialize repository
            ApplicationUser = new ApplicationUserRepository(_context);
            ShoppingCart = new ShoppingCartRepository(_context);
            OrderDetail = new OrderDetailRepository(_context);
            OrderHeader = new OrderHeaderRepository(_context);
        }
        public ICategoryRepository Category { private set; get; }
        public ICoverTypeRepository CoverType { private set; get; }
        public ISP_Call SP_Call { private set; get; }
        public IProductRepository Product { private set; get; } 
        public ICompanyRepository Company { private set; get; }
        public IApplicationUserRepository ApplicationUser { private set; get; }
        public IShoppingCartRepository ShoppingCart { private set; get; }
        public IOrderHeaderRepository OrderHeader { private set; get; }
        public IOrderDetailRepository OrderDetail { private set; get; }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
           _context.SaveChanges();
        }
    }
}
