using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
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

        public static string DateToString (string Year, string Month, string Day, string InOrOut)
        {
            string[] YearArray = Year.Split(' ');
            string[] MonthArray = Month.Split(' ');
            string[] DayArray = Day.Split(' ');

            Year = YearArray[0];
            Month = MonthArray[1].Length == 1 ? "0" + MonthArray[1] : MonthArray[1];

            if (InOrOut == "out")
                Day = DayArray[1].Length == 1 ? "0" + (Convert.ToInt32(DayArray[1])+1).ToString() : (Convert.ToInt32(DayArray[1]) + 1).ToString();
            else
                Day = DayArray[1].Length == 1 ? "0" + DayArray[1] : DayArray[1];

            var date = string.Format("{0}-{1}-{2}", Year, Month, Day);

            return date;
        }

        public static string fOrNumRandomId()
        {
            Random rand = new Random();

            var randNumber = rand.Next(100000, 1000000);

            var Year = DateTime.Now.Year.ToString();
            var Month = DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
            var Day = DateTime.Now.Day.ToString().Length == 1 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();

            return string.Format("{0}{1}{2}{3}", Year, Month, Day, randNumber);
        }

        public static string DateTimeNow()
        {
            var Year = DateTime.Now.Year.ToString();
            var Month = DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
            var Day = DateTime.Now.Day.ToString().Length == 1 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();

            return string.Format("{0}-{1}-{2}", Year, Month, Day);
        }

        public static int?[] FillingArray(int?[] array)
        {
            int?[] emptyArray = new int?[3];
            Array.Copy(array, emptyArray, array.Length);

            return emptyArray;
        }

        public static decimal DaysMoney(int days, int roomType)
        {
            var money = roomType == 1 ? days * 1700 : days * 1900;

            return money;
        }

        public static void SendEmail(string emailAddress, string body)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(emailAddress);
            msg.From = new MailAddress("longlifewithpet@gmail.com", "榕沛社區", System.Text.Encoding.UTF8);
            msg.Subject = "榕沛會員：密碼更改";
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = "驗證碼為:" + body;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("longlifewithpet@gmail.com", "llwp3135");
            client.Host = "smtp.gmail.com";
            client.Port = 25;
            client.EnableSsl = true;
            client.Send(msg);
            client.Dispose();
            msg.Dispose();
        }
    }
}
