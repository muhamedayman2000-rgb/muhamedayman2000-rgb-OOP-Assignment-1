using Hotel_Reservation_System.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel_Reservation_System
{
    public class Room
    {
        private readonly List<Reservation> _reservations = new List<Reservation>();
        private static int _nextRoomNumber = 1;
        public int Number { get; }

        public RoomType Type { get; }

        public decimal NightlyRate { get; private set; }

        public bool IsUnderMaintenance { get; private set; } 


        public Room(RoomType type , decimal nightlyRate)
        {
            ValidateRate(nightlyRate);
            Number = _nextRoomNumber++;
            Type = type;
            NightlyRate = nightlyRate;

        }
        public void StartMaintenance() => IsUnderMaintenance = true;
        public void EndMaintenance() => IsUnderMaintenance = false;
        public void SetNightlyRate(decimal newRate)
        {
           ValidateRate(newRate);
            NightlyRate = newRate;
        }
        public void AddReservation(Reservation reservation)
        {
            if (reservation.Room != this)
            {
                throw new InvalidOperationException("Reservation does not belong to this room.");
            }
            _reservations.Add(reservation);
        }
        public bool IsAvailable(DateTime checkIn, DateTime checkOut)
        {
            if (IsUnderMaintenance)
            {
                return false;
            }
                foreach (var existing in _reservations)
                {

                    if (existing.Status == ReservationStatus.Cancelled ||
                        existing.Status == ReservationStatus.CheckedOut)
                        continue;

                    if (checkIn < existing.CheckOutDate && existing.CheckInDate < checkOut)
                        return false;
                    
                }
            return true;
        }
        private static void ValidateRate(decimal rate)
        {
            if (rate <= 0)
                throw new ArgumentException("Nightly rate must be a positive value.", nameof(rate));
        }

    }
    }




