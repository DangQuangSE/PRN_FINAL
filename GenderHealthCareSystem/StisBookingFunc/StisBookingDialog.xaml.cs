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

namespace GenderHealthCareSystem.StisBookingFunc
{
    /// <summary>
    /// Interaction logic for StisBookingDialog.xaml
    /// </summary>
    public partial class StisBookingDialog : Window
    {
        public int ServiceId => (int?)cbService.SelectedValue ?? 0;
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
        public string PaymentMethod => (cbPaymentMethod.SelectedItem as ComboBoxItem)?.Content?.ToString();

        public StisBookingDialog(bool isEdit = false)
        {
            InitializeComponent();
            if (isEdit)
            {
                Title = "Edit STIs Booking";
                cbPaymentMethod.Visibility = Visibility.Collapsed;
                tbNote.Visibility = Visibility.Collapsed;
                cbService.Visibility = Visibility.Collapsed;
                lbnote.Visibility = Visibility.Collapsed;
                lbpayment.Visibility = Visibility.Collapsed;
                lbservice.Visibility = Visibility.Collapsed;
                IsEdit = true;

            }
            else
            {
                Title = "Add STIs Booking";
                IsEdit = false;
            }

        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                if (BookingDate == null || BookingTime == null)
                {
                    MessageBox.Show("Please fill all required fields.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            if (ServiceId == 0 || BookingDate == null || BookingTime == null || string.IsNullOrWhiteSpace(PaymentMethod))
            {
                MessageBox.Show("Please fill all required fields.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
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
