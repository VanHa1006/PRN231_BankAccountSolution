using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class AccountType
{
    public string TypeId { get; set; }

    public string TypeName { get; set; }

    public string TypeDesc { get; set; }

    public virtual ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
}
