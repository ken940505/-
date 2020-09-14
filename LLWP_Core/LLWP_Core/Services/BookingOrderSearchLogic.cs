using LLWP_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LLWP_Core.Services
{
    public class BookingOrderSearchLogic
    {
        private readonly dbLLWPContext _db;
        public BookingOrderSearchLogic(dbLLWPContext db)
        {
            _db = db;
        }
        public List<TOrTable> GetSearchBookOrders(BookingOrderSearchModel searchModel)
        {
            List<TOrTable> result = new List<TOrTable>();

            var numberValue = 0;

            if (searchModel.droplist == "OrNum")
                result = _db.TOrTable.Where(x => x.FOrNum == searchModel.droplistInputValue).ToList();
            else
            {
                try
                {
                    numberValue = Convert.ToInt32(searchModel.droplistInputValue);
                    result = _db.TOrTable.Where(x => x.FOrGuestOneId == numberValue).ToList();
                }
                catch (Exception)
                {
                    int i = 1;
                }
            }

            if (!String.IsNullOrEmpty(searchModel.txtfOrCheckInValue))
            {
                if (result.Count == 0)
                {
                    result = _db.TOrTable.Where(x => x.FOrCheckIn == searchModel.txtfOrCheckInValueInputValue).ToList();
                }
                else
                {
                    result = result.Where(x => x.FOrCheckIn == searchModel.txtfOrCheckInValueInputValue).ToList();
                }

                //string i = searchModel.droplistInputValue;
            }

            if (!String.IsNullOrEmpty(searchModel.txtfOrdateValue))
            {
                if (result.Count == 0)
                {
                    result = _db.TOrTable.Where(x => x.FOrDate == searchModel.txtfOrdateValueInputValue).ToList();
                }
                else
                {
                    result = result.Where(x => x.FOrDate == searchModel.txtfOrdateValueInputValue).ToList();
                }

                //string i = searchModel.droplistInputValue;
            }

            return result;
        }
    }
}
