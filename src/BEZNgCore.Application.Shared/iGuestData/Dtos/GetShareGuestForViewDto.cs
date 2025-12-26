using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore.IStay.Dtos
{
    public class GetShareGuestForViewDto
    {
        public Guid? GuestKey { get; set; }

        public string Name { get; set; }
    }
}
