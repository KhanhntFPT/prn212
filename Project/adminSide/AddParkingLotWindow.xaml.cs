using Project.viewModel;
using System;
using System.Windows;

namespace Project.adminSide
{
    public partial class AddParkingLotWindow : Window
    {
        public AddParkingLotWindow()
        {
            InitializeComponent();
        }

        // Thêm parking lot
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string lotSector = lotSectorTextBox.Text;
            int? userId = int.TryParse(userIdTextBox.Text, out int tempUserId) ? tempUserId : (int?)null;
            string status = statusTextBox.Text;
            int? amount = int.TryParse(amountTextBox.Text, out int tempAmount) ? tempAmount : (int?)null;

            ParkingLotManagement parkingLotManagement = new ParkingLotManagement();
            try
            {
                parkingLotManagement.AddParkingLot(lotSector, userId, status, amount);
                MessageBox.Show("Parking Lot added successfully!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            lotSectorTextBox.Clear();
            userIdTextBox.Clear();
            statusTextBox.Clear();
            amountTextBox.Clear();
        }

    }
}
