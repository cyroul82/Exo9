using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Exo9
{
    /// <summary>
    /// classe des sections de stagiaires
    /// </summary>
    public class MSection
    {

        private String identifiant;
        private String libelle;
        private DateTime dateDebut;
        private DateTime dateFin;
        private static SortedDictionary<Int32, MStagiaire> listStagiaires = new SortedDictionary<Int32, MStagiaire>();
        private DataTable dt;

        public MSection(String identifiant, String libelle, DateTime dateDebut, DateTime dateFin)
        {
            Identifiant = identifiant;
            Libelle = libelle;
            DateDebut = dateDebut;
            DateFin = dateFin;
            //listStagiaires = new SortedDictionary<Int32, MStagiaire>();
        }

        public DataTable ListerStagiaires()
        {
            dt = new DataTable();
            dt.Columns.Add("NumOsia", typeof(String));
            dt.Columns.Add("Nom", typeof(String));
            dt.Columns.Add("Prénom", typeof(String));

            foreach(KeyValuePair<Int32, MStagiaire> s in MSection.listStagiaires)
            {
                DataRow row = dt.NewRow();
                row["NumOsia"] = s.Key;
                row["Nom"] = s.Value.Nom;
                row["Prénom"] = s.Value.Prenom;
                dt.Rows.Add(row);
            }

            return dt;

        }

        public void Ajouter(MStagiaire stagiaire)
        {
            MSection.listStagiaires.Add(stagiaire.NumOsia, stagiaire);
        }

        public void Supprimer(MStagiaire stagiaire)
        {
            if (!MSection.listStagiaires.Remove(stagiaire.NumOsia))
            {
                throw new Exception("Can't delete this stagiaire, does not exist !");
            }

        }

        public void Supprimer(Int32 numOsia)
        {

            if (!MSection.listStagiaires.Remove(numOsia))
            {
                throw new Exception("Can't delete this numOsia, does not exist !");
            }
        }

        public void Remplacer(MStagiaire stagiaire)
        {
            if (MSection.listStagiaires.ContainsKey(stagiaire.NumOsia))
            {
                MSection.listStagiaires[stagiaire.NumOsia] = stagiaire;
            }
            else
            {
                throw new Exception("Can't remplace this stagiaire, can't find the numOsia !");
            }
        }

        public MStagiaire RestituerStagiaire(Int32 numOsia)
        {
            if (MSection.listStagiaires.ContainsKey(numOsia))
            {
                return MSection.listStagiaires[numOsia];
            }
            else
            {
                throw new Exception("Can't find this numOsia, this Stagiaire does not exist !");
            }
        }

        public string Identifiant
        {
            get
            {
                return identifiant;
            }

            set
            {
                identifiant = value;
            }
        }

        public string Libelle
        {
            get
            {
                return libelle;
            }

            set
            {
                libelle = value;
            }
        }

        public DateTime DateDebut
        {
            get
            {
                return dateDebut;
            }

            set
            {
                dateDebut = value;
            }
        }

        public DateTime DateFin
        {
            get
            {
                return dateFin;
            }

            set
            {
                dateFin = value;
            }
        }
        
    }
}
