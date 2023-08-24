﻿namespace Client.Models;

public partial class Unit
{
    public int Id { get; set; }

    public string DisplayName { get; set; }

    public virtual ICollection<Object> Objects { get; set; } = new List<Object>();
}