using QLSV.Data.Entities;
using QLSV.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public Task AddNewStudentAsync(Student student)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStudentAsync(Student student)
        {
            throw new NotImplementedException();
        }

        public Task<List<Student>> GetAllStudentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetStudentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStudentAsync(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
