using APITrip.Entities;
using APITrip.models.Voyages;
using System.Collections.Generic;

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
        public IEnumerable<Voyage> GetAll()
        {
            throw new NotImplementedException();
        }
        public Voyage GetById(int id)
        {
            throw new NotImplementedException();
        }
        public void Create(VoyageCreateRequest model)
        {
            throw new NotImplementedException();
        }
        public void Update(int id, VoyageUpdateRequest model)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
