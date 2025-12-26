using System;
using Abp.Application.Services.Dto;

namespace BEZNgCore.IStay.Dtos
{
    public class GuestDto : EntityDto<Guid>
    {
        public Guid GuestKey { get; set; }

        public string AccNo { get; set; }

        public int? Status { get; set; }

        public int? Active { get; set; }

        public string CarNo { get; set; }

        public string Gender { get; set; }

        public DateTime? DOB { get; set; }

        public string Tel { get; set; }

        public string Mobile { get; set; }

        public string Fax { get; set; }

        public string EMail { get; set; }

        public string Postal { get; set; }

        public Guid? CountryKey { get; set; }

        public Guid? NationalityKey { get; set; }

        public string Interest { get; set; }

        public decimal? CreditLimit { get; set; }

        public int? Terms { get; set; }

        public Guid? Group1Key { get; set; }

        public string GuestCode { get; set; }

        public Guid? Group2Key { get; set; }

        public Guid? Group3Key { get; set; }

        public Guid? Group4Key { get; set; }

        public Guid? SourceKey { get; set; }

        public Guid? StaffKey { get; set; }

        public int? DefaultCompany { get; set; }

        public string Company1Name { get; set; }

        public string Company1Relation { get; set; }

        public string Company1Department { get; set; }

        public string Company1Occupation { get; set; }

        public string Company2Name { get; set; }

        public string Company2Relation { get; set; }

        public string Company2Department { get; set; }

        public string Company2Occupation { get; set; }

        public string Company3Name { get; set; }

        public string Company3Relation { get; set; }

        public string Company3Department { get; set; }

        public string Company3Occupation { get; set; }

        public string Company4Name { get; set; }

        public string Company4Relation { get; set; }

        public string Company4Department { get; set; }

        public string Company4Occupation { get; set; }

        public Guid? LastModifiedStaff { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? Sort { get; set; }

        public int? Sync { get; set; }

        public int Seq { get; set; }

        public string TS { get; set; }

        public DateTime? PassportExpiry { get; set; }

        public int? Extra1 { get; set; }

        public int? Extra2 { get; set; }

        public int? Extra3 { get; set; }

        public int? Extra4 { get; set; }

        public int? Extra5 { get; set; }

        public int? Extra6 { get; set; }

        public int? Extra7 { get; set; }

        public int? Extra8 { get; set; }

        public int? Extra9 { get; set; }

        public int? Extra10 { get; set; }

        public int? Extra11 { get; set; }

        public int? Extra12 { get; set; }

        public int? Extra13 { get; set; }

        public int? Extra14 { get; set; }

        public int? Extra15 { get; set; }

        public int? Extra16 { get; set; }

        public int? Extra17 { get; set; }

        public int? Extra18 { get; set; }

        public int? Extra19 { get; set; }

        public int? Extra20 { get; set; }

        public int? Extra21 { get; set; }

        public int? Extra22 { get; set; }

        public int? Extra23 { get; set; }

        public int? Extra24 { get; set; }

        public Guid? RegionKey { get; set; }

        public int? GuestStay { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Name { get; set; }

        public string ShortCode { get; set; }
        

        public string Address { get; set; }

        public string City { get; set; }

        public Guid? GuestIdentityTypeKey { get; set; }

        public string Passport { get; set; }

        public string LanguageCode { get; set; }

        public string Subscribe { get; set; }

        public Guid? PropertyKey { get; set; }

        public Guid? OrgGuestKey { get; set; }

        public string OrgAccNo { get; set; }

        public int? DoNotContact { get; set; }

        public int? OldGuestStay { get; set; }

        public Guid? Users { get; set; }

        public string Computer { get; set; }

        public DateTime? Access { get; set; }

        public string tPassport { get; set; }

        public string X_Company { get; set; }

        public string X_Dorm { get; set; }

        public string X_Sector { get; set; }
        

        public Guid? CityKey { get; set; }

        public Guid? TitleKey { get; set; }

    }
}