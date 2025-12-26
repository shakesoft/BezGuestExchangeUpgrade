using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore.iGuestData.Dtos
{
    public class POSChargeDto
    {
        public DateTime Date { get; set; }

        public string Time { get; set; }

        public string Room { get; set; }

        public decimal? Charge { get; set; }

        //public string Ref { get; set; }

        //public Guid PostcodeKey { get; set; }

        //public string Source { get; set; }

        //public string Description { get; set; }

        public string Docketno { get; set; }

        public string Outlet { get; set; }

        public string Revenue { get; set; }

        public string Period { get; set; }

        public string Covers { get; set; }
    }
}
