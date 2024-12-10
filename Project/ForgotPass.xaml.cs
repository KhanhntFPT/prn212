using Microsoft.AspNetCore.Identity;
using Project.Models;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Project
{
    public partial class Password : Window
    {
        public Password()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string Email = EmailTextBox.Text;

            var CusList = ParkingManagementContext.Ins.Accounts.ToList();
            foreach (var cus in CusList)
            {
                if (cus.Username.Equals(Email))
                {
                    // Chuyển hướng sang trang Verify.xaml
                    VerifyForgot verifyWindow = new VerifyForgot(Email);
                    verifyWindow.Show();
                    this.Close();
                    return;
                }
            }
            
        }
    }
}
