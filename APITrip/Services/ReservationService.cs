using APITrip.Entities;
using APITrip.models.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private static List<Reservation> _reservations = new List<Reservation>();
        private static int _nextId = 1;

        public IEnumerable<Reservation> GetAll()
        {
            return _reservations;
        }

        public Reservation GetById(int id)
        {
            return _reservations.FirstOrDefault(r => r.IdReservation == id);
        }

        public void Create(ReservationCreateRequest model)
        {
            var reservation = new Reservation
            {
                IdReservation = _nextId++,
                // ici tu copies les propriétés depuis le model, par exemple :
                // IdVoyage = model.IdVoyage,
                // ClientName = model.ClientName,
                // DateReservation = model.DateReservation,
                // etc.
            };

            // Ex: si tu as une propriété IdVoyage et DateReservation
            // reservation.IdVoyage = model.IdVoyage;
            // reservation.DateReservation = model.DateReservation;
            // ... à adapter selon ta classe Reservation

            _reservations.Add(reservation);
        }

        public void Update(int id, ReservationUpdateRequest model)
        {
            var reservation = GetById(id);
            if (reservation == null)
                throw new KeyNotFoundException($"La réservation avec l'id {id} n'a pas été trouvée.");

            // Met à jour les propriétés, ex :
            // reservation.IdVoyage = model.IdVoyage;
            // reservation.DateReservation = model.DateReservation;
            // ... selon les propriétés de ta classe
        }

        public void Delete(int id)
        {
            var reservation = GetById(id);
            if (reservation == null)
                throw new KeyNotFoundException($"La réservation avec l'id {id} n'a pas été trouvée.");

            _reservations.Remove(reservation);
        }
    }
}
