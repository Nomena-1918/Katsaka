using System;
using System.Collections.Generic;

namespace Katsaka.Models;

public partial class Responsable
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public virtual ICollection<Parcelle> Parcelles { get; set; } = new List<Parcelle>();
}
