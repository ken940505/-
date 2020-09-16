using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LLWP_Core.Models
{
    public class cRoomOrderListViewModel
    {
        public TOrTable tOrTableOrderContent { get; set; }
        public TMemberdata  tMemberdataOneContent { get; set; }
        public TMemberdata tMemberdataTwoContent { get; set; }
        //public IEnumerable<tMemberdata> tMemberdataContent { get; set; }
        public TActivitydata tG1ActivitydataAContent { get; set; }
        public TActivitydata tG1ActivitydataBContent { get; set; }
        public TActivitydata tG1ActivitydataCContent { get; set; }
        public TActivitydata tG2ActivitydataAContent { get; set; }
        public TActivitydata tG2ActivitydataBContent { get; set; }
        public TActivitydata tG2ActivitydataCContent { get; set; }
        public TRmTable tRmTableContent { get; set; }
        public TTryPetTable tTryPetTableContent { get; set; }
        public TMempetdata tMempetdataContent { get; set; }
    }
}
