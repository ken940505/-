using System;
using System.Collections.Generic;

namespace LLWP_Core.Models
{
    public partial class TRmTable
    {
        public int FRmId { get; set; }
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
