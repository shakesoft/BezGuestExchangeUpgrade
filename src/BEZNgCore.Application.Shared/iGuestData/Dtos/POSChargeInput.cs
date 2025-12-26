using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BEZNgCore.iGuestData.Dtos
{
    //:EntityDto<Guid>
    public class POSChargeInput 
    {        
        public DateTime Date { get; set; }

        public string Time { get; set; }
          
       
        public string Room { get; set; }

        
        public decimal? Charge { get; set; }

        //public string Ref { get; set; }

        ////[Required]
        ////public Guid ItemKey { get; set; }
        
        //public string Source { get; set; }

        //public string Description { get; set; }

        public string Docketno { get; set; }

        public string Outlet { get; set; }

        public string Revenue { get; set; }

        public string Period { get; set; }

        public string Covers { get; set; }

    }
}
