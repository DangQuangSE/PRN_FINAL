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
using GenderHealthCareSystem.Dashboard;

namespace GenderHealthCareSystem.ConsultantBookingFunc
{
    /// <summary>
    /// Interaction logic for ManageConsultantBooking.xaml
    /// </summary>
    public partial class ManageConsultantBooking : Window
    {
        private readonly BLL.Service.ConsultationBookingService bookingService;
        
        public ManageConsultantBooking()
        {
            InitializeComponent();
            bookingService = new BLL.Service.ConsultationBookingService();
            LoadBookings();
        }
        
        private void LoadBookings()
        {
            try
            {
                dgConsultantBookingList.ItemsSource = bookingService.GetAllBookings();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i khi t?i danh sách booking: {ex.Message}", "L?i", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchText = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(searchText))
                {
                    var filteredBookings = bookingService.SearchBookings(customerName: searchText);
                    dgConsultantBookingList.ItemsSource = filteredBookings;
                    
                    if (!filteredBookings.Any())
                    {
                        MessageBox.Show("Không tìm th?y booking nào phù h?p.", "K?t qu? tìm ki?m", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    LoadBookings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i khi tìm ki?m: {ex.Message}", "L?i", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnStatus_Click(object sender, RoutedEventArgs e)
        {
            if (dgConsultantBookingList.SelectedItem is ConsultationBooking selectedBooking)
            {
                // Pre-select current status
                foreach (ComboBoxItem item in cbBookingStatus.Items)
                {
                    if (item.Content.ToString() == selectedBooking.Status)
                    {
                        cbBookingStatus.SelectedItem = item;
                        break;
                    }
                }
                
                // Pre-select current payment status
                foreach (ComboBoxItem item in cbPaymentStatus.Items)
                {
                    if (item.Content.ToString() == selectedBooking.PaymentStatus)
                    {
                        cbPaymentStatus.SelectedItem = item;
                        break;
                    }
                }
                
                ppStatus.IsOpen = true;
            }
            else
            {
                MessageBox.Show("Vui lòng ch?n m?t booking ?? c?p nh?t.", "Ch?a ch?n", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnGenerateLink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgConsultantBookingList.SelectedItem is ConsultationBooking selectedBooking)
                {
                    var dialog = new MeetLinkDialog(selectedBooking);
                    dialog.Owner = this;
                    
                    if (dialog.ShowDialog() == true)
                    {
                        selectedBooking.MeetLink = dialog.MeetLink;
                        bookingService.UpdateBooking(selectedBooking);
                        LoadBookings();
                        
                        MessageBox.Show($"Meeting link has been set:\n{dialog.MeetLink}\n\nThe link has been saved to the system.", "Link Added", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a booking to add meeting link.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding meeting link: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgConsultantBookingList.SelectedItem is ConsultationBooking selectedBooking)
                {
                    if (cbBookingStatus.SelectedItem is ComboBoxItem selectedStatus && 
                        cbPaymentStatus.SelectedItem is ComboBoxItem selectedPaymentStatus)
                    {
                        string newStatus = selectedStatus.Content.ToString();
                        string newPaymentStatus = selectedPaymentStatus.Content.ToString();
                        
                        bookingService.UpdateBookingStatus(selectedBooking.BookingId, newStatus);
                        selectedBooking.PaymentStatus = newPaymentStatus;
                        bookingService.UpdateBooking(selectedBooking);
                        
                        LoadBookings();
                        ppStatus.IsOpen = false;
                        MessageBox.Show("C?p nh?t tr?ng thái booking thành công.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng ch?n c? tr?ng thái booking và tr?ng thái thanh toán.", "Thi?u thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng ch?n m?t booking ?? c?p nh?t.", "Ch?a ch?n", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "L?i validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i khi c?p nh?t tr?ng thái: {ex.Message}", "L?i", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClosePopup_Click(object sender, RoutedEventArgs e)
        {
            ppStatus.IsOpen = false;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ManagerDashboard managerDashboard = new ManagerDashboard();
            managerDashboard.Show();
            this.Close();
        }
    }
}