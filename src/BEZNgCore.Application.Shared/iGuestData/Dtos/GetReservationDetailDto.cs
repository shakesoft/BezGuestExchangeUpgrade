using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore.IStay.Dtos
{
    public class GetReservationDetailDto
    {
        public Guid? ReservationKey { get; set; }
        public Guid? GuestKey { get; set; }
        public Guid? RateTypeKey { get; set; }
        public Guid? RoomTypeKey { get; set; }

        public DateTime? DocDate { get; set; }
        public string DocNo { get; set; }

        public int? Status { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public string Remark { get; set; }

        public Guid? PurposeStayKey { get; set; }

        public int? PreCheckInCount { get; set; }

        public string RateDescription { get; set; }

        public string RoomSeq { get; set; }

        public string RoomDescription { get; set; }

        public bool btnUpdate { get; set; }

        public bool btnCheckIn { get; set; }
        
        public bool btnCheckOut { get; set; }
        public string Nights
        {
            get
            {
                TimeSpan ts = this.CheckOutDate - this.CheckInDate;
                return ts.Days.ToString();
            }
        }
    }
}
