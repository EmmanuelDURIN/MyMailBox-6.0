﻿using Microsoft.EntityFrameworkCore;

namespace MyMailBox.Models
{
  public class MailBoxContext : DbContext
  {
    public MailBoxContext(DbContextOptions<MailBoxContext> options)
            : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
    public DbSet<MailBox> MailBoxes => Set<MailBox>();
  }

}
