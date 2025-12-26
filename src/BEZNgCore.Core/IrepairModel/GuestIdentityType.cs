using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEZNgCore.IrepairModel
{   
    [Table("GuestIdentityType")]
    public class GuestIdentityType : Entity<Guid>, IMayHaveTenant
    {

        [Column("GuestIdentityTypeKey")]
        public override Guid Id { get; set; }
        public int? TenantId { get; set; }

        public virtual int? Sort { get; set; }
        public virtual int? Sync { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Seq { get; set; }
        [Timestamp]
        public virtual byte[] TS { get; set; }

        [StringLength(20, MinimumLength = 0)]
        public virtual string Type { get; set; }
                       
        public virtual byte DefaultType { get; set; }

        public virtual int? Active { get; set; }
    }

}
