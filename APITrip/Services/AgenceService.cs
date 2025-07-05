using APITrip.Entities;
using APITrip.models.Agences;
using APITrip.Helpers;
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
        private readonly DataContext _context;

        public AgenceService(DataContext context) // Corrected constructor name
        {
            _context = context;
        }

        public IEnumerable<Agence> GetAll()
        {
            try
            {
                return _context.Agences;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching agences", ex);
            }
        }
        public Agence GetById(int id)
        {
            try
            {
                var agence = _context.Agences.Find(id);
                if (agence == null) throw new KeyNotFoundException("Agence not found");
                return agence;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching agence by ID", ex);
            }
        }
        public void Create(AgenceCreateRequest model)
        {
            var agence = new Agence
            {
                AdresseAgence = model.AdresseAgence,
                Longitude = model.Longitude,
                Latitude = model.Latitude,
                NineaGestionnaire = model.NineaGestionnaire,
                RccmGestionnaire = model.RccmGestionnaire,
                IdGestionnaire = model.IdGestionnaire.ToString()
            };
            // Save agence to database
            _context.Agences.Add(agence);
            _context.SaveChanges();
        }
        public void Update(int id, AgenceUpdateRequest model)
        {
            try
            {
                var agence = _context.Agences.Find(id);
                if (agence == null) throw new KeyNotFoundException("Agence not found");
                // Map properties from model
                agence.AdresseAgence = model.AdresseAgence;
                agence.Longitude = model.Longitude;
                agence.Latitude = model.Latitude;
                _context.Agences.Update(agence);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating agence", ex);
            }
        }
        public void Delete(int id)
        {
            try
            {
                var agence = _context.Agences.Find(id);
                if (agence == null) throw new KeyNotFoundException("Agence not found");
                _context.Agences.Remove(agence);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting agence", ex);
            }
        }
    }
}