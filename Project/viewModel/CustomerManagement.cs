using Microsoft.AspNetCore.Identity;
using Project.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Project.viewModel
{
    public class CustomerManagement
    {
        public List<PersonalInfo> GetCustomers()
        {
            ParkingManagementContext context = new ParkingManagementContext();
            return context.PersonalInfos.ToList();
        }

        public List<PersonalInfo> SearchCustomerByName(string search)
        {
            ParkingManagementContext context = new ParkingManagementContext();
            return context.PersonalInfos.Where(customer => customer.Name.ToLower().Contains(search.ToLower())).ToList();
        }

        public void UpdateCustomer(PersonalInfo updatedCustomer)
        {
            ParkingManagementContext context = new ParkingManagementContext();
            var existingCustomer = context.PersonalInfos.FirstOrDefault(c => c.Id == updatedCustomer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.Name = updatedCustomer.Name;
                existingCustomer.Email = updatedCustomer.Email;
                existingCustomer.Balance = updatedCustomer.Balance;
                existingCustomer.ParkingDate = updatedCustomer.ParkingDate;
                existingCustomer.RetrievalDate = updatedCustomer.RetrievalDate;
                existingCustomer.LicensePlate = updatedCustomer.LicensePlate;

                context.SaveChanges();
            }
            else
            {
                throw new Exception("Customer not found");
            }
        }

        public void AddCustomer(string name, string email, int? balance, string licensePlate, string password)
        {
            using (var context = new ParkingManagementContext())
            {
                // 1. Kiểm tra Email hợp lệ
                if (string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Email is required!");
                    return;
                }

                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(email, emailPattern))
                {
                    MessageBox.Show("Invalid email address format!");
                    return;
                }

                // 2. Kiểm tra sự tồn tại của Email
                var existingAccount = context.Accounts.FirstOrDefault(a => a.Username == email);
                if (existingAccount != null)
                {
                    MessageBox.Show("Email address already exists!");
                    return;
                }

                // 3. Kiểm tra Mật khẩu hợp lệ
                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Password is required!");
                    return;
                }

                if (password.Length < 4)
                {
                    MessageBox.Show("Password length must be greater than 4 characters!");
                    return;
                }

                bool hasLetter = password.Any(char.IsLetter);
                bool hasDigit = password.Any(char.IsDigit);
                if (!hasLetter || !hasDigit)
                {
                    MessageBox.Show("Password must contain both letters and numbers!");
                    return;
                }

                // 4. Tạo Account và hash mật khẩu
                Account newAccount = new Account
                {
                    Username = email,
                    Role = "customer"
                };

                PasswordHasher<string> hasher = new PasswordHasher<string>();
                string hashedPassword = hasher.HashPassword(null, password);
                newAccount.Password = hashedPassword;

                // 5. Lưu Account vào cơ sở dữ liệu
                context.Accounts.Add(newAccount);
                context.SaveChanges(); // Lưu tài khoản vào cơ sở dữ liệu

                // 6. Tạo PersonalInfo và liên kết với Account
                PersonalInfo newCustomer = new PersonalInfo
                {
                    Name = name,
                    Email = email,
                    Balance = balance,
                    LicensePlate = licensePlate,
                    IdNavigation = newAccount // Liên kết với tài khoản
                };

                // 7. Lưu PersonalInfo vào cơ sở dữ liệu
                context.PersonalInfos.Add(newCustomer);
                context.SaveChanges();

                MessageBox.Show("Customer registered successfully!");
            }
        }

        public void RemoveCustomer(int id)
        {
            using (ParkingManagementContext context = new ParkingManagementContext())
            {
                var customerToRemove = context.PersonalInfos.FirstOrDefault(c => c.Id == id);
                if (customerToRemove != null)
                {
                    context.PersonalInfos.Remove(customerToRemove);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Customer not found");
                }
            }
        }
    }
}
