using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BEZNgCore.iGuestData.Dtos
{
    //EntityDto<Guid>
    public class POSChargePaymentInput
    {
        public DateTime Date { get; set; }

        public string Time { get; set; }
        
        public string Docketno { get; set; }
      
        public decimal? Amount { get; set; }      

        public string Outlet { get; set; }

        public string Payment { get; set; }

        public string Period { get; set; }

        public string Covers { get; set; }
    }
}
