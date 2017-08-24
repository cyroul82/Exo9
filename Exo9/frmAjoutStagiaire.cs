using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Exo9
{
    /// <summary>
    /// classe dérivée de frmStagiaire : form spécialisé pour création d'un stagiaire
    /// </summary>
    public partial class frmAjoutStagiaire : frmStagiaire
    {
        /// <summary>
        /// la section du stagiaire en cours
        /// </summary>
        private MSection laSection;

        /// <summary>
        /// constructeur 
        /// </summary>
        /// <param name="uneSection">la section de ce stagiaire</param>
        public frmAjoutStagiaire(MSection uneSection)
        {
            InitializeComponent();
            this.laSection = uneSection;
        }

        /// <summary>
        /// bouton OK : contrôler les données saisies en formulaire,
        /// instancier un objet Mstagiaire et le référencer en collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            // controle vraissemblance des données saisies sur le form
            if (this.controle())
            {
                //  instancie le bon objet MStagiaire et l'ajoute à la collection de sa section  
                if (this.instancie())
                    {
                        // cas général :
                        // si l'instanciation stagiaire et 
                        // son ajout au tableau est OK :
                        // - fermeture de la boite de dialogue par validation
                        this.DialogResult = DialogResult.OK;
                    }
             }
        }


 
        /// <summary>
        /// fonction qui instancie un nouvel objet MStagiaire spécialisé
        /// puis tente d'affecter ses membres (attributs ou prop.)
        /// avec interception erreur éventuelle levée par la classe MStagiaire
        /// et ajoute la référence de ce stagiaire dans la collection de sa section
        /// </summary>
        /// <returns>Boolean : true = OK, false = erreur</returns>
        private Boolean instancie()
        {
            // créer une référence d'objet MStagiaire 
            MStagiaire nouveauStagiaire;

            try
            {
                MSections sections = MSections.Instance();
                Int32 numOsia;
                if (Int32.TryParse(txtOSIA.Text, out numOsia))
                {
                    if (!sections.isNumOsiaExist(numOsia)) { 

                        if (this.rbtDE.Checked) // c'est un DE
                        {
                        // instancier un stagiaire spécialisé DE et lui affecter toutes ses propriétés
                        nouveauStagiaire = new MStagiaireDE(numOsia, txtNom.Text, txtPrenom.Text, txtAdresse.Text, txtVille.Text, txtCodePostal.Text, false);
                        }
                        else // c'est un CIF
                        {
                            // déterminer le type de CIF
                            TypeCifEnum leTypeCIF;
                            if (this.rbtCDD.Checked)
                            {
                                leTypeCIF = TypeCifEnum.CDD;
                            }
                            else if (this.rbtCDI.Checked)
                            {
                                leTypeCIF = TypeCifEnum.CDI;
                            }
                            else
                            {
                                leTypeCIF = TypeCifEnum.TT;
                            }

                            // instancier un stagiaire spécialisé CIF et lui affecter toutes ses propriétés
                            nouveauStagiaire = new MStagiaireCIF(numOsia, txtNom.Text, txtPrenom.Text, txtAdresse.Text, txtVille.Text, txtCodePostal.Text, txtFongecif.Text, leTypeCIF);

                        }
                    }
                    else
                    {
                        throw new Exception("This Osia Number already exists");
                    }
                }
                else
                {
                    throw new Exception("The Osia Number is incorrect, type of Int32");
                }

                

                // dans tous les cas, ajouter la référence d'objet MStagiaire dans la collection de sa section
                laSection.Ajouter(nouveauStagiaire);
                    return true;

                }
                catch (Exception ex)
                {
                    nouveauStagiaire = null;
                    MessageBox.Show("Erreur : \n" + ex.Message, "Ajout de stagiaire");
                    return false;
                }

        }


        /// <summary>
        /// fonction de contrôle de vraissemblance des différents champs du form
        /// (appelée avant d'instancier un objet MStagiaire et d'affecter ses membres)
        /// </summary>
        /// <returns>Boolean : true = OK, false = erreurs de saisie sur code postal ou numosia</returns>
        private Boolean controle()
        {
            // contrôler la vraissemblance de tous les champs
            Boolean code = true; // le code de retour ; OK a priori

            if(!(Outils.EstEntier(this.txtOSIA.Text))) // appel fonction générique de contrôle
            {
                // la chaîne reçue n'est pas convertible
                code = false;
                MessageBox.Show("le code OSIA saisi n'est pas un entier valide", "ERREUR", MessageBoxButtons.OK);
            }
            if (!(Outils.EstEntier(this.txtCodePostal.Text)))
            {
                code = false;
                MessageBox.Show("le code postal saisi n'est pas correct", "ERREUR", MessageBoxButtons.OK);
            }
            return code;
        }


        /// <summary>
        /// bouton annuler : fermer sans suite la boite de dialogue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            // fermeture de la boite de dialogue par abandon
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// checkbox CIF : activer/désactiver les options supp du form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtCIF_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtCIF.Checked)
            {
                this.grpTypeCIF.Enabled = true;

            }
            else
            {
                this.grpTypeCIF.Enabled = false;
            }
        }

        /// <summary>
        /// checkbox DE : activer/désactiver les options supp du form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtDE_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtDE.Checked)
            {
                this.chkRemuAfpa.Enabled = true;
            }
            else
            {
                this.chkRemuAfpa.Enabled = false;
            }
        }
    }
}
    

