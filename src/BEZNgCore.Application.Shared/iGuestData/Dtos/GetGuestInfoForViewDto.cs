using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BEZNgCore.IStay.Dtos
{
    public class GetGuestInfoForViewDto
    {
        public Guid? TitleKey { get; set; }
        public string TitleName { get; set; }

        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Passport { get; set; }
        public DateTime? PassportExpiry { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }

        [Required]
        public DateTime? DOB { get; set; }

        [Required]
        public string Address { get; set; }

        public string Address1 { get; set; }

        [Required]
        public string Postal { get; set; }

        public string CityName { get; set; }

        public Guid? CityKey { get; set; }

        public Guid? CountryKey { get; set; }

        public Guid? NationalityKey { get; set; }

        public string NationalityName { get; set; }

        [Required]
        public string Mobile { get; set; }

        public string Tel { get; set; }

        public string Fax { get; set; }

        public int? GuestStay { get; set; }

        public Guid GuestKey { get; set; }

    }
}
