using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Katsaka.Models;

[Keyless]
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



    // Différence de résultats entre le dernier suivi et la récolte
    public DiffSuiviRecolte GetDiffSuiviRecolte() {
        DiffSuiviRecolte diff = new()
        {
            LongueurmoyenepisRecolte = LongueurmoyenepisRecolte - LongueurmoyenepisSuivi,
            Nbrtotalepis = Nbrtotalepis - Nbrepistotalsuivi,
            diffDate = Daterecolte - Datesuivi
        };

        return diff;
    }

    // Anomalies à partir de GetDiffSuiviRecolte()
    public Dictionary<string, string> GetAnomalie(DiffSuiviRecolte diffSuivis)
    {
        Dictionary<string, string> listAnomalies = new();

        if (diffSuivis.LongueurmoyenepisRecolte < 0)
        {
            listAnomalies.Add("LongueurmoyenepisDiff", "diminution");
        }

        if (diffSuivis.Nbrtotalepis < 0)
        {
            listAnomalies.Add("NbrepissuiviDiff", "diminution");
        }

        // Aucune anomalie détectée
        if (listAnomalies.Count == 0)
            listAnomalies.Add("aucun", "aucun");


        return listAnomalies;
    }


}
