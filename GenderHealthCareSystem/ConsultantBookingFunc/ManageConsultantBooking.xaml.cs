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
    /// Giao diện quản lý lịch hẹn tư vấn
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
                MessageBox.Show($"Lỗi khi tải danh sách lịch hẹn: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        MessageBox.Show("Không tìm thấy lịch hẹn phù hợp.", "Kết quả tìm kiếm", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    LoadBookings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnStatus_Click(object sender, RoutedEventArgs e)
        {
            if (dgConsultantBookingList.SelectedItem is ConsultationBooking selectedBooking)
            {
                // Chọn sẵn trạng thái hiện tại
                foreach (ComboBoxItem item in cbBookingStatus.Items)
                {
                    if (item.Content.ToString() == selectedBooking.Status)
                    {
                        cbBookingStatus.SelectedItem = item;
                        break;
                    }
                }

                // Chọn sẵn trạng thái thanh toán hiện tại
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
                MessageBox.Show("Vui lòng chọn một lịch hẹn để cập nhật.", "Chưa chọn lịch hẹn", MessageBoxButton.OK, MessageBoxImage.Information);
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

                        MessageBox.Show($"Đã tạo liên kết cuộc họp:\n{dialog.MeetLink}\n\nLiên kết đã được lưu vào hệ thống.", "Đã thêm liên kết", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một lịch hẹn để tạo liên kết cuộc họp.", "Chưa chọn lịch hẹn", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo liên kết cuộc họp: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        MessageBox.Show("Cập nhật trạng thái lịch hẹn thành công.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn cả trạng thái lịch hẹn và trạng thái thanh toán.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một lịch hẹn để cập nhật.", "Chưa chọn lịch hẹn", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật trạng thái: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
