using System;
using System.Collections.Generic;

namespace HistoryGenPin.Areas.Admin.Data.DatabaseContext
{
    public partial class ErrorLog
    {
        public int Id { get; set; }
        public string Api { get; set; }
        public string Parameter { get; set; }
        public string ErrMessage { get; set; }
        public string CallSession { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
