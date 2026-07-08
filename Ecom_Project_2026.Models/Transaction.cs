using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Project_2026.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
