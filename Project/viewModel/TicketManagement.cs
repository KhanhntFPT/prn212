using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.viewModel
{
    public class TicketManagement
    {
        public List<TicketType> GetTicketTypes()
        {
            ParkingManagementContext context = new ParkingManagementContext();

            return context.TicketTypes.ToList();
        }
        public List<TicketType> SearchTicketByName(String search)
        {
            ParkingManagementContext context = new ParkingManagementContext();

            return context.TicketTypes.Where(ticket => ticket.TypeName.ToLower().Contains(search)).ToList();
        }
        public void UpdateTicketType(TicketType updatedTicketType)
        {
            ParkingManagementContext context = new ParkingManagementContext();
            // Tìm kiếm TicketType cần cập nhật dựa vào TypeId.
            var existingTicketType = context.TicketTypes.FirstOrDefault(t => t.TypeId == updatedTicketType.TypeId);
            if (existingTicketType != null)
            {
                // Cập nhật các thuộc tính của TicketType.
                existingTicketType.TypeName = updatedTicketType.TypeName;
                existingTicketType.Price = updatedTicketType.Price;
                existingTicketType.ValidityDays = updatedTicketType.ValidityDays;
                existingTicketType.Description = updatedTicketType.Description;
            }
            else
            {
                throw new Exception("TicketType not found");
            }
        }
        public void AddTicketType(String name, int price, int? validDay, String description)
        {
            ParkingManagementContext context = new ParkingManagementContext();
            TicketType ticketType = new TicketType();
            ticketType.TypeName = name;
            ticketType.Price = price;
            ticketType.ValidityDays = validDay;
            ticketType.Description = description;
            context.TicketTypes.Add(ticketType);
            context.SaveChanges();
        }
    }
}
