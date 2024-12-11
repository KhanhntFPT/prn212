using Project.Models;
using Project.viewModel;
using System.Windows;

namespace Project.adminSide
{
    public partial class TicketTypeDetail : Window
    {
        private TicketType _ticketType;

        public TicketTypeDetail(TicketType ticketType)
        {
            InitializeComponent();
            _ticketType = ticketType;

            // Hiển thị thông tin của TicketType vào các trường
            typeIdTextBox.Text = _ticketType.TypeId.ToString();
            typeNameTextBox.Text = _ticketType.TypeName;
            priceTextBox.Text = _ticketType.Price.ToString();
            validityDaysTextBox.Text = _ticketType.ValidityDays?.ToString();
            descriptionTextBox.Text = _ticketType.Description;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ các trường và cập nhật thông tin
            _ticketType.TypeName = typeNameTextBox.Text;
            _ticketType.Price = int.Parse(priceTextBox.Text);
            _ticketType.ValidityDays = string.IsNullOrEmpty(validityDaysTextBox.Text) ? (int?)null : int.Parse(validityDaysTextBox.Text);
            _ticketType.Description = descriptionTextBox.Text;

            // Gọi phương thức cập nhật từ TicketManagement
            TicketManagement ticketManagement = new TicketManagement();
            ticketManagement.UpdateTicketType(_ticketType);

            // Đóng cửa sổ sau khi lưu thành công
            MessageBox.Show("Ticket Type updated successfully!");
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
    }
}
