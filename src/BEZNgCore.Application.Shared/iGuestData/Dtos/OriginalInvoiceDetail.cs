using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore.iGuestData.Dtos
{
    public class OriginalInvoiceDetail
    {
        //Inventory information
        public int ItemType { get; set; }
        public int LineNumber { get; set; }
        public int SortOrder { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UnitName { get; set; }        
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal DiscountAmountOC { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal AmountOC { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountWithoutVATOC { get; set; }
        //public decimal AmountWithoutVAT { get; set; }
        public string VATRateName { get; set; }
        //public decimal AmountAfterTax { get; set; }
        public decimal VATAmountOC { get; set; }
        public decimal VATAmount { get; set; }

        //public string CustomField1Detail { get; set; }
        //public string CustomField2Detail { get; set; }
        //public string CustomField3Detail { get; set; }
        //public string CustomField4Detail { get; set; }
        //public string CustomField5Detail { get; set; }
        //public string CustomField6Detail { get; set; }
        //public string CustomField7Detail { get; set; }
        //public string CustomField8Detail { get; set; }
        //public string CustomField9Detail { get; set; }
        //public string CustomField10Detail { get; set; }
        
    }
}
