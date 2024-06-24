using FluentNHibernate.Mapping;
using QLSV.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Data.Mappings
{
    internal class StudentEntityMap : ClassMap<Student>
    {
        public StudentEntityMap()
        {
            Id(s => s.Id, "student_id").GeneratedBy.Identity();
            Map(s => s.FullName, "student_fullname").Not.Nullable();
            Map(s => s.Birthday, "student_birthday").Not.Nullable();
            Map(s => s.Address, "student_address").Not.Nullable();
            References(s => s.StudentClass, "student_class_id").Not.Nullable();
        }
    }
}
