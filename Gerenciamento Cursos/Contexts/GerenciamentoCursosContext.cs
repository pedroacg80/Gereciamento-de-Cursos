using System;
using System.Collections.Generic;
using Gerenciamento_Cursos.Domains;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_Cursos.Contexts;

public partial class GerenciamentoCursosContext : DbContext
{
    public GerenciamentoCursosContext()
    {
    }

    public GerenciamentoCursosContext(DbContextOptions<GerenciamentoCursosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aluno> Aluno { get; set; }

    public virtual DbSet<AreaEspecializacao> AreaEspecializacao { get; set; }

    public virtual DbSet<Curso> Curso { get; set; }

    public virtual DbSet<Instrutor> Instrutor { get; set; }

    public virtual DbSet<Matricula> Matricula { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=GerenciamentoCursos;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.HasKey(e => e.AlunoId).HasName("PK__Aluno__C1967D8F759CC941");

            entity.HasIndex(e => e.Email, "UQ__Aluno__A9D10534EBEEC859").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);
        });

        modelBuilder.Entity<AreaEspecializacao>(entity =>
        {
            entity.HasKey(e => e.AreaID).HasName("PK__AreaEspe__70B82028194691E2");

            entity.Property(e => e.Nome).HasMaxLength(40);
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.CursoID).HasName("PK__Curso__7E023A3703E2E4BF");

            entity.HasIndex(e => e.Nome, "UQ__Curso__7D8FE3B2D604087A").IsUnique();

            entity.Property(e => e.Nome).HasMaxLength(80);

            entity.HasOne(d => d.Instrutor).WithMany(p => p.Curso)
                .HasForeignKey(d => d.InstrutorID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Curso_Instrutor_InstrutorId");
        });

        modelBuilder.Entity<Instrutor>(entity =>
        {
            entity.HasKey(e => e.InstrutorID).HasName("PK__Instruto__096B84F4DB040981");

            entity.HasIndex(e => e.Email, "UQ__Instruto__A9D10534995C9E9E").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);

            entity.HasOne(d => d.AreaEspecializacao).WithMany(p => p.Instrutor)
                .HasForeignKey(d => d.AreaEspecializacaoID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Instrtutor_AreaEspecializacao_AreaEspecializacaoId");

            entity.HasMany(d => d.CursoNavigation).WithMany(p => p.Intrutor)
                .UsingEntity<Dictionary<string, object>>(
                    "InstrutorCurso",
                    r => r.HasOne<Curso>().WithMany()
                        .HasForeignKey("CursoID")
                        .HasConstraintName("FK_IntrutorCurso_CursoId"),
                    l => l.HasOne<Instrutor>().WithMany()
                        .HasForeignKey("IntrutorID")
                        .HasConstraintName("FK_IntrutorCurso_InstrutorID"),
                    j =>
                    {
                        j.HasKey("IntrutorID", "CursoID").HasName("PK_IntrutorCurso_InstrutorID_CursoID");
                    });
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => new { e.CursoID, e.AlunoID }).HasName("PK_CursoAluno_CursoID_AlunoID");

            entity.Property(e => e.StatusMatricula).HasDefaultValue(true);

            entity.HasOne(d => d.Aluno).WithMany(p => p.Matricula)
                .HasForeignKey(d => d.AlunoID)
                .HasConstraintName("FK_CursoAluno_AlunoID");

            entity.HasOne(d => d.Curso).WithMany(p => p.Matricula)
                .HasForeignKey(d => d.CursoID)
                .HasConstraintName("FK_CursoAluno_CursoID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
