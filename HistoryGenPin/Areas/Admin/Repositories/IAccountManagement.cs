using HistoryGenPin.Areas.Admin.Models.Account;
using HistoryGenPin.Common;

namespace HistoryGenPin.Areas.Admin.Repositories
{
    public interface IAccountManagement
    {
        object GetAllUsers();
        ResponseService<bool> CreateAccount(CreateAccount account);
        ResponseService<bool> EditAccount(EditAccount account);
        ResponseService<bool> DeleteAccount(int idAccount);
        ResponseService<bool> AssignRoleAccount(AssignRoleAccount assignRole);
        ResponseService<InfoAccountResponse> GetInfoAccount(int idAccount);
        ResponseService<bool> CheckExistNameAccount(string userName);
        ResponseService<bool> ChangePassword(ChangePassword changePassword);
    }
}
