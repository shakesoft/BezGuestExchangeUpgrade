using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace BEZNgCore.IStay.Dtos
{
    public class CreateOrEditGuestDto : EntityDto<Guid?>
    {

        [Required]
        public Guid GuestKey { get; set; }

        [StringLength(GuestConsts.MaxAccNoLength, MinimumLength = GuestConsts.MinAccNoLength)]
        public string AccNo { get; set; }

        public int? Status { get; set; }

        public int? Active { get; set; }

        [StringLength(GuestConsts.MaxCarNoLength, MinimumLength = GuestConsts.MinCarNoLength)]
        public string CarNo { get; set; }

        [StringLength(GuestConsts.MaxGenderLength, MinimumLength = GuestConsts.MinGenderLength)]
        public string Gender { get; set; }

        public DateTime? DOB { get; set; }

        [StringLength(GuestConsts.MaxTelLength, MinimumLength = GuestConsts.MinTelLength)]
        public string Tel { get; set; }

        [StringLength(GuestConsts.MaxMobileLength, MinimumLength = GuestConsts.MinMobileLength)]
        public string Mobile { get; set; }

        [StringLength(GuestConsts.MaxFaxLength, MinimumLength = GuestConsts.MinFaxLength)]
        public string Fax { get; set; }

        [StringLength(GuestConsts.MaxEMailLength, MinimumLength = GuestConsts.MinEMailLength)]
        public string EMail { get; set; }

        [StringLength(GuestConsts.MaxPostalLength, MinimumLength = GuestConsts.MinPostalLength)]
        public string Postal { get; set; }

        public Guid CountryKey { get; set; }

        public Guid NationalityKey { get; set; }

        [StringLength(GuestConsts.MaxInterestLength, MinimumLength = GuestConsts.MinInterestLength)]
        public string Interest { get; set; }

        public decimal? CreditLimit { get; set; }

        public int? Terms { get; set; }

        public Guid Group1Key { get; set; }

        public Guid Group2Key { get; set; }

        public Guid Group3Key { get; set; }

        public Guid Group4Key { get; set; }

        public Guid SourceKey { get; set; }

        public Guid StaffKey { get; set; }

        public int? DefaultCompany { get; set; }

        [StringLength(GuestConsts.MaxCompany1NameLength, MinimumLength = GuestConsts.MinCompany1NameLength)]
        public string Company1Name { get; set; }

        [StringLength(GuestConsts.MaxCompany1RelationLength, MinimumLength = GuestConsts.MinCompany1RelationLength)]
        public string Company1Relation { get; set; }

        [StringLength(GuestConsts.MaxCompany1DepartmentLength, MinimumLength = GuestConsts.MinCompany1DepartmentLength)]
        public string Company1Department { get; set; }

        [StringLength(GuestConsts.MaxCompany1OccupationLength, MinimumLength = GuestConsts.MinCompany1OccupationLength)]
        public string Company1Occupation { get; set; }

        [StringLength(GuestConsts.MaxCompany2NameLength, MinimumLength = GuestConsts.MinCompany2NameLength)]
        public string Company2Name { get; set; }

        [StringLength(GuestConsts.MaxCompany2RelationLength, MinimumLength = GuestConsts.MinCompany2RelationLength)]
        public string Company2Relation { get; set; }

        [StringLength(GuestConsts.MaxCompany2DepartmentLength, MinimumLength = GuestConsts.MinCompany2DepartmentLength)]
        public string Company2Department { get; set; }

        [StringLength(GuestConsts.MaxCompany2OccupationLength, MinimumLength = GuestConsts.MinCompany2OccupationLength)]
        public string Company2Occupation { get; set; }

        [StringLength(GuestConsts.MaxCompany3NameLength, MinimumLength = GuestConsts.MinCompany3NameLength)]
        public string Company3Name { get; set; }

        [StringLength(GuestConsts.MaxCompany3RelationLength, MinimumLength = GuestConsts.MinCompany3RelationLength)]
        public string Company3Relation { get; set; }

        [StringLength(GuestConsts.MaxCompany3DepartmentLength, MinimumLength = GuestConsts.MinCompany3DepartmentLength)]
        public string Company3Department { get; set; }

        [StringLength(GuestConsts.MaxCompany3OccupationLength, MinimumLength = GuestConsts.MinCompany3OccupationLength)]
        public string Company3Occupation { get; set; }

        [StringLength(GuestConsts.MaxCompany4NameLength, MinimumLength = GuestConsts.MinCompany4NameLength)]
        public string Company4Name { get; set; }

        [StringLength(GuestConsts.MaxCompany4RelationLength, MinimumLength = GuestConsts.MinCompany4RelationLength)]
        public string Company4Relation { get; set; }

        [StringLength(GuestConsts.MaxCompany4DepartmentLength, MinimumLength = GuestConsts.MinCompany4DepartmentLength)]
        public string Company4Department { get; set; }

        [StringLength(GuestConsts.MaxCompany4OccupationLength, MinimumLength = GuestConsts.MinCompany4OccupationLength)]
        public string Company4Occupation { get; set; }

        public Guid LastModifiedStaff { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? Sort { get; set; }

        public int? Sync { get; set; }

        [Required]
        public int Seq { get; set; }

        [StringLength(GuestConsts.MaxTSLength, MinimumLength = GuestConsts.MinTSLength)]
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

        public Guid RegionKey { get; set; }

        public int? GuestStay { get; set; }

        [StringLength(GuestConsts.MaxTitleLength, MinimumLength = GuestConsts.MinTitleLength)]
        public string Title { get; set; }

        [StringLength(GuestConsts.MaxCompanyLength, MinimumLength = GuestConsts.MinCompanyLength)]
        public string Company { get; set; }

        [StringLength(GuestConsts.MaxLastNameLength, MinimumLength = GuestConsts.MinLastNameLength)]
        public string LastName { get; set; }

        [StringLength(GuestConsts.MaxFirstNameLength, MinimumLength = GuestConsts.MinFirstNameLength)]
        public string FirstName { get; set; }

        [StringLength(GuestConsts.MaxNameLength, MinimumLength = GuestConsts.MinNameLength)]
        public string Name { get; set; }

        [StringLength(GuestConsts.MaxShortCodeLength, MinimumLength = GuestConsts.MinShortCodeLength)]
        public string ShortCode { get; set; }

        [StringLength(GuestConsts.MaxGuestLength, MinimumLength = GuestConsts.MinGuestLength)]
        public string Guest { get; set; }

        [StringLength(GuestConsts.MaxAddressLength, MinimumLength = GuestConsts.MinAddressLength)]
        public string Address { get; set; }

        [StringLength(GuestConsts.MaxCityLength, MinimumLength = GuestConsts.MinCityLength)]
        public string City { get; set; }

        public Guid GuestIdentityTypeKey { get; set; }

        [StringLength(GuestConsts.MaxPassportLength, MinimumLength = GuestConsts.MinPassportLength)]
        public string Passport { get; set; }

        [StringLength(GuestConsts.MaxLanguageCodeLength, MinimumLength = GuestConsts.MinLanguageCodeLength)]
        public string LanguageCode { get; set; }

        [StringLength(GuestConsts.MaxSubscribeLength, MinimumLength = GuestConsts.MinSubscribeLength)]
        public string Subscribe { get; set; }

        public Guid PropertyKey { get; set; }

        public Guid OrgGuestKey { get; set; }

        [StringLength(GuestConsts.MaxOrgAccNoLength, MinimumLength = GuestConsts.MinOrgAccNoLength)]
        public string OrgAccNo { get; set; }

        public int? DoNotContact { get; set; }

        public int? OldGuestStay { get; set; }

        public Guid Users { get; set; }

        [StringLength(GuestConsts.MaxComputerLength, MinimumLength = GuestConsts.MinComputerLength)]
        public string Computer { get; set; }

        public DateTime? Access { get; set; }

        [StringLength(GuestConsts.MaxtPassportLength, MinimumLength = GuestConsts.MintPassportLength)]
        public string tPassport { get; set; }

        [StringLength(GuestConsts.MaxX_CompanyLength, MinimumLength = GuestConsts.MinX_CompanyLength)]
        public string X_Company { get; set; }

        [StringLength(GuestConsts.MaxX_DormLength, MinimumLength = GuestConsts.MinX_DormLength)]
        public string X_Dorm { get; set; }

        [StringLength(GuestConsts.MaxX_SectorLength, MinimumLength = GuestConsts.MinX_SectorLength)]
        public string X_Sector { get; set; }
        

        public Guid? CityKey { get; set; }

        public Guid? TitleKey { get; set; }

    }
}