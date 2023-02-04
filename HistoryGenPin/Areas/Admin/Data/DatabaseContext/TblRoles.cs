﻿using System;
using System.Collections.Generic;

namespace HistoryGenPin.Areas.Admin.Data.DatabaseContext
{
    public partial class TblRoles
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
