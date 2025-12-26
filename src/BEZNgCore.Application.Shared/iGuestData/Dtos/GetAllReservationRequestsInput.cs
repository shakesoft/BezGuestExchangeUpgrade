using Abp.Application.Services.Dto;
using System;

namespace BEZNgCore.IStay.Dtos
{
    public class GetAllReservationRequestsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public bool? CancelFilter { get; set; }
        public bool? OpenFilter { get; set; }

        public bool? ProgressFilter { get; set; }

        public bool? CompletedFilter { get; set; }

        public DateTime? MaxRequestDateFilter { get; set; }
        public DateTime? MinRequestDateFilter { get; set; }

    }

    public class RequestDate
    {
        public DateTime? MaxRequestDateFilter { get; set; }
        public DateTime? MinRequestDateFilter { get; set; }
    }
}