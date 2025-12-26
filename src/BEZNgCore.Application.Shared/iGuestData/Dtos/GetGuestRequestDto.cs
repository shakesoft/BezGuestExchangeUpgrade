using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore.IStay.Dtos
{
    public class GetGuestRequestDto
    {        
        public Guid? TitleKey { get; set; }
        public string TitleName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public Guid? RequestTypeKey  { get; set; }     
        public Guid GuestKey { get; set; }

        public string GuestRequest { get; set; }
    }
}
