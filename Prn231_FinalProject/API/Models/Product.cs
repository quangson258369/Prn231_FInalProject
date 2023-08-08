using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<TransactionDetail> TransactionDetails { get; set; } = new List<TransactionDetail>();
}
