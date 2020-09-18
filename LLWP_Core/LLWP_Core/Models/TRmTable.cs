using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LLWP_Core.Models
{
    public partial class TRmTable
    {
        public int FRmId { get; set; }

        [DisplayName("房號")]
        public string FRmNum { get; set; }
        public string FRmBlock { get; set; }
        public string FRmPet { get; set; }
        public int FRmHall { get; set; }
        public int FRmFloor { get; set; }
        public decimal? FRmPriceDayOld { get; set; }
        public decimal? FRmPriceDay { get; set; }
        public decimal? FRmPriceMonthSinglePerson { get; set; }
        public decimal? FRmPriceMonthDoublePerson { get; set; }
    }
}
