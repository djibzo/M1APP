using APITrip.Entities;
using APITrip.Helpers;
using APITrip.models.Offres;
using System.Collections.Generic;

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

        private readonly DataContext _context;

        public OffreService(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<Offre> GetAll()
        {
            try
            {
                return _context.Offres;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching offres", ex);
            }
        }

        public Offre GetById(int id)
        {
            try
            {
                var offre = _context.Offres.Find(id);
                if (offre == null) throw new KeyNotFoundException("Offre not found");
                return offre;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching offre by ID", ex);
            }
        }

        public void Create(OffreCreateRequest model)
        {
            var offre = new Offre
            {
                DescriptionOffre = model.DescriptionOffre,
                PrixOffre = model.PrixOffre,
                DisponibiliteOffre = model.DisponibiliteOffre,
                IdAgence = model.IdAgence
            };
            // Save offre to database
            _context.Offres.Add(offre);
            _context.SaveChanges();
        }

        public void Update(int id, OffreUpdateRequest model)
        {
            try
            {
                var offre = _context.Offres.Find(id);
                if (offre == null) throw new KeyNotFoundException("Offre not found");
                // Map properties from model
                offre.DescriptionOffre = model.DescriptionOffre;
                offre.PrixOffre = model.PrixOffre;
                offre.DisponibiliteOffre = model.DisponibiliteOffre;
                _context.Offres.Update(offre);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating offre", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var offre = _context.Offres.Find(id);
                if (offre == null) throw new KeyNotFoundException("Offre not found");
                _context.Offres.Remove(offre);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting offre", ex);
            }
        }
    }
}
