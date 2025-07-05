using APITrip.Entities;
using APITrip.models.Reservations;
using System.Collections.Generic;
using APITrip.Helpers;

namespace APITrip.Services
{
    public interface IReservationService
    {
        IEnumerable<Reservation> GetAll();
        Reservation GetById(int id);
        void Create(ReservationCreateRequest model);
        void Update(int id, ReservationUpdateRequest model);
        void Delete(int id);
    }
    public class ReservationService : IReservationService
    {
        private readonly DataContext _context;

        public ReservationService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _context.Reservations;
        }

        public Reservation GetById(int id)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation == null) throw new KeyNotFoundException("Reservation not found");
            return reservation;
        }

        public void Create(ReservationCreateRequest model)
        {
            var reservation = new Reservation
            {
                DateReservation = model.DateReservation,
                MontantReservation = model.MontantReservation,
                StatutReservation = model.StatutReservation,
                ClientId = model.ClientId.ToString() // Assuming ClientId is a string in the Reservation entity
            };
            // Save reservation to database
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        public void Update(int id, ReservationUpdateRequest model)
        {
            var reservation = GetById(id);
            // Update properties
            _context.Reservations.Update(reservation);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var reservation = GetById(id);
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }
    }
}
