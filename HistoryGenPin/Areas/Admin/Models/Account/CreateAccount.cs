using System.ComponentModel.DataAnnotations;

namespace HistoryGenPin.Areas.Admin.Models.Account
{
    public class CreateAccount
    {
        [Required(ErrorMessage = "UserName is not empty")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is not empty")]
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
