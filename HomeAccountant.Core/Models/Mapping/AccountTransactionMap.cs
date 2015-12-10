using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HomeAccountant.Core.Models.Mapping
{
    public class AccountTransactionMap : EntityTypeConfiguration<AccountTransaction>
    {
        public AccountTransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("AccountTransaction");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.DebitAccountId).HasColumnName("DebitAccountId");
            this.Property(t => t.CreditAccountId).HasColumnName("CreditAccountId");
            this.Property(t => t.TransactionAmount).HasColumnName("TransactionAmount");

            // Relationships
            this.HasRequired(t => t.Account)
                .WithMany(t => t.AccountTransactions)
                .HasForeignKey(d => d.CreditAccountId);
            this.HasRequired(t => t.Account1)
                .WithMany(t => t.AccountTransactions1)
                .HasForeignKey(d => d.DebitAccountId);

        }
    }
}
