using Project.Models;
using Project.viewModel;
using System;
using System.Windows;

namespace Project.adminSide
{
    public partial class CustomerDetailWindow : Window
    {
        private PersonalInfo _customer;

        public CustomerDetailWindow(PersonalInfo customer)
        {
            InitializeComponent();
            _customer = customer;
            LoadCustomerDetails();
        }

        private void LoadCustomerDetails()
        {
            idTextBox.Text = _customer.Id.ToString();
            nameTextBox.Text = _customer.Name;
            emailTextBox.Text = _customer.Email;
            balanceTextBox.Text = _customer.Balance?.ToString();
            parkingDateTextBox.Text = _customer.ParkingDate?.ToString("yyyy-MM-dd");
            retrievalDateTextBox.Text = _customer.RetrievalDate?.ToString("yyyy-MM-dd");
            licensePlateTextBox.Text = _customer.LicensePlate;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _customer.Name = nameTextBox.Text.Trim();
                _customer.Email = emailTextBox.Text.Trim();
                _customer.Balance = string.IsNullOrWhiteSpace(balanceTextBox.Text) ? null : (int?)int.Parse(balanceTextBox.Text);
                _customer.ParkingDate = string.IsNullOrWhiteSpace(parkingDateTextBox.Text) ? null : DateTime.Parse(parkingDateTextBox.Text);
                _customer.RetrievalDate = string.IsNullOrWhiteSpace(retrievalDateTextBox.Text) ? null : DateTime.Parse(retrievalDateTextBox.Text);
                _customer.LicensePlate = licensePlateTextBox.Text.Trim();

                CustomerManagement customerManagement = new CustomerManagement();
                customerManagement.UpdateCustomer(_customer);

                MessageBox.Show("Customer details saved successfully!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            LoadCustomerDetails();
        }
    }
}
