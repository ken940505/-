using LLWP_Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LLWP_Core.ViewModels
{
    public class ActivityVM
    {
        public TActivitydata activitydata { get; set; }
        public IFormFile _actpic { get; set; }
        public IFormFile actpic { get => _actpic; set => _actpic = value; }
    }
}
