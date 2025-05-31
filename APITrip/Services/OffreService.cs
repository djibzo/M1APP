using APITrip.Entities;
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
        public IEnumerable<Offre> GetAll()
        {
            throw new System.NotImplementedException();
        }
        public Offre GetById(int id)
        {
            throw new System.NotImplementedException();
        }
        public void Create(OffreCreateRequest model)
        {
            throw new System.NotImplementedException();
        }
        public void Update(int id, OffreUpdateRequest model)
        {
            throw new System.NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
