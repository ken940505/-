using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LLWP_Core.Models;
using Microsoft.AspNetCore.Mvc;
using LLWP_Core.Services;
using X.PagedList;
using OfficeOpenXml;
using System.IO;
using LLWP_Core.Utility;


namespace LLWP_Core.Controllers
{
    public class RoomOrderController : Controller
    {
        private readonly dbLLWPContext _db;
        public RoomOrderController(dbLLWPContext db)
        {
            _db = db;
        }

        int pageSize = 10;
        // GET: roomOrder
        public ActionResult List()
        {
            //dbLLWPEntities1 db = new dbLLWPEntities1();
            ////pagedList分頁套件
            //int currentPage = page < 1 ? 1 : page;
            //var roomOrders = db.tOrTable.OrderBy(m => m.fOrNum).ToList();
            //var result = roomOrders.ToPagedList(currentPage, pageSize);
            //return View(result);
            return View();
        }

        public ActionResult ListPagedPartial(string SearchBy, string SearchValue, string txtfOrdate, string txtfOrCheckIn, int page = 1)
        {
            var roomOrders = new List<TOrTable>();
            //pagedList分頁套件
            int currentPage = page < 1 ? 1 : page;

            if (SearchBy == null && SearchValue == null && txtfOrdate == null && txtfOrCheckIn == null)
                roomOrders = _db.TOrTable.OrderBy(m => m.FOrNum).ToList();
            else
            {
                var searchModel = new BookingOrderSearchModel();
                var business = new BookingOrderSearchLogic(_db);
                //if (SearchValue == null)
                //    return Json(orderList, JsonRequestBehavior.AllowGet);

                searchModel.droplist = SearchBy;
                searchModel.droplistInputValue = SearchValue;

                //txtfOrdate = txtfOrdate.Replace("/", "-");
                searchModel.txtfOrdateValue = txtfOrdate;
                searchModel.txtfOrdateValueInputValue = txtfOrdate;

                //txtfOrCheckIn = txtfOrCheckIn.Replace("/", "-");
                searchModel.txtfOrCheckInValue = txtfOrCheckIn;
                searchModel.txtfOrCheckInValueInputValue = txtfOrCheckIn;

                roomOrders = business.GetSearchBookOrders(searchModel);
            }
            var result = roomOrders.ToPagedList(currentPage, pageSize);
            ViewData.Model = result;
            return PartialView("List_pagedAjax");
        }
        //把商業邏輯拆開、查詢條件弄成model的複雜寫法
        //public ActionResult GetBookingOrderSearchingData(string SearchBy, string SearchValue, string txtfOrdate, string txtfOrCheckIn)
        //{
        //    var searchModel = new BookingOrderSearchModel();
        //    var business = new BookingOrderSearchLogic();
        //    //if (SearchValue == null)
        //    //    return Json(orderList, JsonRequestBehavior.AllowGet);

        //    searchModel.droplist = SearchBy;
        //    searchModel.droplistInputValue = SearchValue;

        //    //txtfOrdate = txtfOrdate.Replace("/", "-");
        //    searchModel.txtfOrdateValue = txtfOrdate;
        //    searchModel.txtfOrdateValueInputValue = txtfOrdate;

        //    //txtfOrCheckIn = txtfOrCheckIn.Replace("/", "-");
        //    searchModel.txtfOrCheckInValue = txtfOrCheckIn;
        //    searchModel.txtfOrCheckInValueInputValue = txtfOrCheckIn;

        //    var model = business.GetSearchBookOrders(searchModel);

        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}


