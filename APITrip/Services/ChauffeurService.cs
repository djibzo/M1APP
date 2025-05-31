using APITrip.Entities;
using APITrip.models.Chauffeurs;
using System.Collections.Generic;

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
        public IEnumerable<Chauffeur> GetAll()
        {
            // À remplacer par l'accès à la base de données (exemple : return _context.Chauffeurs;)
            throw new NotImplementedException();
        }
        public Chauffeur GetById(int id)
        {
            // À remplacer par l'accès à la base de données (exemple : return _context.Chauffeurs.Find(id);)
            throw new NotImplementedException();
        }
        public void Create(ChauffeurCreateRequest model)
        {
            // À remplacer par la logique de création en base de données
            throw new NotImplementedException();
        }
        public void Update(int id, ChauffeurUpdateRequest model)
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
