using APITrip.Entities;
using APITrip.models.Offres;
using System.Collections.Generic;

namespace APITrip.Services
{
    public interface IOffreService
    {
        IEnumerable<Offre> GetAll();
        Offre GetById(int id);
        void Create(CreateRequest model);
        void Update(int id, UpdateRequest model);
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
        public void Create(CreateRequest model)
        {
            throw new System.NotImplementedException();
        }
        public void Update(int id, UpdateRequest model)
        {
            throw new System.NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
