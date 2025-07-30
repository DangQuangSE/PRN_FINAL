using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using DAL.Repository;

namespace BLL.Service
{
    public class ConsultationBookingService
    {
        private ConsultationBookingRepository _repository;
        private UserRepository _userRepository;

        public ConsultationBookingService()
        {
            _repository = new ConsultationBookingRepository();
            _userRepository = new UserRepository();
        }

        public List<ConsultationBooking> GetAllBookings()
        {
            return _repository.GetAllBookings();
        }

        public List<ConsultationBooking> GetBookingsByConsultant(int consultantId)
        {
            return _repository.GetBookingsByConsultant(consultantId);
        }

        public List<ConsultationBooking> GetBookingsByCustomer(int customerId)
        {
            return _repository.GetBookingsByCustomer(customerId);
        }

        public ConsultationBooking? GetBookingById(int bookingId)
        {
            return _repository.GetBookingById(bookingId);
        }

        public void AddBooking(ConsultationBooking booking, User customer = null)
        {
            // Validate booking date
            if (booking.BookingDate <= DateTime.Now)
            {
                throw new ArgumentException("Khong the dat lich trong qua khu.");
            }

            if (booking.BookingDate <= DateTime.Now.AddMinutes(30))
            {
                throw new ArgumentException("Phai dat lich truoc it nhat 30 phut.");
            }

            if (booking.BookingDate >= DateTime.Now.AddDays(30))
            {
                throw new ArgumentException("Chi duoc dat lich trong vong 30 ngay toi.");
            }

            // Check for conflicts
            if (_repository.HasConflictingBooking(booking.ConsultantId.Value, booking.BookingDate.Value))
            {
                throw new ArgumentException("Tu van vien da co lich hen vao thoi diem nay.");
            }

            booking.CreatedAt = DateTime.Now;
            booking.Status = "Pending";
            booking.PaymentStatus = booking.PaymentStatus ?? "Unpaid";

            // If customer is provided, create customer first
            if (customer != null)
            {
                var createdCustomer = _userRepository.CreateCustomer(customer);
                booking.CustomerId = createdCustomer.UserId;
            }

            _repository.AddBooking(booking);
        }

        public void UpdateBooking(ConsultationBooking booking)
        {
            _repository.UpdateBooking(booking);
        }

        public void DeleteBooking(int bookingId)
        {
            _repository.DeleteBooking(bookingId);
        }

        public List<User> GetAllConsultants()
        {
            return _repository.GetAllConsultants();
        }

        // Reschedule booking
        public bool RescheduleBooking(int bookingId, DateTime newBookingDate, int? userId = null)
        {
            var booking = _repository.GetBookingById(bookingId);
            if (booking == null)
            {
                throw new ArgumentException("Booking khong ton tai.");
            }

            // Check if user has permission (if userId provided)
            if (userId.HasValue && booking.CustomerId != userId.Value)
            {
                throw new UnauthorizedAccessException("Ban khong co quyen thay doi lich hen nay.");
            }

            // Validate new date
            if (newBookingDate <= DateTime.Now)
            {
                throw new ArgumentException("Khong the dat lich trong qua khu.");
            }

            if (newBookingDate <= DateTime.Now.AddMinutes(30))
            {
                throw new ArgumentException("Phai dat lich truoc it nhat 30 phut.");
            }

            // Check for conflicts (exclude current booking)
            if (_repository.HasConflictingBooking(booking.ConsultantId.Value, newBookingDate, bookingId))
            {
                throw new ArgumentException("Tu van vien da co lich hen vao thoi diem moi nay.");
            }

            booking.BookingDate = newBookingDate;
            booking.Status = "Pending"; // Reset status when rescheduled
            _repository.UpdateBooking(booking);

            return true;
        }

        // Cancel booking
        public bool CancelBooking(int bookingId, int? userId = null, string? reason = null)
        {
            var booking = _repository.GetBookingById(bookingId);
            if (booking == null)
            {
                throw new ArgumentException("Booking khong ton tai.");
            }

            // Check if user has permission (if userId provided)
            if (userId.HasValue && booking.CustomerId != userId.Value)
            {
                throw new UnauthorizedAccessException("Ban khong co quyen huy lich hen nay.");
            }

            // Check if booking can be cancelled
            if (booking.Status == "Cancelled")
            {
                throw new ArgumentException("Lich hen da duoc huy truoc do.");
            }

            if (booking.Status == "Completed")
            {
                throw new ArgumentException("Khong the huy lich hen da hoan thanh.");
            }

            booking.Status = "Cancelled";
            if (!string.IsNullOrEmpty(reason))
            {
                booking.Note = (booking.Note ?? "") + $" [Ly do huy: {reason}]";
            }

            _repository.UpdateBooking(booking);
            return true;
        }

        // Search bookings
        public List<ConsultationBooking> SearchBookings(string? customerName = null, 
            string? consultantName = null, 
            DateTime? startDate = null, 
            DateTime? endDate = null, 
            string? status = null)
        {
            return _repository.SearchBookings(customerName, consultantName, startDate, endDate, status);
        }

        // Get upcoming bookings
        public List<ConsultationBooking> GetUpcomingBookings(int days = 7)
        {
            var fromDate = DateTime.Now;
            var toDate = DateTime.Now.AddDays(days);
            return _repository.GetUpcomingBookings(fromDate, toDate);
        }

        // Update booking status with validation
        public bool UpdateBookingStatus(int bookingId, string newStatus, int? userId = null)
        {
            var booking = _repository.GetBookingById(bookingId);
            if (booking == null)
            {
                throw new ArgumentException("Booking khong ton tai.");
            }

            // Validate status
            var validStatuses = new[] { "Pending", "Confirmed", "Completed", "Cancelled" };
            if (!validStatuses.Contains(newStatus))
            {
                throw new ArgumentException("Trang thai khong hop le.");
            }

            // Business rules for status updates
            if (booking.Status == "Completed" && newStatus != "Completed")
            {
                throw new ArgumentException("Khong the thay doi trang thai cua lich hen da hoan thanh.");
            }

            if (booking.Status == "Cancelled" && newStatus != "Cancelled")
            {
                throw new ArgumentException("Khong the thay doi trang thai cua lich hen da huy.");
            }

            booking.Status = newStatus;
            _repository.UpdateBooking(booking);
            return true;
        }

        // Check consultant availability
        public bool IsConsultantAvailable(int consultantId, DateTime bookingDate)
        {
            return !_repository.HasConflictingBooking(consultantId, bookingDate);
        }

        // Get consultant's available time slots
        public List<DateTime> GetAvailableTimeSlots(int consultantId, DateTime date)
        {
            var availableSlots = new List<DateTime>();
            var startHour = 8; // 8 AM
            var endHour = 17; // 5 PM

            for (int hour = startHour; hour <= endHour; hour++)
            {
                var timeSlot = date.Date.AddHours(hour);
                if (timeSlot > DateTime.Now && IsConsultantAvailable(consultantId, timeSlot))
                {
                    availableSlots.Add(timeSlot);
                }
            }

            return availableSlots;
        }
    }
}