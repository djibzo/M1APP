using APITrip.Entities;
using APITrip.models.Agences;
using System.Collections.Generic;

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
        public IEnumerable<Agence> GetAll()
        {
            throw new NotImplementedException();
        }
        public Agence GetById(int id)
        {
            throw new NotImplementedException();
        }
        public void Create(AgenceCreateRequest model)
        {
            throw new NotImplementedException();
        }
        public void Update(int id, AgenceUpdateRequest model)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
