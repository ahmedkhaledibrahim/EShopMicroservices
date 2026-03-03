using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Interfaces.ICommon
{
    public interface IUnitOfWork : IDisposable
    {
        public IBaseRepository<T> GetRepository<T>() where T : class;
        public Task SaveChangesAsync();

    }
}
