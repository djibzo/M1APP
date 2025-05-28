using APITrip.Entities;
using APITrip.models.Reservations;
using System.Collections.Generic;

namespace APITrip.Services
{
    public interface IReservationService
    {
        IEnumerable<Reservation> GetAll();
        Reservation GetById(int id);
        void Create(CreateRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }
    public class ReservationService : IReservationService
    {
        public IEnumerable<Reservation> GetAll()
        {
            throw new NotImplementedException();
        }
        public Reservation GetById(int id)
        {
            throw new NotImplementedException();
        }
        public void Create(CreateRequest model)
        {
            throw new NotImplementedException();
        }
        public void Update(int id, UpdateRequest model)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
