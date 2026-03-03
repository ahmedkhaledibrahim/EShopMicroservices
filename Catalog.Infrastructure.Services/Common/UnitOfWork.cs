using Catalog.Application.Interfaces.ICommon;
using Marten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Services.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDocumentSession _documentSession;
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IBaseRepository<T> GetRepository<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
