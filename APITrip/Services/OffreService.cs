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
                DescriptionOffre = model.DescriptionOffre,
                PrixOffre = model.PrixOffre,
                DisponibiliteOffre = model.DisponibiliteOffre,
                IdAgence = model.IdAgence
            };

            _offres.Add(offre);
        }

        public void Update(int id, OffreUpdateRequest model)
        {
            var offre = GetById(id);
            if (offre == null)
                throw new KeyNotFoundException($"Offre avec l'id {id} non trouvé.");

            offre.DescriptionOffre = model.DescriptionOffre;
            offre.PrixOffre = model.PrixOffre;
            offre.DisponibiliteOffre = model.DisponibiliteOffre;
            offre.IdAgence = model.IdAgence;
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
