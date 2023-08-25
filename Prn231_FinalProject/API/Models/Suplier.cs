﻿namespace API.Models;

public partial class Suplier
{
    public int Id { get; set; }

    public string DisplayName { get; set; }

    public string Address { get; set; }

    public string Phone { get; set; }

    public string? Email { get; set; }

    public string? MoreInfo { get; set; }

    public DateTime? ContractDate { get; set; }

    public virtual ICollection<Object> Objects { get; set; } = new List<Object>();
}
