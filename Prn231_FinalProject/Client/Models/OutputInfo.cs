namespace Client.Models;

public partial class OutputInfo
{
    public string Id { get; set; }

    public string IdObject { get; set; }

    public string IdInputInfo { get; set; }

    public int IdCustomer { get; set; }

    public int? Count { get; set; }

    public string Status { get; set; }
    public virtual OutputInfo IdOutputInfoNavigation { get; set; }
    public virtual Customer IdCustomerNavigation { get; set; }

    public virtual InputInfo IdInputInfoNavigation { get; set; }

    public virtual Object IdObjectNavigation { get; set; }
}
