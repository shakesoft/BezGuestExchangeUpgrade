using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore.iGuestData.Dtos
{
    public class InvoiceData
    {
        public string RefID { get; set; }
        public string InvSeries { get; set; }
        //public string InvoiceName { get; set; }
        
        public string InvDate { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }
        //public decimal DiscountRate { get; set; }
        //public bool IsTaxReduction43 { get; set; }
        public string PaymentMethodName { get; set; }

        public bool IsSendEmail { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }

        //Buyer Information
        public string BuyerLegalName { get; set; }
        public string BuyerTaxCode { get; set; }
        public string BuyerAddress { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerFullName { get; set; }
        public string BuyerEmail { get; set; }
        
        public string BuyerPhoneNumber { get; set; }

        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string RoomNo { get; set; }

        //public string BuyerBankAccount { get; set; }
        //public string BuyerBankName { get; set; }
        public string BuyerIDNumber { get; set; }
        public string BuyerPassport { get; set; }
        public string BuyerBudgetCode { get; set; }
        //public string ContactName { get; set; }

        //Total Amount Information
        public decimal TotalAmountWithoutVATOC { get; set; }
        public decimal TotalVATAmountOC { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountWithoutVAT { get; set; }
        public decimal TotalVATAmount { get; set; }
        public decimal TotalDiscountAmountOC { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal TotalSaleAmountOC { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public decimal TotalAmountOC { get; set; }
        public string TotalAmountInWords { get; set; }
        public List<OriginalInvoiceDetail> OriginalInvoiceDetail { get; set; }
        public List<TaxRateInfo> TaxRateInfo { get; set; }
        //public List<FeeInfo> FeeInfo { get; set; }
        public OptionUserDefined OptionUserDefined { get; set; }
        //public bool IsInvoiceCalculatingMachine { get; set; }
        
        //Hotel Information
        
        //public decimal ExciseTaxRate { get; set; }
        //public decimal ServiceFeeRate { get; set; }
        //public decimal ServiceAmountOC { get; set; }
        //public decimal ServiceAmount { get; set; }
        //public decimal ExciseTaxAmountOC { get; set; }
        //public decimal ExciseTaxAmount { get; set; }
        //public string AccountObjectIdentificationNumber { get; set; }

        //Adjust/Replace information
        //public int ReferenceType { get; set; }
        //public int OrgInvoiceType { get; set; }
        //public string OrgInvTemplateNo { get; set; }
        //public string OrgInvSeries { get; set; }
        //public string OrgInvNo { get; set; }
        //public string OrgInvDate { get; set; }
        //public string InvoiceNote { get; set; }

        //Custom field information
        //public string CustomField1 { get; set; }
        //public string CustomField2 { get; set; }
        //public string CustomField3 { get; set; }
        //public string CustomField4 { get; set; }
        //public string CustomField5 { get; set; }
        //public string CustomField6 { get; set; }
        //public string CustomField7 { get; set; }
        //public string CustomField8 { get; set; }
        //public string CustomField9 { get; set; }
        //public string CustomField10 { get; set; }


    }
}
