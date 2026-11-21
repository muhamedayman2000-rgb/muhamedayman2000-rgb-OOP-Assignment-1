using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel_Reservation_System
{
    public class Guest
    {
        private readonly List<Reservation> _reservations = new List<Reservation>();
        public IReadOnlyList<Reservation> Reservations => _reservations;
        private static int _nextId = 1;
        public int Id { get; }
        public string FullName { get; }
        public string PhoneNumber { get; }

        public Guest(string fullName, string phoneNumber)
        {
            
            if(string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("Full name cannot be null or empty.");
            }
            if(string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentException("Phone number cannot be null or empty.");
            }
            Id = _nextId++;
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }
        public Reservation MakeReservation(Room room, DateTime checkIn, DateTime checkOut)
        {
           if(!room.IsAvailable(checkIn, checkOut))
                throw new InvalidOperationException("Selected room is not available for the specified dates.");
            
           if (room==null)
                throw new ArgumentNullException(nameof(room), "Room cannot be null.");

            var reservation = new Reservation(room, checkIn, checkOut);
            _reservations.Add(reservation);
           
            return reservation;
        }
        public void DisplayReservations()
        {
            if (_reservations.Count == 0)
            {
                Console.WriteLine("No reservations found.");
                return;
            }
            foreach (var reservation in _reservations)
            {
                Console.WriteLine($"Reservation ID: {reservation.Id}, Room Number: {reservation.Room.Number}, Check-In: {reservation.CheckInDate.ToShortDateString()}, Check-Out: {reservation.CheckOutDate.ToShortDateString()}, Status: {reservation.Status}");
            }
        }

    }
}
