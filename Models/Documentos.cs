using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace pWPortalLogista.Models
{
    public partial class Documentos
    {
        public Documentos()
        {
            DocuserOpen = new HashSet<DocuserOpen>();
        }

        public int Iddoc { get; set; }
        public string Nodoc { get; set; }
        public DateTime? Dtvenc { get; set; }
        public bool? Flgopen { get; set; }
        public DateTime? Dtopen { get; set; }
        public string Txluc { get; set; }
        public DateTime? Dtfat { get; set; }

        public virtual ICollection<DocuserOpen> DocuserOpen { get; set; }
    }
}
