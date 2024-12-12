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
    /// Interaction logic for InfoUserPopup.xaml
    /// </summary>
    public partial class InfoUserPopup : Window
    {
        public PersonalInfo PersonalInfo { get; set; }

        public InfoUserPopup(PersonalInfo personalInfo)
        {
            InitializeComponent();
            PersonalInfo = personalInfo;

            // Gán giá trị cho các trường
            txtId.Text = PersonalInfo.Id.ToString();
            txtName.Text = PersonalInfo.Name;
            txtEmail.Text = PersonalInfo.Email;
            txtLicensePlate.Text = PersonalInfo.LicensePlate;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Cho phép chỉnh sửa thông tin
            txtName.IsEnabled = true;
            txtEmail.IsEnabled = true;
            txtLicensePlate.IsEnabled = true;
            btnSave.IsEnabled = true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Lưu thông tin
            PersonalInfo.Name = txtName.Text;
            PersonalInfo.Email = txtEmail.Text;
            PersonalInfo.LicensePlate = txtLicensePlate.Text;
            ParkingManagementContext.Ins.SaveChanges();
            // Đóng popup
            this.DialogResult = true;
            this.Close();
        }
    }
}
