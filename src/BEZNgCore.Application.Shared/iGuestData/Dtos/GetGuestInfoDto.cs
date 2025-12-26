using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BEZNgCore.iGuestData.Dtos
{
    //:EntityDto<Guid>
    public class GetGuestInfoDto
    {       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Name { get; set; }
        public string Title { get; set; }
        
        public string Gender { get; set; }        
        public DateTime? DOB { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Postal { get; set; }        
        public string Address { get; set; }
        //public string GuestShortCode { get; set; }
        public string PassportIdentityNo { get; set; }        
        //public Guid? StatusKey { get; set; }
        //public int? Status { get; set; }
        public string GuestDocType { get; set; }
        
        //public Guid? GuestDocTypeKey { get; set; }
        //public string GuestInterest { get; set; }        
        //public Guid? RegionKey { get; set; }
        public string RegionName { get; set; }
        
        //public Guid? CityKey { get; set; }
        public string CityName { get; set; }
        //public Guid? CountryKey { get; set; }
        public string CountryName { get; set; }
        
        //public Guid? NationalityKey { get; set; }
        public string NationalityName { get; set; }
    }
}
