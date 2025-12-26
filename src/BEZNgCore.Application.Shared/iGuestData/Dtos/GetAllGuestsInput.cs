using Abp.Application.Services.Dto;
using System;

namespace BEZNgCore.IStay.Dtos
{
    public class GetAllGuestsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }       
    }
}