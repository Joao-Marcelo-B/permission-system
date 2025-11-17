using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using PermissionSystem.Application.Data.Entities;

namespace PermissionSystem.Application.Data;

public class PermissionSystemContext : DbContext
{
    public PermissionSystemContext(DbContextOptions<PermissionSystemContext> options) : base(options)
    { }

    public DbSet<SystemEntity> Systems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<PermissionGroup> PermissionGroups { get; set; }
    public DbSet<GroupUser> GroupUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        Env.Load();

        var connectionString = ConnectionStringBuilder.BuildMySqlConnectionString();

        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Nome da tabela System → evitar conflito com palavra reservada
        modelBuilder.Entity<SystemEntity>().ToTable("System");

        // Relacionamentos 1:N
        modelBuilder.Entity<SystemEntity>()
            .HasMany(s => s.Users)
            .WithOne(u => u.System)
            .HasForeignKey(u => u.SystemId);

        modelBuilder.Entity<SystemEntity>()
            .HasMany(s => s.Groups)
            .WithOne(g => g.System)
            .HasForeignKey(g => g.SystemId);

        // N:N via tabelas associativas
        modelBuilder.Entity<GroupUser>()
            .HasOne(gu => gu.User)
            .WithMany(u => u.GroupUsers)
            .HasForeignKey(gu => gu.UserId);

        modelBuilder.Entity<GroupUser>()
            .HasOne(gu => gu.Group)
            .WithMany(g => g.GroupUsers)
            .HasForeignKey(gu => gu.GroupId);

        modelBuilder.Entity<PermissionGroup>()
            .HasOne(pg => pg.Permission)
            .WithMany(p => p.PermissionGroups)
            .HasForeignKey(pg => pg.PermissionId);

        modelBuilder.Entity<PermissionGroup>()
            .HasOne(pg => pg.Group)
            .WithMany(g => g.PermissionGroups)
            .HasForeignKey(pg => pg.GroupId);
    }
}
