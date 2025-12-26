using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore.iGuestData.Dtos
{
    public class CMessage
    {
        public int MessageID { get; set; }
        public RequestType SendOrReceived { get; set; }
        public MessageType MsgType { get; set; }
        public string Data { get; set; }
        public DateTime? ProcessedDateTime { get; set; }

        public int? TenantId { get; set; }

        public enum RequestType : int
        {
            SendData = 1,
            ReceivedData = 2
        }

        public enum MessageType : int
        {
            OTA_READRQ = 1,
            OTA_RESRETRIEVERS = 2,
            OTA_EINVOICEISSUERQ = 3,
            OTA_EINVOICESTATUSRQ = 4,
            OTA_EINVOICEDOWNLOADRQ = 5,
            OTA_EINVOICECANCELRQ = 6,
            OTA_EINVOICEISSUENOTIFRS = 7,
            OTA_EINVOICESTATUSNOTIFRS = 8,
            OTA_EINVOICEDOWNLOADNOTIFRS = 9,
            OTA_EINVOICECANCELNOTIFRS = 10,
        }
    }
}
