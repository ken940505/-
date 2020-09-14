using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LLWP_Core.Models
{
    public class BookingOrderSearchModel
    {
        public string droplist { get; set; }
        public string droplistInputValue { get; set; }
        //需要一個變數來接droplist傳回來的值
        public string txtfOrdateValue { get; set; }
        public string txtfOrdateValueInputValue { get; set; }
        //需要一個變數來接txtfOrdateValue傳回來的值
        public string txtfOrCheckInValue { get; set; }
        public string txtfOrCheckInValueInputValue { get; set; }
        //需要一個變數來接txtfOrCheckInValue傳回來的值
    }
}
