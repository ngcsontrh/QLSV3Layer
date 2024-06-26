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
    public class TeacherRepository : ITeacherRepository
    {
        public async Task<List<Teacher>?> GetAllTeachersAsync()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    return await session.Query<Teacher>().ToListAsync();
                }
            }
        }

        public async Task<Teacher?> GetTeacherByIdAsync(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    return await session.GetAsync<Teacher>(id);
                }
            }
        }
    }
}
