using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore.iGuestData.Dtos
{
    public class POSMisaDto
    {
        public int SignType { get; set; }
        public List<InvoiceData> InvoiceData { get; set; }

        public List<PublishInvoiceData> PublishInvoiceData { get; set; }
    }
}
