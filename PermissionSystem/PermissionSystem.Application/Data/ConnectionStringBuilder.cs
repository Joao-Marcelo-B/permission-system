using DotNetEnv;

namespace PermissionSystem.Application.Data;

public static class ConnectionStringBuilder
{
    public static string BuildMySqlConnectionString()
    {
        Env.Load("../.env");
        string host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
        string port = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
        string database = Environment.GetEnvironmentVariable("DB_DATABASE") ?? "PermissionSystemDb";
        string user = Environment.GetEnvironmentVariable("DB_USER") ?? "usuario";
        string password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "senha";

        return $"server={host};" +
               $"port={port};" +
               $"database={database};" +
               $"user={user};" +
               $"password={password};" +
               $"CharSet=utf8mb4;" +
               $"SslMode=None;";
    }
}
