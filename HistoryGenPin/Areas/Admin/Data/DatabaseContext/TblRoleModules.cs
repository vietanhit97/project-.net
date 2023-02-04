using System;
using System.Collections.Generic;

namespace HistoryGenPin.Areas.Admin.Data.DatabaseContext
{
    public partial class TblRoleModules
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsDownload { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
