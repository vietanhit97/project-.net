using HistoryGenPin.Areas.Admin.Data.DatabaseContext;
using HistoryGenPin.Areas.Admin.Models.Login;
using HistoryGenPin.Areas.Admin.Repositories;
using System.Linq;

namespace HistoryGenPin.Areas.Admin.Services
{
    public class LoginManagement: ILoginManagement
    {
        private readonly GenPinContext _db;
        public LoginManagement(GenPinContext db)
        {
            this._db = db;
        }

        public TblUsers Login(Login loginModel)
        {
            string encryptionPassword = Common.Encryption.EncryptPassword(loginModel.Password);
            TblUsers user = _db.TblUsers.Where(r => r.UserName == loginModel.UserName && r.Password == encryptionPassword && r.IsDelete != true).FirstOrDefault();
            return user;
        }
    }
}
