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

namespace GenderHealthCareSystem.Auth
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private UserService _svUser;
        private AccountService _svAccount;
        public SignUpWindow()
        {
            InitializeComponent();
            _svUser = new UserService();
            _svAccount = new AccountService();
            LoadGenderOptions();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void LoadGenderOptions()
        {
            var genderOptions = new[] 
            {
                new  { DisplayName = "Nam", Value = "male" },
                new  { DisplayName = "Nữ", Value = "female" },
            };
            cbGender.ItemsSource = genderOptions;
            cbGender.DisplayMemberPath = "DisplayName";
            cbGender.SelectedValuePath = "Value";
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            //validate
            if (string.IsNullOrEmpty(txtUsername.Text) 
                || string.IsNullOrEmpty(txtPassword.Password)
                || string.IsNullOrEmpty(txtFullName.Text)
                || string.IsNullOrEmpty(txtPhoneNumber.Text)
                || string.IsNullOrEmpty(txtAddress.Text)
                || string.IsNullOrEmpty(txtEmail.Text)
                || cbGender.SelectedValue == null
                || txtBirthDate.Text == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin đăng nhập.",
                               "Thiếu thông tin",
                               MessageBoxButton.OK,
                               MessageBoxImage.Warning);
                return;
            }

            if (DateTime.Parse(txtBirthDate.Text) >= DateTime.Today)
            {
                MessageBox.Show("Ngày sinh không được là ngày hôm nay hoặc trong tương lai.",
                        "Ngày sinh không hợp lệ",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                return;
            }

            var user = new User()
            {
                Address = txtAddress.Text,
                BirthDate = DateOnly.Parse(txtBirthDate.Text),
                FullName = txtFullName.Text,
                Gender = cbGender.SelectedValue.ToString(),
                Phone = txtPhoneNumber.Text,
                Provider = "local",
            };

            if(_svAccount.checkAcount(txtUsername.Text, txtEmail.Text))
            {
                MessageBox.Show("Tên đăng nhập hoặc Email đã được sử dụng.",
                               "Đăng ký không thành công",
                               MessageBoxButton.OK,
                               MessageBoxImage.Warning);
                return;
            }

            _svUser.SignUpUser(user, txtEmail.Text, txtUsername.Text, txtPassword.Password);
            MessageBox.Show("Đăng ký thành công. Giờ bạn có thể đăng nhập!",
                            "Đăng ký hoàn tất",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
} 