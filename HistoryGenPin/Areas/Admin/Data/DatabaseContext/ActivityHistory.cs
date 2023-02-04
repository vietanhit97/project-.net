using System;
using System.Collections.Generic;

namespace HistoryGenPin.Areas.Admin.Data.DatabaseContext
{
    public partial class ActivityHistory
    {
        public int Id { get; set; }
        public string CallSession { get; set; }
        public string Type { get; set; }
        public bool? IsSuccess { get; set; }
        public string Note { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
