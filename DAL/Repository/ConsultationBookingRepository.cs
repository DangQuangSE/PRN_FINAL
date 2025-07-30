using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.Repository
{
    public class ConsultationBookingRepository
    {
        private GenderHealthCareSystemContext _context;

        public ConsultationBookingRepository()
        {
            _context = new GenderHealthCareSystemContext();
        }

        public List<ConsultationBooking> GetAllBookings()
        {
            return _context.ConsultationBookings
                .Include(b => b.Customer)
                .Include(b => b.Consultant)
                .OrderByDescending(b => b.CreatedAt)
                .ToList();
        }

        public List<ConsultationBooking> GetBookingsByConsultant(int consultantId)
        {
            return _context.ConsultationBookings
                .Include(b => b.Customer)
                .Include(b => b.Consultant)
                .Where(b => b.ConsultantId == consultantId)
                .OrderByDescending(b => b.BookingDate)
                .ToList();
        }

        public List<ConsultationBooking> GetBookingsByCustomer(int customerId)
        {
            return _context.ConsultationBookings
                .Include(b => b.Customer)
                .Include(b => b.Consultant)
                .Where(b => b.CustomerId == customerId)
                .OrderByDescending(b => b.BookingDate)
                .ToList();
        }

        public ConsultationBooking? GetBookingById(int bookingId)
        {
            return _context.ConsultationBookings
                .Include(b => b.Customer)
                .Include(b => b.Consultant)
                .FirstOrDefault(b => b.BookingId == bookingId);
        }

        public void AddBooking(ConsultationBooking booking)
        {
            try
            {
                // Validate required fields before saving
                if (booking.ConsultantId == null || booking.ConsultantId <= 0)
                {
                    throw new ArgumentException("ConsultantId is required and must be greater than 0");
                }

                if (booking.CustomerId == null || booking.CustomerId <= 0)
                {
                    throw new ArgumentException("CustomerId is required and must be greater than 0");
                }

                if (booking.BookingDate == null)
                {
                    throw new ArgumentException("BookingDate is required");
                }

                // Check if customer exists
                var customerExists = _context.Users.Any(u => u.UserId == booking.CustomerId);
                if (!customerExists)
                {
                    throw new ArgumentException($"Customer with ID {booking.CustomerId} does not exist");
                }

                // Check if consultant exists
                var consultantExists = _context.Users.Any(u => u.UserId == booking.ConsultantId);
                if (!consultantExists)
                {
                    throw new ArgumentException($"Consultant with ID {booking.ConsultantId} does not exist");
                }

                // Set default values if not provided
                if (string.IsNullOrEmpty(booking.Status))
                {
                    booking.Status = "Pending";
                }

                if (string.IsNullOrEmpty(booking.PaymentStatus))
                {
                    booking.PaymentStatus = "Unpaid";
                }

                if (booking.CreatedAt == null || booking.CreatedAt == DateTime.MinValue)
                {
                    booking.CreatedAt = DateTime.Now;
                }

                _context.ConsultationBookings.Add(booking);
                _context.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                // Get inner exception details
                var innerException = dbEx.InnerException?.Message ?? "No inner exception";
                throw new Exception($"Database error while adding booking: {dbEx.Message}. Inner exception: {innerException}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding booking: {ex.Message}", ex);
            }
        }

        public void UpdateBooking(ConsultationBooking booking)
        {
            try
            {
                _context.ConsultationBookings.Update(booking);
                _context.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                var innerException = dbEx.InnerException?.Message ?? "No inner exception";
                throw new Exception($"Database error while updating booking: {dbEx.Message}. Inner exception: {innerException}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating booking: {ex.Message}", ex);
            }
        }

        public void DeleteBooking(int bookingId)
        {
            try
            {
                var booking = _context.ConsultationBookings.Find(bookingId);
                if (booking != null)
                {
                    _context.ConsultationBookings.Remove(booking);
                    _context.SaveChanges();
                }
            }
            catch (DbUpdateException dbEx)
            {
                var innerException = dbEx.InnerException?.Message ?? "No inner exception";
                throw new Exception($"Database error while deleting booking: {dbEx.Message}. Inner exception: {innerException}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting booking: {ex.Message}", ex);
            }
        }

        public List<User> GetAllConsultants()
        {
            return _context.Users
                .Include(u => u.Role)
                .Where(u => u.Role != null && u.Role.RoleName == "Consultant")
                .ToList();
        }

        // Ki?m tra xung ??t l?ch h?n
        public bool HasConflictingBooking(int consultantId, DateTime bookingDate, int? excludeBookingId = null)
        {
            var startTime = bookingDate.AddMinutes(-30);
            var endTime = bookingDate.AddMinutes(30);

            var query = _context.ConsultationBookings
                .Where(b => b.ConsultantId == consultantId &&
                           b.BookingDate >= startTime &&
                           b.BookingDate <= endTime &&
                           b.Status != "Cancelled");

            if (excludeBookingId.HasValue)
            {
                query = query.Where(b => b.BookingId != excludeBookingId.Value);
            }

            return query.Any();
        }

        // Tìm ki?m booking v?i nhi?u tiêu chí
        public List<ConsultationBooking> SearchBookings(string? customerName = null, 
            string? consultantName = null, 
            DateTime? startDate = null, 
            DateTime? endDate = null, 
            string? status = null)
        {
            var query = _context.ConsultationBookings
                .Include(b => b.Customer)
                .Include(b => b.Consultant)
                .AsQueryable();

            if (!string.IsNullOrEmpty(customerName))
            {
                query = query.Where(b => b.Customer != null && 
                    b.Customer.FullName.Contains(customerName));
            }

            if (!string.IsNullOrEmpty(consultantName))
            {
                query = query.Where(b => b.Consultant != null && 
                    b.Consultant.FullName.Contains(consultantName));
            }

            if (startDate.HasValue)
            {
                query = query.Where(b => b.BookingDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(b => b.BookingDate <= endDate.Value);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(b => b.Status == status);
            }

            return query.OrderByDescending(b => b.BookingDate).ToList();
        }

        // L?y booking trong kho?ng th?i gian
        public List<ConsultationBooking> GetUpcomingBookings(DateTime fromDate, DateTime toDate)
        {
            return _context.ConsultationBookings
                .Include(b => b.Customer)
                .Include(b => b.Consultant)
                .Where(b => b.BookingDate >= fromDate && 
                           b.BookingDate <= toDate &&
                           b.Status == "Confirmed")
                .OrderBy(b => b.BookingDate)
                .ToList();
        }
    }
}