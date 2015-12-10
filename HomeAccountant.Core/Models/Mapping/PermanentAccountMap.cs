using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HomeAccountant.Core.Models.Mapping
{
    public class PermanentAccountMap : EntityTypeConfiguration<PermanentAccount>
    {
        public PermanentAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("PermanentAccount");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AccountId).HasColumnName("AccountId");

            // Relationships
            this.HasRequired(t => t.Account)
                .WithMany(t => t.PermanentAccounts)
                .HasForeignKey(d => d.AccountId);

        }
    }
}
