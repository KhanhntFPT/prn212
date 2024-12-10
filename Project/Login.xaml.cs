using System;
using System.Linq;
using System.Windows;
using Project.Models; // Đảm bảo namespace đúng với lớp Account
using Microsoft.AspNetCore.Identity;

namespace Project
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password.");
                return;
            }

            var account = ParkingManagementContext.Ins.Accounts
                .FirstOrDefault(a => a.Username.Equals(email));

            if (account == null)
            {
                MessageBox.Show("Account not found.");
                return;
            }

            var passwordHasher = new PasswordHasher<string>();
            var result = passwordHasher.VerifyHashedPassword(null, account.Password, password);

            if (result == PasswordVerificationResult.Success)
            {
                if (account.Role == "Admin")
                {
                    adminSide.MainScreenAdmin adminWindow = new adminSide.MainScreenAdmin();
                    adminWindow.Show();
                }
                else if (account.Role == "Customer")
                {
                    CustomerSide.MainScreenCus customerWindow = new CustomerSide.MainScreenCus();
                    customerWindow.Show();
                }
                else
                {
                    MessageBox.Show("Unknown role.");
                    return;
                }

                this.Close(); // Đóng cửa sổ đăng nhập
            }
            else
            {
                MessageBox.Show("Invalid password.");
            }
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            Register registerWindow = new Register();
            registerWindow.Show();
            this.Close();
        }
    }
}
