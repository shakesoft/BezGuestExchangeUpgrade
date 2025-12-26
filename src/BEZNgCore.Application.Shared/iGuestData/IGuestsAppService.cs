using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BEZNgCore.Dto;
using BEZNgCore.iGuestData;
using BEZNgCore.iGuestData.Dtos;
using BEZNgCore.IStay.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BEZNgCore.IStay
{
    public interface IGuestsAppService : IApplicationService
    {
        Task<PagedResultDto<GetShareGuestForViewDto>> GetAll(GetAllGuestsInput input);

        Task<GetGuestForViewDto> GetGuestForView(Guid id);

        Task<GetGuestForEditOutput> GetGuestForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditGuestDto input);

        Task Delete(EntityDto<Guid> input);

        //Task<FileDto> GetGuestsToExcel(GetAllGuestsForExcelInput input);
        Task<List<GuestRegionLookupTableDto>> GetAllRegionForTableDropdown();
        Task<List<GuestNationalityLookupTableDto>> GetAllNationalityForTableDropdown();

        Task<List<GuestCityLookupTableDto>> GetAllCityForTableDropdown();

        Task<List<GuestTitleLookupTableDto>> GetAllTitleForTableDropdown();

        Task<List<GuestStatusLookupTableDto>> GetAllGuestStatusForTableDropdown();

        Task<List<GuestDocumentTypeLookupTableDto>> GetAllGuestDocumentTypeForTableDropdown();        
        Task<List<GetGuestInfoDto>> GetGuestInfoList(string firstName);
        Task<GetGuestReservationDto> GetGuestReservationData(string docno);

        Task<CustomAppRoomCriteriaResponse<object>> GetRoomChargeGuest(string roomNumber);

        Task<CustomAppResponse<object>> PostPOSPayment(POSChargePaymentInput input);
        Task<CustomAppResponse<object>> PostPOSCharge(POSChargeInput input);

        Task<CustomAppResponse<object>> PostPOSMISA(POSMisaDto input);

        Task<CustomAppResponse<object>> GuestInfoUpdate(GuestExchangeInputDto input);
    }
}