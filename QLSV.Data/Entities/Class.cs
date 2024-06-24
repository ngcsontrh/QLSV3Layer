using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Data.Entities
{
    public class Class
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; } = null!;
        public virtual string Subject { get; set; } = null!;

        public virtual Teacher ClassTeacher { get; set; } = null!;
        public virtual List<Student> ClassStudents { get; set; } = new List<Student>();
    }
}
