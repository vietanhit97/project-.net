using System;
using System.Collections.Generic;

namespace HistoryGenPin.Areas.Admin.Data.DatabaseContext
{
    public partial class PhoneHistory
    {
        public int Id { get; set; }
        public string CallSession { get; set; }
        public string Phone { get; set; }
        public bool? IsSuccess { get; set; }
        public string Note { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
