using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class ParkingLot
{
    public int LotId { get; set; }

    public string LotSector { get; set; } = null!;

    public int? UserId { get; set; }

    public int? EmployeeId { get; set; }

    public string Status { get; set; } = null!;

    public int? Amount { get; set; }

    public virtual Account? Employee { get; set; }

    public virtual PersonalInfo? User { get; set; }
    //public virtual DbSet<ParkTime> ParkTimes { get; set; }

}
