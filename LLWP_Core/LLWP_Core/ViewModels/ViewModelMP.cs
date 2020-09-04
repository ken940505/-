using LLWP_Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LLWP_Core.ViewModels
{
    public class ViewModelMP
    {
        public TMemberdata merberData { set; get; }
        public TMempetdata petData { set; get; }
        public IFormFile fPhotodata { get; set; }
        public IFormFile fPePhotodata { get; set; }
    }
}
