using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace BEZNgCore.IStay.Dtos
{
    public class CreateOrEditReservationRequestDto : EntityDto<Guid?>
    {

        public Guid ReservationRequestKey { get; set; }

        [StringLength(ReservationRequestConsts.MaxReservationRequestIDLength, MinimumLength = ReservationRequestConsts.MinReservationRequestIDLength)]
        public string ReservationRequestID { get; set; }

        public Guid RequestTypeKey { get; set; }

        public int? Status { get; set; }

        public Guid ReservationKey { get; set; }

        public Guid GuestKey { get; set; }

        [StringLength(ReservationRequestConsts.MaxGuestRequestLength, MinimumLength = ReservationRequestConsts.MinGuestRequestLength)]
        public string GuestRequest { get; set; }

        public DateTime? RequestDate { get; set; }

        [StringLength(ReservationRequestConsts.MaxHotelResponseLength, MinimumLength = ReservationRequestConsts.MinHotelResponseLength)]
        public string HotelResponse { get; set; }

        public DateTime? ResponseDate { get; set; }

    }
}