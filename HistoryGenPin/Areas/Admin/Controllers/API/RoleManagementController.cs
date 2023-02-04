using HistoryGenPin.Areas.Admin.Models.Role;
using HistoryGenPin.Areas.Admin.Repositories;
using Microsoft.AspNetCore.Mvc;
using static HistoryGenPin.Common.CommonFunc;

namespace HistoryGenPin.Areas.Admin.Controllers.API
{
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class RoleManagementController : Controller
    {
        private readonly IRoleManagement _roleRepository;

        public RoleManagementController(IRoleManagement roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            return Json(_roleRepository.GetAllRoles());
        }

        [HttpPost]
        [Route("CreateRole")]
        public IActionResult CreateRole(CreateRole createRole)
        {
            var response = _roleRepository.CreateRole(createRole);
            if (response.status)
            {
                var result_CreateRole = B_ExceptionMiddleApp.ReadCode("M_CreateRole_Success", true);
                return Json(result_CreateRole);
            }
            else
            {
                var result_CreateRole = B_ExceptionMiddleApp.ReadCode("M_CreateRole_Failed", false, response.message);
                return Json(result_CreateRole);
            }

        }

        [HttpPost]
        [Route("UpdateRole")]
        public IActionResult UpdateRole(UpdateRole updateRole)
        {
            var response = _roleRepository.UpdateRole(updateRole);
            if (response.status)
            {
                var result_UpdateRole = B_ExceptionMiddleApp.ReadCode("M_UpdateRole_Success", true);
                return Json(result_UpdateRole);
            }
            else
            {
                var result_UpdateRole = B_ExceptionMiddleApp.ReadCode("M_UpdateRole_Failed", false, response.message);
                return Json(result_UpdateRole);
            }
        }

        [HttpPost]
        [Route("DeleteRole")]
        public IActionResult DeleteRole(int idRole)
        {
            var response = _roleRepository.DeleteRole(idRole);
            if (response.status)
            {
                var result_DeleteRole = B_ExceptionMiddleApp.ReadCode("M_DeleteRole_Success", true);
                return Json(result_DeleteRole);
            }
            else
            {
                var result_DeleteRole = B_ExceptionMiddleApp.ReadCode("M_DeleteRole_Failed", false, response.message);
                return Json(result_DeleteRole);
            }
        }

        [HttpPost]
        [Route("GetInfoRole")]
        public IActionResult GetInfoRole(int idRole)
        {
            var response = _roleRepository.GetInfoRole(idRole);
            if (response.status)
            {
                var result_InfoAccount = B_ExceptionMiddleApp.ReadCode("M_InfoAccount_Success", true, response.data);
                return Json(result_InfoAccount);
            }
            else
            {
                var result_InfoAccount = B_ExceptionMiddleApp.ReadCode("M_InfoAccount_Failed", false);
                return Json(result_InfoAccount);
            }
        }
    }
}
