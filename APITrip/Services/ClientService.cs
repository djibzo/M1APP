using APITrip.Entities;
using APITrip.models.Clients;
using System.Collections.Generic;
using APITrip.Helpers;

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
        private readonly DataContext _context;

        public ClientService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Client> GetAll()
        {
            return _context.Clients;
        }

        public Client GetById(int id)
        {
            var client = _context.Clients.Find(id);
            if (client == null) throw new KeyNotFoundException("Client not found");
            return client;
        }

        public void Create(ClientCreateRequest model)
        {
            var client = new Client
            {
                CniClient = model.CniClient
            };
            // Save client to database
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public void Update(int id, ClientUpdateRequest model)
        {
            var client = GetById(id);
            // Update properties
            _context.Clients.Update(client);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var client = GetById(id);
            _context.Clients.Remove(client);
            _context.SaveChanges();
        }
    }
}
