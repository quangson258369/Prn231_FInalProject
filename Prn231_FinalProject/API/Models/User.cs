using System;
using System.Collections.Generic;

namespace API.Models;

public partial class User
{
    public int UserId { get; set; }

    public int? UserTypeId { get; set; }

    public string UserName { get; set; }

    public string UserPhone { get; set; }

    public string UserAddress { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual UserType UserType { get; set; }
}
