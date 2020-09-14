using LLWP_Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LLWP_Core.ViewModels
{
    public class TTryPetTableVM
    {
        public TTryPetTable tryPetTable { get; set; }

        public IFormFile fImage { get; set; }
    }
}
