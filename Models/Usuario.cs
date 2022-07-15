using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace pWPortalLogista.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            DocuserOpen = new HashSet<DocuserOpen>();
            UserLuc = new HashSet<UserLuc>();
        }

        public int Iduser { get; set; }
        public string Txpwd { get; set; }
        public string Txemail { get; set; }
        public bool? Isadmin { get; set; }
        public DateTime? Dtlastaccess { get; set; }
        public decimal? Nrarea { get; set; }
        public string Txcnpjcpf { get; set; }
        public string Txfantasia { get; set; }
        public string Txqualif { get; set; }
        public string Txcontrato { get; set; }
        public string Txnopessoa { get; set; }
        public string Hashkey { get; set; }
        public DateTime? Dtvalidhash { get; set; }
        public string Txendereco { get; set; }
        public string Txbairro { get; set; }
        public string Txcep { get; set; }
        public string Txinsest { get; set; }
        public string Txinsmun { get; set; }
        public bool? Isactive { get; set; }
        public bool? Isallow { get; set; }
        public string Txluc { get; set; }

        public virtual ICollection<DocuserOpen> DocuserOpen { get; set; }
        public virtual ICollection<UserLuc> UserLuc { get; set; }
    }
}
