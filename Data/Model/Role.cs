using Data.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model
{
    public class Role:BaseEntity
    {
        public string RoleName { get; set; }

        public ICollection<Account>Accounts { get; set; }
    }
}
