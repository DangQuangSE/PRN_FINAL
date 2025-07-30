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

namespace GenderHealthCareSystem.ServiceMangement
{
    /// <summary>
    /// Interaction logic for StisServiceDialog.xaml
    /// </summary>
    public partial class StisServiceDialog : Window
    {
        public StisService Service { get; private set; }

        public StisServiceDialog(StisService service = null)
        {
            InitializeComponent();
            Service = service != null
               ? new StisService
               {
                   ServiceId = service.ServiceId,
                   ServiceName = service.ServiceName,
                   Type = service.Type,
                   Price = service.Price,
                   Duration = service.Duration,
                   Status = service.Status
               }
               : new StisService { Status = "active" };

            txtName.Text = Service.ServiceName;
            txtType.Text = Service.Type;
            txtPrice.Text = Service.Price?.ToString();
            txtDuration.Text = Service.Duration;
            cbStatus.SelectedItem = new ComboBoxItem { Content = Service.Status };
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Service.ServiceName = txtName.Text;
            Service.Type = txtType.Text;
            Service.Price = decimal.TryParse(txtPrice.Text, out var price) ? price : 0;
            Service.Duration = txtDuration.Text;
            Service.Status = (cbStatus.SelectedItem as ComboBoxItem)?.Content.ToString();

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
