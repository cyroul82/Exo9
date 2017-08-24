using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Exo9
{
    /// <summary>
    /// classe qui g�re la collection des sections
    /// </summary>
    public class MSections
    {
        // Declare une reference MSections static
        private static MSections instance = null;

        private SortedDictionary<String, MSection> listSections; 

        // Creer un constructor priv�
        private MSections()
        {
            listSections = new SortedDictionary<String, MSection>();
        }

        // Cr�er une m�thode static retournant la reference de MSection
        public static MSections Instance()
        {
            if(instance == null)
            {
                instance = new MSections();
            }
            return instance;
        }

        public void Ajouter(MSection section)
        {
            listSections.Add(section.Identifiant, section);
        }

        public void Supprimer(MSection section)
        {
            if (!listSections.Remove(section.Identifiant))
            {
                throw new Exception("Can't delete this section, does not exist !");
            }

        }

        public void Supprimer(String identifiant)
        {

            if (!listSections.Remove(identifiant))
            {
                throw new Exception("Can't delete this identifiant, does not exist !");
            }
        }

        public void Remplacer(MSection section)
        {
            if (listSections.ContainsKey(section.Identifiant))
            {
                listSections[section.Identifiant] = section;
            }
            else
            {
                throw new Exception("Can't remplace this section, can't find the numOsia !");
            }
        }

        public MSection RestituerSection(String identifiant)
        {
            if (listSections.ContainsKey(identifiant))
            {
                return listSections[identifiant];
            }
            else
            {
                return null;
            }
        }

        public Boolean isNumOsiaExist(Int32 numOsia, out String identifiantSection)
        {
            Boolean exist = false;
            identifiantSection = null;
            foreach(MSection section in listSections.Values)
            {
                if (section.isNumOsiaExist(numOsia))
                {
                    exist = true;
                    identifiantSection = section.Identifiant;
                }
            }

            return exist;
        }

        public MStagiaire getStagiaire(Int32 numOsia)
        {
            MStagiaire stagiaire = null;
            foreach (MSection section in listSections.Values)
            {
               
            }
            return stagiaire;
        }


        public DataTable ListerSections()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Identifiant", typeof(String));
            dt.Columns.Add("Libelle", typeof(String));

            foreach (KeyValuePair<String, MSection> s in listSections)
            {
                DataRow row = dt.NewRow();
                row["Identifiant"] = s.Key;
                row["Libelle"] = s.Value.Libelle;
                dt.Rows.Add(row);
            }

            return dt;
        }

        public Int32 getSectionsCount()
        {
            return listSections.Count;
        }

    }
}
