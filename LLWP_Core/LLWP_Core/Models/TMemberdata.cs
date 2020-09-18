using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LLWP_Core.Models
{
    public partial class TMemberdata
    {
        public int FMeId { get; set; }

        [DisplayName("性別")]
        [Required]
        public string FMeGender { get; set; }

        [DisplayName("會員編號")]
        public string FMeNumber { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "姓名不可空白")]
        public string FMeName { get; set; }

        [DisplayName("生日")]
        [Required(ErrorMessage = "生日不可空白")]
        public string FMeBirth { get; set; }

        [DisplayName("信箱")]
        [Required(ErrorMessage = "信箱不可空白")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "email格式錯誤")]

        public string FMeMail { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "密碼不可空白")]
        [StringLength(12, ErrorMessage = "密碼為6位", MinimumLength = 6)]

        public string FMePass { get; set; }

        [DisplayName("電話")]
        [Required(ErrorMessage = "電話不可空白")]
        [StringLength(10, ErrorMessage = "電話必須為10位", MinimumLength = 10)]

        public string FMePhone { get; set; }

        [DisplayName("年齡")]
        [Required]

        public string FMeAge { get; set; }

        [DisplayName("身分證")]
        [Required]
        [StringLength(10, ErrorMessage = "證號必須為10位", MinimumLength = 10)]

        public string FMePersonId { get; set; }
        public string FMePhoto { get; set; }

        [DisplayName("緊急聯絡人")]
        [Required]

        public string FMeEmerName { get; set; }

        [DisplayName("緊急連絡人電話")]
        [Required]
        [StringLength(10, ErrorMessage = "電話必須為10位", MinimumLength = 10)]

        public string FMeEmerPhone { get; set; }
    }
}
