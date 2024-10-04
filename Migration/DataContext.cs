using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Apsen;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Email> Emails { get; set; }

    public virtual DbSet<Endereco> Enderecos { get; set; }

    public virtual DbSet<Telefone> Telefones { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.CNPJ);

            entity.ToTable("CLIENTE");

            entity.Property(e => e.CNPJ)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("CNPJ");

            entity.Property(e => e.FlagStatusAtivo).HasColumnName("FLAG_STATUS_ATIVO");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOME");
            entity.Property(e => e.Sobrenome)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("SOBRENOME");
        });

        modelBuilder.Entity<Email>(entity =>
        {
            entity.ToTable("EMAIL");

            entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();

            entity.Property(e => e.CnpjCliente)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("CNPJ_CLIENTE");
            entity.Property(e => e.EnderecoEmail)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("ENDERECO_EMAIL");

            entity.HasOne(d => d.CnpjClienteNavigation).WithMany(p => p.Emails)
                .HasForeignKey(d => d.CnpjCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMAIL_CLIENTE");
        });

        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.ToTable("ENDERECO");

            entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();
            entity.Property(e => e.Bairro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BAIRRO");
            entity.Property(e => e.Cep)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("CEP");
            entity.Property(e => e.Complemento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COMPLEMENTO");
            entity.Property(e => e.CpnjCliente)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("CPNJ_CLIENTE");
            entity.Property(e => e.Logradouro)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("ENDERECO");
            entity.Property(e => e.Numero)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NUMERO");

            entity.HasOne(d => d.CpnjClienteNavigation).WithMany(p => p.Enderecos)
                .HasForeignKey(d => d.CpnjCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ENDERECO_CLIENTE");
        });

        modelBuilder.Entity<Telefone>(entity =>
        {
            entity.ToTable("TELEFONE");

            entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();
            entity.Property(e => e.CnpjCliente)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("CNPJ_CLIENTE");
            entity.Property(e => e.Ddd)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("DDD");
            entity.Property(e => e.Numero)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("NUMERO");

            entity.HasOne(d => d.CnpjClienteNavigation).WithMany(p => p.Telefones)
                .HasForeignKey(d => d.CnpjCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TELEFONE_CLIENTE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
