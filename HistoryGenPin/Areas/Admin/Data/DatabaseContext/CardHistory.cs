using System;
using System.Collections.Generic;

namespace HistoryGenPin.Areas.Admin.Data.DatabaseContext
{
    public partial class CardHistory
    {
        public int Id { get; set; }
        public string CallSession { get; set; }
        public string CardNo { get; set; }
        public bool? IsSuccess { get; set; }
        public string Note { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
