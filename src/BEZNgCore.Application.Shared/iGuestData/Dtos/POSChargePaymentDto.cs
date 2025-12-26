using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore.iGuestData.Dtos
{
    public class POSChargePaymentDto
    {
        public DateTime Date { get; set; }

        public string Time { get; set; }

        public decimal? Amount { get; set; }
        public string Docketno { get; set; }

        public string Outlet { get; set; }

        public string Payment { get; set; }

        public string Period { get; set; }                

        public string Covers { get; set; }
    }
}
