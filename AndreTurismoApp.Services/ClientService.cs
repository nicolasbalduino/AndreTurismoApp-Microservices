using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AndreTurismoApp.Models;
using AndreTurismoApp.Repositories;

namespace AndreTurismoApp.Services
{
    public class ClientService
    {
        IClientRepository clientRepository;

        public ClientService()
        {
            clientRepository = new ClientRepository();
        }

        public int Insert(Client client)
        {
            return clientRepository.Insert(client);
        }

        public List<Client> FindAll()
        {
            return clientRepository.FindAll();
        }

        public List<Client> FindByName(string name)
        {
            return clientRepository.FindByName(name);
        }

        public Client FindById(int id)
        {
            return clientRepository.FindById(id);
        }

        public int Update(int id, Client newClient)
        {
            return clientRepository.Update(id, newClient);
        }

        public int Delete(int id)
        {
            return clientRepository.Delete(id);
        }
    }
}
