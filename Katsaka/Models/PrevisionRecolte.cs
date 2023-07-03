using System;
using System.Collections.Generic;
using Npgsql;

namespace Katsaka.Models
{
	public class PrevisionRecolte
    {
        // Dernier suivi maïs pour prévision
        VListDernierSuivi dernierSuivi { get; set; }

        // Avanat dernier suivi pour prévision de manière linéaire
        VListDernierSuivi avantDernierSuivi { get; set; }

        // kg de graine de maïs par cm d'épis
        public decimal? coeffKgCmEpis { get; set; }


        // Pour les prévisions par rapport aux 2 derniers rapports - méthode linéaire (plus proche de la réalité)
        public decimal? EvolutionNbrEpis { get; set; }
        public decimal? EvolutionLongEpis { get; set; }
        public decimal? EvolutionNbrPousse { get; set; }


        // ============ CONSTRUCTORS =============
        public PrevisionRecolte(Recolte recolte_ref)
        {
            setCoeffKgCmEpis(recolte_ref);
        }


        // ============ SETTERS =================

        // MINIMUM
        public void setCoeffKgCmEpis(Recolte recolte_ref) {
            // Calcul du poids minimal prévu
            coeffKgCmEpis = recolte_ref.Poidstotalgraine / (recolte_ref.Nbrtotalepis * recolte_ref.Longueurmoyenepis);
        }


        // LINEAIRE
        public void setEvolutionLinear(VListDernierSuivi dernierSuivi, VListDernierSuivi avantDernierSuivi)
        {
            this.dernierSuivi = dernierSuivi;

            setEvolutionNbrEpis(avantDernierSuivi);
            setEvolutionLongEpis(avantDernierSuivi);
            setEvolutionNbrPousse(avantDernierSuivi);
        }


        public void setEvolutionNbrEpis(VListDernierSuivi avantDernierSuivi)
        {
            if (avantDernierSuivi != null)
                EvolutionNbrEpis = dernierSuivi.Nbrepismoyenparpousse - avantDernierSuivi.Nbrepismoyenparpousse;

            else
                EvolutionNbrEpis = dernierSuivi.Nbrepismoyenparpousse;
        }

        public void setEvolutionLongEpis(VListDernierSuivi avantDernierSuivi)
        {
            if (avantDernierSuivi != null)
                EvolutionLongEpis = dernierSuivi.Longueurmoyenepis - avantDernierSuivi.Longueurmoyenepis;

            else
                EvolutionLongEpis = dernierSuivi.Longueurmoyenepis;
        }

        public void setEvolutionNbrPousse(VListDernierSuivi avantDernierSuivi)
        {
            if (avantDernierSuivi != null)
                EvolutionNbrPousse = dernierSuivi.Nbrpousse - avantDernierSuivi.Nbrpousse;

            else
                EvolutionNbrPousse = dernierSuivi.Nbrpousse;
        }






        // ============ PREVISIONS =================

        // MINIMUM - Recolte à partir du dernier suivi
        public Recolte getPrevisionRecolteMin(VListDernierSuivi dernierSuivi)
        {
            Recolte recolte_prevue = new();

            // Les attributs qui changent pas
            recolte_prevue.IdparcelleNavigation = new();
            recolte_prevue.IdparcelleNavigation.Id = (int)dernierSuivi.Idparcelle;
            recolte_prevue.IdparcelleNavigation.Nom = dernierSuivi.Nomparcelle;
            recolte_prevue.Longueurmoyenepis = dernierSuivi.Longueurmoyenepis;
            recolte_prevue.Nbrtotalepis = dernierSuivi.Nbrepismoyenparpousse * dernierSuivi.Nbrpousse;


            // Calcul à partir du coeffKgCmEpis
            recolte_prevue.Poidstotalgraine = coeffKgCmEpis * recolte_prevue.Nbrtotalepis * recolte_prevue.Longueurmoyenepis;

            return recolte_prevue;
        }


        // LINEAIRE - Recolte à partir de la différence entre le dernier suivi et l'avant dernier suvi
        public Recolte getPrevisionRecolteLinear()
        {
            Recolte recolte_prevue = new();

            // Les attributs qui changent pas
            recolte_prevue.IdparcelleNavigation = new();
            recolte_prevue.IdparcelleNavigation.Id = (int)dernierSuivi.Idparcelle;
            recolte_prevue.IdparcelleNavigation.Nom = dernierSuivi.Nomparcelle;



            // Les attributs calculés de manière linéaire
            recolte_prevue.Longueurmoyenepis = dernierSuivi.Longueurmoyenepis + EvolutionLongEpis;

            var Nbrepismoyenparpousse_prevu = dernierSuivi.Nbrepismoyenparpousse + EvolutionNbrEpis;
            var Nbrpousse_prevu = dernierSuivi.Nbrpousse + EvolutionNbrPousse;
            recolte_prevue.Nbrtotalepis = (int?)(Nbrepismoyenparpousse_prevu * Nbrpousse_prevu);


            // Calcul à partir du coeffKgCmEpis
            recolte_prevue.Poidstotalgraine = coeffKgCmEpis * recolte_prevue.Nbrtotalepis * recolte_prevue.Longueurmoyenepis;

            return recolte_prevue;
        }




    }
}

