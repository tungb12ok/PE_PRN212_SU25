using System;
using System.Windows;
using BIL.Service;
using BIL.IServices;
using DAL.Models;

namespace JLPTMockTestManagement__SE180090
{
    public partial class LoginWindow : Window
    {
        private readonly IJlptaccountService _jlptaccountService;

        public LoginWindow()
        {
            InitializeComponent();
            _jlptaccountService = new JlptaccountService();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Email and Password cannot be empty.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Jlptaccount? user = _jlptaccountService.Login(email, password);

                if (user == null)
                {
                    MessageBox.Show("Invalid email or password.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (user.Role == 4)
                {
                    MessageBox.Show("You have no permission to access this function!", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var main = new JLPTMockTestManagementWindow(user);
                main.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while logging in.\n" + ex.Message, "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
