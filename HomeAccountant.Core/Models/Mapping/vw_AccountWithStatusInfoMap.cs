using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HomeAccountant.Core.Models.Mapping
{
    public class vw_AccountWithStatusInfoMap : EntityTypeConfiguration<vw_AccountWithStatusInfo>
    {
        public vw_AccountWithStatusInfoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Id, t.AccountType, t.OpenDate, t.IsPermenantAccount });

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.AccountType)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vw_AccountWithStatusInfo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ParentAccountId).HasColumnName("ParentAccountId");
            this.Property(t => t.AccountType).HasColumnName("AccountType");
            this.Property(t => t.OpenDate).HasColumnName("OpenDate");
            this.Property(t => t.CloseDate).HasColumnName("CloseDate");
            this.Property(t => t.IsPermenantAccount).HasColumnName("IsPermenantAccount");
        }
    }
}
