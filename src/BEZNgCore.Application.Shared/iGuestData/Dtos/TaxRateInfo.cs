using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore.iGuestData.Dtos
{
    public class TaxRateInfo
    {
        public string VATRateName { get; set; }
        public decimal AmountWithoutVATOC { get; set; }
        public decimal VATAmountOC { get; set; }
    }
}
