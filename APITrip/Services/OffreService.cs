using APITrip.Entities;
using APITrip.models.Offres;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APITrip.Services
{
    public interface IOffreService
    {
        IEnumerable<Offre> GetAll();
        Offre GetById(int id);
        void Create(OffreCreateRequest model);
        void Update(int id, OffreUpdateRequest model);
        void Delete(int id);
    }

    public class OffreService : IOffreService
    {
        private static List<Offre> _offres = new List<Offre>();
        private static int _nextId = 1;

        public IEnumerable<Offre> GetAll()
        {
            return _offres;
        }

        public Offre GetById(int id)
        {
            return _offres.FirstOrDefault(o => o.IdOffre == id);
        }

        public void Create(OffreCreateRequest model)
        {
            var offre = new Offre
            {
                IdOffre = _nextId++,
                // Copier ici les propriétés de model vers offre
                // Par exemple :
                // Titre = model.Titre,
                // Description = model.Description,
                // Prix = model.Prix,
                // DateDebut = model.DateDebut,
                // DateFin = model.DateFin,
            };

            _offres.Add(offre);
        }

        public void Update(int id, OffreUpdateRequest model)
        {
            var offre = GetById(id);
            if (offre == null)
                throw new KeyNotFoundException($"Offre avec l'id {id} non trouvé.");

            // Mettre à jour les propriétés ici
            // Par exemple :
            // offre.Titre = model.Titre;
            // offre.Description = model.Description;
            // offre.Prix = model.Prix;
            // offre.DateDebut = model.DateDebut;
            // offre.DateFin = model.DateFin;
        }

        public void Delete(int id)
        {
            var offre = GetById(id);
            if (offre == null)
                throw new KeyNotFoundException($"Offre avec l'id {id} non trouvé.");

            _offres.Remove(offre);
        }
    }
}
