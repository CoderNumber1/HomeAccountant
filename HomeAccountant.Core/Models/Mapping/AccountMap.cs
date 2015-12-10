using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HomeAccountant.Core.Models.Mapping
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Account");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AccountTypeId).HasColumnName("AccountTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ParentAccountId).HasColumnName("ParentAccountId");
            this.Property(t => t.IsPermanent).HasColumnName("IsPermanent");

            // Relationships
            this.HasOptional(t => t.Account2)
                .WithMany(t => t.Account1)
                .HasForeignKey(d => d.ParentAccountId);
            this.HasRequired(t => t.AccountType)
                .WithMany(t => t.Accounts)
                .HasForeignKey(d => d.AccountTypeId);

        }
    }
}
