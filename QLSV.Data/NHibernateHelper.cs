using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using QLSV.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Data
{
    internal class NHibernateHelper
    {
        public static ISession OpenSession()
        {
            string connectionString =
                "Data Source=.;" +
                "Initial Catalog=qlsv;" +
                "Integrated Security=True;" +
                "Encrypt=True;" +
                "TrustServerCertificate=True";

            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<Teacher>()
                    .AddFromAssemblyOf<Class>()
                    .AddFromAssemblyOf<Student>())
                .BuildSessionFactory();

            return sessionFactory.OpenSession();
        }
    }
}
