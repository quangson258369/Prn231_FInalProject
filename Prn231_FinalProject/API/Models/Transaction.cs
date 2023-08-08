using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? TransactionTypeId { get; set; }

    public int? UserId { get; set; }

    public decimal? Total { get; set; }

    public decimal? TotalPaid { get; set; }

    public DateTime? DateCreated { get; set; }

    public bool? Status { get; set; }

    public int? AccountId { get; set; }

    public virtual ICollection<TransactionDetail> TransactionDetails { get; set; } = new List<TransactionDetail>();

    public virtual TransactionType TransactionType { get; set; }

    public virtual User User { get; set; }
}
