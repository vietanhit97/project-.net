using HistoryGenPin.Areas.Admin.Models.Role;
using HistoryGenPin.Common;

namespace HistoryGenPin.Areas.Admin.Repositories
{
    public interface IRoleManagement
    {
        object GetAllRoles();

        ResponseService<bool> CreateRole(CreateRole createRole);

        ResponseService<bool> UpdateRole(UpdateRole updateRole);

        ResponseService<bool> DeleteRole(int idRole);

        ResponseService<ResponseInfoRole> GetInfoRole(int idRole);
    }
}
