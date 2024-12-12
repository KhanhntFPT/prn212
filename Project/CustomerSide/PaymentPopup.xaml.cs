using Project.Models;
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

namespace Project.CustomerSide
{
    public partial class PaymentPopup : Window
    {
        private Account _account;
        private ParkingLot _parkingLot;
        private long _amount;
        public PaymentPopup(Account account, ParkingLot parkingLot,long _amount)
        {
            InitializeComponent();
            _account = account;
            _parkingLot = parkingLot;
            giovao.Text = $"{_account.PersonalInfo.ParkingDate?.ToString("g") ?? "N/A"}";
            giora.Text = $"{_account.PersonalInfo.RetrievalDate?.ToString("g") ?? "N/A"}";
            // Show parking fee and user balance
            ParkingFeeText.Text = $"Phí đỗ xe : {_amount:C}";
            NameText.Text = $"{_account.Username:C}";
            bienso.Text = $"{_account.PersonalInfo.LicensePlate:C}";
            BalanceText.Text = $"Số dư tài khoản: {_account.PersonalInfo.Balance:C}";
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if the user has enough balance
            if (_account.PersonalInfo.Balance >= _amount)
            {
                // Deduct the fee from the user's balance
                _account.PersonalInfo.Balance -= (int)_amount;

                // Update parking lot status to "Available" and clear the UserId
                _parkingLot.Status = "Available";
                _parkingLot.UserId = null;

                // Save changes to the database
                ParkingManagementContext.Ins.SaveChanges();

                // Inform the user
                MessageBox.Show("Thanh toán thành công! Xe đã được trả lại.", "Thanh toán", MessageBoxButton.OK, MessageBoxImage.Information);

                // Close the payment popup
                this.Close();
            }
            else
            {
                // Show error if balance is insufficient
                MessageBox.Show("Số dư tài khoản không đủ. Vui lòng nạp tiền.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
