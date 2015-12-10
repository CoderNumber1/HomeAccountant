using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using HomeAccountant.Core.Models.Mapping;

namespace HomeAccountant.Core.Models
{
    public partial class HomeAccountantContext : DbContext
    {
        static HomeAccountantContext()
        {
            Database.SetInitializer<HomeAccountantContext>(null);
        }

        public HomeAccountantContext()
            : base("Name=HomeAccountantContext")
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountStatu> AccountStatus { get; set; }
        public DbSet<AccountStatusType> AccountStatusTypes { get; set; }
        public DbSet<AccountTransaction> AccountTransactions { get; set; }
        public DbSet<AccountTransactionNote> AccountTransactionNotes { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<PermanentAccount> PermanentAccounts { get; set; }
        public DbSet<vw_AccountWithStatusInfo> vw_AccountWithStatusInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new AccountStatuMap());
            modelBuilder.Configurations.Add(new AccountStatusTypeMap());
            modelBuilder.Configurations.Add(new AccountTransactionMap());
            modelBuilder.Configurations.Add(new AccountTransactionNoteMap());
            modelBuilder.Configurations.Add(new AccountTypeMap());
            modelBuilder.Configurations.Add(new PermanentAccountMap());
            modelBuilder.Configurations.Add(new vw_AccountWithStatusInfoMap());
        }
    }
}
