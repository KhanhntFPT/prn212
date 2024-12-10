using System.Windows;
using Microsoft.AspNetCore.Identity;
using Project.Models; // Thêm namespace cho lớp xử lý dữ liệu, nếu cần.

namespace Project
{
    public partial class ChangePassword : Window
    {
        public string Email { get; set; } // Thuộc tính lưu giá trị email

        public ChangePassword(string email)
        {
            InitializeComponent();
            this.Email = email; // Gán giá trị email được truyền vào
        }

        private void SetPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = NewPasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                ErrorMessage.Text = "Please fill in both password fields.";
                return;
            }

            if (newPassword != confirmPassword)
            {
                ErrorMessage.Text = "Passwords do not match. Please try again.";
                return;
            }

            // Logic to update password for the given email
            bool isUpdated = UpdatePassword(Email, newPassword);

            if (isUpdated)
            {
                MessageBox.Show("Password successfully updated!");
                Login login = new Login();
                login.Show();
                this.Close();
            }
            else
            {
                ErrorMessage.Text = "An error occurred while updating the password. Please try again.";
            }
        }

        private bool UpdatePassword(string email, string newPassword)
        {
            try
            {
                // Giả sử bạn đang sử dụng Entity Framework để cập nhật dữ liệu
                using (var context = new ParkingManagementContext())
                {
                    var user = context.Accounts.SingleOrDefault(u => u.Username == email);
                    if (user != null)
                    {
                        PasswordHasher<string> hasher = new PasswordHasher<string>();
                        string hashedPassword = hasher.HashPassword(email, newPassword);
                        user.Password = hashedPassword; 
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("No user found with this email.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
