using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class TicketType
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public int Price { get; set; }

    public int? ValidityDays { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
