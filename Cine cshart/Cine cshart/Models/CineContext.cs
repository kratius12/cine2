using Microsoft.EntityFrameworkCore;

namespace Cine_cshart.Models;

public partial class CineContext : DbContext
{
    public CineContext()
    {
    }

    public CineContext(DbContextOptions<CineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    public virtual DbSet<Ranking> Rankings { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=cine;integrated security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83FFF3FA245");

            entity.ToTable("categoria");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pelicula__3213E83F51DB338D");

            entity.ToTable("peliculas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Peliculas)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_idCategoria");
        });

        modelBuilder.Entity<Ranking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ranking__3213E83F75D49534");

            entity.ToTable("ranking");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdPel).HasColumnName("idPel");
            entity.Property(e => e.Puntuacion).HasColumnName("puntuacion");

            entity.HasOne(d => d.IdPelNavigation).WithMany(p => p.Rankings)
                .HasForeignKey(d => d.IdPel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idPel");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuario__3213E83F668591BC");

            entity.ToTable("usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Confircontra)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("confircontra");
            entity.Property(e => e.Contra)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contra");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
