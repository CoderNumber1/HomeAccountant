using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountant.Core.Models
{
    public partial class HomeAccountantContext
    {
        public HomeAccountantContext(string connectionString)
            : base(connectionString) { }

        public IQueryable<Account> SearchAccounts(int? accountType = null, int? parentAccountId = null, DateTime? searchDate = null, bool? permanantAccount = null)
        {
            var accountTypeParam = new SqlParameter("AccountType", System.Data.DbType.Int32) { Value = (object)accountType ?? DBNull.Value };
            var parentAccountParam = new SqlParameter("ParentAccountId", System.Data.DbType.Int32) { Value = (object)parentAccountId ?? DBNull.Value };
            var searchDateParam = new SqlParameter("SearchDate", System.Data.DbType.DateTime2) { Value = (object)searchDate ?? DBNull.Value };
            var permanentAccountParam = new SqlParameter("PermanentAccount", System.Data.DbType.Boolean) { Value = (object)permanantAccount ?? DBNull.Value };

            return base.Database.SqlQuery<Account>("exec dbo.usp_AccountSearch @AccountType, @ParentAccountId, @SearchDate, @PermanentAccount", accountTypeParam, parentAccountParam, searchDateParam, permanentAccountParam).AsQueryable();
        }
    }
}
