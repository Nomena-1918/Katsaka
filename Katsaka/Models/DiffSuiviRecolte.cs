using System;
namespace Katsaka.Models
{
	public class DiffSuiviRecolte : VDerniersuiviAvantRecolte
    {
        public TimeSpan? diffDate { get; set; }
        public DiffSuiviRecolte()
		{
		}
	}
}

