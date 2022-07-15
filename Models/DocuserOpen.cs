using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace pWPortalLogista.Models
{
    public partial class DocuserOpen
    {
        public int Iddocuser { get; set; }
        public int Iddoc { get; set; }
        public int Iduser { get; set; }
        public DateTime? Dtopen { get; set; }

        public virtual Documentos IddocNavigation { get; set; }
        public virtual Usuario IduserNavigation { get; set; }
    }
}
