using System;
using System.Collections.Generic;

namespace API.Models;

public partial class InputInfo
{
    public string Id { get; set; }

    public string IdObject { get; set; }

    public string IdInput { get; set; }

    public int? Count { get; set; }

    public double? InputPrice { get; set; }

    public double? OutputPrice { get; set; }

    public string Status { get; set; }

    public virtual Input IdInputNavigation { get; set; }

    public virtual Object IdObjectNavigation { get; set; }

    public virtual ICollection<InputSelect> InputSelects { get; set; } = new List<InputSelect>();
}
