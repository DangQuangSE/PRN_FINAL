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
using GenderHealthCareSystem.Dashboard;

namespace GenderHealthCareSystem.Auth
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly AccountService _svAccount;
        private readonly UserService _userService;
        public LoginWindow()
        {
            InitializeComponent();
            _svAccount = new AccountService();
            _userService = new UserService();
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
                var user = _userService.GetUserByAccountId(account);
                if (user.RoleId == 4)
                {
                    CustomerDashboard customerDashboard = new CustomerDashboard(user);
                    customerDashboard.Show();
                    this.Close();
                }
                else if (user.RoleId == 2)
                {
                    ManagerDashboard managerDashboard = new ManagerDashboard();
                    managerDashboard.user = user;
                    managerDashboard.Show();
                    this.Close();
                }
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
