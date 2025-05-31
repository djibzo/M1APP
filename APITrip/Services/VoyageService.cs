using APITrip.Entities;
using APITrip.models.Voyages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APITrip.Services
{
    public interface IVoyageService
    {
        IEnumerable<Voyage> GetAll();
        Voyage GetById(int id);
        void Create(VoyageCreateRequest model);
        void Update(int id, VoyageUpdateRequest model);
        void Delete(int id);
    }

    public class VoyageService : IVoyageService
    {
        // Liste statique simulant une base de données en mémoire
        private static List<Voyage> _voyages = new List<Voyage>();
        private static int _nextId = 1;

        public IEnumerable<Voyage> GetAll()
        {
            return _voyages;
        }

        public Voyage GetById(int id)
        {
            return _voyages.FirstOrDefault(v => v.IdVoyage == id);
        }

        public void Create(VoyageCreateRequest model)
        {
            var voyage = new Voyage
            {
                IdVoyage = _nextId++,
                Destination = model.Destination,
                DateDepart = model.DateDepart,
                DateArrivee = model.DateArrivee,
                Prix = model.Prix
            };

            _voyages.Add(voyage);
        }

        public void Update(int id, VoyageUpdateRequest model)
        {
            var voyage = GetById(id);
            if (voyage == null)
                throw new KeyNotFoundException($"Le voyage avec l'id {id} n'a pas été trouvé.");

            voyage.Destination = model.Destination;
            voyage.DateDepart = model.DateDepart;
            voyage.DateArrivee = model.DateArrivee;
            voyage.Prix = model.Prix;
        }

        public void Delete(int id)
        {
            var voyage = GetById(id);
            if (voyage == null)
                throw new KeyNotFoundException($"Le voyage avec l'id {id} n'a pas été trouvé.");

            _voyages.Remove(voyage);
        }
    }
}
