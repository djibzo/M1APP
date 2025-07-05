using APITrip.Entities;
using APITrip.models.Chauffeurs;
using System.Collections.Generic;
using APITrip.Helpers;

namespace APITrip.Services
{
    public interface IChauffeurService
    {
        IEnumerable<Chauffeur> GetAll();
        Chauffeur GetById(int id);
        void Create(ChauffeurCreateRequest model);
        void Update(int id, ChauffeurUpdateRequest model);
        void Delete(int id);
    }
    public class ChauffeurService : IChauffeurService
    {
        private readonly DataContext _context;

        public ChauffeurService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Chauffeur> GetAll()
        {
            return _context.Chauffeurs;
        }

        public Chauffeur GetById(int id)
        {
            var chauffeur = _context.Chauffeurs.Find(id);
            if (chauffeur == null) throw new KeyNotFoundException("Chauffeur not found");
            return chauffeur;
        }

        public void Create(ChauffeurCreateRequest model)
        {
            var chauffeur = new Chauffeur { /* Map properties from model */ };
            _context.Chauffeurs.Add(chauffeur);
            _context.SaveChanges();
        }

        public void Update(int id, ChauffeurUpdateRequest model)
        {
            var chauffeur = GetById(id);
            // Update properties
            _context.Chauffeurs.Update(chauffeur);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var chauffeur = GetById(id);
            _context.Chauffeurs.Remove(chauffeur);
            _context.SaveChanges();
        }
    }
}
