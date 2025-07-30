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
using BLL.Service;
using DAL.Entities;

namespace GenderHealthCareSystem.ServiceMangement
{
    /// <summary>
    /// Interaction logic for StisServiceWindow.xaml
    /// </summary>
    public partial class StisServiceWindow : UserControl
    {
        private readonly StisServiceService _service;

        public StisServiceWindow()
        {
            InitializeComponent();
            _service = new StisServiceService();
            LoadServices();
        }

        private void LoadServices()
        {
            List<StisService> services = _service.GetAllServices();
            dgServices.ItemsSource = services;
            txtCount.Text = services.Count.ToString();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StisServiceDialog();
            if (dialog.ShowDialog() == true)
            {
                _service.AddService(dialog.Service);
                LoadServices();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgServices.SelectedItem is StisService selected)
            {
                var dialog = new StisServiceDialog(selected);
                if (dialog.ShowDialog() == true)
                {
                    _service.UpdateService(dialog.Service);
                    LoadServices();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dịch vụ để sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDelete_Click_1(object sender, RoutedEventArgs e)
        {
            if (dgServices.SelectedItem is StisService selected)
            {
                var confirm = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xoá dịch vụ '{selected.ServiceName}'?",
                    "Xác nhận xoá",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (confirm == MessageBoxResult.Yes)
                {
                    _service.DeleteService(selected.ServiceId);
                    LoadServices();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dịch vụ để xoá.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
