using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITIL_Lab_Test.Repositories
{
    public interface IListRepository<T> where T: class
    {
        Task<List<T>> GetAll();
    }
}
