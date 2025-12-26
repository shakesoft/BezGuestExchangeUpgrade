using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore.iGuestData.Dtos
{
    public class AuthData
    {        
        public bool success { get; set; }
        public string errorCode { get; set; }
        public string descriptionErrorCode { get; set; }
        public List<string> errors { get; set; } // Assuming Errors is a list of strings
        public string data { get; set; }
        public string customData { get; set; }
    }
}
