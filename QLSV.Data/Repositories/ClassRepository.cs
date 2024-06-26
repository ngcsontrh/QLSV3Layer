using NHibernate.Linq;
using QLSV.Data.Entities;
using QLSV.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Data.Repositories
{
    public class ClassRepository : IClassRepository
    {
        public async Task<List<Class>?> GetAllClassesAsync()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    return await session.Query<Class>().ToListAsync();
                }
            }
        }

        public async Task<Class?> GetClassByIdAsync(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    return await session.GetAsync<Class>(id);
                }
            }
        }
    }
}
