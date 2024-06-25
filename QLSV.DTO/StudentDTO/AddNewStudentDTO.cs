using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.DTO.StudentDTO
{
    public class AddNewStudentDTO
    {
        public string FullName { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public string Address { get; set; } = null!;
        public int StudentClassId { get; set; }
    }
}
