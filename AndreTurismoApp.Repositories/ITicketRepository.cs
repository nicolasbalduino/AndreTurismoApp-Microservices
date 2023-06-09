﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Repositories
{
    public interface ITicketRepository
    {
        int Insert(Ticket ticket);
        List<Ticket> FindAll();
        Ticket FindById(int id);
        int Update(Ticket ticket);
        int Delete(int id);
    }
}
