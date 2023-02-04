using HistoryGenPin.Areas.Admin.Data.DatabaseContext;
using HistoryGenPin.Areas.Admin.Models.Role;
using HistoryGenPin.Areas.Admin.Repositories;
using HistoryGenPin.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HistoryGenPin.Areas.Admin.Services
{
    public class RoleManagement: IRoleManagement
    {
        private readonly GenPinContext _db;
        public RoleManagement(GenPinContext db)
        {
            this._db = db;
        }
        public object GetAllRoles()
        {
            try
            {
                var obj = (from a in _db.TblRoles

                           select new
                           {
                               a.Id,
                               a.RoleName,
                               a.Description,
                               a.CreatedDate
                           }).Distinct().OrderByDescending(r => r.CreatedDate).ToList();
                var result = obj.AsEnumerable().Select((x, index) => new
                {
                    index = index + 1,
                    x.Id,
                    x.RoleName,
                    x.Description,
                    x.CreatedDate
                }).ToList();

                return new { data = result };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseService<bool> CreateRole(CreateRole createRole)
        {
            try
            {
                TblRoles role = new TblRoles();
                role.RoleName = createRole.RoleName;
                role.Description = createRole.Description;
                role.CreatedDate = DateTime.Now;
                role.CreatedBy = "Administrator";
                _db.TblRoles.Add(role);
                _db.SaveChanges();

                TblRoleModules roleModule = new TblRoleModules();
                roleModule.RoleId = role.Id;
                roleModule.IsShow = createRole.DetailRole.IsShow;
                roleModule.IsDownload = createRole.DetailRole.IsDowload;
                roleModule.CreatedDate = DateTime.Now;
                roleModule.CreatedBy = "Administrator";
                _db.TblRoleModules.Add(roleModule);
                _db.SaveChanges();

                return new ResponseService<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResponseService<bool>(ex);
            }
        }

        public ResponseService<bool> UpdateRole(UpdateRole updateRole)
        {
            try
            {
                var role = _db.TblRoles.Where(r => r.Id == Int32.Parse(updateRole.RoleId)).FirstOrDefault();
                if (role != null)
                {
                    role.RoleName = updateRole.RoleName;
                    role.Description = updateRole.Description;
                    role.ModifiedDate = DateTime.Now;
                    _db.Entry(role).State = EntityState.Modified;

                    var roleModule = _db.TblRoleModules.Where(r => r.RoleId == Int32.Parse(updateRole.RoleId)).FirstOrDefault();
                    if (roleModule != null)
                    {
                        roleModule.IsShow = updateRole.DetailRole.IsShow;
                        roleModule.IsDownload = updateRole.DetailRole.IsDowload;
                        roleModule.ModifiedDate = DateTime.Now;
                        roleModule.CreatedBy = "Administrator";
                        _db.Entry(role).State = EntityState.Modified;
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

        public ResponseService<bool> DeleteRole(int idRole)
        {
            try
            {
                var role = _db.TblRoles.Where(r => r.Id == idRole).FirstOrDefault();
                if (role != null)
                {
                    _db.TblRoles.Remove(role);
                    var roleUser = _db.TblRoleUsers.Where(r => r.RoleId == role.Id).ToList();
                    if (roleUser.Count > 0)
                    {
                        _db.TblRoleUsers.RemoveRange(roleUser);
                    }

                    var roleModule = _db.TblRoleModules.Where(r => r.RoleId == role.Id).FirstOrDefault();
                    if (roleModule != null)
                    {
                        _db.TblRoleModules.Remove(roleModule);
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

        public ResponseService<ResponseInfoRole> GetInfoRole(int idRole)
        {
            try
            {
                ResponseInfoRole response = new ResponseInfoRole();
                DetailRole detailRole = new DetailRole();
                var role = _db.TblRoles.Where(r => r.Id == idRole).FirstOrDefault();
                response.Id = role.Id;
                response.RoleName = role.RoleName;
                response.Description = role.Description;

                var roleModule = _db.TblRoleModules.Where(r => r.RoleId == role.Id).FirstOrDefault();
                if (roleModule != null)
                {
                    detailRole.IsShow = roleModule.IsShow;
                    detailRole.IsDowload = roleModule.IsDownload;
                }
                else
                {
                    detailRole.IsShow = false;
                    detailRole.IsDowload = false;
                }
                response.DetailRole = detailRole;

                return new ResponseService<ResponseInfoRole>(response);
            }
            catch (Exception ex)
            {
                return new ResponseService<ResponseInfoRole>(ex);
            }
        }
    }
}
