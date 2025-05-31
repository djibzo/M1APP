using APITrip.Entities;
using APITrip.models.Clients;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private static List<Client> _clients = new List<Client>();
        private static int _nextId = 1;

        public IEnumerable<Client> GetAll()
        {
            return _clients;
        }

        public Client GetById(int id)
        {
            return _clients.FirstOrDefault(c => c.Id == id);
        }

        public void Create(ClientCreateRequest model)
        {
            var client = new Client
            {
                Id = _nextId++,
                // Copier ici les propriétés du modèle vers l'entité Client
                // Exemple:
                // Nom = model.Nom,
                // Prenom = model.Prenom,
                // Email = model.Email,
                // Telephone = model.Telephone,
            };

            _clients.Add(client);
        }

        public void Update(int id, ClientUpdateRequest model)
        {
            var client = GetById(id);
            if (client == null)
                throw new KeyNotFoundException($"Client avec l'id {id} non trouvé.");

            // Mettre à jour les propriétés
            // Exemple:
            // client.Nom = model.Nom;
            // client.Prenom = model.Prenom;
            // client.Email = model.Email;
            // client.Telephone = model.Telephone;
        }

        public void Delete(int id)
        {
            var client = GetById(id);
            if (client == null)
                throw new KeyNotFoundException($"Client avec l'id {id} non trouvé.");

            _clients.Remove(client);
        }
    }
}
