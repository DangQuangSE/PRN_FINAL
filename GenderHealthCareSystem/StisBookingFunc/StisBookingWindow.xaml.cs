using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DAL.Entities;

namespace GenderHealthCareSystem.StisBookingFunc
{
    /// <summary>
    /// Interaction logic for StisBookingWindow.xaml
    /// </summary>
    public partial class StisBookingWindow : Window
    {
        private readonly BLL.Service.StisBookingService bookingService;
        private readonly BLL.Service.StisServiceService serviceService;
        public int CustomerId { get; set; }
        public StisBookingWindow()
        {
            InitializeComponent();
            bookingService = new BLL.Service.StisBookingService();
            serviceService = new BLL.Service.StisServiceService();
            LoadBookings();
        }
        private void LoadBookings()
        {
            // Assuming you have a method to get the current user's customer ID
            var bookings = bookingService.GetBookingsByCustomerId(CustomerId);
            dgStisBookingList.ItemsSource = bookings;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StisBookingDialog();
            dialog.Owner = this;

            // Optionally, set available services for the dialog
            dialog.cbService.ItemsSource = serviceService.GetAllServices();
            dialog.cbService.DisplayMemberPath = "ServiceName";
            dialog.cbService.SelectedValuePath = "ServiceId";

            if (dialog.ShowDialog() == true)
            {
                var newBooking = new StisBooking
                {
                    ServiceId = dialog.ServiceId,
                    BookingDate = dialog.BookingDate.Value + dialog.BookingTime.Value,
                    CustomerId = CustomerId,
                    Note = dialog.Note,
                    PaymentMethod = dialog.PaymentMethod
                };

                bookingService.AddBooking(newBooking);
                LoadBookings();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgStisBookingList.SelectedItem is StisBooking selectedBooking)
            {
                var result = MessageBox.Show("Are you sure you want to cancel this booking?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    bookingService.DeleteBooking(selectedBooking.BookingId);
                    LoadBookings();
                }
            }
            else
            {
                MessageBox.Show("Please select a booking to cancel.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgStisBookingList.SelectedItem is StisBooking selectedBooking)
            {
                var dialog = new StisBookingDialog(true);
                dialog.Owner = this;

                // Optionally, set available services for the dialog
                dialog.cbService.ItemsSource = serviceService.GetAllServices();
                dialog.cbService.DisplayMemberPath = "ServiceName";
                dialog.cbService.SelectedValuePath = "ServiceId";

                if (dialog.ShowDialog() == true)
                {
                    var newBooking = selectedBooking;
                    newBooking.BookingDate = dialog.BookingDate.Value + dialog.BookingTime.Value;

                    bookingService.UpdateBooking(newBooking);
                    LoadBookings();
                }
            }
            else
            {
                MessageBox.Show("Please select a booking to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
           
        }

        private void btnViewResult_Click(object sender, RoutedEventArgs e)
        {
            if (dgStisBookingList.SelectedItem is StisBooking selectedBooking)
            {
                ppresult.IsOpen = true; // Open the result popup
                var result = selectedBooking?.StisResults?.FirstOrDefault();
                if (result != null)
                {
                    tbresult.Text = result.Note;
                    // dùng note nếu cần
                }
                else
                {
                    // xử lý nếu không có kết quả
                    MessageBox.Show("Không có kết quả xét nghiệm để hiển thị.");
                }
            }
            else
            {
                MessageBox.Show("Please select a booking to view result.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void btnClosePopup_Click(object sender, RoutedEventArgs e)
        {
            ppresult.IsOpen = false; // Close the result popup
        }
        
    }
}
