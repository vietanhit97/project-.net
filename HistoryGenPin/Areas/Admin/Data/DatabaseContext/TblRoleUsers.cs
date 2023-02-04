using System;
using System.Collections.Generic;

namespace HistoryGenPin.Areas.Admin.Data.DatabaseContext
{
    public partial class TblRoleUsers
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? UserId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
