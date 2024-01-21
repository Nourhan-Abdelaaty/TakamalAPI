using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context;
    public class DataContext  : DbContext
    {
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ClientCalls> ClientCalls { get; set; }
}

