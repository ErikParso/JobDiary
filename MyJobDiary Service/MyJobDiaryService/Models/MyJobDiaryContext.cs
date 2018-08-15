using Microsoft.Azure.Mobile.Server.Tables;
using MyJobDiaryService.DataObjects;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace MyJobDiaryService.Models
{
    public class MyJobDiaryContext : DbContext
    {
        private const string connectionStringName = "Name=MS_TableConnectionString";

        public MyJobDiaryContext() : base(connectionStringName)
        {
        }

        public DbSet<Shift> Shifts { get; set; }
        public DbSet<DietRecord> DietRecords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
        }
    }

}
