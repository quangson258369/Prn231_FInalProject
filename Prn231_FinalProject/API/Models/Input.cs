namespace API.Models;

public partial class Input
{
    public string Id { get; set; }

    public DateTime? DateInput { get; set; }

    public virtual ICollection<InputInfo> InputInfos { get; set; } = new List<InputInfo>();
}
