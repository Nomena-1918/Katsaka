using System;
using System.Collections.Generic;

namespace Katsaka.Models;

public partial class Parcelle
{
    public int Id { get; set; }

    public int Idchamp { get; set; }

    public string Nom { get; set; } = null!;

    public string? Remarque { get; set; }

    public int Idresponsable { get; set; }

    public virtual Champ IdchampNavigation { get; set; } = null!;

    public virtual Responsable IdresponsableNavigation { get; set; } = null!;

    public virtual ICollection<Recolte> Recoltes { get; set; } = new List<Recolte>();

    public virtual ICollection<Suivimai> Suivimais { get; set; } = new List<Suivimai>();
}
