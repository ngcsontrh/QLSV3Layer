using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.DTO.StudentDTO
{
    public class GetDetailStudentByIdDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Birthday { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string ClassName { get; set; } = null!;
        public string ClassSubject { get; set; } = null!;
        public string ClassTeacherFullName { get; set; } = null!;
        public string ClassTeacherBirthday { get; set; } = null!;
    }
}
