using QLSV.Data.Entities;
using QLSV.DTO.StudentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Data.Repositories.Contracts
{
    public interface IStudentRepository
    {
        Task<List<GetAllStudentDTO>?> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        Task AddNewStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(Student student);
        Task<GetDetailStudentByIdDTO?> GetDetailStudentByIdAsync(int id);
    }
}
