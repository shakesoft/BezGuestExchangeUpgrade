using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace BEZNgCore.IStay.Dtos
{
    public class GetReservationRequestForEditOutput
    {
        public CreateOrEditReservationRequestDto ReservationRequest { get; set; }

    }
}