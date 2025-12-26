using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore.iGuestData.Dtos
{
    public class GetReservationInfoDto
    {
        //public Guid ReservationKey { get; set; }
        public string DocNo { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        //public Guid? RoomTypeKey { get; set; }
        public string RoomTypeName { get; set; }
        //public Guid? RateTypeKey { get; set; }
        public string RateTypeName { get; set; }
        //public Guid? RoomKey { get; set; }
        public string RoomUnit { get; set; }

    }
}
