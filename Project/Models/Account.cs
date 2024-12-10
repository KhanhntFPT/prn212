using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Account
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<ParkingLot> ParkingLots { get; set; } = new List<ParkingLot>();

    public virtual PersonalInfo? PersonalInfo { get; set; }
}
