using Project.Model;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.viewModel
{
    public class ParkTimeManagement
    {
        // Get all ParkTime records
        public List<ParkTime> GetParkTimes()
        {
            using (ParkingManagementContext context = new ParkingManagementContext())
            {
                return context.ParkTimes.ToList();
            }
        }

        // Search ParkTime records by user or ticket ID (example)
        public List<ParkTime> SearchParkTimeByUserId(int userId)
        {
            using (ParkingManagementContext context = new ParkingManagementContext())
            {
                return context.ParkTimes.Where(pt => pt.UserId == userId).ToList();
            }
        }

        // Add a new ParkTime record
        public void AddParkTime(int userId, int parkingLotId, int ticketId, DateTime parkedTime, DateTime retrievedTime, int totalAmount)
        {
            using (ParkingManagementContext context = new ParkingManagementContext())
            {
                ParkTime parkTime = new ParkTime
                {
                    UserId = userId,
                    ParkingLotId = parkingLotId,
                    TicketId = ticketId,
                    ParkedTime = parkedTime,
                    RetrievedTime = retrievedTime,
                    TotalAmount = totalAmount
                };
                context.ParkTimes.Add(parkTime);
                context.SaveChanges();
            }
        }

        // Update an existing ParkTime record
        public void UpdateParkTime(ParkTime updatedParkTime)
        {
            using (ParkingManagementContext context = new ParkingManagementContext())
            {
                var existingParkTime = context.ParkTimes.FirstOrDefault(pt => pt.ParkTimeId == updatedParkTime.ParkTimeId);
                if (existingParkTime != null)
                {
                    existingParkTime.UserId = updatedParkTime.UserId;
                    existingParkTime.ParkingLotId = updatedParkTime.ParkingLotId;
                    existingParkTime.TicketId = updatedParkTime.TicketId;
                    existingParkTime.ParkedTime = updatedParkTime.ParkedTime;
                    existingParkTime.RetrievedTime = updatedParkTime.RetrievedTime;
                    existingParkTime.TotalAmount = updatedParkTime.TotalAmount;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("ParkTime not found");
                }
            }
        }

        // Remove a ParkTime record by ID
        public void RemoveParkTime(int parkTimeId)
        {
            using (ParkingManagementContext context = new ParkingManagementContext())
            {
                var parkTimeToRemove = context.ParkTimes.FirstOrDefault(pt => pt.ParkTimeId == parkTimeId);
                if (parkTimeToRemove != null)
                {
                    context.ParkTimes.Remove(parkTimeToRemove);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("ParkTime not found");
                }
            }
        }
        private int GetParkedCarsCount(DateTime startDate, DateTime endDate)
        {
            using (var context = new ParkingManagementContext())
            {
                return context.ParkTimes
                              .Where(pt => pt.ParkedTime >= startDate && pt.ParkedTime <= endDate)
                              .Count();
            }
        }
        private int GetTotalPayment(DateTime startDate, DateTime endDate)
        {
            using (var context = new ParkingManagementContext())
            {
                return context.ParkTimes
                              .Where(pt => pt.ParkedTime >= startDate && pt.ParkedTime <= endDate)
                              .Sum(pt => (int?)pt.TotalAmount) ?? 0; // Xử lý null bằng 0
            }
        }
        private int GetCurrentlyParkedCarsCount()
        {
            using (var context = new ParkingManagementContext())
            {
                return context.ParkTimes
                              .Where(pt => pt.RetrievedTime == null)
                              .Count();
            }
        }


    }
}
