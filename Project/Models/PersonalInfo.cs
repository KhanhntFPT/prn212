using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class PersonalInfo
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int? Balance { get; set; }

    public DateTime? ParkingDate { get; set; }

    public DateTime? RetrievalDate { get; set; }

    public string LicensePlate { get; set; } = null!;

    public virtual Account IdNavigation { get; set; } = null!;

    public virtual ICollection<ParkingLot> ParkingLots { get; set; } = new List<ParkingLot>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
