using LLWP_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LLWP_Core.ViewModels
{
    public class BookingPaymentVM
    {
        public int roomType { get; set; }
        public decimal daysMoney { get; set; }
        public decimal activityMoney { get; set; }
        public TMemberdata memberdata { get; set; }
        public TOrTable tortable { get; set; }
        public TTryPetTable tryPetTable { get; set; }
        public IEnumerable<TActivitydata> activitydataOne { get; set; }
        public IEnumerable<TActivitydata> activitydataTwo { get; set; }
    }
}
