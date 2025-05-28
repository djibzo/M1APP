using APITrip.Entities;
using APITrip.models.Clients;
using System.Collections.Generic;

namespace APITrip.Services
{
    public interface IClientService
    {
        IEnumerable<Client> GetAll();
        Client GetById(int id);
        void Create(ClientCreateRequest model);
        void Update(int id, ClientUpdateRequest model);
        void Delete(int id);
    }
    public class ClientService : IClientService
    {
        public IEnumerable<Client> GetAll()
        {
            throw new NotImplementedException();
        }
        public Client GetById(int id)
        {
            throw new NotImplementedException();
        }
        public void Create(ClientCreateRequest model)
        {
            throw new NotImplementedException();
        }
        public void Update(int id, ClientUpdateRequest model)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
