using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BAL.DTOS.AccountTypes
{
    public class GetAccountType
    {
        [Key]
        public string TypeId { get; set; }

        public string TypeName { get; set; }
    }
}
