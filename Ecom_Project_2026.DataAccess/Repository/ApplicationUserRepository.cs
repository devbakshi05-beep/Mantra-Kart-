using Ecom_Project_2026.Data;
using Ecom_Project_2026.DataAccess.Repository.IRepository;
using Ecom_Project_2026.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Project_2026.DataAccess.Repository
{
    public class ApplicationUserRepository:Repository<ApplicationUser>,IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRepository(ApplicationDbContext context)
            :base(context) 
        {
            _context = context;
        }
    }
}
