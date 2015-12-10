using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HomeAccountant.Core.Models.Mapping
{
    public class AccountTransactionNoteMap : EntityTypeConfiguration<AccountTransactionNote>
    {
        public AccountTransactionNoteMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Note)
                .IsRequired()
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("AccountTransactionNote");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AccountTransactionId).HasColumnName("AccountTransactionId");
            this.Property(t => t.OrderBy).HasColumnName("OrderBy");
            this.Property(t => t.Note).HasColumnName("Note");

            // Relationships
            this.HasRequired(t => t.AccountTransaction)
                .WithMany(t => t.AccountTransactionNotes)
                .HasForeignKey(d => d.AccountTransactionId);

        }
    }
}
