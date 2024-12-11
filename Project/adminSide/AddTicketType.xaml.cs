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
    /// Interaction logic for AddTicketType.xaml
    /// </summary>
    public partial class AddTicketType : Window
    {
        private TicketType _ticketType;
        private TicketManagement _ticketManagement;
        public AddTicketType()
        {
            InitializeComponent();
            _ticketManagement = new TicketManagement(); // Khởi tạo TicketManagement.
            _ticketType = new TicketType(); // Khởi tạo TicketType.
        }
        public delegate void TicketTypeAddedHandler();
        public event TicketTypeAddedHandler? TicketTypeAdded;

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            String name = typeNameTextBox.Text;
            int.TryParse(priceTextBox.Text, out int price);
            int.TryParse(validityDaysTextBox.Text, out int day);
            String description = descriptionTextBox.Text;
            _ticketManagement.AddTicketType(name, price, day, description);
            MessageBox.Show("Ticket Type added successfully!");
           
            this.Close();
        }


        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Khôi phục các trường về giá trị gốc
            typeNameTextBox.Text = _ticketType.TypeName;
            priceTextBox.Text = _ticketType.Price.ToString();
            validityDaysTextBox.Text = _ticketType.ValidityDays?.ToString();
            descriptionTextBox.Text = _ticketType.Description;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            if (Owner is MainScreenAdmin mainScreenAdmin)
            {
                mainScreenAdmin.LoadTicketType(sender: null, null); // Gọi lại phương thức LoadTicketType trên MainScreenAdmin.
            }
        }
    }
}
