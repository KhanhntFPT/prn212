using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.viewModel
{
    public class CustomerManagement
    {
        public List<PersonalInfo> GetTicketTypes()
        {
            ParkingManagementContext context = new ParkingManagementContext();

            return context.PersonalInfos.ToList();
        }
    }
}
