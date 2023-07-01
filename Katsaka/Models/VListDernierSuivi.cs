using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Katsaka.Models;

public partial class VListDernierSuivi
{
    [Key]
    public int? Id { get; set; }

    public int? Idparcelle { get; set; }

    public decimal? Longueurmoyenpousse { get; set; }

    public int? Couleurmoyenpousse { get; set; }

    public int? Nbrpousse { get; set; }

    public int? Nbrepismoyenparpousse { get; set; }

    public decimal? Longueurmoyenepis { get; set; }

    public DateTime? Datesuivi { get; set; }

    public string? Nomparcelle { get; set; }
}
