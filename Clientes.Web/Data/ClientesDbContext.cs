using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Clientes.Web.Models;

namespace Clientes.Web.Data
{
    public class ClientesDbContext : DbContext
    {
        public ClientesDbContext (DbContextOptions<ClientesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Clientes.Web.Models.Cliente> Cliente { get; set; } = default!;
    }
}
