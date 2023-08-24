using System;
using System.Collections.Generic;

namespace API.Models;

public partial class OutputInfo
{
    public string Id { get; set; }

    public string IdObject { get; set; }

    public string IdInputSelct { get; set; }

    public int IdCustomer { get; set; }

    public int? Count { get; set; }

    public string Status { get; set; }

    public string IdOutput { get; set; }

    public virtual Customer IdCustomerNavigation { get; set; }

    public virtual InputSelect IdInputSelctNavigation { get; set; }

    public virtual Object IdObjectNavigation { get; set; }

    public virtual Output IdOutputNavigation { get; set; }
}
