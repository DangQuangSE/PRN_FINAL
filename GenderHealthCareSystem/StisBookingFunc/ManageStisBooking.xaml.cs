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
    /// Interaction logic for ManageStisBooking.xaml
    /// </summary>
    public partial class ManageStisBooking : Window
    {
        private readonly BLL.Service.StisBookingService bookingService;
        public ManageStisBooking()
        {
            InitializeComponent();
            bookingService = new BLL.Service.StisBookingService();
            LoadBookings();
        }
        private void LoadBookings()
        {

            dgStisBookingList.ItemsSource = bookingService.GetAllBookings();
        }

        private void btnstatus_Click(object sender, RoutedEventArgs e)
        {
            if (dgStisBookingList.SelectedItem is StisBooking selectedBooking)
            {

                ppStatus.IsOpen = true;
            }
            else
            {
                MessageBox.Show("Please select a booking to update.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgStisBookingList.SelectedItem is StisBooking selectedBooking)
            {
                var result = MessageBox.Show("Are you sure you want to delete this booking?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    bookingService.DeleteBooking(selectedBooking.BookingId);
                    LoadBookings();
                }
            }
            else
            {
                MessageBox.Show("Please select a booking to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnUpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            if (dgStisBookingList.SelectedItem is StisBooking selectedBooking)
            {

                if (cbBookingStatus.SelectedItem is ComboBoxItem selectedStatus)
                {
                    string newStatus = selectedStatus.Content.ToString();
                    selectedBooking.Status = newStatus;
                    bookingService.UpdateBooking(selectedBooking);
                    LoadBookings();
                    ppStatus.IsOpen = false; // Close the popup after updating
                }
                else
                {
                    MessageBox.Show("Please select a status to update.", "No Status Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select a booking to update.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnClosePopup_Click(object sender, RoutedEventArgs e)
        {
           ppStatus.IsOpen = false; // Close the popup without updating
        }

        private void btnAddResult_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
