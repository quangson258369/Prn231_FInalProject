namespace API.Models;

public partial class InputSelect
{
    public string Id { get; set; }

    public string IdInputInfo { get; set; }

    public int? Quantity { get; set; }

    public string IdOutputInfo { get; set; }

    public virtual InputInfo IdInputInfoNavigation { get; set; }

    public virtual OutputInfo IdOutputInfoNavigation { get; set; }
}
