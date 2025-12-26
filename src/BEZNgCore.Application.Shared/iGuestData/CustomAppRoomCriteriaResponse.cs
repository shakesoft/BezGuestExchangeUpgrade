using BEZNgCore.iGuestData.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore.iGuestData
{
    public class CustomAppRoomCriteriaResponse<T>
    {
        public string Criteria { get; set; }

        public string Reason { get; set; }

        public string GuestName { get; set; }

        public string BookingNo { get; set; }

        public static CustomAppRoomCriteriaResponse<T> RoomChargeGuestResponse(GetRoomTenderCirteriaDto dto)
        {
            return new CustomAppRoomCriteriaResponse<T> { Criteria = dto.Criteria, Reason = dto.Reason,GuestName=dto.GuestName,BookingNo=dto.BookingNo };
        }
        public static CustomAppRoomCriteriaResponse<T> ExceptionResponse()
        {
            return new CustomAppRoomCriteriaResponse<T> { Criteria = "N", Reason = "We’re sorry, but there was an issue processing your request", GuestName = "", BookingNo = "" };
        }
    }
}
