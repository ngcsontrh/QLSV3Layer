using QLSV.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Data.Repositories.Contracts
{
    internal interface IStudentRepository
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task AddNewStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(Student student);
        Task<Student> GetStudentByIdAsync(int id);
    }
}
