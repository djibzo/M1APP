using APITrip.Entities;
using APITrip.models.Agences;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APITrip.Services
{
    public interface IAgenceService
    {
        IEnumerable<Agence> GetAll();
        Agence GetById(int id);
        void Create(AgenceCreateRequest model);
        void Update(int id, AgenceUpdateRequest model);
        void Delete(int id);
    }

    public class AgenceService : IAgenceService
    {
        private static List<Agence> _agences = new List<Agence>();
        private static int _nextId = 1;

        public IEnumerable<Agence> GetAll()
        {
            return _agences;
        }

        public Agence GetById(int id)
        {
            return _agences.FirstOrDefault(a => a.IdAgence == id);
        }

        public void Create(AgenceCreateRequest model)
        {
            var agence = new Agence
            {
                IdAgence = _nextId++,
                // Copier les propriétés depuis model vers agence
                // Exemple :
                // Nom = model.Nom,
                // Adresse = model.Adresse,
                // Téléphone = model.Telephone,
                // etc.
            };

            _agences.Add(agence);
        }

        public void Update(int id, AgenceUpdateRequest model)
        {
            var agence = GetById(id);
            if (agence == null)
                throw new KeyNotFoundException($"L'agence avec l'id {id} n'a pas été trouvée.");

            // Mettre à jour les propriétés
            // Exemple :
            // agence.Nom = model.Nom;
            // agence.Adresse = model.Adresse;
            // agence.Telephone = model.Telephone;
        }

        public void Delete(int id)
        {
            var agence = GetById(id);
            if (agence == null)
                throw new KeyNotFoundException($"L'agence avec l'id {id} n'a pas été trouvée.");

            _agences.Remove(agence);
        }
    }
}
