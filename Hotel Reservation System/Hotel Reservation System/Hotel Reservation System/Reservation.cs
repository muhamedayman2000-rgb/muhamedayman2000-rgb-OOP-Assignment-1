using Hotel_Reservation_System.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel_Reservation_System
{
    public class Reservation
    {
        private static int _nextId = 1;
        public int Id { get; }

        public DateTime CheckInDate { get; }
        public DateTime CheckOutDate { get; }
        public Room Room { get; }
        public ReservationStatus Status { get; private set; }

        public int NumberOfNights => (int)(CheckOutDate.Date - CheckInDate).Days;

        public decimal TotalCost => NumberOfNights * Room.NightlyRate;


        public Reservation(Room room , DateTime checkInDate, DateTime checkOutDate)
        {
            if(room==null)
            {
                throw new ArgumentNullException(nameof(room), "Room cannot be null.");
            }
            if(!IsValidDateRange(checkInDate, checkOutDate))
            {
                throw new ArgumentException("Invalid date range.");
            }
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Room = room;
            Status = ReservationStatus.Pending;
            room.AddReservation(this);
            Id = _nextId++;

        }
        private bool IsValidDateRange(DateTime checkIn, DateTime checkOut)
        {
            return checkIn < checkOut;
        }
        public void Confirm() => ChangeStatusTo(ReservationStatus.Confirmed);
        public void CheckIn() => ChangeStatusTo(ReservationStatus.CheckedIn);
        public void CheckOut() => ChangeStatusTo(ReservationStatus.CheckedOut);
        public void Cancel() => ChangeStatusTo(ReservationStatus.Cancelled);

        private void ChangeStatusTo(ReservationStatus newStatus)
        {
            ReservationStatus[] allowed = Status switch
            {
                ReservationStatus.Pending => new[] { ReservationStatus.Confirmed, ReservationStatus.Cancelled },
                ReservationStatus.Confirmed => new[] { ReservationStatus.CheckedIn, ReservationStatus.Cancelled },
                ReservationStatus.CheckedIn => new[] { ReservationStatus.CheckedOut },
                _ => Array.Empty<ReservationStatus>()
            };

            if (Array.IndexOf(allowed, newStatus) < 0)
            {
                throw new InvalidOperationException(
                    $"Cannot change reservation status from {Status} to {newStatus}.");
            }

            Status = newStatus;
        }
       
    }
}
