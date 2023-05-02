using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;
using AndreTurismoApp.Models.DTO;
using AndreTurismoApp.Repositories;
using Newtonsoft.Json;

namespace AndreTurismoApp.Services
{
    public class AddressService
    {
        //static readonly HttpClient cityClient = new HttpClient();
        static readonly HttpClient addressClient = new HttpClient();
        private IAddressRepository addressRepository;

        public AddressService()
        {
            addressRepository = new AddressRepository();
        }

        public async Task<Address> Insert(Address address) 
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(address), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await AddressService.addressClient.PostAsync("https://localhost:5001/api/Addresses", content);
                response.EnsureSuccessStatusCode();
                string addressResp = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Address>(addressResp);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
            //return addressRepository.Insert(address);
        }

        public async Task<List<Address>> FindAll()
        {
            try
            {
                HttpResponseMessage response = await AddressService.addressClient.GetAsync("https://localhost:5001/api/Addresses");
                response.EnsureSuccessStatusCode();
                string address = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Address>>(address);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
            //return addressRepository.FindAll();
        }

        public async Task<Address> FindById(int id)
        {
            try
            {
                HttpResponseMessage response = await AddressService.addressClient.GetAsync("https://localhost:5001/api/Addresses/" + id);
                response.EnsureSuccessStatusCode();
                string address = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Address>(address);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
            //return addressRepository.FindById(id);
        }

        public int Update(int id, Address newAddress) 
        {
            return addressRepository.Update(id, newAddress);
        }

        public int Delete(int id) 
        {
            return addressRepository.Delete(id);
        }
    }
}
