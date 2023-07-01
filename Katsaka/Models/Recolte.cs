using System;
using System.Collections.Generic;

namespace Katsaka.Models;

public partial class Recolte
{
    public int Id { get; set; }

    public int Idparcelle { get; set; }

    public decimal? Poidstotalgraine { get; set; }

    public int? Nbrtotalepis { get; set; }

    public decimal? Longueurmoyenepis { get; set; }

    public DateTime Daterecolte { get; set; }

    public virtual Parcelle IdparcelleNavigation { get; set; } = null!;
}
