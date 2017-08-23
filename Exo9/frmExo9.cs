using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic; 


namespace Exo9
{
    /// <summary>
    /// form de d�marrage : datagridview des stagiaires de la section DI
    /// </summary>
    public partial class frmExo9 : Form
    {

        /// <summary>
        /// la section de stagiaires g�r�e par ce form
        /// </summary>
        private MSection laSection; 
        
        /// <summary>
        /// Constructeur 
        /// (initialise la collection de sections et ins�re en dur la section DI)
        /// </summary>
        public frmExo9(MSection laSection)
        {
            InitializeComponent();
            // initialisation de la collection de sections
            this.Text = "Visulisation des Stagiaires de la section " + laSection.Identifiant;
            this.laSection = laSection;

            // afficher la liste des stagiaires de la section
            this.afficheStagiaires();
        }

        public MSection getSection()
        {
            return laSection;
        }

        /// <summary>
        /// r�tablit la source de donn�es de la dataGridView
        /// et rafra�chit son affichage
        /// </summary>
        private void afficheStagiaires()
        {
            this.grdStagiaires.DataSource = null;
            this.grdStagiaires.DataSource = laSection.ListerStagiaires();
            // d�terminer l'origine des donn�es � afficher : 
            // appel de la m�thode de la classe MSection 
            // qui alimente et retourne une datatable
            // � partir de sa collection de stagiaires
          
            // refra�chir l'affichage
            this.grdStagiaires.Refresh();
            // gestion bouton supprimer
            this.btnSupprimer.Enabled = (this.grdStagiaires.CurrentRow == null ? false: true);
        }

        /// <summary>
        /// Bouton  ajouter : instancie un form de saisie stagiaire
        /// et lui passe la r�f�rence � la section en cours
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            // instancier un form de saisie de stagiaire et l'afficher en modal
            // il faut pr�ciser la r�f�rence � la section que l'on traite
            frmAjoutStagiaire frmAjout = new frmAjoutStagiaire(laSection);
            // si on sort de la saisie par OK
            if (frmAjout.ShowDialog() == DialogResult.OK)
            {
                // r�g�n�rer l'affichage du dataGridView 
                this.afficheStagiaires();
            }
        }

        /// <summary>
        /// Double-clic sur le datagridview :
        /// ouvrir la feuille d�tail en y affichant
        /// le stagiaire correspondant � la ligne double-cliqu�e
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdStagiaires_DoubleClick(object sender, EventArgs e)
        {
            modifyStagiaire();
        }

        /// <summary>
        /// bouton fermer : fermer le form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        /// <summary>
        /// bouton supprimer : g�rer la suppression du stagiaire point� dans la datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            // si un stagiaire est point� dans la datagridview
            if (this.grdStagiaires.CurrentRow != null)
            {
                // r�cup�rer la cl� du stagiaire point�
                Int32 cleStagiaire;
                if (Int32.TryParse(this.grdStagiaires.CurrentRow.Cells[0].Value.ToString(), out cleStagiaire))
                {
                    // demander confirmation de la suppression
                    // NB: messagebox retourne une valeur exploitable !
                    if (MessageBox.Show("Voulez-vous supprimer le stagiaire num�ro :" + cleStagiaire.ToString(), "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // supprimer et compacter la collection
                        laSection.Supprimer(cleStagiaire);
                        // r�afficher la datagridview
                        this.afficheStagiaires();
                    }
                }
                else
                {
                    throw new InvalidCastException("can't convert cell[0], cleStagiaire");
                }
            }

        }
        private void modifyStagiaire()
        {
            MStagiaire leStagiaire;
            Int32 laCle;
            if (Int32.TryParse(this.grdStagiaires.CurrentRow.Cells[0].Value.ToString(), out laCle))
            {
                // instancier un objet stagiaire pointant vers 
                // le stagiaire d'origine dans la collection
                leStagiaire = laSection.RestituerStagiaire(laCle);
                // instancier un form d�tail pour ce stagiaire
                frmVisuStagiaire frmVisu = new frmVisuStagiaire(leStagiaire);
                // personnaliser le titre du form
                frmVisu.Text = leStagiaire.ToString();
                // afficher le form d�tail en modal
                frmVisu.ShowDialog();

                // en sortie du form d�tail, refraichir la datagridview
                this.afficheStagiaires();
            }

            else
            {
                throw new InvalidCastException("can't convert cell[0] laCle");
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            modifyStagiaire();
        }
    }
    
}