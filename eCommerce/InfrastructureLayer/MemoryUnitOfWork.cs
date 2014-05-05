using eCommerce.Helpers.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.InfrastructureLayer
{
    public class MemoryUnitOfWork : IUnitOfWork
    {
        public void Commit()
        {
            //commit
        }

        public void Rollback()
        {
            //rollback
        }

        public void Dispose()
        {
            //dispose
        }
    }
}
