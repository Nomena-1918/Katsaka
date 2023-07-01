using System;
using System.Collections.Generic;

namespace Katsaka.Models;

public partial class VDerniersuiviAvantRecolte
{
    public int? Idsuivi { get; set; }

    public int? Idparcelle { get; set; }

    public decimal? Longueurmoyenpousse { get; set; }

    public int? Couleurmoyenpousse { get; set; }

    public int? Nbrepistotalsuivi { get; set; }

    public decimal? LongueurmoyenepisSuivi { get; set; }

    public DateTime? Datesuivi { get; set; }

    public int? Idrecolte { get; set; }

    public decimal? Poidstotalgraine { get; set; }

    public int? Nbrtotalepis { get; set; }

    public decimal? LongueurmoyenepisRecolte { get; set; }

    public DateTime? Daterecolte { get; set; }
}
