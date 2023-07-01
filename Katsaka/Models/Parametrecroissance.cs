using System;
using System.Collections.Generic;

namespace Katsaka.Models;

public partial class Parametrecroissance
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public decimal? Valeur { get; set; }
}
