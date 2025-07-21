using DAL.Entities;
using Microsoft.EntityFrameworkCore;
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
using DAL.Repository;
using DAL;
using BIL.Service;
using BIL.IServices;


namespace JLPTMockTestManagement__SE180090
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IAccountService _accountService;

        public LoginWindow()
        {
            InitializeComponent();
            _accountService = new AccountService();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password;

            //1.CHECK EMAIL AND PASSWORD CÓ RỖNG KHÔNG
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Email and Password cannot be empty.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //2.NHẬP TÀI KHOẢN PASSWORD ===>GỌI SERVICE
            UserAccount? user = _accountService.Login(email, password);

            //3.CHECK LOGIN EMAIL AND PASSWORD COI CÓ THÀNH CÔNG KHÔNG
            if (user == null)
            {
                //LOGIN KHÔNG THÀNH CÔNG
                MessageBox.Show("Invalid email or password.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //4.PHÂN QUYỀN NGƯỜI DÙNG
            if (user.Role == 4)
            {
                //USER IS MEMBER  
                MessageBox.Show("You have no permission to access this function!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //CHẶN QUYỀN CRUD CỦA MEMBER
            //MỞ MÀN HÌNH CHÍNH RESEARCH PROJECT MANAGEMENT WINDOW,
            //CÒN LẠI ADMIN, MANAGER, STAFF
            ResearchProjectManagementWindow main = new  ResearchProjectManagementWindow(user);
            main.Show();
            this.Close(); //HIỂN THỊ MAIN RỒI ẨN LOGIN 
        }
    }
}
