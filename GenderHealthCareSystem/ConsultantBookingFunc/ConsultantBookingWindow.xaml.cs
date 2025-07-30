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
using BLL.Service;
using GenderHealthCareSystem.Dashboard;

namespace GenderHealthCareSystem.ConsultantBookingFunc
{
    /// <summary>
    /// Interaction logic for ConsultantBookingWindow.xaml
    /// </summary>
    public partial class ConsultantBookingWindow : Window
    {
        private readonly ConsultationBookingService bookingService;
        private readonly UserService userService;
        public int customerId { get; set; }

        public ConsultantBookingWindow(int UserId)
        {
            InitializeComponent();
            customerId = UserId;
            bookingService = new ConsultationBookingService();
            userService = new UserService();
            LoadBookings();
        }
        
        private void LoadBookings()
        {
            try
            {
                var bookings = bookingService.GetBookingsByCustomer(customerId);
                dgConsultantBookingList.ItemsSource = bookings;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bookings:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var consultants = bookingService.GetAllConsultants();
                if (consultants == null || !consultants.Any())
                {
                    MessageBox.Show("No consultants found. Please ensure there are consultants in the system.", "No Consultants", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var dialog = new ConsultantBookingDialog();
                dialog.Owner = this;
                dialog.cbConsultant.ItemsSource = consultants;
                dialog.cbConsultant.DisplayMemberPath = "FullName";
                dialog.cbConsultant.SelectedValuePath = "UserId";

                if (dialog.ShowDialog() == true)
                {
                    var newBooking = new ConsultationBooking
                    {
                        ConsultantId = dialog.ConsultantId,
                        BookingDate = dialog.BookingDate.Value + dialog.BookingTime.Value,
                        CustomerId = customerId,
                        Note = dialog.Note ?? "",
                        MeetLink = null, // Will be set by consultant later
                        Status = "Pending",
                        PaymentStatus = "Unpaid",
                        CreatedAt = DateTime.Now
                    };

                    bookingService.AddBooking(newBooking);
                    LoadBookings();
                    MessageBox.Show("Booking created successfully!\n\nThe consultant will provide the meeting link after confirmation.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating booking:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgConsultantBookingList.SelectedItem is ConsultationBooking selectedBooking)
                {
                    var result = MessageBox.Show("Are you sure you want to cancel this booking?", "Confirm Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        bookingService.CancelBooking(selectedBooking.BookingId, customerId, "Cancelled by customer");
                        LoadBookings();
                        MessageBox.Show("Booking cancelled successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a booking to cancel.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cancelling booking:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgConsultantBookingList.SelectedItem is ConsultationBooking selectedBooking)
                {
                    var dialog = new ConsultantBookingDialog(true);
                    dialog.Owner = this;

                    if (selectedBooking.BookingDate.HasValue)
                    {
                        dialog.dpBookingDate.SelectedDate = selectedBooking.BookingDate.Value.Date;
                        var currentTime = selectedBooking.BookingDate.Value.TimeOfDay;
                        foreach (ComboBoxItem item in dialog.tbBookingTime.Items)
                        {
                            if (TimeSpan.TryParse(item.Content.ToString(), out var itemTime) && itemTime == currentTime)
                            {
                                dialog.tbBookingTime.SelectedItem = item;
                                break;
                            }
                        }
                    }

                    if (dialog.ShowDialog() == true)
                    {
                        bookingService.RescheduleBooking(selectedBooking.BookingId, dialog.BookingDate.Value + dialog.BookingTime.Value, customerId);
                        LoadBookings();
                        MessageBox.Show("Booking rescheduled successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a booking to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing booking:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var user = userService.GetUserByUserId(customerId);
            CustomerDashboard customer = new CustomerDashboard(user);
            customer.Show();
            this.Close();
        }
    }
}