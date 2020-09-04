using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LLWP_Core.Utility
{
    public static class SD
    {
        public static string CodeCreate(int Digits)
        {
            Random r = new Random();
            var code = "";
            for (int i = 0; i < Digits; i++)
            {
                code += r.Next(0, 10).ToString();
            }

            return code;
        }

    }
}
