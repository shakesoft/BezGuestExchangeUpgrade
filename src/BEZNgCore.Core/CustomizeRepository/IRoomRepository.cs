using Abp.Domain.Repositories;
using BEZNgCore.iGuestData.Dtos;
using BEZNgCore.IRepairIAppService.Dto;
using BEZNgCore.IrepairModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEZNgCore.CustomizeRepository
{
    public interface IRoomRepository : IRepository<Room, Guid>
    {
        Task<int> InsertMessage(CMessage message);
        Task<int> UpdateSendDataByID(tblSendDataMisa log);
        Task<List<tblSendDataMisa>> GetXmlByFlag(string status);
        Task<List<GetHotelRoomFloorOutPut>> GetHotelRoomFloor();
        Task<List<GetDashRoomByMaidKeyOutput>> GetRoomByMaidKey();
        Task<List<MaidHasStartedTaskOutput>> GetRoomCountByMaidKey(DateTime dtBusinessDate, string maidKey, string floorNo);
        Task<List<MaidStatusListOutPut>> BindMaidStatusListCount(DateTime dtBusinessDate, string maidKey, string floorNo);
        Task<GetRoomTenderCirteriaDto> GetRoomTenderCirteria(string roomNumber);
        Task<string> UpdatePOSCharge(POSChargeDto p,Guid postcodekey);
        Task<string> UpdatePOSChargePayment(POSChargePaymentDto p,Guid postcodekey,string DcoNo);
        Task<int> GuestExchangeUpdate(GuestExchangeUpdateDto p);
        Task<Guid> GetDetaulfPostCodeForPaymentType();
        Task<Guid> GetPostcodeKeyBy(int GroupId, int LocationId, int ServicePeriodId);

        Task<int> GetGroupIdByRevenue(string revenue);

        Task<int> GetLocationIdByOutlet(string outlet);

        Task<int> GetServciePeriodIdByPeriod(string period);
        Task<Guid> GetPostcodeKeyByPayment(string payment);
        Task<Guid> GetPostcodeKeyByRevenue(string revenue);
        Task<string> GetPaymentByDocketno(string DocketNo);
        Task<string> GetFolioPayment(string payment);

        Task<string> UpdatePOSChargeFolio(POSChargeDto p, Guid PostcodeKey, string docno);

        Task<int> InsertPosHistoryForCharge(POSChargeDto p);
        Task<int> InsertPosHistoryForPayment(POSChargePaymentDto p);



        // List<MaidStatusListOutPut> BindMaidStatusListCountSupAsync(DateTime dtBusinessDate, string maidKey, string floorNo);
        //Task<List<MaidStatusListOutPut>> BindMaidStatusListCountSup(DateTime dtBusinessDate, string maidKey, string floorNo);
    }
}
