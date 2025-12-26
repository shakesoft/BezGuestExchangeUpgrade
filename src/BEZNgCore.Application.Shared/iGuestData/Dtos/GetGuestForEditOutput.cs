using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace BEZNgCore.IStay.Dtos
{
    public class GetGuestForEditOutput
    {
        public CreateOrEditGuestDto Guest { get; set; }

        public string NationalityNationality1 { get; set; }

        public string CityCity1 { get; set; }

        public string TitleTitle1 { get; set; }

    }
}