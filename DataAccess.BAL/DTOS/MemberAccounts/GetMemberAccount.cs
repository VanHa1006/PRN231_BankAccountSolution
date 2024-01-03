using System.ComponentModel.DataAnnotations;

namespace DataAccess.BAL.DTOS.MemberAccounts
{
    public class GetMemberAccount
    {
        [Key]
        public string UserId { get; set; }

        public string AccessToken { get; set; }

        public string UserName { get; set; }

        public string UserRole { get; set; }
    }
}
