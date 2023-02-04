using System;
using System.Collections.Generic;

namespace HistoryGenPin.Areas.Admin.Data.DatabaseContext
{
    public partial class HistoryIvr
    {
        public Guid CallId { get; set; }
        public string Phone { get; set; }
        public string Gttt { get; set; }
        public string CardNo { get; set; }
        public int? TypeAction { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Result { get; set; }
        public string Dob { get; set; }
        public string CustomerName { get; set; }
        public bool? IsConnect { get; set; }
        public bool? IsDrop { get; set; }
        public string CallSession { get; set; }
        public string ExecutionChannel { get; set; }
        public string Conditicon { get; set; }
        public string Executor { get; set; }
        public string ContractNumber { get; set; }
        public bool? IsGenPin { get; set; }
    }
}
