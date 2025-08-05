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
    /// Giao diện quản lý lịch hẹn của khách hàng
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
                MessageBox.Show($"Lỗi khi tải lịch hẹn:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var consultants = bookingService.GetAllConsultants();
                if (consultants == null || !consultants.Any())
                {
                    MessageBox.Show("Không tìm thấy chuyên gia tư vấn. Vui lòng kiểm tra lại hệ thống.", "Không có tư vấn viên", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                        MeetLink = null, // Tư vấn viên sẽ cung cấp sau
                        Status = "Pending",
                        PaymentStatus = "Unpaid",
                        CreatedAt = DateTime.Now
                    };

                    bookingService.AddBooking(newBooking);
                    LoadBookings();
                    MessageBox.Show("Tạo lịch hẹn thành công!\n\nTư vấn viên sẽ cung cấp liên kết cuộc họp sau khi xác nhận.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo lịch hẹn:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgConsultantBookingList.SelectedItem is ConsultationBooking selectedBooking)
                {
                    var result = MessageBox.Show("Bạn có chắc chắn muốn hủy lịch hẹn này không?", "Xác nhận hủy", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        bookingService.CancelBooking(selectedBooking.BookingId, customerId, "Khách hàng hủy");
                        LoadBookings();
                        MessageBox.Show("Hủy lịch hẹn thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một lịch hẹn để hủy.", "Chưa chọn lịch hẹn", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hủy lịch hẹn:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        bookingService.RescheduleBooking(
                            selectedBooking.BookingId,
                            dialog.BookingDate.Value + dialog.BookingTime.Value,
                            customerId);
                        LoadBookings();
                        MessageBox.Show("Đổi lịch hẹn thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một lịch hẹn để chỉnh sửa.", "Chưa chọn lịch hẹn", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chỉnh sửa lịch hẹn:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
