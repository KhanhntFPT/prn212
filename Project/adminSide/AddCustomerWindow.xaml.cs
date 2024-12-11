using System;
using System.Windows;
using Project.viewModel;

namespace Project.adminSide
{
    public partial class AddCustomerWindow : Window
    {
        private readonly CustomerManagement _customerManagement;

        public AddCustomerWindow()
        {
            InitializeComponent();
            _customerManagement = new CustomerManagement();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ các trường nhập
                string name = nameTextBox.Text;
                string email = emailTextBox.Text;
                int balance = int.TryParse(balanceTextBox.Text, out int result) ? result : 0;
                string licensePlate = licensePlateTextBox.Text;
                string password = passwordBox.Password;

                // Thêm khách hàng vào cơ sở dữ liệu và tạo tài khoản
                _customerManagement.AddCustomer(name, email, balance, licensePlate, password);

                MessageBox.Show("Customer added successfully!");
                this.Close(); // Đóng cửa sổ sau khi thêm
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Reset các trường nhập liệu
            nameTextBox.Clear();
            emailTextBox.Clear();
            balanceTextBox.Clear();
            licensePlateTextBox.Clear();
            passwordBox.Clear();
        }
    }
}
