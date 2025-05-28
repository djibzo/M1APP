using APITrip.Entities;
using APITrip.models.Gestionnaires;
using System.Collections.Generic;

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
        public IEnumerable<Gestionnaire> GetAll()
        {
            throw new NotImplementedException();
        }
        public Gestionnaire GetById(int id)
        {
            throw new NotImplementedException();
        }
        public void Create(GestionnaireCreateRequest model)
        {
            throw new NotImplementedException();
        }
        public void Update(int id, GestionnaireUpdateRequest model)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
