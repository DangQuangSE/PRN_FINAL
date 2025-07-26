using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class StisBookingRepository
    {
        private readonly GenderHealthCareSystemContext _context;

        public StisBookingRepository()
        {
            _context = new GenderHealthCareSystemContext();
        }

        public List<StisBooking> GetBookingsByCustomerId(int customerId)
        {
            return _context.StisBookings
                .Include(b => b.Service)
                .Where(b => b.CustomerId == customerId)
                .OrderByDescending(b => b.BookingDate)
                .ToList();
        }
        public StisBooking? GetBookingById(int bookingId)
        {
            return _context.StisBookings
                .Include(b => b.Service)
                .FirstOrDefault(b => b.BookingId == bookingId);
        }
        public void AddBooking(StisBooking booking)
        {
            booking.CreatedAt = DateTime.Now;
            booking.Status = "pending";
            _context.StisBookings.Add(booking);
            _context.SaveChanges();
        }
        public void UpdateBooking(StisBooking booking)
        {
            var existingBooking = _context.StisBookings.Find(booking.BookingId);
            if (existingBooking != null)
            {
                existingBooking.BookingDate = booking.BookingDate;
                existingBooking.Note = booking.Note;
                existingBooking.PaymentMethod = booking.PaymentMethod;
                existingBooking.Status = booking.Status;
                existingBooking.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
            }
        }
        public void DeleteBooking(int bookingId)
        {
            var booking = _context.StisBookings.Find(bookingId);
            if (booking != null)
            {
                booking.Status = "CANCELLED"; // Soft delete
                _context.SaveChanges();
            }
        }
        public List<StisBooking> GetAllBookings()
        {
            return _context.StisBookings
                .Include(b => b.Customer)
                .Include(b => b.Service)
                .OrderByDescending(b => b.CreatedAt)
                .ToList();
        }

    }
}
