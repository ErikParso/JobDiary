using Microsoft.Azure.Mobile.Server.Tables;
using MyJobDiaryService.DataObjects;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace MyJobDiaryService.Models
{
    public class MyJobDiaryContext : DbContext
    {

        public MyJobDiaryContext()
            : base(Environment.GetEnvironmentVariable("SQLAZURECONNSTR_MyJobDiary"))
        {

        }

        public DbSet<Shift> Shifts { get; set; }
        public DbSet<DietPaymentItem> DietPaymentItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
        }
    }

}
