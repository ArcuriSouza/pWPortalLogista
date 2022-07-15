using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace pWPortalLogista.Models
{
    public partial class UserLuc
    {
        public int IdUserluc { get; set; }
        public int? Iduser { get; set; }
        public int? Idluc { get; set; }
        public bool? Isallow { get; set; }

        public virtual Luc IdlucNavigation { get; set; }
        public virtual Usuario IduserNavigation { get; set; }
    }
}
