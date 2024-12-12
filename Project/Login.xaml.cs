using System;
using System.Linq;
using System.Windows;
using Project.Models; // Đảm bảo namespace đúng với lớp Account
using Microsoft.AspNetCore.Identity;
using Project.CustomerSide;

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
            using (var context = new ParkingManagementContext()) // Tạo context mới
            {
                var account = ParkingManagementContext.Ins.Accounts
                .FirstOrDefault(a => a.Username.Equals(email)&&a.Password.Equals(password));

                if (account == null)
                {
                    MessageBox.Show("Account not found.");
                    return;
                }
                context.Entry(account).Reload();
                //var passwordHasher = new PasswordHasher<string>();
                //var result = passwordHasher.VerifyHashedPassword(null, account.Password, password);

                if (account!= null)
                {
                    if (account.Role == "admin")
                    {
                        adminSide.MainScreenAdmin adminWindow = new adminSide.MainScreenAdmin();
                        adminWindow.Show();
                    }
                    else if (account.Role.Equals("Customer"))
                    {
                        FormParkingLot formParkingLot = new FormParkingLot(account);   
                        formParkingLot.Show();
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
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            Register registerWindow = new Register();
            registerWindow.Show();
            this.Close();
        }

        private void ForgotPassword_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Password forgotPass = new Password();
            forgotPass.Show();
            this.Close();
        }

        private void Login_Load(object sender, RoutedEventArgs e)
        {

        }
    }
}
