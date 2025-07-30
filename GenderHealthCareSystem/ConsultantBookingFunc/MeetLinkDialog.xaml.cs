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

namespace GenderHealthCareSystem.ConsultantBookingFunc
{
    /// <summary>
    /// Interaction logic for MeetLinkDialog.xaml
    /// </summary>
    public partial class MeetLinkDialog : Window
    {
        public string MeetLink => tbMeetLink.Text;
        
        public MeetLinkDialog(ConsultationBooking booking)
        {
            InitializeComponent();
            
            // Display booking information
            tbCustomerInfo.Text = booking.Customer?.FullName ?? "Unknown Customer";
            tbBookingInfo.Text = booking.BookingDate?.ToString("dddd, MMMM dd, yyyy 'at' HH:mm") ?? "Unknown Date";
            
            // Pre-fill current link if exists
            if (!string.IsNullOrEmpty(booking.MeetLink))
            {
                tbMeetLink.Text = booking.MeetLink;
            }
        }

        private void btnZoom_Click(object sender, RoutedEventArgs e)
        {
            // Generate a sample Zoom link
            var zoomId = new Random().Next(100000000, 999999999);
            tbMeetLink.Text = $"https://zoom.us/j/{zoomId}";
        }

        private void btnGoogleMeet_Click(object sender, RoutedEventArgs e)
        {
            // Generate a Google Meet link
            var meetCode = Guid.NewGuid().ToString("N")[..10];
            tbMeetLink.Text = $"https://meet.google.com/{meetCode}";
        }

        private void btnTeams_Click(object sender, RoutedEventArgs e)
        {
            // Generate a Teams link
            var teamsId = Guid.NewGuid().ToString();
            tbMeetLink.Text = $"https://teams.microsoft.com/l/meetup-join/{teamsId}";
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbMeetLink.Text))
            {
                MessageBox.Show("Please enter a meeting link.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Uri.TryCreate(tbMeetLink.Text, UriKind.Absolute, out _))
            {
                MessageBox.Show("Please enter a valid URL.", "Invalid Link", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}