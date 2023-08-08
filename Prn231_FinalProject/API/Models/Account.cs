using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string AccountUsername { get; set; }

    public string AccountPassword { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
