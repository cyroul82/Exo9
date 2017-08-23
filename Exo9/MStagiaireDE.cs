using System;
using System.Collections.Generic;
using System.Text;

namespace Exo9
{
    public class MStagiaireDE : MStagiaire
    {
        private Boolean remuAfpa;
        public MStagiaireDE(Int32 numOsia, String nom, String prenom, String rue, String ville, String codePostal, Boolean remuAfpa) : base(numOsia, nom, prenom, rue, ville, codePostal)
        {
            this.RemuAfpa = remuAfpa;
        }

        public bool RemuAfpa
        {
            get
            {
                return remuAfpa;
            }

            set
            {
                remuAfpa = value;
            }
        }
    }
}
