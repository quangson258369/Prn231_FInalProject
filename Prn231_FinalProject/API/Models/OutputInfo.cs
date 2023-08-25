using System.Diagnostics.CodeAnalysis;

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

    [AllowNull]
    public virtual API.Models.Customer IdCustomerNavigation { get; set; }
    [AllowNull]
    public virtual Object IdObjectNavigation { get; set; }
    [AllowNull]
    public virtual API.Models.Output IdOutputNavigation { get; set; }
    [AllowNull]
    public virtual ICollection<InputSelect>? InputSelects { get; set; } = new List<InputSelect>();
}
