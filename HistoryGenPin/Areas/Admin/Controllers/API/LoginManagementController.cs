using HistoryGenPin.Areas.Admin.Data.DatabaseContext;
using HistoryGenPin.Areas.Admin.Models.Login;
using HistoryGenPin.Areas.Admin.Repositories;
using HistoryGenPin.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using static HistoryGenPin.Common.CommonFunc;

namespace HistoryGenPin.Areas.Admin.Controllers.API
{
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class LoginManagementController : Controller
    {
        private readonly ILoginManagement _loginRepository;
        private readonly IConfiguration _config;

        public LoginManagementController(IConfiguration config, ILoginManagement loginRepository)
        {
            _config = config;
            _loginRepository = loginRepository;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login(Login login)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Keys
                   .Where(k => ModelState[k].Errors.Count > 0)
                   .Select(k => new
                   {
                       errorMessage = ModelState[k].Errors[0].ErrorMessage
                   }).ToList();
                var result_CreateAccount = B_ExceptionMiddleApp.ReadCode("M_Login_Failed", false, errors);
                return Json(result_CreateAccount);
            }
            CommonFunc.ValidationModel(login);
            var response = _loginRepository.Login(login);
            
            if(response == null)
            {
                var result = B_ExceptionMiddleApp.ReadCode("M_Login_Failed", false);

                return Json(result);
            }
            var tokenString = GenerateJSONWebToken(response);
            var responseNew = new { token = tokenString };

            var result_Login = B_ExceptionMiddleApp.ReadCode("M_Login_Success", true, responseNew);

            return Json(result_Login);
        }

        [HttpGet]
        private string GenerateJSONWebToken(TblUsers userInfo)
        {
            var expiress = DateTime.Now.AddMinutes(Convert.ToInt32(_config["Jwt:Expires"])).ToUniversalTime();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();

            claims.Add(new Claim("userName", userInfo.UserName));
            claims.Add(new Claim("userId", userInfo.Id.ToString()));

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims: claims,
              expires: expiress,
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
