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
    /// Interaction logic for FormParkingLot.xaml
    /// </summary>
    public partial class FormParkingLot : Window
    {
        Account _account;
        public FormParkingLot(Account account)
        {
            InitializeComponent();
            LoadParkingLot();
            UserNameText.Text = account.Username;
            _account = account;
        }

        // Function to load parking lot data and populate UI
        private void LoadParkingLot()
        {
            // Sample data for demonstration purposes
            var parkingLots = ParkingManagementContext.Ins.ParkingLots.ToList();

            foreach (var lot in parkingLots)
            {
                // Create a container for each parking slot
                var slotContainer = new StackPanel
                {
                    Width = 120,
                    Height = 150,
                    Margin = new Thickness(10),
                    Background = lot.Status == "Occupied" ? Brushes.LightGray : Brushes.White,
                    VerticalAlignment = VerticalAlignment.Top
                };

                // Add car image
                var carImage = new Image
                {
                    Source = new BitmapImage(new Uri("pack://application:,,,/Resources/car.png")),
                    Width = 100,
                    Height = 80,
                    Margin = new Thickness(10)
                };
                slotContainer.Children.Add(carImage);

                // Add parking lot ID
                var lotText = new TextBlock
                {
                    Text = lot.LotSector,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                slotContainer.Children.Add(lotText);

                var border = new Border
                {
                    Width = 130,
                    Height = 160,
                    BorderBrush = Brushes.Black, // Color of the border
                    BorderThickness = new Thickness(2), // Thickness of the border
                    CornerRadius = new CornerRadius(5), // Optional: rounded corners
                    Margin = new Thickness(10), // Spacing between borders
                    Child = slotContainer
                };

                // Add the Border to the WrapPanel
                LayoutPanel.Children.Add(border);
            }
        }

        private void BtnReload_Click(object sender, RoutedEventArgs e)
        {
            LayoutPanel.Children.Clear();
            LoadParkingLot();
        }
        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            var parkingInfoView = new ParkingLotView(_account);
            this.Close();
            parkingInfoView.Show();
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
