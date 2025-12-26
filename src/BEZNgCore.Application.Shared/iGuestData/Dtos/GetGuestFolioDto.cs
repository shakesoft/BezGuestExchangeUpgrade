using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore.IStay.Dtos
{
    public class GetGuestFolioDto
    {
        public Guid ReservationKey { get; set; }
        public DateTime? CheckinDate { get; set; }
        public DateTime? CheckoutDate { get; set; }

        public string DocNo { get; set; }

        public int? Adult { get; set; }

        public int? Child { get; set; }

        public string RateCode { get; set; }

        public string Unit { get; set; }

        public decimal? Amount { get; set; }

        public string RoomType { get; set; }

        public string StatusDesc { get; set; }
    }
}
