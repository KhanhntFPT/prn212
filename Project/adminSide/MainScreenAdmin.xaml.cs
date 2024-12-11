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
        public MainScreenAdmin()
        {
            InitializeComponent();
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
    }
}
