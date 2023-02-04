using System.ComponentModel.DataAnnotations;

namespace HistoryGenPin.Areas.Admin.Models.Login
{
    public class Login
    {
        [Required(ErrorMessage = "UserName is not empty")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is not empty")]
        public string Password { get; set; }
    }
}
