using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;
using AndreTurismoApp.Repositories;

namespace AndreTurismoApp.Services
{
    public class TicketService
    {
        ITicketRepository ticketRepository;

        public TicketService()
        {
            ticketRepository = new TicketRepository();
        }

        public int Insert(Ticket ticket)
        {
            return ticketRepository.Insert(ticket);
        }

        public List<Ticket> FindAll()
        {
            return ticketRepository.FindAll();
        }

        public Ticket FindById(int id)
        {
            return ticketRepository.FindById(id);
        }

        public int Update(Ticket ticket)
        {
            return ticketRepository.Update(ticket);
        }

        public int Delete(int id)
        {
            return ticketRepository.Delete(id);
        }
    }
}
