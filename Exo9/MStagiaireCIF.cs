using System;
using System.Collections.Generic;
using System.Text;

namespace Exo9
{
    public enum TypeCifEnum
    {
        CDI,
        CDD,
        TT
    }

    public class MStagiaireCIF : MStagiaire
    {
        private String fongecif;
        private TypeCifEnum typeCif;

        public MStagiaireCIF(Int32 numOsia, String nom, String prenom, String rue, String ville, String codePostal, String fongecif, TypeCifEnum typeCif) : base(numOsia, nom, prenom, rue, ville, codePostal)
        {
            Fongecif = fongecif;
            TypeCif = TypeCif;
        }

        public string Fongecif
        {
            get
            {
                return fongecif;
            }

            set
            {
                fongecif = value;
            }
        }

        public TypeCifEnum TypeCif
        {
            get
            {
                return typeCif;
            }

            set
            {
                typeCif = value;
            }
        }
    }
}
