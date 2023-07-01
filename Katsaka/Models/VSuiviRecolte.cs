using System;
using System.Collections.Generic;

namespace Katsaka.Models;

public partial class VSuiviRecolte
{
    public int? Id { get; set; }

    public int? Idparcelle { get; set; }

    public decimal? Longueurmoyenpousse { get; set; }

    public int? Couleurmoyenpousse { get; set; }

    public int? Nbrepistotalsuivi { get; set; }

    public decimal? Longueurmoyenepis { get; set; }

    public DateTime? Datesuivi { get; set; }
}
