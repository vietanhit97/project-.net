using HistoryGenPin.Areas.Admin.Data.DatabaseContext;
using HistoryGenPin.Areas.Admin.Models.Account;
using HistoryGenPin.Areas.Admin.Repositories;
using HistoryGenPin.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HistoryGenPin.Areas.Admin.Services
{
    public class AccountManagement: IAccountManagement
    {
        private readonly GenPinContext _db;
        public AccountManagement(GenPinContext db)
        {
            this._db = db;
        }

        public object GetAllUsers()
        {
            try
            {
                var obj = (from a in _db.TblUsers
                           where a.IsDelete == false
                           select new
                           {
                               a.Id,
                               a.UserName,
                               a.CreatedDate
                           }).Distinct().OrderByDescending(r => r.CreatedDate).ToList();
                var result = obj.AsEnumerable().Select((x, index) => new
                {
                    index = index + 1,
                    x.Id,
                    x.UserName,
                    CreatedDate = string.Format("{0:dd-MM-yyyy HH:mm:ss}", x.CreatedDate)
                }).ToList();

                return new { data = result };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseService<InfoAccountResponse> GetInfoAccount(int idAccount)
        {
            InfoAccountResponse response = new InfoAccountResponse();
            try
            {
                var account = _db.TblUsers.Where(r => r.Id == idAccount).FirstOrDefault();
                if (account != null)
                {
                    response.UserName = account.UserName;
                    response.Id = account.Id;

                    var role = _db.TblRoleUsers.Where(x => x.UserId == account.Id).FirstOrDefault();
                    if (role != null)
                    {
                        response.RoleId = role.RoleId;
                    }
                    else
                    {
                        response.RoleId = 0;
                    }

                    return new ResponseService<InfoAccountResponse>(response);
                }
                else
                {
                    return new ResponseService<InfoAccountResponse>();
                }
            }
            catch (Exception ex)
            {
                return new ResponseService<InfoAccountResponse>(ex);
            }
        }

        public ResponseService<bool> CreateAccount(CreateAccount account)
        {
            try
            {
                TblUsers user = new TblUsers();
                user.UserName = account.UserName;
                user.Password = Common.Encryption.EncryptPassword(account.Password);
                user.CreatedDate = DateTime.Now;
                user.Status = true;
                user.IsDelete = false;
                user.CreatedBy = "Administrator";
                _db.TblUsers.Add(user);
                _db.SaveChanges();
                return new ResponseService<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResponseService<bool>(ex);
            }
        }

        public ResponseService<bool> EditAccount(EditAccount account)
        {
            try
            {
                TblUsers user = _db.TblUsers.Where(r => r.Id == Int32.Parse(account.Id)).FirstOrDefault();
                if (user != null)
                {
                    user.UserName = account.UserName;
                    _db.Entry(user).State = EntityState.Modified;

                    var roleUser = _db.TblRoleUsers.Where(r => r.UserId == user.Id).FirstOrDefault();
                    if (roleUser != null)
                    {
                        roleUser.RoleId = Int32.Parse(account.RoleId);
                        _db.Entry(user).State = EntityState.Modified;
                    }
                    else
                    {
                        TblRoleUsers tblRoleUser = new TblRoleUsers();
                        tblRoleUser.UserId = user.Id;
                        tblRoleUser.RoleId = Int32.Parse(account.RoleId);
                        tblRoleUser.CreatedDate = DateTime.Now;
                        tblRoleUser.CreatedBy = "Administrator";
                        _db.TblRoleUsers.Add(tblRoleUser);
                    }

                    _db.SaveChanges();
                    return new ResponseService<bool>(true);
                }
                else
                {
                    return new ResponseService<bool>();
                }
            }
            catch (Exception ex)
            {
                return new ResponseService<bool>(ex);
            }
        }

        public ResponseService<bool> DeleteAccount(int idAccount)
        {
            try
            {
                TblUsers user = _db.TblUsers.Where(r => r.Id == idAccount).FirstOrDefault();
                if (user != null)
                {
                    user.IsDelete = true;
                    _db.Entry(user).State = EntityState.Modified;
                    _db.SaveChanges();
                    return new ResponseService<bool>(true);
                }
                else
                {
                    return new ResponseService<bool>(false);
                }
            }
            catch (Exception ex)
            {
                return new ResponseService<bool>(ex);
            }

        }

        public ResponseService<bool> AssignRoleAccount(AssignRoleAccount assignRole)
        {
            try
            {
                TblRoleUsers roleUser = _db.TblRoleUsers.Where(r => r.UserId == assignRole.AccountId).FirstOrDefault();
                if (roleUser != null)
                {
                    roleUser.RoleId = assignRole.RoleId;
                }
                else
                {
                    TblRoleUsers tblRoleUser = new TblRoleUsers();
                    tblRoleUser.UserId = assignRole.AccountId;
                    tblRoleUser.RoleId = assignRole.RoleId;
                    tblRoleUser.CreatedDate = DateTime.Now;
                    tblRoleUser.CreatedBy = "Administrator";
                    _db.TblRoleUsers.Add(tblRoleUser);
                    _db.SaveChanges();
                }
                return new ResponseService<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResponseService<bool>(ex);
            }
        }

        public ResponseService<bool> CheckExistNameAccount(string userName)
        {
            try
            {
                var user = _db.TblUsers.Where(r => r.UserName.ToLower() == userName.ToLower()).FirstOrDefault();
                if (user != null)
                {
                    return new ResponseService<bool>(true);
                }
                else
                {
                    return new ResponseService<bool>();
                }
            }
            catch (Exception ex)
            {
                return new ResponseService<bool>(ex);
            }
        }

        public ResponseService<bool> ChangePassword(ChangePassword changePassword)
        {
            try
            {
                var user = _db.TblUsers.Where(r => r.Id == changePassword.UserId).FirstOrDefault();
                if (user != null)
                {
                    user.Password = Common.Encryption.EncryptPassword(changePassword.Password);
                    user.ModifiedDate = DateTime.Now;
                    user.ModifiedBy = "Administrator";
                    _db.Entry(user).State = EntityState.Modified;
                    _db.SaveChanges();
                    return new ResponseService<bool>(true);
                }
                else
                {
                    return new ResponseService<bool>();
                }
            }
            catch (Exception ex)
            {
                return new ResponseService<bool>(ex);
            }
        }
    }
}
