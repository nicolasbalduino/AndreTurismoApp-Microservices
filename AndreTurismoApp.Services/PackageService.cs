using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;
using AndreTurismoApp.Repositories;

namespace AndreTurismoApp.Services
{
    public class PackageService
    {
        IPackageRepository packageRepository;

        public PackageService()
        {
            packageRepository = new PackageRepository();
        }

        public int Insert(Package package)
        {
            return packageRepository.Insert(package); 
        }

        public List<Package> FindAll()
        {
            return packageRepository.FindAll();
        }

        public Package FindById(int id)
        {
            return packageRepository.FindById(id);
        }

        public int Update(Package package)
        {
            return packageRepository.Update(package);
        }

        public int Delete(int id)
        {
            return packageRepository.Delete(id);
        }
    }
}
