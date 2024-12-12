using System;
using System.Collections.Generic;

namespace Project.Model;

public partial class ParkTime
{
    public int ParkTimeId { get; set; }

    public int UserId { get; set; }

    public int ParkingLotId { get; set; }

    public int TicketId { get; set; }

    public DateTime ParkedTime { get; set; }

    public DateTime? RetrievedTime { get; set; }

    public int? TotalAmount { get; set; }
}
