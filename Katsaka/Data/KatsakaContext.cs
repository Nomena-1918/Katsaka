using System;
using System.Collections.Generic;
using Katsaka.Models;
using Microsoft.EntityFrameworkCore;

namespace Katsaka.Data;

public partial class KatsakaContext : DbContext
{
    public KatsakaContext()
    {
    }

    public KatsakaContext(DbContextOptions<KatsakaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Champ> Champs { get; set; }

    public virtual DbSet<Parametrecroissance> Parametrecroissances { get; set; }

    public virtual DbSet<Parametrefrequence> Parametrefrequences { get; set; }

    public virtual DbSet<Parcelle> Parcelles { get; set; }

    public virtual DbSet<Recolte> Recoltes { get; set; }

    public virtual DbSet<Responsable> Responsables { get; set; }

    public virtual DbSet<Suivimai> Suivimais { get; set; }

    public virtual DbSet<VDerniersuiviAvantRecolte> VDerniersuiviAvantRecoltes { get; set; }

    public virtual DbSet<VListDateDernierSuivi> VListDateDernierSuivis { get; set; }

    public virtual DbSet<VListDernierSuivi> VListDernierSuivis { get; set; }

    public virtual DbSet<VSuiviRecolte> VSuiviRecoltes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:KatsakaContext");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Champ>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("champ_pkey");

            entity.ToTable("champ");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Parametrecroissance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("parametrecroissance_pkey");

            entity.ToTable("parametrecroissance");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
            entity.Property(e => e.Valeur)
                .HasPrecision(10, 2)
                .HasColumnName("valeur");
        });

        modelBuilder.Entity<Parametrefrequence>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("parametrefrequence_pkey");

            entity.ToTable("parametrefrequence");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
            entity.Property(e => e.Valeur).HasColumnName("valeur");
        });

        modelBuilder.Entity<Parcelle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("parcelle_pkey");

            entity.ToTable("parcelle");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idchamp).HasColumnName("idchamp");
            entity.Property(e => e.Idresponsable).HasColumnName("idresponsable");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
            entity.Property(e => e.Remarque)
                .HasMaxLength(200)
                .HasColumnName("remarque");

            entity.HasOne(d => d.IdchampNavigation).WithMany(p => p.Parcelles)
                .HasForeignKey(d => d.Idchamp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("parcelle_idchamp_fkey");

            entity.HasOne(d => d.IdresponsableNavigation).WithMany(p => p.Parcelles)
                .HasForeignKey(d => d.Idresponsable)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("parcelle_idresponsable_fkey");
        });

        modelBuilder.Entity<Recolte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recolte_pkey");

            entity.ToTable("recolte");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Daterecolte)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("daterecolte");
            entity.Property(e => e.Idparcelle).HasColumnName("idparcelle");
            entity.Property(e => e.Longueurmoyenepis)
                .HasPrecision(10, 2)
                .HasColumnName("longueurmoyenepis");
            entity.Property(e => e.Nbrtotalepis).HasColumnName("nbrtotalepis");
            entity.Property(e => e.Poidstotalgraine)
                .HasPrecision(10, 2)
                .HasColumnName("poidstotalgraine");

            entity.HasOne(d => d.IdparcelleNavigation).WithMany(p => p.Recoltes)
                .HasForeignKey(d => d.Idparcelle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recolte_idparcelle_fkey");
        });

        modelBuilder.Entity<Responsable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("responsable_pkey");

            entity.ToTable("responsable");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Suivimai>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("suivimais_pkey");

            entity.ToTable("suivimais");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Couleurmoyenpousse).HasColumnName("couleurmoyenpousse");
            entity.Property(e => e.Datesuivi)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datesuivi");
            entity.Property(e => e.Idparcelle).HasColumnName("idparcelle");
            entity.Property(e => e.Longueurmoyenepis)
                .HasPrecision(10, 2)
                .HasColumnName("longueurmoyenepis");
            entity.Property(e => e.Longueurmoyenpousse)
                .HasPrecision(10, 2)
                .HasColumnName("longueurmoyenpousse");
            entity.Property(e => e.Nbrepismoyenparpousse).HasColumnName("nbrepismoyenparpousse");
            entity.Property(e => e.Nbrpousse).HasColumnName("nbrpousse");

            entity.HasOne(d => d.IdparcelleNavigation).WithMany(p => p.Suivimais)
                .HasForeignKey(d => d.Idparcelle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("suivimais_idparcelle_fkey");
        });

        modelBuilder.Entity<VDerniersuiviAvantRecolte>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_derniersuivi_avant_recolte");

            entity.Property(e => e.Couleurmoyenpousse).HasColumnName("couleurmoyenpousse");
            entity.Property(e => e.Daterecolte)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("daterecolte");
            entity.Property(e => e.Datesuivi)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datesuivi");
            entity.Property(e => e.Idparcelle).HasColumnName("idparcelle");
            entity.Property(e => e.Idrecolte).HasColumnName("idrecolte");
            entity.Property(e => e.Idsuivi).HasColumnName("idsuivi");
            entity.Property(e => e.LongueurmoyenepisRecolte)
                .HasPrecision(10, 2)
                .HasColumnName("longueurmoyenepis_recolte");
            entity.Property(e => e.LongueurmoyenepisSuivi)
                .HasPrecision(10, 2)
                .HasColumnName("longueurmoyenepis_suivi");
            entity.Property(e => e.Longueurmoyenpousse)
                .HasPrecision(10, 2)
                .HasColumnName("longueurmoyenpousse");
            entity.Property(e => e.Nbrepistotalsuivi).HasColumnName("nbrepistotalsuivi");
            entity.Property(e => e.Nbrtotalepis).HasColumnName("nbrtotalepis");
            entity.Property(e => e.Poidstotalgraine)
                .HasPrecision(10, 2)
                .HasColumnName("poidstotalgraine");
        });

        modelBuilder.Entity<VListDateDernierSuivi>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_list_date_dernier_suivi");

            entity.Property(e => e.Idparcelle).HasColumnName("idparcelle");
            entity.Property(e => e.Maxdatesuivi)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("maxdatesuivi");
        });

        modelBuilder.Entity<VListDernierSuivi>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_list_dernier_suivi");

            entity.Property(e => e.Couleurmoyenpousse).HasColumnName("couleurmoyenpousse");
            entity.Property(e => e.Datesuivi)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datesuivi");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idparcelle).HasColumnName("idparcelle");
            entity.Property(e => e.Longueurmoyenepis)
                .HasPrecision(10, 2)
                .HasColumnName("longueurmoyenepis");
            entity.Property(e => e.Longueurmoyenpousse)
                .HasPrecision(10, 2)
                .HasColumnName("longueurmoyenpousse");
            entity.Property(e => e.Nbrepismoyenparpousse).HasColumnName("nbrepismoyenparpousse");
            entity.Property(e => e.Nbrpousse).HasColumnName("nbrpousse");
        });

        modelBuilder.Entity<VSuiviRecolte>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_suivi_recolte");

            entity.Property(e => e.Couleurmoyenpousse).HasColumnName("couleurmoyenpousse");
            entity.Property(e => e.Datesuivi)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datesuivi");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idparcelle).HasColumnName("idparcelle");
            entity.Property(e => e.Longueurmoyenepis)
                .HasPrecision(10, 2)
                .HasColumnName("longueurmoyenepis");
            entity.Property(e => e.Longueurmoyenpousse)
                .HasPrecision(10, 2)
                .HasColumnName("longueurmoyenpousse");
            entity.Property(e => e.Nbrepistotalsuivi).HasColumnName("nbrepistotalsuivi");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
