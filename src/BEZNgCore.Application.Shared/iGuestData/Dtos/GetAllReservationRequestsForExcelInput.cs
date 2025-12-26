using Abp.Application.Services.Dto;
using System;

namespace BEZNgCore.IStay.Dtos
{
    public class GetAllReservationRequestsForExcelInput
    {
        public string Filter { get; set; }

        public int? MaxStatusFilter { get; set; }
        public int? MinStatusFilter { get; set; }

        public DateTime? MaxRequestDateFilter { get; set; }
        public DateTime? MinRequestDateFilter { get; set; }

    }
}