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
        public MainScreenAdmin()
        {
            InitializeComponent();
        }
        private void load()
        {
            if (isTicket)
            {
                TicketGrid.Visibility = Visibility.Visible;
                accountGrid.Visibility = Visibility.Collapsed;
                parkingLotGrid.Visibility = Visibility.Collapsed;
                revenueGrid.Visibility = Visibility.Collapsed;
            }
            else { ticketTypeGrid.Visibility = Visibility.Collapsed; }
            if (isAccount)
            {
                TicketGrid.Visibility = Visibility.Collapsed;
                accountGrid.Visibility = Visibility.Visible;
                parkingLotGrid.Visibility = Visibility.Collapsed;
                revenueGrid.Visibility = Visibility.Collapsed;
            }
            else { accountGrid.Visibility = Visibility.Collapsed; }
            if (isPark)
            {
                TicketGrid.Visibility = Visibility.Collapsed;
                accountGrid.Visibility = Visibility.Collapsed;
                parkingLotGrid.Visibility = Visibility.Visible;
                revenueGrid.Visibility = Visibility.Collapsed;
            }
            else { parkingLotGrid.Visibility = Visibility.Collapsed; }
            if (isRenevue)
            {
                TicketGrid.Visibility = Visibility.Collapsed;
                accountGrid.Visibility = Visibility.Collapsed;
                parkingLotGrid.Visibility = Visibility.Collapsed;
                revenueGrid.Visibility = Visibility.Visible;
            }
            else { revenueGrid.Visibility = Visibility.Collapsed; }
        }
        private void TicketButton_Click(object sender, RoutedEventArgs e)
        {
            isRenevue = false;
            isAccount = false;
            isPark = false;
            isTicket = true;
            ticketTypeGrid.Visibility = Visibility.Visible;
            load();
        }
        private void RevenueButton_Click(object sender, RoutedEventArgs e)
        {
            isRenevue = true;
            isAccount = false;
            isPark = false;
            isTicket = false;
            load();
        }

        private void ParkingButton_Click(object sender, RoutedEventArgs e)
        {
            isRenevue = false;
            isAccount = false;
            isPark = true;
            isTicket = false;
            load();
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            isRenevue = false;
            isAccount = true;
            isPark = false;
            isTicket = false;
            load();
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
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
            ticketType.Show();
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

    

    }
}
