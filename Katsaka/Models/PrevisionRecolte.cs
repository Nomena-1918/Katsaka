using System;
using System.Collections.Generic;
using Npgsql;

namespace Katsaka.Models
{
	public class PrevisionRecolte
    {
        public PrevisionRecolte() {
            parcelle = new();
        }

        // Pour les prévisions par rapport aux 2 derniers rapports - méthode linéaire (plus proche de la réalité)
        public decimal? coeffEvolutionNbrEpis { get; set; }
        public decimal? coeffEvolutionLongEpis { get; set; }
        public decimal? coeffEvolutionNbrPousse { get; set; }
        public List<VListDernierSuivi> listDerniersSuivi { get; set; }
        public Parcelle parcelle { get; set; }


        public void setListDerniersSuivi(NpgsqlConnection connection, VListDernierSuivi suiviPourPrev) {

            // Sélection des 2 derniers rapports
            listDerniersSuivi = VListDernierSuivi.GetDerniersRapports(connection, (int)suiviPourPrev.Idparcelle);

            parcelle.Id = (int)suiviPourPrev.Idparcelle;
            parcelle.Nom = suiviPourPrev.Nomparcelle;
        }
        
        public void setCoeffs() {
            setCoeffEvolutionLongEpis(listDerniersSuivi);
            setCoeffEvolutionNbrEpis(listDerniersSuivi);
            setCoeffEvolutionNbrPousse(listDerniersSuivi);
        }
        
        // Prevision d'une parcelle à partir d'une parcelle
        // Simple régle de trois + Approximation linéaire
        public  Recolte getPrevisionRecolte(VDerniersuiviAvantRecolte suivi_recolte_ref)
        {
            Recolte recolte = new();
            
            int estimationNbrEpis, estimationNbrPousse;
            if(listDerniersSuivi.Count > 1) {
                estimationNbrEpis = (int)listDerniersSuivi[1].Nbrepismoyenparpousse;
                estimationNbrPousse = (int)listDerniersSuivi[1].Nbrpousse;
                recolte.Longueurmoyenepis = coeffEvolutionLongEpis * listDerniersSuivi[1].Longueurmoyenepis;
            }
            else {
                estimationNbrEpis = 1;
                estimationNbrPousse = 1;
                recolte.Longueurmoyenepis = coeffEvolutionLongEpis * listDerniersSuivi[0].Longueurmoyenepis;
            }


            int nbrEpisParPoussePrevu = (int)(coeffEvolutionNbrEpis * estimationNbrEpis);
            int nbrPoussePrevu = (int)(coeffEvolutionNbrPousse * estimationNbrPousse);
            
            recolte.Nbrtotalepis = nbrEpisParPoussePrevu * nbrPoussePrevu;
            
            recolte.Poidstotalgraine = recolte.Nbrtotalepis * (suivi_recolte_ref.Poidstotalgraine / (suivi_recolte_ref.Nbrtotalepis + (decimal)0.001));
            recolte.IdparcelleNavigation = parcelle;

            return recolte;
        }


        
        public void setCoeffEvolutionLongEpis(List<VListDernierSuivi> listDerniersSuivi)
        {
            if (listDerniersSuivi.Count > 1)
                coeffEvolutionLongEpis = listDerniersSuivi[0].Longueurmoyenepis / (listDerniersSuivi[1].Longueurmoyenepis + (decimal)0.001);
            else
                coeffEvolutionLongEpis = 1;
        }

        public void setCoeffEvolutionNbrEpis(List<VListDernierSuivi> listDerniersSuivi)
        {
            if (listDerniersSuivi.Count > 1)
                coeffEvolutionNbrEpis = (decimal?)(listDerniersSuivi[0].Nbrepismoyenparpousse / (listDerniersSuivi[1].Nbrepismoyenparpousse + 0.001));
            else
                coeffEvolutionNbrEpis =  1;
        }

        public void setCoeffEvolutionNbrPousse(List<VListDernierSuivi> listDerniersSuivi)
        {
            if (listDerniersSuivi.Count > 1)
                coeffEvolutionNbrPousse = (decimal?)(listDerniersSuivi[0].Nbrpousse / (listDerniersSuivi[1].Nbrpousse + 0.001));
            else
                coeffEvolutionNbrPousse = 1;
        }



    }
}

