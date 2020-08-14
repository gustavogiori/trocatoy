using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.UnitWork
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}
