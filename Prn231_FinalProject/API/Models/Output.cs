using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Output
{
    public string Id { get; set; }

    public DateTime? DateOutput { get; set; }

    public virtual ICollection<OutputInfo> OutputInfos { get; set; } = new List<OutputInfo>();
}
