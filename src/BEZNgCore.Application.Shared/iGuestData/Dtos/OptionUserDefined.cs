using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore.iGuestData.Dtos
{
    public class OptionUserDefined
    {
        public string MainCurrency { get; set; }
        public string QuantityDecimalDigits { get; set; }
        public string AmountDecimalDigits { get; set; }
        public string AmountOCDecimalDigits { get; set; }
        public string UnitPriceOCDecimalDigits { get; set; }
        public string UnitPriceDecimalDigits { get; set; }
        
        public string CoefficientDecimalDigits { get; set; }
        public string ExchangRateDecimalDigits { get; set; }
        public string ClockDecimalDigits { get; set; }
    }
}
