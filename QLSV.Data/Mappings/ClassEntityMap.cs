using FluentNHibernate.Mapping;
using QLSV.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Data.Mappings
{
    internal class ClassEntityMap : ClassMap<Class>
    {
        public ClassEntityMap()
        {
            Table("class");
            Id(c => c.Id, "class_id");
            Map(c => c.Name, "class_name").Not.Nullable();
            Map(c => c.Subject, "class_subject").Not.Nullable();
            References(c => c.ClassTeacher, "teacher_id").Not.Nullable();
            HasMany(c => c.ClassStudents)
                .KeyColumn("student_id")
                .Inverse()
                .Cascade.All();
        }
    }
}
