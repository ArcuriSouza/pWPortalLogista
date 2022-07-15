using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace pWPortalLogista.Models
{
    public partial class Luc
    {
        public Luc()
        {
            UserLuc = new HashSet<UserLuc>();
        }

        public int Idluc { get; set; }
        public string Txluc { get; set; }
        public string Txfantasia { get; set; }

        public virtual ICollection<UserLuc> UserLuc { get; set; }
    }
}
