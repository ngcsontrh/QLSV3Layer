using QLSV.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Data.Repositories.Contracts
{
    public interface IStudentRepository
    {
        Task<List<Student>?> GetAllStudentsAsync();
        Task<int> CountStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        Task<Student?> GetStudentDetailsByIdAsync(int id);
        Task AddNewStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(Student student);
    }
}
