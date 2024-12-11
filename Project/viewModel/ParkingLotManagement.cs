using Microsoft.EntityFrameworkCore;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.viewModel
{
    public class ParkingLotManagement
    {
        // Lấy danh sách các ParkingLot
        public List<ParkingLotDTO> GetParkingLots()
        {
            using (var context = new ParkingManagementContext())
            {
                var parkingLots = (from lot in context.ParkingLots
                                   join user in context.PersonalInfos on lot.UserId equals user.Id into userGroup
                                   from user in userGroup.DefaultIfEmpty() // This creates a left join
                                   select new ParkingLotDTO
                                   {
                                       LotId = lot.LotId,
                                       LotSector = lot.LotSector,
                                       Status = lot.Status,
                                       UserId = lot.UserId,
                                       Amount = lot.Amount,
                                       LicensePlate = user != null ? user.LicensePlate : null, // Handle null user
                                       Name = user != null ? user.Name : null // Handle null user
                                   }).ToList();

                return parkingLots;
            }
        }

        // Tìm kiếm ParkingLot theo sector hoặc trạng thái
        public List<ParkingLot> SearchParkingLot(string search)
        {
            using (ParkingManagementContext context = new ParkingManagementContext())
            {
                return context.ParkingLots
                    .Where(parkingLot => parkingLot.User != null &&
                                         parkingLot.User.Name.ToLower().Contains(search.ToLower()))
                    .ToList();
            }
        }
        public List<ParkingLotDTO> FilterParkingLots(String selectedIndex)
        {
            using (var context = new ParkingManagementContext())
            {
                // Build the query with a left join
                var query = from lot in context.ParkingLots
                            join user in context.PersonalInfos on lot.UserId equals user.Id into userGroup
                            from user in userGroup.DefaultIfEmpty()  // This creates a left join
                            select new ParkingLotDTO
                            {
                                LotId = lot.LotId,
                                LotSector = lot.LotSector,
                                Status = lot.Status,
                                Amount = lot.Amount,
                                UserId = lot.UserId,
                                LicensePlate = user != null ? user.LicensePlate : null, // Handle null user
                                Name = user != null ? user.Name : null // Handle null user
                            };

                // Apply filtering based on selectedIndex
                switch (selectedIndex)
                {
                    case "Occupied": // "Occupied"
                        query = query.Where(lot => lot.Status == "Occupied");
                        break;
                    case "Available": // "Available"
                        query = query.Where(lot => lot.Status == "Available");
                        break;
                    case "All": // "All" (no filter needed)
                    default:
                        break;
                }

                // Execute the query and return the list
                return query.ToList();
            }
        }


        // Cập nhật thông tin ParkingLot
        public void UpdateParkingLot(ParkingLotDTO updatedParkingLot)
        {
            using (ParkingManagementContext context = new ParkingManagementContext())
            {
                var existingParkingLot = context.ParkingLots.FirstOrDefault(p => p.LotId == updatedParkingLot.LotId);
                if (existingParkingLot != null)
                {
                    existingParkingLot.LotSector = updatedParkingLot.LotSector;
                    existingParkingLot.UserId = updatedParkingLot.UserId;
                    existingParkingLot.Status = updatedParkingLot.Status;
                    existingParkingLot.Amount = updatedParkingLot.Amount;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("ParkingLot not found");
                }
            }
        }

        // Thêm mới ParkingLot
        public void AddParkingLot(string lotSector, int? userId, string status, int? amount)
        {
            using (ParkingManagementContext context = new ParkingManagementContext())
            {
                ParkingLot parkingLot = new ParkingLot
                {
                    LotSector = lotSector,
                    UserId = userId,
                    Status = status,
                    Amount = amount
                };

                context.ParkingLots.Add(parkingLot);
                context.SaveChanges();
            }
        }

        // Xóa ParkingLot
        public void RemoveParkingLot(int lotId)
        {
            using (ParkingManagementContext context = new ParkingManagementContext())
            {
                var parkingLotToRemove = context.ParkingLots.FirstOrDefault(p => p.LotId == lotId);
                if (parkingLotToRemove != null)
                {
                    context.ParkingLots.Remove(parkingLotToRemove);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("ParkingLot not found");
                }
            }
        }
    }
}
