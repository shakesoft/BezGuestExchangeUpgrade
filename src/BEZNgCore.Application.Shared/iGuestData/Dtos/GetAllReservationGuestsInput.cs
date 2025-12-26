using Abp.Application.Services.Dto;
using System;

namespace BEZNgCore.IStay.Dtos
{
    public class GetAllReservationGuestsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public Guid? ReservationGuestKeyFilter { get; set; }

        public Guid? ReservationKeyFilter { get; set; }

        public Guid? ReservationRoomKeyFilter { get; set; }

        public Guid? GuestKeyFilter { get; set; }

        public int? MaxSortFilter { get; set; }
        public int? MinSortFilter { get; set; }

        public int? MaxSyncFilter { get; set; }
        public int? MinSyncFilter { get; set; }

        public int? MaxSeqFilter { get; set; }
        public int? MinSeqFilter { get; set; }

        public string TSFilter { get; set; }

        public int? MaxGuestStayFilter { get; set; }
        public int? MinGuestStayFilter { get; set; }

        public DateTime? MaxCheckInDateFilter { get; set; }
        public DateTime? MinCheckInDateFilter { get; set; }

        public DateTime? MaxCheckOutDateFilter { get; set; }
        public DateTime? MinCheckOutDateFilter { get; set; }

        public Guid? ShareGuestKeyFilter { get; set; }

        public DateTime? MaxX_CheckInDateFilter { get; set; }
        public DateTime? MinX_CheckInDateFilter { get; set; }

        public DateTime? MaxX_CheckOutDateFilter { get; set; }
        public DateTime? MinX_CheckOutDateFilter { get; set; }

        public int? MaxReservationStatusFilter { get; set; }
        public int? MinReservationStatusFilter { get; set; }

        public DateTime? MaxX_BillCheckInDateFilter { get; set; }
        public DateTime? MinX_BillCheckInDateFilter { get; set; }

        public DateTime? MaxX_BillCheckOutDateFilter { get; set; }
        public DateTime? MinX_BillCheckOutDateFilter { get; set; }

        public DateTime? MaxX_Bill2CheckInDateFilter { get; set; }
        public DateTime? MinX_Bill2CheckInDateFilter { get; set; }

        public DateTime? MaxX_Bill2CheckOutDateFilter { get; set; }
        public DateTime? MinX_Bill2CheckOutDateFilter { get; set; }

        public DateTime? MaxX_Bill3CheckInDateFilter { get; set; }
        public DateTime? MinX_Bill3CheckInDateFilter { get; set; }

        public DateTime? MaxX_Bill3CheckOutDateFilter { get; set; }
        public DateTime? MinX_Bill3CheckOutDateFilter { get; set; }

        public DateTime? MaxX_Bill4CheckInDateFilter { get; set; }
        public DateTime? MinX_Bill4CheckInDateFilter { get; set; }

        public DateTime? MaxX_Bill4CheckOutDateFilter { get; set; }
        public DateTime? MinX_Bill4CheckOutDateFilter { get; set; }

        public DateTime? MaxX_Bill5CheckInDateFilter { get; set; }
        public DateTime? MinX_Bill5CheckInDateFilter { get; set; }

        public DateTime? MaxX_Bill5CheckOutDateFilter { get; set; }
        public DateTime? MinX_Bill5CheckOutDateFilter { get; set; }

        public DateTime? MaxX_Bill6aCheckInDateFilter { get; set; }
        public DateTime? MinX_Bill6aCheckInDateFilter { get; set; }

        public DateTime? MaxX_Bill6aCheckOutDateFilter { get; set; }
        public DateTime? MinX_Bill6aCheckOutDateFilter { get; set; }

        public DateTime? MaxX_Bill6bCheckInDateFilter { get; set; }
        public DateTime? MinX_Bill6bCheckInDateFilter { get; set; }

        public DateTime? MaxX_Bill6bCheckOutDateFilter { get; set; }
        public DateTime? MinX_Bill6bCheckOutDateFilter { get; set; }

        public DateTime? MaxX_Bill7CheckInDateFilter { get; set; }
        public DateTime? MinX_Bill7CheckInDateFilter { get; set; }

        public DateTime? MaxX_Bill7CheckOutDateFilter { get; set; }
        public DateTime? MinX_Bill7CheckOutDateFilter { get; set; }

    }
}