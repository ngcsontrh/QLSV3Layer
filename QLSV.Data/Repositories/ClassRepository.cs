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
        public async Task<List<Class>?> GetAllClassAsync()
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

        public async Task<string?> GetClassNameByIdAsync(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    return await session.QueryOver<Class>()
                        .Where(c => c.Id == id)
                        .Select(c => c.Name)
                        .SingleOrDefaultAsync<string>();
                }
            }
        }
    }
}
