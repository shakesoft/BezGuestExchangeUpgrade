using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore.iGuestData.Dtos
{
    public class GetGuestReservationDto
    {
        public GetReservationInfoDto GetReservationInfo { get; set; }
        public GetGuestInfoDto GetGuestInfo { get; set; }
    }
}
