using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace pWPortalLogista.Models
{
    public partial class PLDbContext : DbContext
    {
        public PLDbContext()
        {
        }

        public PLDbContext(DbContextOptions<PLDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Documentos> Documentos { get; set; }
        public virtual DbSet<DocuserOpen> DocuserOpen { get; set; }
        public virtual DbSet<Luc> Luc { get; set; }
        public virtual DbSet<UserLuc> UserLuc { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=pgsql.realemp.com.br;Port=5432;Database=realemp;User Id=realemp;Password=Rj21dev;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Documentos>(entity =>
            {
                entity.HasKey(e => e.Iddoc)
                    .HasName("DOCUMENTOS_pkey");

                entity.ToTable("DOCUMENTOS");

                entity.Property(e => e.Iddoc).HasColumnName("IDDOC");

                entity.Property(e => e.Dtfat).HasColumnName("DTFAT");

                entity.Property(e => e.Dtopen).HasColumnName("DTOPEN");

                entity.Property(e => e.Dtvenc)
                    .HasColumnName("DTVENC")
                    .HasColumnType("date");

                entity.Property(e => e.Flgopen).HasColumnName("FLGOPEN");

                entity.Property(e => e.Nodoc)
                    .IsRequired()
                    .HasColumnName("NODOC")
                    .HasMaxLength(150);

                entity.Property(e => e.Txluc)
                    .HasColumnName("TXLUC")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<DocuserOpen>(entity =>
            {
                entity.HasKey(e => e.Iddocuser)
                    .HasName("DOCUSER_OPEN_pkey");

                entity.ToTable("DOCUSER_OPEN");

                entity.Property(e => e.Iddocuser).HasColumnName("IDDOCUSER");

                entity.Property(e => e.Dtopen).HasColumnName("DTOPEN");

                entity.Property(e => e.Iddoc).HasColumnName("IDDOC");

                entity.Property(e => e.Iduser).HasColumnName("IDUSER");

                entity.HasOne(d => d.IddocNavigation)
                    .WithMany(p => p.DocuserOpen)
                    .HasForeignKey(d => d.Iddoc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DOCUSER_OPEN_IDDOC_fkey");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.DocuserOpen)
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DOCUSER_OPEN_IDUSER_fkey");
            });

            modelBuilder.Entity<Luc>(entity =>
            {
                entity.HasKey(e => e.Idluc)
                    .HasName("LUC_pkey");

                entity.ToTable("LUC");

                entity.Property(e => e.Idluc).HasColumnName("IDLUC");

                entity.Property(e => e.Txfantasia)
                    .IsRequired()
                    .HasColumnName("TXFANTASIA")
                    .HasMaxLength(200);

                entity.Property(e => e.Txluc)
                    .IsRequired()
                    .HasColumnName("TXLUC")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<UserLuc>(entity =>
            {
                entity.HasKey(e => e.IdUserluc)
                    .HasName("USER_LUC_pkey");

                entity.ToTable("USER_LUC");

                entity.Property(e => e.IdUserluc).HasColumnName("ID_USERLUC");

                entity.Property(e => e.Idluc).HasColumnName("IDLUC");

                entity.Property(e => e.Iduser).HasColumnName("IDUSER");

                entity.Property(e => e.Isallow).HasColumnName("ISALLOW");

                entity.HasOne(d => d.IdlucNavigation)
                    .WithMany(p => p.UserLuc)
                    .HasForeignKey(d => d.Idluc)
                    .HasConstraintName("USER_LUC_IDLUC_fkey");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.UserLuc)
                    .HasForeignKey(d => d.Iduser)
                    .HasConstraintName("USER_LUC_IDUSER_fkey");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("USUARIO_pkey");

                entity.ToTable("USUARIO");

                entity.Property(e => e.Iduser).HasColumnName("IDUSER");

                entity.Property(e => e.Dtlastaccess).HasColumnName("DTLASTACCESS");

                entity.Property(e => e.Dtvalidhash).HasColumnName("DTVALIDHASH");

                entity.Property(e => e.Hashkey)
                    .HasColumnName("HASHKEY")
                    .HasMaxLength(50);

                entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");

                entity.Property(e => e.Isadmin).HasColumnName("ISADMIN");

                entity.Property(e => e.Isallow)
                    .HasColumnName("ISALLOW")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Nrarea)
                    .HasColumnName("NRAREA")
                    .HasColumnType("numeric");

                entity.Property(e => e.Txbairro)
                    .HasColumnName("TXBAIRRO")
                    .HasMaxLength(150);

                entity.Property(e => e.Txcep)
                    .HasColumnName("TXCEP")
                    .HasMaxLength(8);

                entity.Property(e => e.Txcnpjcpf)
                    .HasColumnName("TXCNPJCPF")
                    .HasMaxLength(14);

                entity.Property(e => e.Txcontrato)
                    .HasColumnName("TXCONTRATO")
                    .HasMaxLength(7);

                entity.Property(e => e.Txemail)
                    .HasColumnName("TXEMAIL")
                    .HasMaxLength(100);

                entity.Property(e => e.Txendereco)
                    .HasColumnName("TXENDERECO")
                    .HasMaxLength(150);

                entity.Property(e => e.Txfantasia)
                    .HasColumnName("TXFANTASIA")
                    .HasMaxLength(100);

                entity.Property(e => e.Txinsest)
                    .HasColumnName("TXINSEST")
                    .HasMaxLength(20);

                entity.Property(e => e.Txinsmun)
                    .HasColumnName("TXINSMUN")
                    .HasMaxLength(20);

                entity.Property(e => e.Txluc)
                    .HasColumnName("TXLUC")
                    .HasColumnType("character varying");

                entity.Property(e => e.Txnopessoa)
                    .HasColumnName("TXNOPESSOA")
                    .HasMaxLength(100);

                entity.Property(e => e.Txpwd)
                    .HasColumnName("TXPWD")
                    .HasMaxLength(100);

                entity.Property(e => e.Txqualif)
                    .HasColumnName("TXQUALIF")
                    .HasMaxLength(5);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
