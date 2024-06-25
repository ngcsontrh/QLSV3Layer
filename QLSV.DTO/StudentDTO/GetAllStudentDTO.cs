using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.DTO.StudentDTO
{
    public class GetAllStudentDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Birthday { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string ClassName { get; set; } = null!;
    }
}
