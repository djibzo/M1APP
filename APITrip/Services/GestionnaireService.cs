using APITrip.Entities;
using APITrip.models.Gestionnaires;
using System.Collections.Generic;
using APITrip.Helpers;

namespace APITrip.Services
{
    public interface IGestionnaireService
    {
        IEnumerable<Gestionnaire> GetAll();
        Gestionnaire GetById(int id);
        void Create(GestionnaireCreateRequest model);
        void Update(int id, GestionnaireUpdateRequest model);
        void Delete(int id);
    }
    public class GestionnaireService : IGestionnaireService
    {
        private readonly DataContext _context;

        public GestionnaireService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Gestionnaire> GetAll()
        {
            return _context.Gestionnaires;
        }

        public Gestionnaire GetById(int id)
        {
            var gestionnaire = _context.Gestionnaires.Find(id);
            if (gestionnaire == null) throw new KeyNotFoundException("Gestionnaire not found");
            return gestionnaire;
        }

        public void Create(GestionnaireCreateRequest model)
        {
            var gestionnaire = new Gestionnaire
            {
                CNIGestionnaire = model.CNIGestionnaire
            };
            // Save gestionnaire to database
            _context.Gestionnaires.Add(gestionnaire);
            _context.SaveChanges();
        }

        public void Update(int id, GestionnaireUpdateRequest model)
        {
            var gestionnaire = GetById(id);
            // Update properties
            _context.Gestionnaires.Update(gestionnaire);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var gestionnaire = GetById(id);
            _context.Gestionnaires.Remove(gestionnaire);
            _context.SaveChanges();
        }
    }
}
