using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HomeAccountant.Core.Models.Mapping
{
    public class AccountStatuMap : EntityTypeConfiguration<AccountStatu>
    {
        public AccountStatuMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("AccountStatus");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AccountStatusTypeId).HasColumnName("AccountStatusTypeId");
            this.Property(t => t.AccountId).HasColumnName("AccountId");
            this.Property(t => t.StatusDateTime).HasColumnName("StatusDateTime");

            // Relationships
            this.HasRequired(t => t.Account)
                .WithMany(t => t.AccountStatus)
                .HasForeignKey(d => d.AccountId);
            this.HasRequired(t => t.AccountStatusType)
                .WithMany(t => t.AccountStatus)
                .HasForeignKey(d => d.AccountStatusTypeId);

        }
    }
}
