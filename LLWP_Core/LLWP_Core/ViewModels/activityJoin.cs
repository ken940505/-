using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LLWP_Core.ViewModels
{
    public class activityJoin
    {
        public int activityId { get; set; }
        public int? memberId { get; set; }
        public string date { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public string image { get; set; }
    }
}
