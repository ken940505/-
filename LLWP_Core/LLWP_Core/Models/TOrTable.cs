using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LLWP_Core.Models
{
    public partial class TOrTable
    {
        public int FOrId { get; set; }

        [DisplayName("訂單編號")]
        public string FOrNum { get; set; }

        [DisplayName("訂單日期")]   
        public string FOrDate { get; set; }

        [DisplayName("入住日")]
        public string FOrCheckIn { get; set; }

        [DisplayName("退房日")]
        public string FOrCheckOut { get; set; }

        [DisplayName("天數")]
        public int FOrday { get; set; }

        [DisplayName("房客")]
        public int FOrGuestOneId { get; set; }

        [DisplayName("房客二")]
        public int? FOrGuestTwoId { get; set; }

        [DisplayName("人數")]
        public int FOrPeople { get; set; }

        [DisplayName("房間序號")]
        public int FOrRoomId { get; set; }

        [DisplayName("攜帶寵物")]
        public string FOrPetId { get; set; }

        [DisplayName("參加試養計畫")]
        public string FOrTryPet { get; set; }

        [DisplayName("試養寵物序號")]
        public int? FOrTryPetId { get; set; }

        [DisplayName("房客一活動A")]
        public int? FOrGuestOneActivityA { get; set; }

        [DisplayName("房客一活動B")]
        public int? FOrGuestOneActivityB { get; set; }

        [DisplayName("房客一活動C")]
        public int? FOrGuestOneActivityC { get; set; }

        [DisplayName("房客二活動A")]
        public int? FOrGuestTwoActivityA { get; set; }

        [DisplayName("房客二活動B")]
        public int? FOrGuestTwoActivityB { get; set; }

        [DisplayName("房客二活動C")]
        public int? FOrGuestTwoActivityC { get; set; }

        [DisplayName("訂單總金額")]
        [DisplayFormat(DataFormatString = "{0:F0}", ApplyFormatInEditMode = true)]
        public decimal FOrTotalPrice { get; set; }
    }
}
