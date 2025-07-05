using APITrip.Entities;
using APITrip.models.Voyages;
using System.Collections.Generic;
using APITrip.Helpers;

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
        private readonly DataContext _context;

        public VoyageService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Voyage> GetAll()
        {
            return _context.Voyages;
        }

        public Voyage GetById(int id)
        {
            var voyage = _context.Voyages.Find(id);
            if (voyage == null) throw new KeyNotFoundException("Voyage not found");
            return voyage;
        }

        public void Create(VoyageCreateRequest model)
        {
            var voyage = new Voyage
            {
                Destination = model.Destination,
                DateDepart = model.DateDepart,
                DateArrivee = model.DateArrivee,
                Prix = model.Prix
            };
            // Save voyage to database
            _context.Voyages.Add(voyage);
            _context.SaveChanges();
        }

        public void Update(int id, VoyageUpdateRequest model)
        {
            var voyage = GetById(id);
            // Update properties
            _context.Voyages.Update(voyage);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var voyage = GetById(id);
            _context.Voyages.Remove(voyage);
            _context.SaveChanges();
        }
    }
}
