using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BAL.DTOS.BankAccounts
{
    public class GetBankAccount
    {
        [Key]
        public string AccountId { get; set; }

        public string AccountName { get; set; }

        public DateTime? OpenDate { get; set; }

        public string BranchName { get; set; }

        public string TypeName { get; set; }
    }
}
