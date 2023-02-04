using HistoryGenPin.Areas.Admin.Data.DatabaseContext;
using HistoryGenPin.Areas.Admin.Models.Login;

namespace HistoryGenPin.Areas.Admin.Repositories
{
    public interface ILoginManagement
    {
        TblUsers Login(Login login);
    }
}
