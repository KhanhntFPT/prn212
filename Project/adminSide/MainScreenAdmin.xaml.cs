using Microsoft.Identity.Client;
using Project.Model;
using Project.Models;
using Project.viewModel;
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

namespace Project.adminSide
{
    /// <summary>
    /// Interaction logic for MainScreenAdmin.xaml
    /// </summary>
    public partial class MainScreenAdmin : Window
    {
        bool isTicket = false;
        bool isAccount = false;
        bool isRenevue = false;
        bool isPark = false;

        private bool isDeleting = false;
        public MainScreenAdmin()
        {
            InitializeComponent();
        }
        private void load()
        {
            if (isTicket)
            {
                TicketGrid.Visibility = Visibility.Visible;
                AccountGrid.Visibility = Visibility.Collapsed;
                ParkingLotGrid.Visibility = Visibility.Collapsed;
                ParkTimeGrid.Visibility = Visibility.Collapsed;
            }
            else { TicketGrid.Visibility = Visibility.Collapsed; }
            if (isAccount)
            {
                TicketGrid.Visibility = Visibility.Collapsed;
                AccountGrid.Visibility = Visibility.Visible;
                ParkingLotGrid.Visibility = Visibility.Collapsed;
                ParkTimeGrid.Visibility = Visibility.Collapsed;
            }
            else { AccountGrid.Visibility = Visibility.Collapsed; }
            if (isPark)
            {
                TicketGrid.Visibility = Visibility.Collapsed;
                AccountGrid.Visibility = Visibility.Collapsed;
                ParkingLotGrid.Visibility = Visibility.Visible;
                ParkTimeGrid.Visibility = Visibility.Collapsed;
            }
            else { ParkingLotGrid.Visibility = Visibility.Collapsed; }
            if (isRenevue)
            {
                TicketGrid.Visibility = Visibility.Collapsed;
                AccountGrid.Visibility = Visibility.Collapsed;
                ParkingLotGrid.Visibility = Visibility.Collapsed;
                ParkTimeGrid.Visibility = Visibility.Visible;
            }
            else { ParkTimeGrid.Visibility = Visibility.Collapsed; }
        }
        private void TicketButton_Click(object sender, RoutedEventArgs e)
        {
            isRenevue = false;
            isAccount = false;
            isPark = false;
            isTicket = true;
            ticketTypeGrid.Visibility = Visibility.Visible;
            load();
            LoadTicketType(null, null);
        }
        private void RevenueButton_Click(object sender, RoutedEventArgs e)
        {
            isRenevue = true;
            isAccount = false;
            isPark = false;
            isTicket = false;
            parkTimeGrid.Visibility = Visibility.Visible;
            load();
        }

