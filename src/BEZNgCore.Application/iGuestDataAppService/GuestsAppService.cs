using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Net.Mail;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using BEZNgCore.Authorization;
using BEZNgCore.Configuration;
using BEZNgCore.CustomizeRepository;
using BEZNgCore.Dto;
using BEZNgCore.EntityFrameworkCore;
using BEZNgCore.iGuestData;
using BEZNgCore.iGuestData.Dtos;
using BEZNgCore.IrepairModel;
using BEZNgCore.IStay.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.POIFS.Properties;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BEZNgCore.IStay
{
    //[AbpAuthorize(AppPermissions.Pages_Guests)]
    public class GuestsAppService : BEZNgCoreAppServiceBase, IGuestsAppService
    {
        private readonly IRepository<Guest, Guid> _guestRepository;
        private readonly IRepository<Item, Guid> _itemRepository;
        private readonly IRepository<GuestIdentityType, Guid> _guestIdentityRepository;
        private readonly IRepository<GuestStatus, Guid> _guestStatusRepository;
        private readonly IRepository<Nationality, Guid> _nationalityRepository;
        //private readonly IGuestsExcelExporter _guestsExcelExporter;
        private readonly IRepository<Nationality, Guid> _lookup_nationalityRepository;
        private readonly IRepository<City, Guid> _lookup_cityRepository;
        private readonly IRepository<Region, Guid> _lookup_regionRepository;
        private readonly IRepository<Title, Guid> _lookup_titleRepository;
        private readonly IRepository<ReservationGuest, Guid> _resGuestRepository;
        //private readonly IRepository<CHistory, Guid> _chistoryRepository;
        private readonly IRepository<Reservation, Guid> _resRepository;
        private readonly IRepository<RateType, Guid> _rateTypeRepository;
        private readonly IRepository<RoomType, Guid> _roomTypeRepository;
        private readonly IRepository<Control, Guid> _controlRepository;
        private readonly IRepository<ReservationBillingContact, Guid> _resbillingcontactRepository;
        private readonly IRepository<ReservationRate, Guid> _resRateRepository;
        private readonly IRepository<Company, Guid> _companyRateRepository;
        private readonly IRepository<Postcode, Guid> _postcodeRepository;
        private readonly IRepository<BillingCode, Guid> _billingcodeRepository;
        private readonly IRepository<Room, Guid> _roomRepository;
        private readonly IRepository<PaymentType, Guid> _paymenttypeRepository;
        private readonly IRepository<GeneralProfile, Guid> _generalProfileRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IEmailSender _emailSender;
        private readonly IRoomRepository _iroomRepository;
        private readonly HttpClient _httpClient;
        private readonly IAppConfigurationAccessor _appConfigurationAccessor;
        //private readonly IMyAppSession _appsession;
        private readonly IAbpSession _abpSession;

        //private readonly Guid sessReservationKey = new Guid("9A2A8F93-82C0-42A3-96EC-EAAD10B4EB73");
        //private readonly string sessdocno = "184561/096";
        ////E4CB5B5B-AD6E-4A23-AB43-0802088A20CA
        //private readonly Guid sessguestkey = new Guid("D825B6C6-DA04-416E-A476-5E3427BC4970");

        public GuestsAppService(IUnitOfWorkManager unitOfWorkManager, IRoomRepository iroomRepository, IRepository<Guest, Guid> guestRepository, IRepository<Nationality, Guid> lookup_nationalityRepository, IRepository<City, Guid> lookup_cityRepository,
            IRepository<Title, Guid> lookup_titleRepository, IRepository<ReservationGuest, Guid> resGuestRepository, IRepository<Nationality, Guid> nationalityRepository,
            IRepository<Reservation, Guid> resRepository, IRepository<RateType, Guid> rateTypeRepository,
            IRepository<RoomType, Guid> roomTypeRepository, IRepository<Control, Guid> controlRepository, IRepository<ReservationBillingContact, Guid> resbillingcontactRepository
            , IRepository<ReservationRate, Guid> resRateRepository, IRepository<Company, Guid> companyRateRepository, IRepository<Postcode, Guid> postcodeRepository,
            IRepository<BillingCode, Guid> billingcodeRepository, IRepository<Room, Guid> roomRepository, IRepository<PaymentType, Guid> paymenttypeRepository,
            IEmailSender emailSender, IRepository<GeneralProfile, Guid> generalProfileRepository, IRepository<Region, Guid> regionRepository,
            IRepository<GuestIdentityType, Guid> guestIdentityRepository, IRepository<GuestStatus, Guid> guestStatusRepository,
            IRepository<Item, Guid> itemRepository, HttpClient httpClient, IAppConfigurationAccessor appConfigurationAccessor, IAbpSession abpSession
)
        {
            _guestRepository = guestRepository;
            _iroomRepository = iroomRepository;
            _itemRepository = itemRepository;
            _guestStatusRepository = guestStatusRepository;
            _guestIdentityRepository = guestIdentityRepository;
            _lookup_regionRepository = regionRepository;
            _lookup_nationalityRepository = lookup_nationalityRepository;
            _lookup_cityRepository = lookup_cityRepository;
            _lookup_titleRepository = lookup_titleRepository;
            _resGuestRepository = resGuestRepository;
            _nationalityRepository = nationalityRepository;
            _resRepository = resRepository;
            _rateTypeRepository = rateTypeRepository;
            _roomTypeRepository = roomTypeRepository;
            _controlRepository = controlRepository;
            _resbillingcontactRepository = resbillingcontactRepository;
            _resRateRepository = resRateRepository;
            _companyRateRepository = companyRateRepository;
            _postcodeRepository = postcodeRepository;
            _billingcodeRepository = billingcodeRepository;
            _roomRepository = roomRepository;
            _paymenttypeRepository = paymenttypeRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _generalProfileRepository = generalProfileRepository;
            _emailSender = emailSender;
            _httpClient = httpClient;
            _appConfigurationAccessor = appConfigurationAccessor;
            _abpSession = abpSession;
        }

        public async Task<PagedResultDto<GetShareGuestForViewDto>> GetAll(GetAllGuestsInput input)
        {
            //var resk = new Guid(_appsession.RESERVATIONKEY);
            var resk = new Guid();
            var filteredGuests = (from rg in _resGuestRepository.GetAll()
                                  join g in _guestRepository.GetAll() on rg.GuestKey equals g.Id into j1
                                  from s1 in j1.DefaultIfEmpty()
                                  where rg.ReservationKey == resk
                                  select new GetShareGuestForViewDto
                                  {
                                      GuestKey = rg.GuestKey,
                                      Name = s1.Name
                                  }).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter));

            var pagedAndFilteredGuests = filteredGuests
                .OrderBy(input.Sorting ?? "Name asc")
                .PageBy(input);

            var totalCount = await filteredGuests.CountAsync();

            return new PagedResultDto<GetShareGuestForViewDto>(
                totalCount,
                await pagedAndFilteredGuests.ToListAsync()
            );
        }

        public async Task<PagedResultDto<GetGuestFolioDto>> GetGuestFolio(GetAllGuestsInput input)
        {
            //var gk = new Guid(_appsession.GUESTKEY);
            var gk = new Guid();
            var outpc = from p in _postcodeRepository.GetAll()
                        join bc in _billingcodeRepository.GetAll() on p.BillingCodeKey equals bc.Id
                        where bc.Code != "NOT APPLICABLE"
                        select p;
            var outpt = _paymenttypeRepository.GetAll().Select(x => x.PostCodeKey);
            var output1 = (from v in _resRepository.GetAll()
                           join y in _rateTypeRepository.GetAll() on v.RateTypeKey equals y.Id
                           join u in _roomRepository.GetAll() on v.RoomKey equals u.Id
                           join rt in _roomTypeRepository.GetAll() on v.RoomTypeKey equals rt.Id
                           join t in _resRateRepository.GetAll() on v.Id equals t.ReservationKey
                           join p in outpc on t.PostCodeKey equals p.Id
                           where v.GuestKey == gk && !outpt.Contains(t.PostCodeKey)
                           select new
                           {
                               ReservationKey = v.Id,
                               CheckinDate = v.CheckInDate,
                               CheckoutDate = v.CheckOutDate,
                               DocNo = v.DocNo,
                               Adult = v.Adult,
                               Child = v.Child,
                               RateCode = y.RateCode,
                               Unit = u.Unit,
                               Amount = t.Rate,
                               RoomType = rt.RoomTypeName,
                               StatusDesc = v.Status == 1 ? "Reservation" : v.Status == 2 ? "Check In" : v.Status == 10 ? "Check Out" : v.Status == 0 ? "Pending"
                               : v.Status == -10 ? "No Show/Cancel" : v.Status == -1 ? "No Show/Cancel" : "Pending"
                           });

            var output = output1.GroupBy(x => new { x.ReservationKey, x.CheckinDate, x.CheckoutDate, x.DocNo, x.Adult, x.Child, x.RateCode, x.Unit, x.RoomType, x.StatusDesc })
                       .Select(o => new GetGuestFolioDto
                       {
                           ReservationKey = o.Key.ReservationKey,
                           CheckinDate = o.Key.CheckinDate,
                           CheckoutDate = o.Key.CheckoutDate,
                           DocNo = o.Key.DocNo,
                           Adult = o.Key.Adult,
                           Child = o.Key.Child,
                           RateCode = o.Key.RateCode,
                           Unit = o.Key.Unit,
                           Amount = o.Sum(x => x.Amount),
                           RoomType = o.Key.RoomType,
                           StatusDesc = o.Key.StatusDesc
                       });

            var pagedAndFilteredGuests = output.OrderBy(input.Sorting ?? "CheckoutDate,DocNo asc").PageBy(input);

            var totalCount = await output.CountAsync();

            return new PagedResultDto<GetGuestFolioDto>(
                totalCount,
                await pagedAndFilteredGuests.ToListAsync()
            );
        }

        public async Task<List<GetGuestInfoDto>> GetGuestInfoList(string firstName)
        {
            var output = (from g in _guestRepository.GetAll()
                          join n in _nationalityRepository.GetAll() on g.NationalityKey equals n.Id into nn
                          from nnn in nn.DefaultIfEmpty()
                          join c in _lookup_cityRepository.GetAll() on g.City equals c.CityCode into cc
                          from ccc in cc.DefaultIfEmpty()
                          join r in _lookup_regionRepository.GetAll() on g.RegionKey equals r.Id into rr
                          from rrr in rr.DefaultIfEmpty()
                          join git in _guestIdentityRepository.GetAll() on g.GuestIdentityTypeKey equals git.Id into gitt
                          from ggitt in gitt.DefaultIfEmpty()
                          join gs in _guestStatusRepository.GetAll() on g.Status equals gs.StatusCode into gss
                          from ggss in gss.DefaultIfEmpty()
                          where g.FirstName == firstName
                          select new GetGuestInfoDto
                          {
                              //Id = g.Id,
                              FirstName = g.FirstName,
                              LastName = g.LastName,
                              //Name = g.Name,
                              Title = g.Title,
                              Gender = g.Gender,
                              DOB = g.DOB,
                              Telephone = g.Tel,
                              Mobile = g.Mobile,
                              Fax = g.Fax,
                              Email = g.EMail,
                              Postal = g.Postal,
                              Address = g.Address,
                              //GuestShortCode = g.ShortCode,
                              PassportIdentityNo = g.Passport,
                              GuestDocType = ggitt.Type,
                              //GuestDocTypeKey = g.GuestIdentityTypeKey,
                              //GuestInterest = g.Interest,
                              //RegionKey = rrr.Id,
                              RegionName = rrr.RegionName,
                              //CityKey = ccc.Id,
                              CityName = ccc.CityCode,
                              //NationalityKey = nnn.Id,
                              NationalityName = nnn.NationalityName,
                              //CountryKey = nnn.Id,
                              CountryName = nnn.NationalityName,
                              //Status = g.Status,
                              //StatusKey = ggss.Id
                          });
            return await output.ToListAsync();
        }
        public async Task<GetGuestReservationDto> GetGuestReservationData(string docno)
        {
            var outputGuest = from g in _guestRepository.GetAll()
                              join bc in _resRepository.GetAll() on g.Id equals bc.GuestKey
                              join n in _nationalityRepository.GetAll() on g.NationalityKey equals n.Id into nn
                              from nnn in nn.DefaultIfEmpty()
                              join c in _lookup_cityRepository.GetAll() on g.City equals c.CityCode into cc
                              from ccc in cc.DefaultIfEmpty()
                              join r in _lookup_regionRepository.GetAll() on g.RegionKey equals r.Id into rr
                              from rrr in rr.DefaultIfEmpty()
                              join git in _guestIdentityRepository.GetAll() on g.GuestIdentityTypeKey equals git.Id into gitt
                              from ggitt in gitt.DefaultIfEmpty()
                              join gs in _guestStatusRepository.GetAll() on g.Status equals gs.StatusCode into gss
                              from ggss in gss.DefaultIfEmpty()
                              where bc.DocNo == docno
                              select new GetGuestInfoDto
                              {
                                  //Id = g.Id,
                                  FirstName = g.FirstName,
                                  LastName = g.LastName,
                                  Title = g.Title,
                                  //Name = g.Name,
                                  Gender = g.Gender,
                                  DOB = g.DOB,
                                  Telephone = g.Tel,
                                  Mobile = g.Mobile,
                                  Fax = g.Fax,
                                  Email = g.EMail,
                                  Postal = g.Postal,
                                  Address = g.Address,
                                  //GuestShortCode = g.ShortCode,
                                  PassportIdentityNo = g.Passport,
                                  GuestDocType = ggitt.Type,
                                  //GuestDocTypeKey = g.GuestIdentityTypeKey,
                                  //GuestInterest = g.Interest,
                                  //RegionKey = rrr.Id,
                                  RegionName = rrr.RegionName,
                                  //CityKey = ccc.Id,
                                  CityName = ccc.CityCode,
                                  //CountryKey = nnn.Id,
                                  CountryName = nnn.NationalityName,
                                  //NationalityKey = nnn.Id,
                                  NationalityName = nnn.NationalityName,
                                  //Status = g.Status,
                                  //StatusKey = ggss.Id
                              };
            var outputRes = from r in _resRepository.GetAll()
                            join roomt in _roomTypeRepository.GetAll() on r.RoomTypeKey equals roomt.Id
                            join ratet in _rateTypeRepository.GetAll() on r.RateTypeKey equals ratet.Id
                            join room in _roomRepository.GetAll() on r.RoomKey equals room.Id
                            where r.DocNo == docno
                            select new GetReservationInfoDto
                            {
                                //ReservationKey = r.Id,
                                DocNo = r.DocNo,
                                CheckIn = r.CheckInDate,
                                CheckOut = r.CheckOutDate,
                                //RoomTypeKey = r.RoomTypeKey,
                                RoomTypeName = roomt.RoomTypeName,
                                //RateTypeKey = r.RateTypeKey,
                                RateTypeName = ratet.RateCode,
                                //RoomKey = r.RoomKey,
                                RoomUnit = room.Unit
                            };
            GetGuestReservationDto gr = new GetGuestReservationDto();
            gr.GetGuestInfo = outputGuest.SingleOrDefault();
            gr.GetReservationInfo = outputRes.SingleOrDefault();

            var result = await Task.Run(() =>
            {
                return gr;
            });
            return result;
        }
        [DontWrapResult]
        public async Task<CustomAppResponse<object>> GuestInfoUpdate(GuestExchangeInputDto input)   
        {
            try
            {
                var guests = await (from g in _guestRepository.GetAll()
                                    where g.Id == input.Id
                                    select g).SingleOrDefaultAsync();
                GuestExchangeUpdateDto guest = new GuestExchangeUpdateDto();
                guest.Id = input.Id;
                guest.FirstName = input.FirstName;
                guest.LastName = input.LastName;
                guest.Title = input.Title;
                guest.Name = input.LastName + ", " + input.FirstName;
                guest.Name = string.IsNullOrEmpty(input.Title) == true ? "" : guest.Name + ", " + input.Title;
                guest.Gender = input.Gender;
                guest.DOB = input.DOB;
                guest.Tel = input.Telephone;
                guest.Mobile = input.Mobile;
                guest.Fax = input.Fax;
                guest.EMail = input.Email;
                guest.Postal = input.Postal;
                guest.Address = input.Address;
                //guest.ShortCode = input.GuestShortCode;
                guest.Passport = input.PassportIdentityNo;
                //if (input.Status != null)
                //{
                //    var s = _guestStatusRepository.GetAll().Where(x => x.Status == input.Status).SingleOrDefault();
                //    if (s != null)
                //    {
                //        guest.Status = s.StatusCode;
                //    }
                //    else
                //    {
                //        GuestStatus gs = new GuestStatus();
                //        gs.Id = Guid.NewGuid();
                //        gs.Status = input.Status;
                //        gs.TenantId = 1;
                //        int max = _guestStatusRepository.GetAll().Max(x => x.StatusCode).Value;
                //        gs.StatusCode = max + 1;
                //        _guestStatusRepository.Insert(gs);
                //        guest.Status = gs.StatusCode;
                //    }
                //}
                //else { guest.Status = 0; }
                if (!string.IsNullOrEmpty(input.GuestDocType))
                {
                    var identity = _guestIdentityRepository.GetAll().Where(x => x.Type == input.GuestDocType).SingleOrDefault();
                    if (identity != null)
                    {
                        guest.GuestIdentityTypeKey = identity.Id;
                    }
                    else
                    {
                        GuestIdentityType type = new GuestIdentityType();
                        type.Id = Guid.NewGuid();
                        type.Type = input.GuestDocType;
                        type.DefaultType = 0;
                        type.Active = 1;
                        type.TenantId = 1;
                        _guestIdentityRepository.Insert(type);
                        guest.GuestIdentityTypeKey = type.Id;
                    }
                }
                else { guest.GuestIdentityTypeKey = null; }
                if (!string.IsNullOrEmpty(input.NationalityName))
                {
                    var identity = _nationalityRepository.GetAll().Where(x => x.NationalityName == input.NationalityName).SingleOrDefault();
                    if (identity != null)
                    {
                        guest.NationalityKey = identity.Id;
                        guest.CountryKey = identity.Id;
                    }
                    else
                    {
                        Nationality type = new Nationality();
                        type.Id = Guid.NewGuid();
                        type.NationalityName = input.NationalityName;
                        type.TenantId = 1;
                        _nationalityRepository.Insert(type);
                        guest.NationalityKey = type.Id;
                        guest.CountryKey = type.Id;
                    }
                }
                guest.Interest = input.GuestInterest;
                if (!string.IsNullOrEmpty(input.RegionName))
                {
                    var identity = _lookup_regionRepository.GetAll().Where(x => x.RegionName == input.RegionName).SingleOrDefault();
                    if (identity != null)
                    {
                        guest.RegionKey = identity.Id;
                    }
                    else
                    {
                        Region type = new Region();
                        type.Id = Guid.NewGuid();
                        type.CountryKey = guest.CountryKey;
                        type.RegionName = input.RegionName;
                        type.TenantId = 1;
                        _lookup_regionRepository.Insert(type);
                        guest.RegionKey = type.Id;
                    }
                }
                if (!string.IsNullOrEmpty(input.CityName))
                {
                    var identity = _lookup_cityRepository.GetAll().Where(x => x.CityCode == input.CityName || x.ShortCode == input.CityName).SingleOrDefault();
                    if (identity != null)
                    {
                        guest.City = input.CityName;
                    }
                    else
                    {
                        City type = new City();
                        type.Id = Guid.NewGuid();
                        //type.ShortCode = input.CityName;
                        type.CityCode = input.CityName;
                        type.TenantId = 1;
                        _lookup_cityRepository.Insert(type);
                        guest.City = input.CityName;
                    }
                }
                //ObjectMapper.Map(guest, guests);
                var result = await _iroomRepository.GuestExchangeUpdate(guest);
                return CustomAppResponse<object>.SuccessResponse(result);
            }
            catch (Exception ex)
            {
                return CustomAppResponse<object>.ErrorResponse(ex.Message);
            }
        }

        [DontWrapResult]
        public async Task<CustomAppResponse<object>> PostPOSChargeBK(POSChargeInput input)
        {
            try
            {
                POSChargeDto dto = new POSChargeDto();
                dto.Date = input.Date;
                dto.Time = input.Time;
                dto.Charge = input.Charge;
                dto.Room = input.Room;
                dto.Docketno = input.Docketno;
                dto.Outlet = input.Outlet;
                dto.Revenue = input.Revenue;
                dto.Period = input.Period;
                dto.Covers = input.Covers;
                int groupid = await _iroomRepository.GetGroupIdByRevenue(input.Revenue);
                int locationid = await _iroomRepository.GetLocationIdByOutlet(input.Outlet);
                int periodid = await _iroomRepository.GetServciePeriodIdByPeriod(input.Period);
                var postcodekey = await _iroomRepository.GetPostcodeKeyBy(groupid, locationid, periodid);
                await _iroomRepository.InsertPosHistoryForCharge(dto);
                if (input.Room == string.Empty)
                {
                    var payment = await _iroomRepository.GetPaymentByDocketno(input.Docketno);
                    if (payment == "")
                    {
                        return CustomAppResponse<object>.ErrorResponse("Payment not found");
                    }
                    else
                    {
                        var docno = await _iroomRepository.GetFolioPayment(payment);
                        if (docno == string.Empty)
                        {
                            return CustomAppResponse<object>.ErrorResponse("Folio not found");
                        }
                        else
                        {
                            if (postcodekey == Guid.Empty)
                            {
                                return CustomAppResponse<object>.ErrorResponse("Postcode not found");
                            }
                            var result = await _iroomRepository.UpdatePOSChargeFolio(dto, postcodekey, docno);
                            if (result.Length > 10)
                            {
                                return CustomAppResponse<object>.ErrorResponse(result);
                            }
                            else
                            {
                                return CustomAppResponse<object>.SuccessResponse(result);
                            }
                                
                        }
                    }
                }
                if (postcodekey == Guid.Empty)
                {
                    return CustomAppResponse<object>.ErrorResponse("Postcode not found");
                }
                else
                {
                    var resultN = await _iroomRepository.GetRoomTenderCirteria(input.Room);
                    if (resultN.Criteria == "Y")
                    {
                        var result = await _iroomRepository.UpdatePOSCharge(dto, postcodekey);
                        if (result.Length > 10)
                        {
                            return CustomAppResponse<object>.ErrorResponse(result);
                        }
                        else
                        {
                            return CustomAppResponse<object>.SuccessResponse(result);
                        }                        
                    }
                    else
                    {
                        return CustomAppResponse<object>.ErrorResponse(resultN.Reason);
                    }
                }
            }
            catch (Exception ex)
            {
                return CustomAppResponse<object>.ErrorResponse(ex.Message);
            }
        }

        [DontWrapResult]
        public async Task<CustomAppResponse<object>> PostPOSCharge(POSChargeInput input)
        {
            try
            {
                POSChargeDto dto = new POSChargeDto();
                dto.Date = input.Date;
                dto.Time = input.Time;
                dto.Charge = input.Charge;
                dto.Room = input.Room;
                dto.Docketno = input.Docketno;
                dto.Outlet = input.Outlet;
                dto.Revenue = input.Revenue;
                dto.Period = input.Period;
                dto.Covers = input.Covers;
                int groupid = await _iroomRepository.GetGroupIdByRevenue(input.Revenue);
                if (groupid == 0)
                {
                    return CustomAppResponse<object>.ErrorResponse("Revenue not found");
                }
                int locationid = await _iroomRepository.GetLocationIdByOutlet(input.Outlet);
                if (locationid == 0)
                {
                    return CustomAppResponse<object>.ErrorResponse("Outlet not found");
                }
                int periodid = await _iroomRepository.GetServciePeriodIdByPeriod(input.Period);
                if (periodid == 0)
                {
                    return CustomAppResponse<object>.ErrorResponse("Meal period not found");
                }
                var postcodekey = await _iroomRepository.GetPostcodeKeyBy(groupid, locationid, periodid);
                await _iroomRepository.InsertPosHistoryForCharge(dto);
                if (input.Room == string.Empty)
                {
                    var payment = await _iroomRepository.GetPaymentByDocketno(input.Docketno);
                    if (payment == "")
                    {
                        return CustomAppResponse<object>.ErrorResponse("Payment not found");
                    }
                    else
                    {
                        var docno = await _iroomRepository.GetFolioPayment(payment);
                        if (docno == string.Empty)
                        {
                            return CustomAppResponse<object>.ErrorResponse("Folio not found");
                        }
                        else
                        {
                            if (postcodekey == Guid.Empty)
                            {
                                return CustomAppResponse<object>.ErrorResponse("Postcode not found");
                            }
                            var result = await _iroomRepository.UpdatePOSChargeFolio(dto, postcodekey, docno);
                            if (result.Length > 10)
                            {
                                return CustomAppResponse<object>.ErrorResponse(result);
                            }
                            else
                            {
                                return CustomAppResponse<object>.SuccessResponse(result);
                            }

                        }
                    }
                }
                if (postcodekey == Guid.Empty)
                {
                    return CustomAppResponse<object>.ErrorResponse("Postcode not found");
                }
                else
                {
                    var resultN = await _iroomRepository.GetRoomTenderCirteria(input.Room);
                    if (resultN.Criteria == "Y")
                    {
                        var result = await _iroomRepository.UpdatePOSCharge(dto, postcodekey);
                        if (result.Length > 10)
                        {
                            return CustomAppResponse<object>.ErrorResponse(result);
                        }
                        else
                        {
                            return CustomAppResponse<object>.SuccessResponse(result);
                        }
                    }
                    else
                    {
                        return CustomAppResponse<object>.ErrorResponse(resultN.Reason);
                    }
                }
            }
            catch (Exception ex)
            {
                return CustomAppResponse<object>.ErrorResponse(ex.Message);
            }
        }

        [DontWrapResult]
        public async Task<CustomAppResponse<object>> PostPOSPayment(POSChargePaymentInput input)
        {
            try
            {
                POSChargePaymentDto dto = new POSChargePaymentDto();
                dto.Date = input.Date;
                dto.Time = input.Time;
                dto.Amount = input.Amount;
                dto.Docketno = input.Docketno;
                dto.Outlet = input.Outlet;
                dto.Payment = input.Payment;
                dto.Period = input.Period;
                dto.Covers = input.Covers;
                //int groupid = await _iroomRepository.GetGroupIdByRevenue(input.Payment);
                //int locationid = await _iroomRepository.GetLocationIdByOutlet(input.Outlet);
                //int periodid = await _iroomRepository.GetServciePeriodIdByPeriod(input.Period);
                var postcodekey = await _iroomRepository.GetPostcodeKeyByPayment(input.Payment);
                if (postcodekey == Guid.Empty)
                {
                    postcodekey = await _iroomRepository.GetDetaulfPostCodeForPaymentType();
                }
                var docno = await _iroomRepository.GetFolioPayment(input.Payment);
                await _iroomRepository.InsertPosHistoryForPayment(dto);
                if (string.IsNullOrEmpty(docno))
                {
                    return CustomAppResponse<object>.ErrorResponse("Payment type not found");
                }
                else
                {
                    var result = await _iroomRepository.UpdatePOSChargePayment(dto, postcodekey, docno);
                    if (result.Length > 10)
                    {
                        return CustomAppResponse<object>.ErrorResponse(result);
                    }
                    else
                    {
                        return CustomAppResponse<object>.SuccessResponse(result);
                    }                    
                }
            }
            //HttpRequestException
            catch (Exception ex)
            {
                return CustomAppResponse<object>.ErrorResponse(ex.Message);
            }
        }

        [DontWrapResult]
        public async Task<CustomAppResponse<object>> PostPOSMISA(POSMisaDto input)
        {
            CMessage message = new CMessage();
            string oKOrNot = "";
            try
            {
                //string path = @"C:\inetpub\wwwroot\BEZGuestExchangeLive\aspnet-core\src\BEZNgCore.Application.Shared\MisaSample\POS_BEZEX_MisaUnCoimment.txt";
                //string contents = File.ReadAllText(path);
                string responseContent = "";
                string jsonContent = JsonConvert.SerializeObject(input);
                //var r=CheckFlag();
                message.SendOrReceived = CMessage.RequestType.SendData;
                message.Data = jsonContent;
                message.MsgType = CMessage.MessageType.OTA_EINVOICEISSUERQ;
                message.TenantId = 1;
                var key = await _iroomRepository.InsertMessage(message);

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetToken());
                _httpClient.DefaultRequestHeaders.Add("CompanyTaxCode", _appConfigurationAccessor.Configuration["Misa:taxcode"]);

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Make the POST request
                var response = _httpClient.PostAsync(_appConfigurationAccessor.Configuration["Misa:IssueInv"], content).Result;

                message = new CMessage();
                message.MsgType = CMessage.MessageType.OTA_EINVOICEISSUENOTIFRS;
                message.SendOrReceived = CMessage.RequestType.ReceivedData;
                message.TenantId = 1;
                if (response.IsSuccessStatusCode)
                {
                    responseContent = await response.Content.ReadAsStringAsync();
                    var resp = System.Text.Json.JsonSerializer.Deserialize<AuthData>(responseContent);
                    if (resp.success == false)
                    {
                        oKOrNot = resp.errorCode;
                        message.Data = responseContent;
                        await _iroomRepository.InsertMessage(message);
                    }
                    else
                    {
                        message.Data = responseContent;
                        await _iroomRepository.InsertMessage(message);
                    }
                }
                else
                {
                    responseContent = await response.Content.ReadAsStringAsync();
                    var responseData = System.Text.Json.JsonSerializer.Deserialize<AuthData>(responseContent);
                    oKOrNot = responseData.errorCode;
                    message.Data = responseContent;
                    await _iroomRepository.InsertMessage(message);
                }
            }
            catch (Exception ex)
            {
                oKOrNot = ex.Message;
                //return CustomAppResponse<object>.ErrorResponse(ex.Message);
            }
            return oKOrNot == "" ? CustomAppResponse<object>.SuccessResponse("") : CustomAppResponse<object>.ErrorResponse(oKOrNot);
        }
        private async Task<string> CheckFlag()
        {
            try
            {
                string responseContent = "";                
                var list = await _iroomRepository.GetXmlByFlag("I");
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        CMessage message = new CMessage();                        
                        _httpClient.DefaultRequestHeaders.Clear();
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetToken());
                        _httpClient.DefaultRequestHeaders.Add("CompanyTaxCode", _appConfigurationAccessor.Configuration["Misa:taxcode"]);

                        var content = new StringContent(item.xmlSendData, Encoding.UTF8, "application/json");

                        // Make the POST request
                        var response = _httpClient.PostAsync(_appConfigurationAccessor.Configuration["Misa:IssueInv"], content).Result;
                        
                        message = new CMessage();
                        message.MsgType = CMessage.MessageType.OTA_EINVOICEISSUENOTIFRS;
                        message.SendOrReceived = CMessage.RequestType.ReceivedData;
                        if (response.IsSuccessStatusCode)
                        {
                            responseContent = await response.Content.ReadAsStringAsync();
                            message.Data = responseContent;                           
                            if (item.Retry != null)
                            {
                                item.Retry += 1;
                            }
                            else
                            {
                                item.Retry = 1;
                            }
                            item.Flag = 1;
                            var u=await _iroomRepository.UpdateSendDataByID(item);
                            var uu = await _iroomRepository.InsertMessage(message);
                        }
                        else
                        {
                            responseContent = "Error: " + response.StatusCode;
                            message.Data = responseContent;
                            if (item.Retry != null)
                            {
                                item.Retry += 1;
                            }
                            else
                            {
                                item.Retry = 1;
                            }
                            item.Flag = 0;
                            var u = await _iroomRepository.UpdateSendDataByID(item);
                            var uu = await _iroomRepository.InsertMessage(message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
            return "";
        }
        private async Task<string> GetToken()
        {
            var url = _appConfigurationAccessor.Configuration["Misa:TestingUrl"] + "/auth/token";
            var body = new
            {
                appid = _appConfigurationAccessor.Configuration["Misa:appid"],
                taxcode = _appConfigurationAccessor.Configuration["Misa:taxcode"],
                username = _appConfigurationAccessor.Configuration["Misa:username"],
                password = _appConfigurationAccessor.Configuration["Misa:password"]
            };

            var json = System.Text.Json.JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            var result = await response.Content.ReadAsStringAsync();

            var token = System.Text.Json.JsonSerializer.Deserialize<AuthData>(result);
            return token.data;
        }
        [DontWrapResult]
        public async Task<CustomAppRoomCriteriaResponse<object>> GetRoomChargeGuest(string roomNumber)
        {
            try
            {
                var result = await _iroomRepository.GetRoomTenderCirteria(roomNumber);
                if (result.Criteria == "Y")
                {
                    return CustomAppRoomCriteriaResponse<object>.RoomChargeGuestResponse(result);
                }
                else
                {
                    return CustomAppRoomCriteriaResponse<object>.RoomChargeGuestResponse(result);
                }
            }
            catch (Exception ex)
            {
                return CustomAppRoomCriteriaResponse<object>.ExceptionResponse();
            }
        }
        public async Task<List<GuestNationalityLookupTableDto>> GetAllNationalityForTableDropdown()
        {
            return await _lookup_nationalityRepository.GetAll().OrderBy(x => x.NationalityName)
                .Select(nationality => new GuestNationalityLookupTableDto
                {
                    Id = nationality.Id.ToString(),
                    DisplayName = nationality == null || nationality.NationalityName == null ? "" : nationality.NationalityName.ToString()
                }).ToListAsync();
        }
        public async Task<List<GuestStatusLookupTableDto>> GetAllGuestStatusForTableDropdown()
        {
            return await _guestStatusRepository.GetAll().OrderBy(x => x.Status)
                .Select(gs => new GuestStatusLookupTableDto
                {
                    Id = gs.Id.ToString(),
                    DisplayName = gs == null || gs.Status == null ? "" : gs.Status.ToString()
                }).ToListAsync();
        }
        //[AbpAuthorize(AppPermissions.Pages_Guests)]
        public async Task<List<GuestCityLookupTableDto>> GetAllCityForTableDropdown()
        {
            return await _lookup_cityRepository.GetAll().OrderBy(x => x.CityCode)
                .Select(city => new GuestCityLookupTableDto
                {
                    Id = city.Id.ToString(),
                    DisplayName = city == null || city.CityCode == null ? "" : city.CityCode.ToString()
                }).ToListAsync();
        }
        public async Task<List<GuestRegionLookupTableDto>> GetAllRegionForTableDropdown()
        {
            return await _lookup_regionRepository.GetAll().OrderBy(x => x.RegionName)
                .Select(r => new GuestRegionLookupTableDto
                {
                    Id = r.Id.ToString(),
                    DisplayName = r == null || r.RegionName == null ? "" : r.RegionName
                }).ToListAsync();
        }
        public async Task<List<GuestDocumentTypeLookupTableDto>> GetAllGuestDocumentTypeForTableDropdown()
        {
            return await _guestIdentityRepository.GetAll().OrderBy(x => x.Seq)
                .Select(t => new GuestDocumentTypeLookupTableDto
                {
                    Id = t.Id.ToString(),
                    DisplayName = t == null || t.Type == null ? "" : t.Type.ToString()
                }).ToListAsync();
        }
        public async Task<GetGuestForViewDto> GetGuestForView(Guid id)
        {
            var guest = await _guestRepository.GetAsync(id);

            var output = new GetGuestForViewDto { Guest = ObjectMapper.Map<GuestDto>(guest) };

            if (output.Guest.NationalityKey != null)
            {
                var _lookupNationality = await _lookup_nationalityRepository.FirstOrDefaultAsync((Guid)output.Guest.NationalityKey);
                output.NationalityNationality1 = _lookupNationality?.NationalityName?.ToString();
            }

            if (output.Guest.CityKey != null)
            {
                var _lookupCity = await _lookup_cityRepository.FirstOrDefaultAsync((Guid)output.Guest.CityKey);
                output.CityCity1 = _lookupCity?.CityCode?.ToString();
            }

            if (output.Guest.TitleKey != null)
            {
                var _lookupTitle = await _lookup_titleRepository.FirstOrDefaultAsync((Guid)output.Guest.TitleKey);
                output.TitleTitle1 = _lookupTitle?.TitleName?.ToString();
            }

            return output;
        }

        public async Task<GetReservationDetailDto> GetReservationDetailByFolio()
        {
            //&& (res.DocNo == _appsession.DOCNO)
            var output = (from res in _resRepository.GetAll()
                          join gs in _rateTypeRepository.GetAll() on res.RateTypeKey equals gs.Id
                          join r in _roomTypeRepository.GetAll() on res.RoomTypeKey equals r.Id
                          where (res.Status == 1 || res.Status == 2)
                          select new GetReservationDetailDto
                          {
                              ReservationKey = res.Id,
                              RateTypeKey = res.RateTypeKey,
                              RoomTypeKey = res.RoomTypeKey,
                              DocDate = res.DocDate,
                              DocNo = res.DocNo == null ? "" : res.DocNo,
                              GuestKey = res.GuestKey,
                              Status = res.Status,
                              CheckInDate = res.CheckInDate,
                              CheckOutDate = res.CheckOutDate,
                              Remark = res.Remark == null ? "" : res.Remark,
                              PurposeStayKey = res.PurposeStayKey,
                              PreCheckInCount = res.PreCheckInCount,
                              RateDescription = gs.Description == null ? "" : gs.Description,
                              RoomSeq = r.Seq.ToString(),
                              RoomDescription = r.Description == null ? "" : r.Description
                          }).FirstOrDefault();

            output.RoomSeq = GetRoomMaxPax(output.RoomSeq);

            return output;
        }

        private string GetRoomMaxPax(string seq)
        {
            string strRetrunValue = "";
            if (seq == "53" || seq == "54" || seq == "57" || seq == "60")
            {
                strRetrunValue = "4 Adults / 2 Adults 2 Children";
            }
            else if (seq == "58")
            {
                strRetrunValue = "2 Adults 2 Children";
            }
            else if (seq == "59")
            {
                strRetrunValue = "3 Adults";
            }
            else if (seq == "63")
            {
                strRetrunValue = "2 Adults";
            }
            else
            {
                strRetrunValue = "2 Adults";
            }
            return strRetrunValue;
        }

        public async Task<GetGuestInfoForViewDto> GetGuestInfoForView()
        {
            //var gk = new Guid(_appsession.GUESTKEY);
            var gk = new Guid();
            var output = await (from gs in _guestRepository.GetAll()
                                join n in _nationalityRepository.GetAll() on gs.CountryKey equals n.Id into gsn
                                from gsnResult in gsn.DefaultIfEmpty()
                                join n1 in _nationalityRepository.GetAll() on gsnResult.Id equals n1.Id into gsn1
                                from gsn1Result in gsn1.DefaultIfEmpty()
                                where gs.Id == gk
                                select new GetGuestInfoForViewDto
                                {
                                    GuestKey = gs.Id,
                                    Gender = gs.Gender,
                                    TitleName = gs.Title == null ? "" : gs.Title,
                                    LastName = gs.LastName == null ? "" : gs.LastName,
                                    FirstName = gs.FirstName,
                                    Email = gs.EMail == null ? "" : gs.EMail,
                                    Passport = gs.Passport == null ? "" : DataTypeConvertor.GetDecryptedString(gs.Passport),
                                    DOB = gs.DOB,
                                    Tel = gs.Tel == null ? "" : gs.Tel,
                                    Mobile = gs.Mobile == null ? "" : gs.Mobile,
                                    Fax = gs.Fax == null ? "" : gs.Fax,
                                    Name = gs.Name,
                                    GuestStay = gs.GuestStay,
                                    Postal = gs.Postal == null ? "" : gs.Postal,
                                    CountryKey = gs.CountryKey,
                                    NationalityName = gsn1Result.NationalityName == null ? "" : gsn1Result.NationalityName,
                                    NationalityKey = gs.NationalityKey,
                                    CityName = gs.City == null ? "" : gs.City,
                                    PassportExpiry = gs.PassportExpiry,
                                    Address = (gs.Address.Trim().Length > 60 ? gs.Address.Trim().Substring(0, 60) : gs.Address.Trim()),
                                    Address1 = (gs.Address.Trim().Length > 60 ? gs.Address.Trim().Substring(60) : "")
                                }).FirstOrDefaultAsync();

            output.CityKey = (string.IsNullOrEmpty(output.CityName)) ? Guid.Empty : _lookup_cityRepository.GetAll().Where(x => x.CityCode == output.CityName).Select(x => x.Id).FirstOrDefault();
            output.TitleKey = _lookup_titleRepository.GetAll().Where(x => x.TitleName == output.TitleName).Select(x => x.Id).FirstOrDefault();

            return output;
        }


        public async Task MainGuestUpdate(GetGuestInfoForViewDto input)
        {
            //var resk = new Guid(_appsession.RESERVATIONKEY);
            var resk = new Guid();
            int preCheckInCount = input.GuestStay.Value + 1;
            using (var unitOfWork = _unitOfWorkManager.Begin())
            {
                var guest = await _guestRepository.GetAll().Where(x => x.Id == input.GuestKey).Select(o => new Guest
                {
                    AccNo = DataTypeConvertor.ConvertNullToString(o.AccNo),
                    Status = o.Status,
                    Active = o.Active,
                    CarNo = DataTypeConvertor.ConvertNullToString(o.CarNo),
                    Gender = DataTypeConvertor.ConvertNullToString(o.Gender),
                    DOB = o.DOB,
                    Tel = DataTypeConvertor.ConvertNullToString(o.Tel),
                    Mobile = DataTypeConvertor.ConvertNullToString(o.Mobile),
                    Fax = DataTypeConvertor.ConvertNullToString(o.Fax),
                    EMail = DataTypeConvertor.ConvertNullToString(o.EMail),
                    Postal = DataTypeConvertor.ConvertNullToString(o.Postal),
                    CountryKey = o.CountryKey,
                    NationalityKey = o.NationalityKey,
                    Interest = DataTypeConvertor.ConvertNullToString(o.Interest),
                    CreditLimit = o.CreditLimit,
                    Terms = o.Terms,
                    Group1Key = o.Group1Key,
                    Group2Key = o.Group2Key,
                    Group3Key = o.Group3Key,
                    Group4Key = o.Group4Key,
                    SourceKey = o.SourceKey,
                    StaffKey = o.StaffKey,
                    DefaultCompany = o.DefaultCompany,
                    Company1Name = DataTypeConvertor.ConvertNullToString(o.Company1Name),
                    Company1Relation = DataTypeConvertor.ConvertNullToString(o.Company1Relation),
                    Company1Department = DataTypeConvertor.ConvertNullToString(o.Company1Department),
                    Company1Occupation = DataTypeConvertor.ConvertNullToString(o.Company1Occupation),
                    Company2Name = DataTypeConvertor.ConvertNullToString(o.Company2Name),
                    Company2Relation = DataTypeConvertor.ConvertNullToString(o.Company2Relation),
                    Company2Department = DataTypeConvertor.ConvertNullToString(o.Company2Department),
                    Company2Occupation = DataTypeConvertor.ConvertNullToString(o.Company2Occupation),
                    Company3Name = DataTypeConvertor.ConvertNullToString(o.Company3Name),
                    Company3Relation = DataTypeConvertor.ConvertNullToString(o.Company3Relation),
                    Company3Department = DataTypeConvertor.ConvertNullToString(o.Company3Department),
                    Company3Occupation = DataTypeConvertor.ConvertNullToString(o.Company3Occupation),
                    Company4Name = DataTypeConvertor.ConvertNullToString(o.Company4Name),
                    Company4Relation = DataTypeConvertor.ConvertNullToString(o.Company4Relation),
                    Company4Department = DataTypeConvertor.ConvertNullToString(o.Company4Department),
                    Company4Occupation = DataTypeConvertor.ConvertNullToString(o.Company4Occupation),
                    LastModifiedStaff = o.LastModifiedStaff,
                    CreatedDate = o.CreatedDate,
                    Sort = o.Sort,
                    Sync = o.Sync,
                    Seq = o.Seq,
                    PassportExpiry = o.PassportExpiry,
                    Extra1 = o.Extra1,
                    Extra2 = o.Extra2,
                    Extra3 = o.Extra3,
                    Extra4 = o.Extra4,
                    Extra5 = o.Extra5,
                    Extra6 = o.Extra6,
                    Extra7 = o.Extra7,
                    Extra8 = o.Extra8,
                    Extra9 = o.Extra9,
                    Extra10 = o.Extra10,
                    Extra11 = o.Extra11,
                    Extra12 = o.Extra12,
                    Extra13 = o.Extra13,
                    Extra14 = o.Extra14,
                    Extra15 = o.Extra15,
                    Extra16 = o.Extra16,
                    Extra17 = o.Extra17,
                    Extra18 = o.Extra18,
                    Extra19 = o.Extra19,
                    Extra20 = o.Extra20,
                    Extra21 = o.Extra21,
                    Extra22 = o.Extra22,
                    Extra23 = o.Extra23,
                    Extra24 = o.Extra24,
                    RegionKey = o.RegionKey,
                    GuestStay = o.GuestStay,
                    Title = DataTypeConvertor.ConvertNullToString(o.Title),
                    Company = DataTypeConvertor.ConvertNullToString(o.Company),
                    LastName = DataTypeConvertor.ConvertNullToString(o.LastName),
                    FirstName = DataTypeConvertor.ConvertNullToString(o.FirstName),
                    Name = DataTypeConvertor.ConvertNullToString(o.Name),
                    ShortCode = DataTypeConvertor.ConvertNullToString(o.ShortCode),
                    Address = DataTypeConvertor.ConvertNullToString(o.Address),
                    City = DataTypeConvertor.ConvertNullToString(o.City),
                    GuestIdentityTypeKey = o.GuestIdentityTypeKey,
                    Passport = DataTypeConvertor.ConvertNullToString(o.Passport),
                    LanguageCode = DataTypeConvertor.ConvertNullToString(o.LanguageCode),
                    Subscribe = DataTypeConvertor.ConvertNullToString(o.Subscribe),
                    PropertyKey = o.PropertyKey,
                    OrgGuestKey = o.OrgGuestKey,
                    OrgAccNo = DataTypeConvertor.ConvertNullToString(o.OrgAccNo),
                    DoNotContact = o.DoNotContact,
                    OldGuestStay = o.OldGuestStay,
                    Users = o.Users,
                    Computer = DataTypeConvertor.ConvertNullToString(o.Computer),
                    Access = o.Access,
                    tPassport = DataTypeConvertor.ConvertNullToString(o.tPassport),
                    //X_Company = DataTypeConvertor.ConvertNullToString(o.X_Company),
                    //X_Dorm = DataTypeConvertor.ConvertNullToString(o.X_Dorm),
                    //X_Sector = DataTypeConvertor.ConvertNullToString(o.X_Sector),
                    Id = o.Id
                }).FirstOrDefaultAsync();

                if (input.TitleKey != Guid.Empty)
                {
                    guest.Title = _lookup_titleRepository.Get(input.TitleKey.Value).TitleName;
                }
                guest.EMail = input.Email;
                guest.Passport = input.Passport;
                guest.DOB = input.DOB;
                if (!string.IsNullOrEmpty(input.Address))
                {
                    guest.Address = DataTypeConvertor.GetCleanSQLString(input.Address) + (!string.IsNullOrEmpty(DataTypeConvertor.GetCleanSQLString(input.Address1.Trim())) ? ", " : "") + DataTypeConvertor.GetCleanSQLString(input.Address1.Trim());
                }
                guest.Postal = DataTypeConvertor.GetCleanSQLString(input.Postal == null ? "" : input.Postal);
                guest.CountryKey = input.CountryKey;
                guest.NationalityKey = input.CountryKey;
                if (input.CityKey != Guid.Empty)
                {
                    guest.City = _lookup_cityRepository.Get(input.CityKey.Value).CityCode;
                }
                guest.Mobile = input.Mobile;
                _guestRepository.Update(guest);

                var res = _resRepository.GetAll().Where(x => x.Id == resk).Select(x => x).FirstOrDefault();
                res.PreCheckInCount = preCheckInCount;
                _resRepository.Update(res);

                //var history = new CHistory();
                //history.Operation = 'U';
                //history.TableName = "Reservation";
                //history.SourceKey = resk;
                //history.Detail = "(Guest Portal) Update Pre-CheckInCount :  " + preCheckInCount;

                //await _chistoryRepository.InsertAsync(history);

                unitOfWork.Complete();
            }

        }

        public async Task<string> MainGuestCheckin(GetGuestInfoForViewDto input)
        {
            string returnO = "";
            //var resk = new Guid(_appsession.RESERVATIONKEY);
            var resk = new Guid();
            if (!string.IsNullOrEmpty(input.Postal) && !string.IsNullOrEmpty(input.Address) && !string.IsNullOrEmpty(input.Mobile) &&
                !string.IsNullOrEmpty(input.Passport) && input.DOB.HasValue)
            {
                int preCheckInCount = input.GuestStay.Value + 1;
                using (var unitOfWork = _unitOfWorkManager.Begin())
                {
                    var guest = _guestRepository.GetAll().Where(x => x.Id == input.GuestKey).Select(o => new Guest
                    {
                        AccNo = DataTypeConvertor.ConvertNullToString(o.AccNo),
                        Status = o.Status,
                        Active = o.Active,
                        CarNo = DataTypeConvertor.ConvertNullToString(o.CarNo),
                        Gender = DataTypeConvertor.ConvertNullToString(o.Gender),
                        DOB = o.DOB,
                        Tel = DataTypeConvertor.ConvertNullToString(o.Tel),
                        Mobile = DataTypeConvertor.ConvertNullToString(o.Mobile),
                        Fax = DataTypeConvertor.ConvertNullToString(o.Fax),
                        EMail = DataTypeConvertor.ConvertNullToString(o.EMail),
                        Postal = DataTypeConvertor.ConvertNullToString(o.Postal),
                        CountryKey = o.CountryKey,
                        NationalityKey = o.NationalityKey,
                        Interest = DataTypeConvertor.ConvertNullToString(o.Interest),
                        CreditLimit = o.CreditLimit,
                        Terms = o.Terms,
                        Group1Key = o.Group1Key,
                        Group2Key = o.Group2Key,
                        Group3Key = o.Group3Key,
                        Group4Key = o.Group4Key,
                        SourceKey = o.SourceKey,
                        StaffKey = o.StaffKey,
                        DefaultCompany = o.DefaultCompany,
                        Company1Name = DataTypeConvertor.ConvertNullToString(o.Company1Name),
                        Company1Relation = DataTypeConvertor.ConvertNullToString(o.Company1Relation),
                        Company1Department = DataTypeConvertor.ConvertNullToString(o.Company1Department),
                        Company1Occupation = DataTypeConvertor.ConvertNullToString(o.Company1Occupation),
                        Company2Name = DataTypeConvertor.ConvertNullToString(o.Company2Name),
                        Company2Relation = DataTypeConvertor.ConvertNullToString(o.Company2Relation),
                        Company2Department = DataTypeConvertor.ConvertNullToString(o.Company2Department),
                        Company2Occupation = DataTypeConvertor.ConvertNullToString(o.Company2Occupation),
                        Company3Name = DataTypeConvertor.ConvertNullToString(o.Company3Name),
                        Company3Relation = DataTypeConvertor.ConvertNullToString(o.Company3Relation),
                        Company3Department = DataTypeConvertor.ConvertNullToString(o.Company3Department),
                        Company3Occupation = DataTypeConvertor.ConvertNullToString(o.Company3Occupation),
                        Company4Name = DataTypeConvertor.ConvertNullToString(o.Company4Name),
                        Company4Relation = DataTypeConvertor.ConvertNullToString(o.Company4Relation),
                        Company4Department = DataTypeConvertor.ConvertNullToString(o.Company4Department),
                        Company4Occupation = DataTypeConvertor.ConvertNullToString(o.Company4Occupation),
                        LastModifiedStaff = o.LastModifiedStaff,
                        CreatedDate = o.CreatedDate,
                        Sort = o.Sort,
                        Sync = o.Sync,
                        Seq = o.Seq,
                        PassportExpiry = o.PassportExpiry,
                        Extra1 = o.Extra1,
                        Extra2 = o.Extra2,
                        Extra3 = o.Extra3,
                        Extra4 = o.Extra4,
                        Extra5 = o.Extra5,
                        Extra6 = o.Extra6,
                        Extra7 = o.Extra7,
                        Extra8 = o.Extra8,
                        Extra9 = o.Extra9,
                        Extra10 = o.Extra10,
                        Extra11 = o.Extra11,
                        Extra12 = o.Extra12,
                        Extra13 = o.Extra13,
                        Extra14 = o.Extra14,
                        Extra15 = o.Extra15,
                        Extra16 = o.Extra16,
                        Extra17 = o.Extra17,
                        Extra18 = o.Extra18,
                        Extra19 = o.Extra19,
                        Extra20 = o.Extra20,
                        Extra21 = o.Extra21,
                        Extra22 = o.Extra22,
                        Extra23 = o.Extra23,
                        Extra24 = o.Extra24,
                        RegionKey = o.RegionKey,
                        GuestStay = o.GuestStay,
                        Title = DataTypeConvertor.ConvertNullToString(o.Title),
                        Company = DataTypeConvertor.ConvertNullToString(o.Company),
                        LastName = DataTypeConvertor.ConvertNullToString(o.LastName),
                        FirstName = DataTypeConvertor.ConvertNullToString(o.FirstName),
                        Name = DataTypeConvertor.ConvertNullToString(o.Name),
                        ShortCode = DataTypeConvertor.ConvertNullToString(o.ShortCode),
                        Address = DataTypeConvertor.ConvertNullToString(o.Address),
                        City = DataTypeConvertor.ConvertNullToString(o.City),
                        GuestIdentityTypeKey = o.GuestIdentityTypeKey,
                        Passport = DataTypeConvertor.ConvertNullToString(o.Passport),
                        LanguageCode = DataTypeConvertor.ConvertNullToString(o.LanguageCode),
                        Subscribe = DataTypeConvertor.ConvertNullToString(o.Subscribe),
                        PropertyKey = o.PropertyKey,
                        OrgGuestKey = o.OrgGuestKey,
                        OrgAccNo = DataTypeConvertor.ConvertNullToString(o.OrgAccNo),
                        DoNotContact = o.DoNotContact,
                        OldGuestStay = o.OldGuestStay,
                        Users = o.Users,
                        Computer = DataTypeConvertor.ConvertNullToString(o.Computer),
                        Access = o.Access,
                        tPassport = DataTypeConvertor.ConvertNullToString(o.tPassport),
                        //X_Company = DataTypeConvertor.ConvertNullToString(o.X_Company),
                        //X_Dorm = DataTypeConvertor.ConvertNullToString(o.X_Dorm),
                        //X_Sector = DataTypeConvertor.ConvertNullToString(o.X_Sector),
                        Id = o.Id
                    }).FirstOrDefault();

                    if (input.TitleKey != Guid.Empty)
                    {
                        guest.Title = _lookup_titleRepository.Get(input.TitleKey.Value).TitleName;
                    }
                    guest.EMail = input.Email;
                    guest.Passport = input.Passport;
                    guest.DOB = input.DOB;
                    if (!string.IsNullOrEmpty(input.Address))
                    {
                        guest.Address = DataTypeConvertor.GetCleanSQLString(input.Address) + (!string.IsNullOrEmpty(DataTypeConvertor.GetCleanSQLString(input.Address1.Trim())) ? ", " : "") + DataTypeConvertor.GetCleanSQLString(input.Address1.Trim());
                    }
                    guest.Postal = DataTypeConvertor.GetCleanSQLString(input.Postal == null ? "" : input.Postal);
                    guest.CountryKey = input.CountryKey;
                    guest.NationalityKey = input.CountryKey;
                    if (input.CityKey != Guid.Empty)
                    {
                        guest.City = _lookup_cityRepository.Get(input.CityKey.Value).CityCode;
                    }
                    guest.Mobile = input.Mobile;
                    _guestRepository.Update(guest);

                    var res = _resRepository.GetAll().Where(x => x.Id == resk).Select(x => x).FirstOrDefault();
                    res.PreCheckInCount = preCheckInCount;
                    _resRepository.Update(res);

                    //var history = new CHistory();
                    //history.Operation = 'U';
                    //history.TableName = "Reservation";
                    //history.SourceKey = resk;
                    //history.Detail = "(Guest Portal) Update Pre-CheckInCount :  " + preCheckInCount;

                    //_chistoryRepository.Insert(history);

                    unitOfWork.Complete();
                }

            }
            else
            {
                returnO = "Please kindly check Passport/ID,Date of Birth,Address,Postal Code,Mobile.";
            }

            return returnO;
        }

        public async Task<CheckOutDto> MainGuestCheckout(GetGuestInfoForViewDto input)
        {
            double billCount = 0;
            //var resk = new Guid(_appsession.RESERVATIONKEY);
            var resk = new Guid();
            CheckOutDto cod = new CheckOutDto();
            var systemDate = _controlRepository.GetAll().Select(x => x.SystemDate).FirstOrDefault();
            var checkOutdate = input.PassportExpiry;
            var expChkout = _resRepository.GetAll().Where(x => x.Id == resk).Select(x => x.ExpressCheckOut).FirstOrDefault();

            var RR = _resRateRepository.GetAll().Where(x => x.ReservationKey == resk).Select(x =>
            new
            {
                ReservationKey = x.ReservationKey,
                BillTo = x.BillTo,
                Rate = x.Rate,
                Tax1 = x.Tax1,
                Tax2 = x.Tax2,
                Tax3 = x.Tax3
            }).GroupBy(s => new { s.ReservationKey, s.BillTo })
            .Select(g => new { ReservationKey = g.Key.ReservationKey, BillTo = g.Key.BillTo, Balance = g.Sum(x => Math.Round(x.Rate.Value + x.Tax1.Value + x.Tax2.Value + x.Tax3.Value)) });

            var BB = _resbillingcontactRepository.GetAll();

            var leftOuterJoin = from b in BB
                                join r in RR on new { x1 = b.ReservationKey, x2 = b.Billing } equals new { x1 = r.ReservationKey, x2 = r.BillTo } into resrate
                                from rate in resrate.DefaultIfEmpty()
                                select new
                                {
                                    ReservationBillingContactKey = b.Id,
                                    AccountKey = b.AccountKey,
                                    ReservationKey = b.ReservationKey == null ? rate.ReservationKey : b.ReservationKey,
                                    Ledger = rate.BillTo == null ? b.Billing : rate.BillTo,
                                    AccountType = b.AccountType,
                                    Balance = rate.Balance,
                                    Invoice = b.Invoice,
                                    InvoiceNo = b.InvoiceNo
                                };

            var rightOuterJoin = from r in RR
                                 join b in BB on new { x1 = r.ReservationKey, x2 = r.BillTo } equals new { x1 = b.ReservationKey, x2 = b.Billing } into resbill
                                 from bill in resbill.DefaultIfEmpty()
                                 select new
                                 {
                                     ReservationBillingContactKey = bill.Id,
                                     AccountKey = bill.AccountKey,
                                     ReservationKey = bill.ReservationKey == null ? r.ReservationKey : bill.ReservationKey,
                                     Ledger = r.BillTo == null ? bill.Billing : r.BillTo,
                                     AccountType = bill.AccountType,
                                     Balance = r.Balance,
                                     Invoice = bill.Invoice,
                                     InvoiceNo = bill.InvoiceNo
                                 };
            var fullOuterJoin = leftOuterJoin.Union(rightOuterJoin);
            var leftJoinWithGuestCompany = (from o in fullOuterJoin
                                            join g in _guestRepository.GetAll() on o.AccountKey equals g.Id into ogGroup
                                            from og in ogGroup.DefaultIfEmpty()
                                            join c in _companyRateRepository.GetAll() on o.AccountKey equals c.Id into ocGroup
                                            from oc in ocGroup.DefaultIfEmpty()
                                            where o.ReservationKey == resk
                                            select new
                                            {
                                                ReservationBillingContactKey = o.ReservationBillingContactKey,
                                                AccountKey = o.AccountKey,
                                                ReservationKey = o.ReservationKey,
                                                Ledger = o.Ledger,
                                                AccountType = o.AccountType,
                                                Account = DataTypeConvertor.ConvertNullToString(oc.Name) == "" ? og.Name : oc.Name,
                                                Balance = o.Balance,
                                                Invoice = o.Invoice,
                                                InvoiceNo = o.InvoiceNo
                                            }).GroupBy(s => new
                                            {
                                                s.ReservationBillingContactKey,
                                                s.AccountKey,
                                                s.ReservationKey,
                                                s.Ledger,
                                                s.AccountType,
                                                s.Account,
                                                s.Balance,
                                                s.Invoice,
                                                s.InvoiceNo
                                            }).OrderBy(x => x.Key.Ledger);

            billCount = Convert.ToDouble(leftJoinWithGuestCompany.Sum(x => x.Key.Balance));

            if (expChkout.Value > 0)
            {
                if (checkOutdate == systemDate)
                {
                    if (billCount == 0)
                    {
                        using (var unitOfWork = _unitOfWorkManager.Begin())
                        {
                            var guest = _guestRepository.GetAll().Where(x => x.Id == input.GuestKey).Select(o => new Guest
                            {
                                AccNo = DataTypeConvertor.ConvertNullToString(o.AccNo),
                                Status = o.Status,
                                Active = o.Active,
                                CarNo = DataTypeConvertor.ConvertNullToString(o.CarNo),
                                Gender = DataTypeConvertor.ConvertNullToString(o.Gender),
                                DOB = o.DOB,
                                Tel = DataTypeConvertor.ConvertNullToString(o.Tel),
                                Mobile = DataTypeConvertor.ConvertNullToString(o.Mobile),
                                Fax = DataTypeConvertor.ConvertNullToString(o.Fax),
                                EMail = DataTypeConvertor.ConvertNullToString(o.EMail),
                                Postal = DataTypeConvertor.ConvertNullToString(o.Postal),
                                CountryKey = o.CountryKey,
                                NationalityKey = o.NationalityKey,
                                Interest = DataTypeConvertor.ConvertNullToString(o.Interest),
                                CreditLimit = o.CreditLimit,
                                Terms = o.Terms,
                                Group1Key = o.Group1Key,
                                Group2Key = o.Group2Key,
                                Group3Key = o.Group3Key,
                                Group4Key = o.Group4Key,
                                SourceKey = o.SourceKey,
                                StaffKey = o.StaffKey,
                                DefaultCompany = o.DefaultCompany,
                                Company1Name = DataTypeConvertor.ConvertNullToString(o.Company1Name),
                                Company1Relation = DataTypeConvertor.ConvertNullToString(o.Company1Relation),
                                Company1Department = DataTypeConvertor.ConvertNullToString(o.Company1Department),
                                Company1Occupation = DataTypeConvertor.ConvertNullToString(o.Company1Occupation),
                                Company2Name = DataTypeConvertor.ConvertNullToString(o.Company2Name),
                                Company2Relation = DataTypeConvertor.ConvertNullToString(o.Company2Relation),
                                Company2Department = DataTypeConvertor.ConvertNullToString(o.Company2Department),
                                Company2Occupation = DataTypeConvertor.ConvertNullToString(o.Company2Occupation),
                                Company3Name = DataTypeConvertor.ConvertNullToString(o.Company3Name),
                                Company3Relation = DataTypeConvertor.ConvertNullToString(o.Company3Relation),
                                Company3Department = DataTypeConvertor.ConvertNullToString(o.Company3Department),
                                Company3Occupation = DataTypeConvertor.ConvertNullToString(o.Company3Occupation),
                                Company4Name = DataTypeConvertor.ConvertNullToString(o.Company4Name),
                                Company4Relation = DataTypeConvertor.ConvertNullToString(o.Company4Relation),
                                Company4Department = DataTypeConvertor.ConvertNullToString(o.Company4Department),
                                Company4Occupation = DataTypeConvertor.ConvertNullToString(o.Company4Occupation),
                                LastModifiedStaff = o.LastModifiedStaff,
                                CreatedDate = o.CreatedDate,
                                Sort = o.Sort,
                                Sync = o.Sync,
                                Seq = o.Seq,
                                PassportExpiry = o.PassportExpiry,
                                Extra1 = o.Extra1,
                                Extra2 = o.Extra2,
                                Extra3 = o.Extra3,
                                Extra4 = o.Extra4,
                                Extra5 = o.Extra5,
                                Extra6 = o.Extra6,
                                Extra7 = o.Extra7,
                                Extra8 = o.Extra8,
                                Extra9 = o.Extra9,
                                Extra10 = o.Extra10,
                                Extra11 = o.Extra11,
                                Extra12 = o.Extra12,
                                Extra13 = o.Extra13,
                                Extra14 = o.Extra14,
                                Extra15 = o.Extra15,
                                Extra16 = o.Extra16,
                                Extra17 = o.Extra17,
                                Extra18 = o.Extra18,
                                Extra19 = o.Extra19,
                                Extra20 = o.Extra20,
                                Extra21 = o.Extra21,
                                Extra22 = o.Extra22,
                                Extra23 = o.Extra23,
                                Extra24 = o.Extra24,
                                RegionKey = o.RegionKey,
                                GuestStay = o.GuestStay,
                                Title = DataTypeConvertor.ConvertNullToString(o.Title),
                                Company = DataTypeConvertor.ConvertNullToString(o.Company),
                                LastName = DataTypeConvertor.ConvertNullToString(o.LastName),
                                FirstName = DataTypeConvertor.ConvertNullToString(o.FirstName),
                                Name = DataTypeConvertor.ConvertNullToString(o.Name),
                                ShortCode = DataTypeConvertor.ConvertNullToString(o.ShortCode),
                                Address = DataTypeConvertor.ConvertNullToString(o.Address),
                                City = DataTypeConvertor.ConvertNullToString(o.City),
                                GuestIdentityTypeKey = o.GuestIdentityTypeKey,
                                Passport = DataTypeConvertor.ConvertNullToString(o.Passport),
                                LanguageCode = DataTypeConvertor.ConvertNullToString(o.LanguageCode),
                                Subscribe = DataTypeConvertor.ConvertNullToString(o.Subscribe),
                                PropertyKey = o.PropertyKey,
                                OrgGuestKey = o.OrgGuestKey,
                                OrgAccNo = DataTypeConvertor.ConvertNullToString(o.OrgAccNo),
                                DoNotContact = o.DoNotContact,
                                OldGuestStay = o.OldGuestStay,
                                Users = o.Users,
                                Computer = DataTypeConvertor.ConvertNullToString(o.Computer),
                                Access = o.Access,
                                tPassport = DataTypeConvertor.ConvertNullToString(o.tPassport),
                                //X_Company = DataTypeConvertor.ConvertNullToString(o.X_Company),
                                //X_Dorm = DataTypeConvertor.ConvertNullToString(o.X_Dorm),
                                //X_Sector = DataTypeConvertor.ConvertNullToString(o.X_Sector),
                                Id = o.Id
                            }).FirstOrDefault();

                            if (input.TitleKey != Guid.Empty)
                            {
                                guest.Title = _lookup_titleRepository.Get(input.TitleKey.Value).TitleName;
                            }
                            guest.EMail = input.Email;
                            guest.Passport = input.Passport;
                            guest.DOB = input.DOB;
                            if (!string.IsNullOrEmpty(input.Address))
                            {
                                guest.Address = DataTypeConvertor.GetCleanSQLString(input.Address) + (!string.IsNullOrEmpty(DataTypeConvertor.GetCleanSQLString(input.Address1.Trim())) ? ", " : "") + DataTypeConvertor.GetCleanSQLString(input.Address1.Trim());
                            }
                            guest.Postal = DataTypeConvertor.GetCleanSQLString(input.Postal == null ? "" : input.Postal);
                            guest.CountryKey = input.CountryKey;
                            guest.NationalityKey = input.CountryKey;
                            if (input.CityKey != Guid.Empty)
                            {
                                guest.City = _lookup_cityRepository.Get(input.CityKey.Value).CityCode;
                            }
                            guest.Mobile = input.Mobile;
                            _guestRepository.Update(guest);

                            var res = _resRepository.GetAll().Where(x => x.Id == resk).Select(x => x).FirstOrDefault();
                            res.Status = 10;
                            _resRepository.Update(res);

                            //var history = new CHistory();
                            //history.Operation = 'U';
                            //history.TableName = "Reservation";
                            //history.SourceKey = resk;
                            //history.Detail = "(Guest Portal) Folio #" + _appsession.DOCNO + " has check out from online check out";

                            //_chistoryRepository.Insert(history);

                            unitOfWork.Complete();
                        }
                        if (!string.IsNullOrEmpty(input.Email.Trim()))
                        {
                            cod.msg = "1";
                            cod.email = input.Email;
                            //cod.docno = _appsession.DOCNO;
                        }
                        else
                        {
                            cod.msg = "2";
                        }
                    }
                    else
                    {
                        cod.msg = "3";
                    }
                }
                else
                {
                    cod.msg = "4";
                }
            }
            else
            {
                cod.msg = "5";
            }
            return cod;
        }

        [AbpAuthorize(AppPermissions.Pages_Guests_Edit)]
        public async Task<GetGuestForEditOutput> GetGuestForEdit(EntityDto<Guid> input)
        {
            var guest = await _guestRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetGuestForEditOutput { Guest = ObjectMapper.Map<CreateOrEditGuestDto>(guest) };

            if (output.Guest.NationalityKey != null)
            {
                var _lookupNationality = await _lookup_nationalityRepository.FirstOrDefaultAsync((Guid)output.Guest.NationalityKey);
                output.NationalityNationality1 = _lookupNationality?.NationalityName?.ToString();
            }

            if (output.Guest.CityKey != null)
            {
                var _lookupCity = await _lookup_cityRepository.FirstOrDefaultAsync((Guid)output.Guest.CityKey);
                output.CityCity1 = _lookupCity?.CityCode?.ToString();
            }

            if (output.Guest.TitleKey != null)
            {
                var _lookupTitle = await _lookup_titleRepository.FirstOrDefaultAsync((Guid)output.Guest.TitleKey);
                output.TitleTitle1 = _lookupTitle?.TitleName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditGuestDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Guests_Create)]
        protected virtual async Task Create(CreateOrEditGuestDto input)
        {
            var guest = ObjectMapper.Map<Guest>(input);

            await _guestRepository.InsertAsync(guest);
        }

        [AbpAuthorize(AppPermissions.Pages_Guests_Edit)]
        protected virtual async Task Update(CreateOrEditGuestDto input)
        {
            var guest = await _guestRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, guest);
        }

        [AbpAuthorize(AppPermissions.Pages_Guests_Delete)]
        public async Task SendEmail(string strToEmail, string folio)
        {
            if (!string.IsNullOrEmpty(strToEmail))
            {
                string hotelResEmail = "reservations@ghihotels.com.sg";
                string strBodyMsg = "";
                string strSubject = "Request for Invoice ( " + folio + " )";
                //bool blnIsFailEmail = false;
                //List<string> lstCCEmail = null;
                //string mailServer = await _generalProfileRepository.GetAll().Where(x => x.ProfileName == "Mail Server").Select(x => x.ProfileValue).FirstOrDefaultAsync();
                //string mailAddress = await _generalProfileRepository.GetAll().Where(x => x.ProfileName == "Mail User Name").Select(x => x.ProfileValue).FirstOrDefaultAsync();
                //string mailPassword = await _generalProfileRepository.GetAll().Where(x => x.ProfileName == "Mail Password").Select(x => x.ProfileValue).FirstOrDefaultAsync();
                //int mailPort = 587;
                //string mailServerPort = await _generalProfileRepository.GetAll().Where(x => x.ProfileName == "Mail Server Port").Select(x => x.ProfileValue).FirstOrDefaultAsync();
                //if (mailServerPort != "")
                //{
                //    mailPort = Convert.ToInt32(mailServerPort);
                //}
                strBodyMsg = mailBody(strToEmail, folio);
                //MailMessage mailMsg = new MailMessage();

                //SmtpClient client = new SmtpClient(mailServer, 587);
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //MailAddress mailAddrSender = new MailAddress(mailAddress, mailAddress);
                //MailAddress mailAddrFrom = new MailAddress(mailAddress, mailAddress);

                //mailMsg.Sender = mailAddrSender;
                //mailMsg.From = mailAddrFrom;
                //if (blnIsFailEmail)
                //{
                //    mailMsg.Bcc.Add("support@brillantez.com");
                //}

                //mailMsg.To.Add("aungkolin1985@gmail.com");

                //if (lstCCEmail != null)
                //{
                //    if (lstCCEmail.Count > 0)
                //    {
                //        foreach (string strTmp in lstCCEmail)
                //        {
                //            mailMsg.CC.Add(strTmp);
                //        }
                //    }
                //}

                //mailMsg.Body = strBodyMsg;
                //mailMsg.Subject = strSubject;
                //// mailMsg.BodyEncoding = System.Text.Encoding.ASCII;
                //mailMsg.IsBodyHtml = true;
                ////mailMsg.ReplyToList.Add(strToEmail);
                ////mailMsg.Headers.Add("Sender", mailAddress);

                //client.EnableSsl = true;
                //client.Credentials = new System.Net.NetworkCredential(mailAddress, mailPassword);
                //client.Send(mailMsg);
                _emailSender.Send(hotelResEmail, strSubject, strBodyMsg, true);
            }
        }

        private static string mailBody(string email, string docno)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("<html>");
                sb.Append("<head></head>");
                sb.Append("<body>");
                sb.Append("<table>");
                sb.Append("<tr>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("<a href=\"mailto:" + email + "\" >Please reply to guest at </a>  " + email);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append(" Thanks & Best Regards ");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</body>");
                sb.Append("</html>");

            }
            catch (Exception ex)
            {

            }
            return sb.ToString();
        }
        [AbpAuthorize(AppPermissions.Pages_Guests_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            //var resk = new Guid(_appsession.RESERVATIONKEY);
            var resk = new Guid();
            using (var unitOfWork = _unitOfWorkManager.Begin())
            {
                var upd = _resGuestRepository.GetAll().Where(x => x.ReservationKey == resk && x.GuestKey == input.Id).Select(x => x).FirstOrDefault();
                upd.ReservationKey = null;
                _resGuestRepository.Update(upd);

                var output = (from gs in _guestRepository.GetAll()
                              join n in _nationalityRepository.GetAll() on gs.CountryKey equals n.Id into gsn
                              from gsnResult in gsn.DefaultIfEmpty()
                              join n1 in _nationalityRepository.GetAll() on gsnResult.Id equals n1.Id into gsn1
                              from gsn1Result in gsn1.DefaultIfEmpty()
                              where gs.Id == input.Id
                              select new GetGuestInfoForViewDto
                              {
                                  //TitleName = gs.Gender,
                                  //LastName = gs.LastName,
                                  //FirstName = gs.FirstName,
                                  //Email = gs.EMail,
                                  //Passport = gs.Passport,
                                  //DOB = gs.DOB,
                                  //Tel = gs.Tel,
                                  //Mobile = gs.Mobile,
                                  //Fax = gs.Fax,
                                  //GuestStay = gs.GuestStay,
                                  //Postal = gs.Postal,
                                  //CountryName = gsnResult.NationalityCode,
                                  //CityCode = gs.City,
                                  //Address = (gs.Address.Trim().Length > 60 ? gs.Address.Trim().Substring(0, 60) : gs.Address.Trim()),
                                  //Address1 = (gs.Address.Trim().Length > 60 ? gs.Address.Trim().Substring(60) : ""),
                                  Name = gs.Name
                              }).FirstOrDefault();
                if (output != null && !string.IsNullOrEmpty(output.Name))
                {
                    //var history = new CHistory();
                    //history.Operation = 'D';
                    //history.TableName = "Reservation";
                    //history.SourceKey = resk;
                    //history.Detail = "(Guest Portal) Remove Shared Guest:  " + output.Name;

                    //await _chistoryRepository.InsertAsync(history);
                }

                unitOfWork.Complete();
            }

        }

        //public async Task<FileDto> GetGuestsToExcel(GetAllGuestsForExcelInput input)
        //{

        //    var filteredGuests = _guestRepository.GetAll()
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.AccNo.Contains(input.Filter) || e.CarNo.Contains(input.Filter) || e.Gender.Contains(input.Filter) || e.Tel.Contains(input.Filter) ||
        //                e.Mobile.Contains(input.Filter) || e.Fax.Contains(input.Filter) || e.EMail.Contains(input.Filter) || e.Postal.Contains(input.Filter) || e.Interest.Contains(input.Filter) || e.Company1Name.Contains(input.Filter) ||
        //                e.Company1Relation.Contains(input.Filter) || e.Company1Department.Contains(input.Filter) || e.Company1Occupation.Contains(input.Filter) || e.Company2Name.Contains(input.Filter) || e.Company2Relation.Contains(input.Filter) ||
        //                e.Company2Department.Contains(input.Filter) || e.Company2Occupation.Contains(input.Filter) || e.Company3Name.Contains(input.Filter) || e.Company3Relation.Contains(input.Filter) || e.Company3Department.Contains(input.Filter) ||
        //                e.Company3Occupation.Contains(input.Filter) || e.Company4Name.Contains(input.Filter) || e.Company4Relation.Contains(input.Filter) || e.Company4Department.Contains(input.Filter) || e.Company4Occupation.Contains(input.Filter) ||
        //                e.Title.Contains(input.Filter) || e.Company.Contains(input.Filter) || e.LastName.Contains(input.Filter) || e.FirstName.Contains(input.Filter) || e.Name.Contains(input.Filter) ||
        //                e.ShortCode.Contains(input.Filter) || e.Address.Contains(input.Filter) || e.City.Contains(input.Filter) || e.Passport.Contains(input.Filter) || e.LanguageCode.Contains(input.Filter) || e.Subscribe.Contains(input.Filter) ||
        //                e.OrgAccNo.Contains(input.Filter) || e.Computer.Contains(input.Filter) || e.tPassport.Contains(input.Filter) || e.X_Company.Contains(input.Filter) || e.X_Dorm.Contains(input.Filter) || e.X_Sector.Contains(input.Filter)
        //               );

        //    var query = (from o in filteredGuests
        //                 join o1 in _lookup_nationalityRepository.GetAll() on o.NationalityKey equals o1.Id into j1
        //                 from s1 in j1.DefaultIfEmpty()

        //                     //join o2 in _lookup_cityRepository.GetAll() on o.CityKey equals o2.Id into j2
        //                     //from s2 in j2.DefaultIfEmpty()

        //                     //join o3 in _lookup_titleRepository.GetAll() on o.TitleKey equals o3.Id into j3
        //                     //from s3 in j3.DefaultIfEmpty()

        //                 select new GetGuestForViewDto()
        //                 {
        //                     Guest = new GuestDto
        //                     {
        //                         GuestKey = o.Id,
        //                         AccNo = o.AccNo,
        //                         Status = o.Status,
        //                         Active = o.Active,
        //                         CarNo = o.CarNo,
        //                         Gender = o.Gender,
        //                         DOB = o.DOB,
        //                         Tel = o.Tel,
        //                         Mobile = o.Mobile,
        //                         Fax = o.Fax,
        //                         EMail = o.EMail,
        //                         Postal = o.Postal,
        //                         CountryKey = o.CountryKey,
        //                         NationalityKey = o.NationalityKey,
        //                         Interest = o.Interest,
        //                         CreditLimit = o.CreditLimit,
        //                         Terms = o.Terms,
        //                         Group1Key = o.Group1Key,
        //                         Group2Key = o.Group2Key,
        //                         Group3Key = o.Group3Key,
        //                         Group4Key = o.Group4Key,
        //                         SourceKey = o.SourceKey,
        //                         StaffKey = o.StaffKey,
        //                         DefaultCompany = o.DefaultCompany,
        //                         Company1Name = o.Company1Name,
        //                         Company1Relation = o.Company1Relation,
        //                         Company1Department = o.Company1Department,
        //                         Company1Occupation = o.Company1Occupation,
        //                         Company2Name = o.Company2Name,
        //                         Company2Relation = o.Company2Relation,
        //                         Company2Department = o.Company2Department,
        //                         Company2Occupation = o.Company2Occupation,
        //                         Company3Name = o.Company3Name,
        //                         Company3Relation = o.Company3Relation,
        //                         Company3Department = o.Company3Department,
        //                         Company3Occupation = o.Company3Occupation,
        //                         Company4Name = o.Company4Name,
        //                         Company4Relation = o.Company4Relation,
        //                         Company4Department = o.Company4Department,
        //                         Company4Occupation = o.Company4Occupation,
        //                         LastModifiedStaff = o.LastModifiedStaff,
        //                         CreatedDate = o.CreatedDate,
        //                         Sort = o.Sort,
        //                         Sync = o.Sync,
        //                         Seq = o.Seq,                                 
        //                         PassportExpiry = o.PassportExpiry,
        //                         Extra1 = o.Extra1,
        //                         Extra2 = o.Extra2,
        //                         Extra3 = o.Extra3,
        //                         Extra4 = o.Extra4,
        //                         Extra5 = o.Extra5,
        //                         Extra6 = o.Extra6,
        //                         Extra7 = o.Extra7,
        //                         Extra8 = o.Extra8,
        //                         Extra9 = o.Extra9,
        //                         Extra10 = o.Extra10,
        //                         Extra11 = o.Extra11,
        //                         Extra12 = o.Extra12,
        //                         Extra13 = o.Extra13,
        //                         Extra14 = o.Extra14,
        //                         Extra15 = o.Extra15,
        //                         Extra16 = o.Extra16,
        //                         Extra17 = o.Extra17,
        //                         Extra18 = o.Extra18,
        //                         Extra19 = o.Extra19,
        //                         Extra20 = o.Extra20,
        //                         Extra21 = o.Extra21,
        //                         Extra22 = o.Extra22,
        //                         Extra23 = o.Extra23,
        //                         Extra24 = o.Extra24,
        //                         RegionKey = o.RegionKey,
        //                         GuestStay = o.GuestStay,
        //                         Title = o.Title,
        //                         Company = o.Company,
        //                         LastName = o.LastName,
        //                         FirstName = o.FirstName,
        //                         Name = o.Name,
        //                         ShortCode = o.ShortCode,
        //                         Address = o.Address,
        //                         City = o.City,
        //                         GuestIdentityTypeKey = o.GuestIdentityTypeKey,
        //                         Passport = o.Passport,
        //                         LanguageCode = o.LanguageCode,
        //                         Subscribe = o.Subscribe,
        //                         PropertyKey = o.PropertyKey,
        //                         OrgGuestKey = o.OrgGuestKey,
        //                         OrgAccNo = o.OrgAccNo,
        //                         DoNotContact = o.DoNotContact,
        //                         OldGuestStay = o.OldGuestStay,
        //                         Users = o.Users,
        //                         Computer = o.Computer,
        //                         Access = o.Access,
        //                         tPassport = o.tPassport,
        //                         X_Company = o.X_Company,
        //                         X_Dorm = o.X_Dorm,
        //                         X_Sector = o.X_Sector,
        //                         Id = o.Id
        //                     },
        //                     //NationalityNationality1 = s1 == null || s1.NationalityCode == null ? "" : s1.NationalityCode.ToString(),
        //                     //CityCity1 = s2 == null || s2.CityCode == null ? "" : s2.CityCode.ToString(),
        //                     //TitleTitle1 = s3 == null || s3.TitleCode == null ? "" : s3.TitleCode.ToString()
        //                 });

        //    var guestListDtos = await query.ToListAsync();

        //    return _guestsExcelExporter.ExportToFile(guestListDtos);
        //}

        //[AbpAuthorize(AppPermissions.Pages_Guests)]

        //[AbpAuthorize(AppPermissions.Pages_Guests)]
        public async Task<List<GuestTitleLookupTableDto>> GetAllTitleForTableDropdown()
        {
            return await _lookup_titleRepository.GetAll().Where(x => x.Active == 1).OrderBy(x => x.Sort)
                .Select(title => new GuestTitleLookupTableDto
                {
                    Id = title.Id.ToString(),
                    DisplayName = title == null || title.TitleName == null ? "" : title.TitleName.ToString()
                }).ToListAsync();
        }

    }
}