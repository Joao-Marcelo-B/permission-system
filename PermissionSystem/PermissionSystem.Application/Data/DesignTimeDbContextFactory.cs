using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PermissionSystem.Application.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PermissionSystemContext>
{
    public PermissionSystemContext CreateDbContext(string[] args)
    {
        Env.Load();

        var connectionString = ConnectionStringBuilder.BuildMySqlConnectionString();

        var optionsBuilder = new DbContextOptionsBuilder<PermissionSystemContext>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        return new PermissionSystemContext(optionsBuilder.Options);
    }
}