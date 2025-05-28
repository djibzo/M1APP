using APITrip.Entities;
using APITrip.models.Clients;
using System.Collections.Generic;

namespace APITrip.Services
{
    public interface IClientService
    {
        IEnumerable<Client> GetAll();
        Client GetById(int id);
        void Create(CreateRequest model);
        void Update(int id, UpdateRequest model);
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
        public void Create(CreateRequest model)
        {
            throw new NotImplementedException();
        }
        public void Update(int id, UpdateRequest model)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
