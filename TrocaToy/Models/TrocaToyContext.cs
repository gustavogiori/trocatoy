using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TrocaToy.Models
{
    public partial class TrocaToyContext : DbContext
    {
        public TrocaToyContext()
        {
        }

        public TrocaToyContext(DbContextOptions<TrocaToyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Anuncio> Anuncio { get; set; }
        public virtual DbSet<Brinquedo> Brinquedo { get; set; }
        public virtual DbSet<Cidade> Cidade { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<ItensProposta> ItensProposta { get; set; }
        public virtual DbSet<Proposta> Proposta { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=TrocaToy;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anuncio>(entity =>
            {
                entity.Property(e => e.DataAnuncio).HasColumnType("datetime");

                entity.HasOne(d => d.IdBrinquedoNavigation)
                    .WithMany(p => p.Anuncio)
                    .HasForeignKey(d => d.IdBrinquedo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Anuncio_Brinquedo");

                entity.HasOne(d => d.IdEnderecoEntregaNavigation)
                    .WithMany(p => p.Anuncio)
                    .HasForeignKey(d => d.IdEnderecoEntrega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Anuncio_Endereco");
            });

            modelBuilder.Entity<Brinquedo>(entity =>
            {
                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Brinquedo)
                   .HasForeignKey(d => d.IdUsuario)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Brinquedo_Usuario");
            });

            modelBuilder.Entity<Cidade>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.Codigo).HasMaxLength(6);
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.Property(e => e.Cep)
                    .HasColumnName("CEP")
                    .HasMaxLength(20);

                entity.Property(e => e.CodCidade).HasMaxLength(6);

                entity.Property(e => e.CodEstado).HasMaxLength(2);

                entity.HasOne(d => d.CodCidadeNavigation)
                    .WithMany(p => p.Endereco)
                    .HasForeignKey(d => d.CodCidade)
                    .HasConstraintName("FK_Endereco_Cidade");

                entity.HasOne(d => d.CodEstadoNavigation)
                    .WithMany(p => p.Endereco)
                    .HasForeignKey(d => d.CodEstado)
                    .HasConstraintName("FK_Endereco_Estado");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Endereco)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Endereco_Usuario");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.Codigo).HasMaxLength(2);
            });

            modelBuilder.Entity<ItensProposta>(entity =>
            {
                entity.HasOne(d => d.IdBrinquedoNavigation)
                    .WithMany(p => p.ItensProposta)
                    .HasForeignKey(d => d.IdBrinquedo)
                    .HasConstraintName("FK_Table_2_Brinquedo");

                entity.HasOne(d => d.IdPropostaNavigation)
                    .WithMany(p => p.ItensProposta)
                    .HasForeignKey(d => d.IdProposta)
                    .HasConstraintName("FK_Table_2_Proposta");
            });

            modelBuilder.Entity<Proposta>(entity =>
            {
                entity.HasOne(d => d.IdUsuarioRequisitadoNavigation)
                    .WithMany(p => p.PropostaIdUsuarioRequisitadoNavigation)
                    .HasForeignKey(d => d.IdUsuarioRequisitado)
                    .HasConstraintName("FK_Proposta_Usuario");

                entity.HasOne(d => d.IdUsuarioSolicitanteNavigation)
                    .WithMany(p => p.PropostaIdUsuarioSolicitanteNavigation)
                    .HasForeignKey(d => d.IdUsuarioSolicitante)
                    .HasConstraintName("FK_Proposta_Usuario1");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasColumnName("CPF");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);


                entity.Property(e => e.Nome).IsRequired();

                entity.Property(e => e.Rg).HasColumnName("RG");

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Telefone).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
