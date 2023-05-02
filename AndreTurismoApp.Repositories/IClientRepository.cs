using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Repositories
{
    public interface IClientRepository
    {
        int Insert(Client client);
        List<Client> FindAll();
        List<Client> FindByName(string name);
        Client FindById(int id);
        int Update(int id, Client newClient);
        int Delete(int id);
    }
}
