using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace MyMailBox.Models
{
  public class MailBoxContextFactory : IDesignTimeDbContextFactory<MailBoxContext>
  {
    //public static string ConnectionString { get; } = @"Server=(localdb)\mssqllocaldb;Database=Mailboxes;Trusted_Connection=True;ConnectRetryCount=0";

    public static string? ConnectionString
    {
      get;
      set;
    }

    public MailBoxContext CreateDbContext(string[] args)
    {
      if(ConnectionString==null)
        throw new NullReferenceException(nameof(ConnectionString));
      var migrationsAssembly = typeof(MailBoxContextFactory).GetTypeInfo().Assembly.GetName().Name;

      var optionsBuilder = new DbContextOptionsBuilder<MailBoxContext>();
      optionsBuilder.UseSqlServer(ConnectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly));

      return new MailBoxContext(optionsBuilder.Options);
    }
  }
}
