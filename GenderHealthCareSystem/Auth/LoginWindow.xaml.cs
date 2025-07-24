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

namespace GenderHealthCareSystem.Auth
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly AccountService _svAccount;
        public LoginWindow()
        {
            InitializeComponent();
            _svAccount = new AccountService();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.Show();
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //validate
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin đăng nhập.",
                               "Thiếu thông tin",
                               MessageBoxButton.OK,
                               MessageBoxImage.Warning);
                return;
            }

            string username = txtUsername.Text;
            string password = txtPassword.Password;
            var account = _svAccount.Login(username, password);
            if (account != null)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu.",
                               "Đăng nhập không thành công",
                               MessageBoxButton.OK,
                               MessageBoxImage.Warning);
                return;
            }

        }

    }
}
