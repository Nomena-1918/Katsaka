using System;
using System.Collections.Generic;

namespace Katsaka.Models;

public partial class Parametrefrequence
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public TimeSpan? Valeur { get; set; }
}
