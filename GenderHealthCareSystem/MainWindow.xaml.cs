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

namespace GenderHealthCareSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStisBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var stisWindow = new StisBookingFunc.ManageStisBooking();
                stisWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening STIs booking window:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnConsultantBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var consultantWindow = new ConsultantBookingFunc.ManageConsultantBooking();
                consultantWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening consultant booking management:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCustomerBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var customerWindow = new ConsultantBookingFunc.ConsultantBookingWindow();
                customerWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening customer booking window:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}