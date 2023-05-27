using App.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Domain
{
    public class EntityBase
    {
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public EntityStatus Status { get; set; }

    }
}