        public JsonResult GetAutoCompleteSearchData(string searchValue, string SearchBy)
        {
            //成功的簡單版本
            List<cfOrNumSearch> fOrNumSearchValue = _db.TOrTable.Where(x => x.FOrNum.Contains(searchValue)).Select(x => new cfOrNumSearch
            {
                fOrId = x.FOrId,
                fOrNum = x.FOrNum
            }).ToList();

            //尚未成功
            //List<cfOrNumSearch> fOrNumSearchValue = null;
            //List<cfOrGuestOneIdSearch> fOrGuestOneIdSearchValue = null;
            //Enumerable

            //if (SearchBy == "OrNum")
            //{
            //    fOrNumSearchValue = db.tOrTable.Where(x => x.fOrNum.Contains(searchValue)).Select(x => new cfOrNumSearch
            //    {
            //        fOrId = x.fOrId,
            //        fOrNum = x.fOrNum
            //    }).ToList();
            //    return new JsonResult { Data = fOrNumSearchValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //}
            //else
            //{
            //    //ERROR
            //    //fOrGuestOneIdSearchValue = db.tOrTable.AsEnumerable().Where(x => (x.fOrGuestOneId).ToString().Contains(searchValue));
            //    //fOrGuestOneIdSearchValue = db.tOrTable.AsEnumerable().Where(x => (x.fOrGuestOneId).ToString().Contains(searchValue)).ToList();

            //    int i = Convert.ToInt32(searchValue);
            //    fOrGuestOneIdSearchValue = db.tOrTable.Where(x => x.fOrGuestOneId == i).Select(x => new cfOrGuestOneIdSearch
            //    {
            //        fOrGuestOneId=x.fOrGuestOneId

            //    }).ToList();
            //    return new JsonResult { Data = fOrNumSearchValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //}

            return Json(new { Data = fOrNumSearchValue});

        }


        public ActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction("List");

            TMemberdata orGeustTwoData = null;
            TMempetdata orMemPet = null;
            TTryPetTable orTryPetData = null;
            TActivitydata orGuestOneActivityAData = null;
            TActivitydata orGuestOneActivityBData = null;
            TActivitydata orGuestOneActivityCData = null;
            TActivitydata orGuestTwoActivityAData = null;
            TActivitydata orGuestTwoActivityBData = null;
            TActivitydata orGuestTwoActivityCData = null;

            var order = _db.TOrTable.FirstOrDefault(m => m.FOrId == id);

            var orGeustOneData = (from o in _db.TOrTable
                                  from m in _db.TMemberdata
                                  where o.FOrId == id && o.FOrGuestOneId == m.FMeId
                                  select m).FirstOrDefault();
            var orRoomData = (from o in _db.TOrTable
                              from r in _db.TRmTable
                              where o.FOrId == id && o.FOrRoomId == r.FRmId
                              select r).FirstOrDefault();

            //if (order.fOrGuestTwoId != null)
            //{
            orGeustTwoData = (from o in _db.TOrTable
                              from m in _db.TMemberdata
                              where o.FOrId == id && o.FOrGuestTwoId == m.FMeId
                              select m).FirstOrDefault();
            //}
            //if (order.fOrPetId != null){
            orMemPet = (from o in _db.TOrTable
                        from mp in _db.TMempetdata
                        where o.FOrId == id && o.FOrTryPet == mp.FPeMemNumber
                        select mp).FirstOrDefault();
            //}
            //if (order.fOrTryPetId != null){   
            orTryPetData = (from o in _db.TOrTable
                            from p in _db.TTryPetTable
                            where o.FOrId == id && o.FOrTryPetId == p.FTryPetId
                            select p).FirstOrDefault();
            //}

            //if (order.fOrGuestOneActivityA != null){

            orGuestOneActivityAData = (from o in _db.TOrTable
                                       from a in _db.TActivitydata
                                       where o.FOrId == id && o.FOrGuestOneActivityA == a.FActivityId
                                       select a).FirstOrDefault();
            //}
            //if (order.fOrGuestOneActivityB != null){

            orGuestOneActivityBData = (from o in _db.TOrTable
                                       from a in _db.TActivitydata
                                       where o.FOrId == id && o.FOrGuestOneActivityB == a.FActivityId
                                       select a).FirstOrDefault();
            //}
            //if (order.fOrGuestOneActivityC != null){

            orGuestOneActivityCData = (from o in _db.TOrTable
                                       from a in _db.TActivitydata
                                       where o.FOrId == id && o.FOrGuestOneActivityC == a.FActivityId
                                       select a).FirstOrDefault();
            //}

            //if (order.fOrGuestTwoActivityA != null){

            orGuestTwoActivityAData = (from o in _db.TOrTable
                                       from a in _db.TActivitydata
                                       where o.FOrId == id && o.FOrGuestTwoActivityA == a.FActivityId
                                       select a).FirstOrDefault();
            //}
            //if (order.fOrGuestTwoActivityB != null){

            orGuestTwoActivityBData = (from o in _db.TOrTable
                                       from a in _db.TActivitydata
                                       where o.FOrId == id && o.FOrGuestTwoActivityB == a.FActivityId
                                       select a).FirstOrDefault();
            //}
            //if (order.fOrGuestTwoActivityC != null){

