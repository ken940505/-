using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LLWP_Core.Models
{
    public partial class TTryPetTable
    {
        public int FTryPetId { get; set; }

        [StringLength(4)]
        [DisplayName("編號")]
        public string FTryPetNum { get; set; }

        [StringLength(8)]
        [DisplayName("姓名")]
        public string FTryPetName { get; set; }

        [StringLength(3)]
        [DisplayName("品種")]
        public string FTryPetVar { get; set; }

        [DisplayName("年齡")]
        public int FTryPetAge { get; set; }

        [DisplayName("體重")]
        public double FTryPetWei { get; set; }

        [DisplayName("性別")]
        public string FTryPetSex { get; set; }

        [DisplayName("個性")]
        public string FTryPetNature { get; set; }

        [DisplayName("施打疫苗")]
        public string FTryPetVac { get; set; }

        [DisplayName("絕育")]
        public string FTryPetFix { get; set; }

        [DisplayName("照片")]
        public string FTryPetPhoto { get; set; }
    }
}
