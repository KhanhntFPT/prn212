using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int UserId { get; set; }

    public int TypeId { get; set; }

    public DateTime PurchaseDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual TicketType Type { get; set; } = null!;

    public virtual PersonalInfo User { get; set; } = null!;
}
