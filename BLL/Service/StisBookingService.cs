using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class StisBookingService
    {
        private readonly DAL.Repository.StisBookingRepository _stisBookingRepository;
        public StisBookingService()
        {
            _stisBookingRepository = new DAL.Repository.StisBookingRepository();
        }
        public List<DAL.Entities.StisBooking> GetBookingsByCustomerId(int customerId)
        {
            return _stisBookingRepository.GetBookingsByCustomerId(customerId);
        }
        public DAL.Entities.StisBooking? GetBookingById(int bookingId)
        {
            return _stisBookingRepository.GetBookingById(bookingId);
        }
        public void AddBooking(DAL.Entities.StisBooking booking)
        {
            _stisBookingRepository.AddBooking(booking);
        }
        public void UpdateBooking(DAL.Entities.StisBooking booking)
        {
            _stisBookingRepository.UpdateBooking(booking);
        }
        public void DeleteBooking(int bookingId)
        {
            _stisBookingRepository.DeleteBooking(bookingId);
        }
        public List<DAL.Entities.StisBooking> SearchBooking(string text)
        {
            return _stisBookingRepository.GetAllBookings().Where(p => p.Customer.FullName.ToLower().Contains(text.ToLower())).ToList();
        }
        public List<DAL.Entities.StisBooking> GetAllBookings()
        {
            return _stisBookingRepository.GetAllBookings();
        }
    }
}
