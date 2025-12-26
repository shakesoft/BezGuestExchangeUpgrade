using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace BEZNgCore.IStay.Dtos
{
    public class CreateOrEditReservationGuestDto : EntityDto<Guid?>
    {

        [Required]
        public Guid ReservationGuestKey { get; set; }

        public Guid ReservationKey { get; set; }

        public Guid ReservationRoomKey { get; set; }

        public Guid GuestKey { get; set; }

        public int? Sort { get; set; }

        public int? Sync { get; set; }

        [Required]
        public int Seq { get; set; }

        [StringLength(ReservationGuestConsts.MaxTSLength, MinimumLength = ReservationGuestConsts.MinTSLength)]
        public string TS { get; set; }

        public int? GuestStay { get; set; }

        public DateTime? CheckInDate { get; set; }

        public DateTime? CheckOutDate { get; set; }

        public Guid ShareGuestKey { get; set; }

        public DateTime? X_CheckInDate { get; set; }

        public DateTime? X_CheckOutDate { get; set; }

        public int? ReservationStatus { get; set; }

        public DateTime? X_BillCheckInDate { get; set; }

        public DateTime? X_BillCheckOutDate { get; set; }

        public DateTime? X_Bill2CheckInDate { get; set; }

        public DateTime? X_Bill2CheckOutDate { get; set; }

        public DateTime? X_Bill3CheckInDate { get; set; }

        public DateTime? X_Bill3CheckOutDate { get; set; }

        public DateTime? X_Bill4CheckInDate { get; set; }

        public DateTime? X_Bill4CheckOutDate { get; set; }

        public DateTime? X_Bill5CheckInDate { get; set; }

        public DateTime? X_Bill5CheckOutDate { get; set; }

        public DateTime? X_Bill6aCheckInDate { get; set; }

        public DateTime? X_Bill6aCheckOutDate { get; set; }

        public DateTime? X_Bill6bCheckInDate { get; set; }

        public DateTime? X_Bill6bCheckOutDate { get; set; }

        public DateTime? X_Bill7CheckInDate { get; set; }

        public DateTime? X_Bill7CheckOutDate { get; set; }

    }
}