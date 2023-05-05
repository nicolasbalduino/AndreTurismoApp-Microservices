using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjRabbitMQ.Producer.Data
{
    public class ProjRabbitMQProducerContext : DbContext
    {
        public ProjRabbitMQProducerContext (DbContextOptions<ProjRabbitMQProducerContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.City> City { get; set; } = default!;
    }
}
