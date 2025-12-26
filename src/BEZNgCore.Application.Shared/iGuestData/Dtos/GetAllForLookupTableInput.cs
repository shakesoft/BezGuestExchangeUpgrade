using Abp.Application.Services.Dto;

namespace BEZNgCore.IStay.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}