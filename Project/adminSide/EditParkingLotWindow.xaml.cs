using Project.Models;
using Project.viewModel;
using System;
using System.Windows;

namespace Project.adminSide
{
    public partial class EditParkingLotWindow : Window
    {
        private ParkingLotDTO _selectedParkingLot;

        // Constructor
        public EditParkingLotWindow(ParkingLotDTO selectedParkingLot)
        {
            InitializeComponent();
            _selectedParkingLot = selectedParkingLot;

            // Populate the fields
            lotSectorTextBox.Text = _selectedParkingLot.LotSector;
            userIdTextBox.Text = _selectedParkingLot.UserId?.ToString() ?? "N/A"; // Hiển thị giá trị hiện tại
            nameTextBox.Text = _selectedParkingLot.Name ?? "N/A";
            licensePlateTextBox.Text = _selectedParkingLot.LicensePlate ?? "N/A";
            statusTextBox.Text = _selectedParkingLot.Status;
            amountTextBox.Text = _selectedParkingLot.Amount?.ToString() ?? string.Empty;
        }


        // Save changes to the parking lot
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            // Validate inputs
            if (string.IsNullOrWhiteSpace(lotSectorTextBox.Text))
            {
                MessageBox.Show("Lot Sector cannot be empty.");
                return;
            }

            // Update only editable fields
            _selectedParkingLot.LotSector = lotSectorTextBox.Text;
            _selectedParkingLot.Status = statusTextBox.Text;
            _selectedParkingLot.Amount = string.IsNullOrWhiteSpace(amountTextBox.Text)
                ? null
                : int.Parse(amountTextBox.Text);

            ParkingLotManagement parkingLotManagement = new ParkingLotManagement();
            try
            {
                parkingLotManagement.UpdateParkingLot(_selectedParkingLot); // Cập nhật dữ liệu
                MessageBox.Show("Parking Lot updated successfully!");
                MainScreenAdmin mainScreenAdmin = new MainScreenAdmin();
                mainScreenAdmin.LoadParkingLotData(null, null);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }


        // Reset button functionality
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            lotSectorTextBox.Text = _selectedParkingLot.LotSector;
            userIdTextBox.Text = _selectedParkingLot.UserId.ToString() ?? "N/A";
            nameTextBox.Text = _selectedParkingLot.Name ?? "N/A";
            licensePlateTextBox.Text = _selectedParkingLot.LicensePlate ?? "N/A";
            statusTextBox.Text = _selectedParkingLot.Status;
            amountTextBox.Text = _selectedParkingLot.Amount?.ToString() ?? string.Empty;
        }
    }
}
