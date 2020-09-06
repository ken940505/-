using LLWP_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LLWP_Core.ViewModels
{
    public class BookingVM
    {
        public IEnumerable<TActivitydata> activitydata { get; set; }
        public IEnumerable<TTryPetTable> tryPetTable { get; set; }

    }
}
