using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BEZNgCore.iGuestData.Dtos
{
    public class GuestExchangeUpdateDto : EntityDto<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }        
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }
        public string Postal { get; set; }
        public string Address { get; set; }
        //public string ShortCode { get; set; }
        public string Passport { get; set; }  
        //public int? Status { get; set; }      
        public Guid? GuestIdentityTypeKey { get; set; }
        public string Interest { get; set; }        
        public Guid? RegionKey { get; set; }        
        public string City { get; set; }
        public Guid? CountryKey { get; set; }               
        public Guid? NationalityKey { get; set; }       
    }
}
