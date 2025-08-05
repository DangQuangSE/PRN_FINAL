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
    /// Giao diện tạo/chỉnh sửa lịch hẹn tư vấn
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
                Title = "Chỉnh sửa lịch hẹn tư vấn";
                cbConsultant.Visibility = Visibility.Collapsed;
                tbNote.Visibility = Visibility.Collapsed;
                lbNote.Visibility = Visibility.Collapsed;
                lbConsultant.Visibility = Visibility.Collapsed;
                IsEdit = true;
            }
            else
            {
                Title = "Tạo lịch hẹn tư vấn";
                IsEdit = false;
            }

            // Giới hạn ngày đặt hẹn: từ hôm nay đến 30 ngày sau
            dpBookingDate.DisplayDateStart = DateTime.Today;
            dpBookingDate.DisplayDateEnd = DateTime.Today.AddDays(30);
            dpBookingDate.SelectedDate = DateTime.Today.AddDays(1); // Mặc định chọn ngày mai
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsEdit)
                {
                    if (BookingDate == null)
                    {
                        MessageBox.Show("Vui lòng chọn ngày hẹn.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (BookingTime == null)
                    {
                        MessageBox.Show("Vui lòng chọn giờ hẹn.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                else
                {
                    if (ConsultantId == 0)
                    {
                        MessageBox.Show("Vui lòng chọn tư vấn viên.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (BookingDate == null)
                    {
                        MessageBox.Show("Vui lòng chọn ngày hẹn.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (BookingTime == null)
                    {
                        MessageBox.Show("Vui lòng chọn giờ hẹn.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    DateTime fullBookingDateTime = BookingDate.Value + BookingTime.Value;

                    if (fullBookingDateTime <= DateTime.Now)
                    {
                        MessageBox.Show("Không thể đặt lịch ở thời điểm đã qua.", "Thời gian không hợp lệ", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (fullBookingDateTime <= DateTime.Now.AddMinutes(30))
                    {
                        MessageBox.Show("Vui lòng đặt lịch trước ít nhất 30 phút.", "Thời gian không hợp lệ", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (fullBookingDateTime >= DateTime.Now.AddDays(30))
                    {
                        MessageBox.Show("Chỉ được đặt lịch trong vòng 30 ngày.", "Thời gian không hợp lệ", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    try
                    {
                        bool isAvailable = bookingService.IsConsultantAvailable(ConsultantId, fullBookingDateTime);

                        if (!isAvailable)
                        {
                            MessageBox.Show("Tư vấn viên đã có lịch vào thời điểm này. Vui lòng chọn thời gian khác.", "Trùng lịch", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi kiểm tra lịch tư vấn:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xác thực dữ liệu:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
