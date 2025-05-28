using APITrip.Entities;
using APITrip.models.Voyages;
using System.Collections.Generic;

namespace APITrip.Services
{
    public class VoyageService
    {
        // Suppression de la liste en mémoire, passage à une logique orientée base de données comme FlotteService
        public IEnumerable<Voyage> GetAll()
        {
            // À remplacer par l'accès à la base de données (exemple : return _context.Voyages;)
            throw new NotImplementedException();
        }
        public Voyage GetById(int id)
        {
            // À remplacer par l'accès à la base de données (exemple : return _context.Voyages.Find(id);)
            throw new NotImplementedException();
        }
        public void Create(CreateRequest model)
        {
            // À remplacer par la logique de création en base de données
            throw new NotImplementedException();
        }
        public void Update(int id, UpdateRequest model)
        {
            // À remplacer par la logique de mise à jour en base de données
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            // À remplacer par la logique de suppression en base de données
            throw new NotImplementedException();
        }
    }
}
