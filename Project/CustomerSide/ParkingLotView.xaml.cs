using Microsoft.EntityFrameworkCore;
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
    /// <summary>
    /// Interaction logic for ParkingLotView.xaml
    /// </summary>
    public partial class ParkingLotView : Window
    {
        Account _account;
        long _amount;
        public ParkingLotView(Account account)
        {
            InitializeComponent();
            ShowParkingInfo(account.Id, this);
            UserNameText.Text = account.Username;
            _account = account;
        }
        private void ShowParkingInfo(int userId, ParkingLotView parkingLotView)
        {
            // Retrieve customer data
            var customerInfo = GetCustomerData(userId);
            if (customerInfo == null)
            {
                MessageBox.Show("Bạn chưa gửi xe");
                return;
            }
            if(customerInfo.Status.Equals("Available"))
            {
                MessageBox.Show("Bạn chưa gửi xe!");
                return;
            }
            CustomerIdText.Text = $"KH{customerInfo.Employee.Id:D4}";
            CustomerNameText.Text = customerInfo.Employee.Username;
      
            LicensePlateText.Text = customerInfo.Employee.PersonalInfo.LicensePlate;
            ParkingDateText.Text = customerInfo.Employee.PersonalInfo.ParkingDate?.ToString("g") ?? "N/A";
            RetrievalDateText.Text = customerInfo.Employee.PersonalInfo.RetrievalDate?.ToString("g") ?? "N/A";
            if (customerInfo.Employee.PersonalInfo.ParkingDate.HasValue)
            {
                var parkingDate = customerInfo.Employee.PersonalInfo.ParkingDate.Value.Date; // Only use the date part
                var retrievalDate = (customerInfo.Employee.PersonalInfo.RetrievalDate ?? DateTime.Now).Date; // Default to today if retrieval date is null

                // Ensure at least 1 day is counted
                int totalDays = (retrievalDate - parkingDate).Days + 1;
                int dailyRate = customerInfo.Amount ?? 0; // Assume Amount holds the daily parking fee
                FeeText.Text = (totalDays * dailyRate).ToString("C"); // Calculate total fee and format as currency
                _amount = totalDays * dailyRate;
            }
            else
            {
                FeeText.Text = "N/A"; // Show N/A if parking date is missing
            } // Placeholder fee
            ParkingInfoView.Visibility = Visibility.Visible;
        }

        private ParkingLot GetCustomerData(int userId)
        {
            return ParkingManagementContext.Ins.ParkingLots.Include(info => info.Employee).Include(info => info.Employee.PersonalInfo).FirstOrDefault(info => info.UserId == userId);
        }
        private void BtnParkingLot_Click(object sender, RoutedEventArgs e)
        {
            FormParkingLot formParkingLot = new FormParkingLot(_account);
            this.Close();
            formParkingLot.Show();
        }
        private void RetrieveCarButton_Click(object sender, RoutedEventArgs e)
        {
            PaymentPopup paymentPopup = new PaymentPopup(_account, GetCustomerData(_account.Id),_amount);
            paymentPopup.ShowDialog();
        }
        private void BtnLogout(object sender, RoutedEventArgs e)
        {
            var parkingInfoView = new Login();
            this.Close();
            parkingInfoView.Show();
        }
        private void BtnInfo_Click1(object sender, RoutedEventArgs e)
        {
            var parkingInfoView = new InfoUserPopup(ParkingManagementContext.Ins.PersonalInfos.FirstOrDefault(info => info.Id == _account.Id));
            parkingInfoView.ShowDialog();
        }

    }
}
