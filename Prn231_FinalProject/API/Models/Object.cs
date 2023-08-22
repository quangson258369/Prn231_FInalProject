﻿namespace API.Models;

public partial class Object
{
    public string Id { get; set; }

    public string DisplayName { get; set; }

    public int IdUnit { get; set; }

    public int IdSuplier { get; set; }

    public string? Qrcode { get; set; }

    public string? BarCode { get; set; }

    public virtual Suplier IdSuplierNavigation { get; set; }

    public virtual Unit IdUnitNavigation { get; set; }

    public virtual ICollection<InputInfo> InputInfos { get; set; } = new List<InputInfo>();

    public virtual ICollection<OutputInfo> OutputInfos { get; set; } = new List<OutputInfo>();
}
