using System;
using System.Collections.Generic;

namespace LLWP_Core.Models
{
    public partial class TMemHealthdata
    {
        public int FHeId { get; set; }
        public string FHeMemNumber { get; set; }
        public string FHeBloodPresureH { get; set; }
        public string FHeBloodPresureL { get; set; }
        public string FHeHeight { get; set; }
        public string FHeWeight { get; set; }
        public double FHeTemperature { get; set; }
        public double FHeBloodOxygen { get; set; }
    }
}
