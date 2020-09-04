using System;
using System.Collections.Generic;

namespace LLWP_Core.Models
{
    public partial class TOrTable
    {
        public int FOrId { get; set; }
        public string FOrNum { get; set; }
        public string FOrDate { get; set; }
        public string FOrCheckIn { get; set; }
        public string FOrCheckOut { get; set; }
        public int FOrday { get; set; }
        public int FOrGuestOneId { get; set; }
        public int? FOrGuestTwoId { get; set; }
        public int FOrPeople { get; set; }
        public int FOrRoomId { get; set; }
        public string FOrPetId { get; set; }
        public string FOrTryPet { get; set; }
        public int? FOrTryPetId { get; set; }
        public int? FOrGuestOneActivityA { get; set; }
        public int? FOrGuestOneActivityB { get; set; }
        public int? FOrGuestOneActivityC { get; set; }
        public int? FOrGuestTwoActivityA { get; set; }
        public int? FOrGuestTwoActivityB { get; set; }
        public int? FOrGuestTwoActivityC { get; set; }
        public decimal FOrTotalPrice { get; set; }
    }
}
