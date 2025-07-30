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

namespace GenderHealthCareSystem.ConsultantBookingFunc
{
    /// <summary>
    /// Interaction logic for ConsultantBookingDialog.xaml
    /// </summary>
    public partial class ConsultantBookingDialog : Window
    {
        public int ConsultantId => (int?)cbConsultant.SelectedValue ?? 0;
        public DateTime? BookingDate => dpBookingDate.SelectedDate;
        public TimeSpan? BookingTime
        {
            get
            {
                if (tbBookingTime.SelectedItem is ComboBoxItem selectedItem)
                {
                    string timeString = selectedItem.Content as string;
                    if (TimeSpan.TryParse(timeString, out var time))
                        return time;
                }
                return null;
            }
        }
        bool IsEdit;
        public string Note => tbNote.Text;

        private readonly BLL.Service.ConsultationBookingService bookingService;

        public ConsultantBookingDialog(bool isEdit = false)
        {
            InitializeComponent();
            bookingService = new BLL.Service.ConsultationBookingService();
            
            if (isEdit)
            {
                Title = "Edit Consultant Booking";
                cbConsultant.Visibility = Visibility.Collapsed;
                tbNote.Visibility = Visibility.Collapsed;
                lbNote.Visibility = Visibility.Collapsed;
                lbConsultant.Visibility = Visibility.Collapsed;
                IsEdit = true;
            }
            else
            {
                Title = "Add Consultant Booking";
                IsEdit = false;
            }
            
            // Set minimum date to today and maximum to 30 days from now
            dpBookingDate.DisplayDateStart = DateTime.Today;
            dpBookingDate.DisplayDateEnd = DateTime.Today.AddDays(30);
            
            // Set default date to tomorrow
            dpBookingDate.SelectedDate = DateTime.Today.AddDays(1);
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Enhanced validation
                if (IsEdit)
                {
                    if (BookingDate == null)
                    {
                        MessageBox.Show("Please select a booking date.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    
                    if (BookingTime == null)
                    {
                        MessageBox.Show("Please select a booking time.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                else
                {
                    if (ConsultantId == 0)
                    {
                        MessageBox.Show("Please select a consultant.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    
                    if (BookingDate == null)
                    {
                        MessageBox.Show("Please select a booking date.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    
                    if (BookingTime == null)
                    {
                        MessageBox.Show("Please select a booking time.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Additional validation for booking time
                    DateTime fullBookingDateTime = BookingDate.Value + BookingTime.Value;
                    
                    // Check if booking is in the past
                    if (fullBookingDateTime <= DateTime.Now)
                    {
                        MessageBox.Show("Cannot book in the past.", "Invalid Time", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    
                    // Check if booking is too soon (less than 30 minutes from now)
                    if (fullBookingDateTime <= DateTime.Now.AddMinutes(30))
                    {
                        MessageBox.Show("Must book at least 30 minutes in advance.", "Invalid Time", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    
                    // Check if booking is too far in the future (more than 30 days)
                    if (fullBookingDateTime >= DateTime.Now.AddDays(30))
                    {
                        MessageBox.Show("Can only book within 30 days.", "Invalid Time", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    
                    // Check if consultant is available at this time
                    try
                    {
                        bool isAvailable = bookingService.IsConsultantAvailable(ConsultantId, fullBookingDateTime);
                        
                        if (!isAvailable)
                        {
                            MessageBox.Show("Consultant already has a booking at this time. Please choose another time.", "Time Conflict", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error checking availability:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                
                // All validations passed
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in validation:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}