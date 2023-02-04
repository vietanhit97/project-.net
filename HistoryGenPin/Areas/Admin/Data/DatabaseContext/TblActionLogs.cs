using System;
using System.Collections.Generic;

namespace HistoryGenPin.Areas.Admin.Data.DatabaseContext
{
    public partial class TblActionLogs
    {
        public string Id { get; set; }
        public string ActionName { get; set; }
        public string UserName { get; set; }
        public string ActionType { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
