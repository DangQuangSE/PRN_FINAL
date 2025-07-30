using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace GenderHealthCareSystem.Dashboard
{
    /// <summary>
    /// Interaction logic for CustomerDashboard.xaml
    /// </summary>
    public partial class CustomerDashboard : Window
    {
        private readonly AccountService _accountService;
        private readonly UserService _userService;
        public User user { get; set; }
        private const string PhoneRegexPattern = @"(^0[35789]\d{8}$)|(^\+84[35789]\d{8}$)";

        public CustomerDashboard(User _user)
        {
            InitializeComponent();
            _accountService = new AccountService();
            _userService = new UserService();
            user = _user;
            LoadGenderOptions();
            LoadProfile();
        }

        private void LoadGenderOptions()
        {
            var genderOptions = new[]
            {
                new  { DisplayName = "Nam", Value = "Male" },
                new  { DisplayName = "Nữ", Value = "Female" },
            };
            cbGender.ItemsSource = genderOptions;
            cbGender.DisplayMemberPath = "DisplayName";
            cbGender.SelectedValuePath = "Value";
        }
        public void LoadProfile()
        {
            var account = _accountService.GetAccountByUserId(user.UserId);
            txtFullName.Text = user.FullName;
            txtEmail.Text = account.Email;
            txtAddress.Text = user.Address;
            txtUsername.Text = account.UserName;
            txtPhone.Text = user.Phone;
            dpBirthDate.Text = user.BirthDate.ToString();
            cbGender.SelectedValue = user.Gender;
        }

        private void btnUpdateProfile_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(txtPhone.Text, PhoneRegexPattern))
            {
                MessageBox.Show("Số điện thoại không đúng định dạng.",
                        "Số điện thoại không hợp lệ",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                return;
            }

            User new_user = new User()
            {
                UserId = user.UserId,
                FullName = txtFullName.Text,
                Phone = txtPhone.Text,
                Address = txtAddress.Text,
                BirthDate = DateOnly.Parse(dpBirthDate.Text),
                Gender = cbGender.SelectedValue.ToString(),
            };
            _userService.UpdateProfile(new_user);
            user = new_user;
            LoadProfile();
        }
    }
}
