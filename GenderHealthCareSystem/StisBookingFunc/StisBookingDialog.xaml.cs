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
                if (TimeSpan.TryParse(tbBookingTime.Text, out var time))
                    return time;
                return null;
            }
        }
        public string Note => tbNote.Text;
        public string PaymentMethod => (cbPaymentMethod.SelectedItem as ComboBoxItem)?.Content?.ToString();

        public StisBookingDialog()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
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
