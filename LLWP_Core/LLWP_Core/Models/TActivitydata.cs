using System;
using System.Collections.Generic;

namespace LLWP_Core.Models
{
    public partial class TActivitydata
    {
        public int FActivityId { get; set; }
        public string FActivityCode { get; set; }
        public string FActivityName { get; set; }
        public string FActivityTime { get; set; }
        public int? FActivitypeopleLimit { get; set; }
        public decimal FActivityPrice { get; set; }
        public string FActivityLocation { get; set; }
        public string FActivityCheck { get; set; }
        public string FActivityImages { get; set; }
        public int? FActivityJoinpeople { get; set; }
    }
}
