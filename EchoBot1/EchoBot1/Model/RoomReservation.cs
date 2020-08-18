using System;
using Bot.Builder.Community.Dialogs.FormFlow;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchoBot1.Model
{
    public class RoomReservation
    {
		public DateTime StartDate { get; set; }
		public int NumberOfNightToStay { get; set; }
		public int NumberOfPepole { get; set; }
		public string BedSize { get; set; }

		public override string ToString()
		{
			return $"入住日期：{StartDate}{Environment.NewLine}" +
			$"入住幾晚：{NumberOfNightToStay}{Environment.NewLine}" +
			$"共幾位：{NumberOfPepole}{Environment.NewLine}" +
			$"床型：{BedSize}{Environment.NewLine}";
		}
	}
}
