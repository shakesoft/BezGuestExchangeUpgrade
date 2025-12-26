using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore.iGuestData.Dtos
{
    public class tblSendDataMisa
    {
        public int intSendMsgId { get; set; }
        public int intMsgType { get; set; }

        public string xmlSendData { get; set; }

        public DateTime? dtSentDateTime { get; set; }
        public int? Flag { get; set; }

        public int? Retry { get; set; }

        public int TenantId { get; set; }
    }
}
