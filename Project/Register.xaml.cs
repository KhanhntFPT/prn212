using Microsoft.AspNetCore.Identity;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace Project
{
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void LoginHere_Click(object sender, RoutedEventArgs e)
        {
            // Create and show the login window
            Login loginWindow = new Login();
            loginWindow.Show();

            // Optionally, close the current register window
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string Fullname = NameTextBox.Text;
            string Email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            string LicensePlate = LicensePlateTextBox.Text;

            if (string.IsNullOrEmpty(Email))
            {
                MessageBox.Show("Enter Email!");
                return;
            }

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(Email, emailPattern))
            {
                MessageBox.Show("Invalid email address");
                return;
            }

            var CusList = ParkingManagementContext.Ins.Accounts.ToList();
            foreach (var cus in CusList)
            {
                if (cus.Username.Equals(Email))
                {
                    MessageBox.Show("Email address already exists");
                    return;
                }
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Enter password!");
                return;
            }
            if (password.Length < 4)
            {
                MessageBox.Show("Password length must be greater than 4!");
                return;
            }
            // Kiểm tra mật khẩu có chứa cả chữ cái và số
            bool hasLetter = password.Any(char.IsLetter);
            bool hasDigit = password.Any(char.IsDigit);

            if (!hasLetter || !hasDigit)
            {
                MessageBox.Show("Password must contain both letters and numbers!");
                return;
            }

            Account newCus = new Account();
            newCus.Username = Email;
            newCus.Role = "customer";

            PersonalInfo personalInfo = new PersonalInfo();
            personalInfo.Id = newCus.Id;
            personalInfo.Email = Email;
            personalInfo.Name = Fullname;
            personalInfo.LicensePlate = LicensePlate;

            PasswordHasher<string> hasher = new PasswordHasher<string>();
            string hashedPassword = hasher.HashPassword(null, password);
            newCus.Password = hashedPassword;

            Verify verify = new Verify(Email);
            verify.ShowDialog();
            if (verify.issucc == true)
            {
                ParkingManagementContext.Ins.Add(newCus);
                ParkingManagementContext.Ins.SaveChanges();
                MessageBox.Show("Register Successful");
                Login login = new Login();
                login.Show();
            }
            else
            {
                MessageBox.Show("Register Failed");
            }

            this.Close();
        }
    }
}