            orGuestTwoActivityCData = (from o in _db.TOrTable
                                       from a in _db.TActivitydata
                                       where o.FOrId == id && o.FOrGuestTwoActivityC == a.FActivityId
                                       select a).FirstOrDefault();
            //}
            //應用roomOrderListViewModel
            cRoomOrderListViewModel viewModel = new cRoomOrderListViewModel();
            viewModel.tOrTableOrderContent = order;
            viewModel.tMemberdataOneContent = orGeustOneData;
            viewModel.tMemberdataTwoContent = orGeustTwoData;
            viewModel.tG1ActivitydataAContent = orGuestOneActivityAData;
            viewModel.tG1ActivitydataBContent = orGuestOneActivityBData;
            viewModel.tG1ActivitydataCContent = orGuestOneActivityCData;
            viewModel.tG2ActivitydataAContent = orGuestTwoActivityAData;
            viewModel.tG2ActivitydataBContent = orGuestTwoActivityBData;
            viewModel.tG2ActivitydataCContent = orGuestTwoActivityCData;
            viewModel.tRmTableContent = orRoomData;
            viewModel.tTryPetTableContent = orTryPetData;
            viewModel.tMempetdataContent = orMemPet;

            return View(viewModel);


        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");

            TOrTable order = _db.TOrTable.FirstOrDefault(o => o.FOrId == id);
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(TOrTable order)
        {
            if (string.IsNullOrEmpty(order.FOrNum))
                return RedirectToAction("List");
            TOrTable orderNew = _db.TOrTable.FirstOrDefault(o => o.FOrId == order.FOrId);
            if (orderNew != null)
            {
                orderNew.FOrNum = order.FOrNum;
                orderNew.FOrDate = order.FOrDate;
                orderNew.FOrCheckIn = order.FOrCheckIn;
                orderNew.FOrCheckOut = order.FOrCheckOut;
                orderNew.FOrday = order.FOrday;
                orderNew.FOrGuestOneId = order.FOrGuestOneId;
                orderNew.FOrGuestTwoId = order.FOrGuestTwoId;
                orderNew.FOrPeople = order.FOrPeople;
                orderNew.FOrRoomId = order.FOrRoomId;
                orderNew.FOrPetId = order.FOrPetId;
                orderNew.FOrTryPet = order.FOrTryPet;
                orderNew.FOrTryPetId = order.FOrTryPetId;
                orderNew.FOrGuestOneActivityA = order.FOrGuestOneActivityA;
                orderNew.FOrGuestOneActivityB = order.FOrGuestOneActivityB;
                orderNew.FOrGuestOneActivityC = order.FOrGuestOneActivityC;
                orderNew.FOrGuestTwoActivityA = order.FOrGuestTwoActivityA;
                orderNew.FOrGuestTwoActivityB = order.FOrGuestTwoActivityB;
                orderNew.FOrGuestTwoActivityC = order.FOrGuestTwoActivityC;
                orderNew.FOrTotalPrice = order.FOrTotalPrice;
                _db.SaveChanges();
            }
            return RedirectToAction("List");
        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TOrTable p)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.TOrTable.Add(p);
                    _db.SaveChanges();
                }
                catch (Exception)
                {
                    int i = 1;
                }
            }
            return RedirectToAction("List");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            TOrTable p = _db.TOrTable.FirstOrDefault(m => m.FOrId == id);
            if (p != null)
            {
                _db.TOrTable.Remove(p);
                _db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        //也是全寫死的做法，不知道如何更改檔名
        //public void exportToExcel()
        //{
        //    var roomOrders = (new dbLLWPEntities1()).tOrTable.ToList();
        //    ExcelPackage pck = new ExcelPackage();
        //    //var roomOrderdata=roomOrders.
        //    //加入工作表
        //    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("工作表");
        //    //工作表名稱
        //    ws.Name = "試住訂單表";
        //    ws.Workbook.Properties.Title = "試住訂單表";

        //    //var displaynames=db.tOrtable.XXXXX
        //    //    for (var i = 0; i < roomOrders.Count; i++)
        //    //    {
        //    //        for (var j = 0; j < 20; j++)
        //    //            ws.Cells[1, j].Value = roomOrders[i].[j].displayname
        //    //        }

        //    ws.Cells["A1"].Value = "訂單編號";
        //    ws.Cells["B1"].Value = "訂單日期";
        //    ws.Cells["C1"].Value = "入住日";
        //    ws.Cells["D1"].Value = "退房日";
        //    ws.Cells["E1"].Value = "天數";
        //    ws.Cells["F1"].Value = "房客一";
        //    ws.Cells["G1"].Value = "房客二";
        //    ws.Cells["H1"].Value = "人數";
        //    ws.Cells["I1"].Value = "房間序號";
        //    ws.Cells["J1"].Value = "攜帶寵物";
        //    ws.Cells["K1"].Value = "參加試養計畫";
        //    ws.Cells["L1"].Value = "試養寵物序號";
        //    ws.Cells["M1"].Value = "房客一活動A";
        //    ws.Cells["N1"].Value = "房客一活動B";
        //    ws.Cells["O1"].Value = "房客一活動C";
        //    ws.Cells["P1"].Value = "房客二活動A";
        //    ws.Cells["Q1"].Value = "房客二活動B";
        //    ws.Cells["R1"].Value = "房客二活動C";
        //    ws.Cells["S1"].Value = "訂單總金額";


        //    //foreach (roomOrder in roomOrders)
        //    //{
        //    //    for (var i = 0; i < roomOrders.Count; i++)
        //    //    {
        //    //        for (var j = 0; j < 20; j++)
        //    //            ws.Cells[1, j].Value = roomOrders[i].[j].displayname
        //    //        }
        //    //}
        //    //第幾列開始放資料
        //    int rowStart = 2;
        //    foreach (var item in roomOrders)
        //    {
        //        ws.Cells[string.Format("A{0}", rowStart)].Value = item.fOrNum;
        //        ws.Cells[string.Format("B{0}", rowStart)].Value = item.fOrDate;
        //        ws.Cells[string.Format("C{0}", rowStart)].Value = item.fOrCheckIn;
        //        ws.Cells[string.Format("D{0}", rowStart)].Value = item.fOrCheckOut;
        //        ws.Cells[string.Format("E{0}", rowStart)].Value = item.fOrday;
        //        ws.Cells[string.Format("F{0}", rowStart)].Value = item.fOrGuestOneId;
        //        ws.Cells[string.Format("G{0}", rowStart)].Value = item.fOrGuestTwoId;
        //        ws.Cells[string.Format("H{0}", rowStart)].Value = item.fOrPeople;
        //        ws.Cells[string.Format("I{0}", rowStart)].Value = item.fOrRoomId;
        //        ws.Cells[string.Format("J{0}", rowStart)].Value = item.fOrPetId;
        //        ws.Cells[string.Format("K{0}", rowStart)].Value = item.fOrTryPet;
        //        ws.Cells[string.Format("L{0}", rowStart)].Value = item.fOrTryPetId;
        //        ws.Cells[string.Format("M{0}", rowStart)].Value = item.fOrGuestOneActivityA;
        //        ws.Cells[string.Format("N{0}", rowStart)].Value = item.fOrGuestOneActivityB;
        //        ws.Cells[string.Format("O{0}", rowStart)].Value = item.fOrGuestOneActivityC;
        //        ws.Cells[string.Format("P{0}", rowStart)].Value = item.fOrGuestTwoActivityA;
        //        ws.Cells[string.Format("Q{0}", rowStart)].Value = item.fOrGuestTwoActivityB;
        //        ws.Cells[string.Format("R{0}", rowStart)].Value = item.fOrGuestTwoActivityC;
        //        ws.Cells[string.Format("S{0}", rowStart)].Value = item.fOrTotalPrice;
        //        rowStart++;
        //    }

        //    //依內容調整寬度
        //    ws.Cells["A:AZ"].AutoFitColumns();

        //    //先清除Response中雜七雜八資料
        //    Response.Clear();
        //    //設定MIME類型
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    //設定預設檔名，Q:檔名改不了?
        //    Response.AddHeader("content-disposition", "attachment: filename=" + "試住訂單表.xlsx");
        //    //用 BinaryWrite 輸出byte[]檔案
        //    Response.BinaryWrite(pck.GetAsByteArray());
        //    Response.End();


        //}
        public ActionResult exportToExcel()
        {
            List<TOrTable> rangerList = _db.TOrTable.ToList();

            //建立Excel
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            ExcelPackage ep = new ExcelPackage();

            //建立第一個Sheet，後方為定義Sheet的名稱
            ExcelWorksheet sheet = ep.Workbook.Worksheets.Add("FirstSheet");

            int col = 1;    //欄:直的，因為要從第1欄開始，所以初始為1

            //第1列是標題列 
            sheet.Cells[1, col++].Value = "訂單編號";
            sheet.Cells[1, col++].Value = "訂單日期";
            sheet.Cells[1, col++].Value = "入住日";
            sheet.Cells[1, col++].Value = "退房日";
            sheet.Cells[1, col++].Value = "天數";
            sheet.Cells[1, col++].Value = "房客一";
            sheet.Cells[1, col++].Value = "房客二";
            sheet.Cells[1, col++].Value = "人數";
            sheet.Cells[1, col++].Value = "房間序號";
            sheet.Cells[1, col++].Value = "攜帶寵物";
            sheet.Cells[1, col++].Value = "參加試養計畫";
            sheet.Cells[1, col++].Value = "試養寵物序號";
            sheet.Cells[1, col++].Value = "房客一活動A";
            sheet.Cells[1, col++].Value = "房客一活動B";
            sheet.Cells[1, col++].Value = "房客一活動C";
            sheet.Cells[1, col++].Value = "房客二活動A";
            sheet.Cells[1, col++].Value = "房客二活動B";
            sheet.Cells[1, col++].Value = "房客二活動C";
            sheet.Cells[1, col++].Value = "訂單總金額";

            //資料從第2列開始
            int row = 2;    //列:橫的
            foreach (TOrTable item in rangerList)
            {
                col = 1;//每換一列，欄位要從1開始
                        //指定Sheet的欄與列(欄名列號ex.A1,B20，在這邊都是用數字)，將資料寫入
                sheet.Cells[row, col++].Value = item.FOrNum;
                sheet.Cells[row, col++].Value = item.FOrDate;
                sheet.Cells[row, col++].Value = item.FOrCheckIn;
                sheet.Cells[row, col++].Value = item.FOrCheckOut;
                sheet.Cells[row, col++].Value = item.FOrday;
                sheet.Cells[row, col++].Value = item.FOrGuestOneId;
                sheet.Cells[row, col++].Value = item.FOrGuestTwoId;
                sheet.Cells[row, col++].Value = item.FOrPeople;
                sheet.Cells[row, col++].Value = item.FOrRoomId;
                sheet.Cells[row, col++].Value = item.FOrPetId;
                sheet.Cells[row, col++].Value = item.FOrTryPet;
                sheet.Cells[row, col++].Value = item.FOrTryPetId;
                sheet.Cells[row, col++].Value = item.FOrGuestOneActivityA;
                sheet.Cells[row, col++].Value = item.FOrGuestOneActivityB;
                sheet.Cells[row, col++].Value = item.FOrGuestOneActivityC;
                sheet.Cells[row, col++].Value = item.FOrGuestTwoActivityA;
                sheet.Cells[row, col++].Value = item.FOrGuestTwoActivityB;
                sheet.Cells[row, col++].Value = item.FOrGuestTwoActivityC;
                sheet.Cells[row, col++].Value = item.FOrTotalPrice;
                row++;
            }

            //因為ep.Stream是加密過的串流，故要透過SaveAs將資料寫到MemoryStream，將MemoryStream使用FileStreamResult回傳到前端
            MemoryStream fileStream = new MemoryStream();
            ep.SaveAs(fileStream);
            ep.Dispose();//如果這邊不下Dispose，建議此ep要用using包起來，但是要記得先將資料寫進MemoryStream在Dispose。
            fileStream.Position = 0;//不重新將位置設為0，excel開啟後會出現錯誤
            return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "試住訂單資料表.xlsx");
        }

        public ActionResult ColumnSort(string sortOrder)
        {
            //return View + viewbag:頁面會刷新
            //ViewBag.fOrNumSortParm = String.IsNullOrEmpty(sortOrder) ? "fOrNum_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            //用session紀錄，點擊很多標題列時會亂掉
            int? count = 1;
            var roomOrders = from s in _db.TOrTable
                             select s;

            var temp = HttpContext.Session.GetObject<string>(CDictionary.SK_TEMP);

            if (temp == sortOrder)
            {
                //ERROR
                //Session["Count"] as int? = count;
                count = HttpContext.Session.GetObject<int?>(CDictionary.SK_COUNT);
                count++;
            }

            switch (sortOrder)
            {
                case "fOrNumSortParm":
                    if (count % 2 != 0)
                        roomOrders = roomOrders.OrderByDescending(s => s.FOrNum);
                    else
                        roomOrders = roomOrders.OrderBy(s => s.FOrNum);
                    break;
                case "fOrDateSortParm":
                    if (count % 2 != 0)
                        roomOrders = roomOrders.OrderByDescending(s => s.FOrDate);
                    else
                        roomOrders = roomOrders.OrderBy(s => s.FOrDate);
                    break;
                case "fOrCheckInSortParm":
                    if (count % 2 != 0)
                        roomOrders = roomOrders.OrderByDescending(s => s.FOrCheckIn);
                    else
                        roomOrders = roomOrders.OrderBy(s => s.FOrCheckIn);
                    break;
                case "fOrCheckOutSortParm":
                    if (count % 2 != 0)
                        roomOrders = roomOrders.OrderByDescending(s => s.FOrCheckOut);
                    else
                        roomOrders = roomOrders.OrderBy(s => s.FOrCheckOut);
                    break;
                case "fOrdaySortParm":
                    if (count % 2 != 0)
                        roomOrders = roomOrders.OrderByDescending(s => s.FOrday);
                    else
                        roomOrders = roomOrders.OrderBy(s => s.FOrday);
                    break;
                case "fOrGuestOneIdSortParm":
                    if (count % 2 != 0)
                        roomOrders = roomOrders.OrderByDescending(s => s.FOrGuestOneId);
                    else
                        roomOrders = roomOrders.OrderBy(s => s.FOrGuestOneId);
                    break;
                case "fOrRoomIdSortParm":
                    if (count % 2 != 0)
                        roomOrders = roomOrders.OrderByDescending(s => s.FOrRoomId);
                    else
                        roomOrders = roomOrders.OrderBy(s => s.FOrRoomId);
                    break;
                case "fOrTotalPriceSortParm":
                    if (count % 2 != 0)
                        roomOrders = roomOrders.OrderByDescending(s => s.FOrTotalPrice);
                    else
                        roomOrders = roomOrders.OrderBy(s => s.FOrTotalPrice);
                    break;

            }

            if (temp != sortOrder)
                HttpContext.Session.SetObject(CDictionary.SK_COUNT, 1);
            else
                HttpContext.Session.SetObject(CDictionary.SK_COUNT, count);

            HttpContext.Session.SetObject(CDictionary.SK_TEMP, sortOrder);

            return Json(roomOrders);
        }

        //成功的簡單寫法
        //public JsonResult GetSearchingData(string SearchBy, string SearchValue)
        //{
        //    //1.接收View回傳的Searching Value
        //    //2.Searching Value和資料庫比對
        //    //3.return Searching Value
        //    List<tOrTable> orderList = new List<tOrTable>();
        //    if (SearchBy == "OrGuestOneId")
        //    {
        //        try
        //        {
        //            int orGuestOneId = Convert.ToInt32(SearchValue);
        //            orderList = db.tOrTable.Where(x => x.fOrGuestOneId == orGuestOneId || SearchValue == null).ToList();
        //        }
        //        catch (Exception)
        //        {
        //            int i = 1;
        //            //MessageBox.Show("輸入錯誤，請重新輸入");
        //        }
        //        //catch(Exception e)
        //        //{
        //        //    Console.WriteLine("輸入錯誤，請重新輸入");
        //        //}
        //        return Json(orderList, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        try
        //        {
        //            //uint orderNumber = Convert.ToUInt32(SearchValue);//太大，無法丟到orderNumber，程式會跳掉
        //            if (SearchValue == null)
        //            {
        //                return Json(orderList, JsonRequestBehavior.AllowGet);
        //            }
        //            string orderNumber = SearchValue.ToString();
        //            //orderList = db.tOrTable.Where(x => x.fOrNum == orderNumber || SearchValue == null).ToList();
        //            orderList = db.tOrTable.Where(x => x.fOrNum.Contains(orderNumber) || SearchValue == null).ToList();
        //        }
        //        catch
        //        {
        //            int i = 2;
        //        }
        //        return Json(orderList, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}