        private void ParkingButton_Click(object sender, RoutedEventArgs e)
        {
            isRenevue = false;
            isAccount = false;
            isPark = true;
            isTicket = false;
            parkingLotGrid.Visibility = Visibility.Visible;
            load();
            LoadParkingLotData(null, null);
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            isRenevue = false;
            isAccount = true;
            isPark = false;
            isTicket = false;
            accountGrid.Visibility = Visibility.Visible;
            load();
            LoadAccountData(null,null);
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
        public void LoadTicketButton_Click(object sender, RoutedEventArgs e)
        {
            TicketManagement ticketManagement = new TicketManagement();
            List<TicketType> ticketTypes = ticketManagement.GetTicketTypes();
            ticketTypeGrid.ItemsSource = ticketTypes;
        }
        private void LoadRevenueButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected start and end dates
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;

            // Validate date selection
            if (!startDate.HasValue || !endDate.HasValue)
            {
                MessageBox.Show("Please select both start and end dates.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (startDate > endDate)
            {
                MessageBox.Show("Start date must be earlier than or equal to end date.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Query the database for the data
                using (var context = new ParkingManagementContext())
                {
                    // Count the number of cars parked in the selected time range
                    int parkedCarsCount = context.ParkTimes
                        .Where(pt => pt.ParkedTime >= startDate && pt.RetrievedTime <= endDate)
                        .Count();

                    // Count the number of cars currently parked
                    int currentlyParkedCount = context.ParkTimes
                        .Where(pt => pt.ParkedTime == null)
                        .Count();

                    // Calculate the total payment in the selected time range
                    int totalPayment = context.ParkTimes
                        .Where(pt => pt.ParkedTime >= startDate && pt.RetrievedTime <= endDate)
                        .Sum(pt => pt.TotalAmount ?? 0);

                    // Update the UI with the calculated values
                    ParkedCarsLabel.Content = parkedCarsCount.ToString();
                    CurrentlyParkedLabel.Content = currentlyParkedCount.ToString();
                    TotalPaymentLabel.Content = totalPayment.ToString("N0"); // Format as a number with commas
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void LoadAccountButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerManagement customerManagement = new CustomerManagement();
            var customers = customerManagement.GetCustomers();
            accountGrid.ItemsSource = customers;
        }
        public void LoadParkingLotButton_Click(object sender, RoutedEventArgs e)
        {
            ParkingLotManagement parkingLotManagement = new ParkingLotManagement();
            var parkingLots = parkingLotManagement.GetParkingLots();
            parkingLotGrid.ItemsSource = parkingLots;
        }
        private void ticketTypeGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (ticketTypeGrid.SelectedItem is TicketType selectedTicket)
            {
                // Mở cửa sổ chi tiết với thông tin của ticketType đã chọn
                TicketTypeDetail detailWindow = new TicketTypeDetail(selectedTicket);
                detailWindow.ShowDialog(); // Sử dụng ShowDialog để chờ cho đến khi cửa sổ đóng lại
            }
        }
        public void LoadTicketType(object sender, RoutedEventArgs e)
        {
            TicketManagement ticketManagement = new TicketManagement();
            List<TicketType> ticketTypes = ticketManagement.GetTicketTypes();
            ticketTypeGrid.ItemsSource = ticketTypes;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = searchBox.Text.Trim().ToLower();
            TicketManagement ticketManagement = new TicketManagement();
            List<TicketType> filteredTickets = ticketManagement.SearchTicketByName(searchTerm);
            ticketTypeGrid.ItemsSource = filteredTickets;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddTicketType ticketType = new AddTicketType();
            ticketType.ShowDialog();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton && deleteButton.Tag is int typeId)
            {
                // Xác nhận xóa
                var result = MessageBox.Show($"Are you sure you want to delete this Ticket Type?",
                                             "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    // Gọi phương thức xóa
                    TicketManagement ticketManagement = new TicketManagement();
                    try
                    {
                        ticketManagement.RemoveTicketType(typeId);
                        MessageBox.Show("Ticket Type deleted successfully!");

                        // Tải lại DataGrid sau khi xóa
                        LoadTicketType(null, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
        }
        private void customerGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (isDeleting) return;
            if (accountGrid.SelectedItem is PersonalInfo selectedCustomer)
            {
                // Hiển thị cửa sổ chi tiết với thông tin khách hàng đã chọn
                CustomerDetailWindow detailWindow = new CustomerDetailWindow(selectedCustomer);
                detailWindow.ShowDialog(); // Chờ cho đến khi cửa sổ đóng
            }
        }

        public void LoadAccountData(object sender, RoutedEventArgs e)
        {
            CustomerManagement customerManagement = new CustomerManagement();
            var customers = customerManagement.GetCustomers();
            accountGrid.ItemsSource = customers;
        }

        private void SearchAccountButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = searchBox.Text.Trim().ToLower();
            CustomerManagement customerManagement = new CustomerManagement();
            var filteredCustomers = customerManagement.SearchCustomerByName(searchTerm);
            accountGrid.ItemsSource = filteredCustomers;
        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            AddCustomerWindow addCustomerWindow = new AddCustomerWindow();
            addCustomerWindow.ShowDialog();
        }

        private void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            isDeleting = true;
            try
            {
                if (accountGrid.SelectedItem is PersonalInfo selectedCustomer)
                {
                    // Xác nhận xóa
                    var result = MessageBox.Show($"Are you sure you want to delete the customer: {selectedCustomer.Name}?",
                                                 "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        CustomerManagement customerManagement = new CustomerManagement();
                        try
                        {
                            customerManagement.RemoveCustomer(selectedCustomer.Id);
                            MessageBox.Show("Customer deleted successfully!");
                            // Tải lại DataGrid sau khi xóa
                            LoadAccountData(null, null);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a customer to delete.");
                }
            }
            finally { isDeleting = false; }
        }
        public void LoadParkingLotData(object sender, RoutedEventArgs e)
        {
            ParkingLotManagement parkingLotManagement = new ParkingLotManagement();
            var parkingLots = parkingLotManagement.GetParkingLots();
            parkingLotGrid.ItemsSource = parkingLots;
        }

        // Thêm ParkingLot
        private void AddParkingButton_Click(object sender, RoutedEventArgs e)
        {
            AddParkingLotWindow addParkingLotWindow = new AddParkingLotWindow();
            addParkingLotWindow.ShowDialog();
        }

        // Tìm kiếm ParkingLot
        private void SearchParkingButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = searchBox.Text.Trim().ToLower();
            ParkingLotManagement parkingLotManagement = new ParkingLotManagement();
            var filteredParkingLots = parkingLotManagement.SearchParkingLot(searchTerm);
            parkingLotGrid.ItemsSource = filteredParkingLots;
        }

        // Xóa ParkingLot
        private void DeleteParkingButton_Click(object sender, RoutedEventArgs e)
        {
            isDeleting = true;
            try
            {
                if (parkingLotGrid.SelectedItem is ParkingLotDTO selectedParkingLot)
                {
                    // Xác nhận xóa
                    var result = MessageBox.Show($"Are you sure you want to delete Parking Lot {selectedParkingLot.LotId}?",
                                                 "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        ParkingLotManagement parkingLotManagement = new ParkingLotManagement();
                        try
                        {
                            parkingLotManagement.RemoveParkingLot(selectedParkingLot.LotId);
                            MessageBox.Show("Parking Lot deleted successfully!");
                            LoadParkingLotData(null, null);  // Tải lại danh sách sau khi xóa
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a Parking Lot to delete.");
                }
            }
            finally { isDeleting = false; }
        }

        // Chỉnh sửa ParkingLot (chỉnh sửa từ grid)
        private void parkingLotGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (isDeleting) return;
            if (parkingLotGrid.SelectedItem is ParkingLotDTO selectedParkingLot)
            {
                // Hiển thị cửa sổ chi tiết để chỉnh sửa thông tin ParkingLot đã chọn
                EditParkingLotWindow editWindow = new EditParkingLotWindow(selectedParkingLot);
                editWindow.ShowDialog();
            }
        }
        

        // Delete a specific Park Time record
        private void DeleteParkTimeButton_Click(object sender, RoutedEventArgs e)
        {
        //    // Get the ParkTimeId from the clicked button's Tag
        //    var button = sender as Button;
        //    var parkTimeId = (int)button.Tag;

        //    // Delete logic (this can be a call to the database or data service)
        //    // For example, remove it from the list or database
        //    DeleteParkTimeRecord(parkTimeId);

        //    // Refresh the grid after deletion
        //    var parkTimeData = GetParkTimes();
        //    parkTimeGrid.ItemsSource = parkTimeData;
        }

        // Search Park Time records based on user input
        private void SearchParkTimeButton_Click(object sender, RoutedEventArgs e)
        {
            //// Get search criteria from the user (e.g., UserId or TicketId)
            //var searchTerm = searchBox.Text.Trim();

            //// Call the method to filter the data based on the search term
            //var filteredData = SearchParkTimeData(searchTerm);
            //parkTimeGrid.ItemsSource = filteredData;
        }

        private void parkTimeGrid_Loaded(object sender, RoutedEventArgs e)
        {
            ParkTimeManagement p = new ParkTimeManagement();
            List<ParkTime> pt = p.GetParkTimes();
            parkTimeGrid.ItemsSource = pt;
        }
    }



}


