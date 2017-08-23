using System;
using System.Collections.Generic;
using System.Text;

namespace Exo9
{
    /// <summary>
    /// classe des stagiaires
    /// </summary>
    abstract public class MStagiaire
    {

        /// <summary>
        /// constructeur par d�faut 
        /// initialise nbrepoints et nbrenotes
        /// </summary>
        public MStagiaire(Int32 numOsia, String nom, String prenom, String rue, String ville, String codePostal)
        {
            this.numOsia = numOsia;
            Nom = nom;
            Prenom = prenom;
            Rue = rue;
            Ville = ville;
            CodePostal = codePostal;
            this.nbreNotes = 0;
            this.pointsNotes = 0;

        }


        /// <summary>
        /// num�ro du stagiaire,
        /// l'appelant devra prendre garde � passer un entier valide
        /// </summary>
        protected Int32 numOsia;

        /// <summary>
        /// obtient le num�ro du stagiaire
        /// </summary>
        public Int32 NumOsia
        {
            get { return numOsia; }
        }

        /// <summary>
        /// le nom du stagiaire
        /// </summary>
        protected String nomStagiaire;

        /// <summary>
        /// obtient ou d�finit le nom du stagiaire (forc� en majuscules)
        /// </summary>
        public String Nom
        {
            get { return nomStagiaire; } // en lecture, retourne la var priv�e
            // cette portion de code sert � l�affectation d�une nouvelle valeur ;
            // c�est ici que l�on effectue des contr�les de saisie ou de format
            // ici : mettre le nom en majuscules :
            set { nomStagiaire = value.Trim().ToUpper(); } // mettre le nom en majuscules 
        }

        /// <summary>
        /// le pr�nom de stagiaire
        /// </summary>
        protected String prenomStagiaire;

        /// <summary>
        /// obtient ou d�finit le pr�nom de stagiaire (forc� en minuscules)
        /// </summary>
        public String Prenom
        {
            get { return prenomStagiaire; } // en lecture, retourne la var priv�e
            set { prenomStagiaire = value.Trim().ToLower(); } // mettre le pr�nom en minuscules 
        }

        /// <summary>
        /// immeuble, rue et num�ro, le format est libre
        /// </summary>
        protected String rue;

        /// <summary>
        /// obtient ou d�finit immeuble, rue et num�ro, le format est libre
        /// </summary>
        public String Rue
        {
            get { return rue; }
            set { rue = value; }
        }

        /// <summary>
        /// le nom de la ville
        /// </summary>
        protected String villeStagiaire;

        /// <summary>
        /// obtient ou d�finit le nom de la ville (forc� en majuscules)
        /// </summary>
        public String Ville
        {
            get { return villeStagiaire; } // en lecture, retourne la var priv�e
            set { villeStagiaire = value.Trim().ToUpper(); } // mettre la ville en majuscules 
        }

        /// <summary>
        /// le code postal, l'appelant devra prendre garde � passer 
        /// un code postal valide � 5 chiffres
        /// </summary>
        protected String codePostalStagiaire;

        /// <summary>
        /// obtient ou d�finit le code postal (contr�le : 5 car tous chiffres)
        /// </summary>
        /// <exception cref="Exception">le code postal n'est pas constitu� de 5 chiffres</exception>
        public String CodePostal          
        {
            get { return codePostalStagiaire; } // en lecture, retourne la var priv�e
            set 
            {
                // l'appelant doit fournir un code postal valide � 5 chiffres
                Int32 i ;               // variable  de boucle
                Boolean erreur = false; // indicateur erreur
                if (value.Length == 5 ) // 5 car. attendus : OK ==> contr�ler un � un
                {
                    for (i = 0; i< value.Length; i++)  // controle chiffres par boucle
                    {
                        if (! (Char.IsDigit(value[i]))) // charabia ??
                            {erreur = true;}
                        
                    } // fin de boucle controle chiffres
                    if (erreur) //on a rencontre un non-chiffre
                    {
                        // lev�e d'exception
                        throw new Exception(value.ToString() + "\n" + "n'est pas un code postal valide : uniquement des chiffres");
                    }
                    else
                    {
                        codePostalStagiaire = value;  // tout est bon, on affecte la propri�t�
                    }
                }
                else // il n'y a pas 5 caract�res
                {
                    // lev�e d'exception
                    throw new Exception(value.ToString() + "\n" + "n'est pas un code postal valide : 5 chiffres, pas plus, pas moins");
                }

            }
          
        }

        /// <summary>
        /// nombre de notes obtenues
        /// </summary>
        protected Int32 nbreNotes;

        /// <summary>
        /// cumul des points obtenus
        /// </summary>
        protected Double pointsNotes;

        /// <summary>
        /// permet d'alimenter NbreNotes et PointsNotes
        /// </summary>
        /// <param name="laNote">la nouvelle note � prendre en compte</param>
        public void RecevoirNote(float laNote)
        {
            nbreNotes++;
            pointsNotes += (Double)laNote;
        }

        /// <summary>
        /// obtient la moyenne des notes re�ues
        /// </summary>
        /// <returns>nouvelle moyenne des notes</returns>
        public Double DonnerMoyenne()            
        {
            if(nbreNotes == 0)
            {
                return 0;
            }
            else {
                return pointsNotes / nbreNotes;
            }
        }

        /// <summary>
        /// obtient un libell� en clair (numosia + nom et pr�nom)
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return "Stagiare : " + Nom + " " + Prenom;
        }

    }
}
