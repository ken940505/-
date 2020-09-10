using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LLWP_Core.ViewModels
{
    public class Clogin
    {
        public string txtAccount { get; set; }
        public string txtPassword { get; set; }
        public string txtCord { get; set; }
        public string MailCord { get; set; }

        [Required]
        public string txtMail { get; set; }

        [Compare("txtPassword", ErrorMessage = "兩組密碼必須相同")]
        public string txtNewPassword { get; set; }
    }
}
