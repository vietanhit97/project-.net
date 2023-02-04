using HistoryGenPin.Areas.Admin.Models.Account;
using HistoryGenPin.Areas.Admin.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static HistoryGenPin.Common.CommonFunc;

namespace HistoryGenPin.Areas.Admin.Controllers.API
{
    //[Authorize]
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class AccountManagementController : Controller
    {
        private readonly IAccountManagement _accountRepository;

        public AccountManagementController(IAccountManagement accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public object GetAllUsers()
        {
            return Json(_accountRepository.GetAllUsers());
        }

        [HttpPost]
        [Route("CreateAccount")]
        public IActionResult CreateAccount(CreateAccount account)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Keys
                   .Where(k => ModelState[k].Errors.Count > 0)
                   .Select(k => new
                   {
                       errorMessage = ModelState[k].Errors[0].ErrorMessage
                   }).ToList();
                var result_CreateAccount = B_ExceptionMiddleApp.ReadCode("M_CreateAccount_Failed", false, errors);
                return Json(result_CreateAccount);
            }
            // CommonFunc.ValidationModel(account);
            var response = _accountRepository.CreateAccount(account);
            if (response.status)
            {
                var result_CreateAccount = B_ExceptionMiddleApp.ReadCode("M_CreateAccount_Success", true);
                return Json(result_CreateAccount);
            }
            else
            {
                var result_CreateAccount = B_ExceptionMiddleApp.ReadCode("M_CreateAccount_Failed", false, response.message);
                return Json(result_CreateAccount);
            }

        }

        [HttpPost]
        [Route("EditAccount")]
        public IActionResult EditAccount(EditAccount account)
        {
            var response = _accountRepository.EditAccount(account);
            if (response.status)
            {
                var result_EditAccount = B_ExceptionMiddleApp.ReadCode("M_EditAccount_Success", true);
                return Json(result_EditAccount);
            }
            else
            {
                var result_EditAccount = B_ExceptionMiddleApp.ReadCode("M_EditAccount_Failed", false);
                return Json(result_EditAccount);
            }
        }

        [HttpPost]
        [Route("DeleteAccount")]
        public IActionResult DeleteAccount(int idAccount)
        {
            _accountRepository.DeleteAccount(idAccount);
            return Ok();
        }

        [HttpPost]
        [Route("AssignRoleAccount")]
        public IActionResult AssignRoleAccount(AssignRoleAccount assignRole)
        {
            _accountRepository.AssignRoleAccount(assignRole);
            return Ok();
        }

        [HttpPost]
        [Route("InforAccount")]
        public IActionResult InforAccount(int idAccount)
        {
            var response = _accountRepository.GetInfoAccount(idAccount);
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

        [HttpPost]
        [Route("CheckExistNameAccount")]
        public IActionResult CheckExistNameAccount(string userName)
        {
            var response = _accountRepository.CheckExistNameAccount(userName);
            if (response.status)
            {
                var result_CheckExistName = B_ExceptionMiddleApp.ReadCode("M_ExistNameAccount_Success", true);
                return Json(result_CheckExistName);
            }
            else
            {
                var result_CheckExistName = B_ExceptionMiddleApp.ReadCode("M_ExistNameAccount_Failed", false);
                return Json(result_CheckExistName);
            }
        }

        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePassword(ChangePassword changePassword)
        {
            var response = _accountRepository.ChangePassword(changePassword);
            if (response.status)
            {
                var result_ChangePassword = B_ExceptionMiddleApp.ReadCode("M_ChangePassword_Success", true);
                return Json(result_ChangePassword);
            }
            else
            {
                var result_ChangePassword = B_ExceptionMiddleApp.ReadCode("M_ChangePassword_Failed", false);
                return Json(result_ChangePassword);
            }
        }
    }
}
