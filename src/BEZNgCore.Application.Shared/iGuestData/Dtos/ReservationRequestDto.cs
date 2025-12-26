using System;
using Abp.Application.Services.Dto;

namespace BEZNgCore.IStay.Dtos
{
    public class ReservationRequestDto : EntityDto<Guid>
    {
        public Guid ReservationRequestKey { get; set; }

        public string ReservationRequestID { get; set; }

        public Guid ReservationKey { get; set; }

        public DateTime? RequestDate { get; set; }

        public DateTime? ResponseDate { get; set; }

        public string DocNo { get; set; }

        public int? Adult { get; set; }

        public int? Child { get; set; }

        public string RateCode { get; set; }

        public string Unit { get; set; }

        public string RequestTypeName { get; set; }

        public string StatusDesc { get; set; }

        public string GuestRequest { get; set; }

        public string HotelResponse { get; set; }
       
    }
}