using QLSV.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Data.Repositories.Contracts
{
    public interface ITeacherRepository
    {
        Task<Teacher?> GetTeacherByIdAsync(int id);
        Task<List<Teacher>?> GetAllTeachersAsync();
    }
}
