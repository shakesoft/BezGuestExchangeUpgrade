using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore.IStay.Dtos
{
    public class CityDto
    {
        public Guid Id { get; set; }
        public string ShortCode { get; set; }
        public Nullable<int> Sort { get; set; }
        public Nullable<int> Sync { get; set; }
        public int Seq { get; set; }
        //public byte[] TS { get; set; }
        public string City1 { get; set; }
    }
}
