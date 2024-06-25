using FluentNHibernate.Mapping;
using QLSV.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Data.Mappings
{
    internal class TeacherEntityMap : ClassMap<Teacher>
    {
        public TeacherEntityMap()
        {
            Id(t => t.Id, "teacher_id");
            Map(t => t.FullName, "teacher_fullname").Not.Nullable();
            Map(t => t.Birthday, "teacher_birthday").Not.Nullable();
            HasMany(t => t.TeacherClasses)
                .KeyColumn("class_id")
                .Inverse()
                .Cascade.None();
        }
    }
}
