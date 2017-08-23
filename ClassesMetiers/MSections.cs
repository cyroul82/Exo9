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
        private SortedDictionary<String, MSection> listSections;

        public MSections()
        {
            listSections = new SortedDictionary<String, MSection>();
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

        public MSection RestituerStagiaire(String identifiant)
        {
            if (listSections.ContainsKey(identifiant))
            {
                return listSections[identifiant];
            }
            else
            {
                throw new Exception("Can't find this identifiant, this Stagiaire does not exist !");
            }
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

    }
}
