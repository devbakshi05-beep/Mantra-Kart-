        using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Project_2026.DataAccess.Repository.IRepository
{
    public interface ISP_Call:IDisposable
    {
        void Execute(string procedureName, DynamicParameters param = null);//for Create, Update, Delete
        T Single<T>(string procedureName, DynamicParameters param = null);//for Find
        T OneRecord<T>(string procedureName, DynamicParameters param = null);//Its also for Find
        IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null);//for Display means(GetAll)
        Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName   , DynamicParameters param = null);//For multiple queries
    }
}
