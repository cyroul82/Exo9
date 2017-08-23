using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic; 


namespace Exo9
{
    /// <summary>
    /// form de démarrage : datagridview des stagiaires de la section DI
    /// </summary>
    public partial class frmExo9 : Form
    {

        /// <summary>
        /// la section de stagiaires gérée par ce form
        /// </summary>
        private MSection laSection; 
        
        /// <summary>
        /// Constructeur 
        /// (initialise la collection de sections et insère en dur la section DI)
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
        /// rétablit la source de données de la dataGridView
        /// et rafraîchit son affichage
        /// </summary>
        private void afficheStagiaires()
        {
            this.grdStagiaires.DataSource = null;
            this.grdStagiaires.DataSource = laSection.ListerStagiaires();
            // déterminer l'origine des données à afficher : 
            // appel de la méthode de la classe MSection 
            // qui alimente et retourne une datatable
            // à partir de sa collection de stagiaires
          
            // refraîchir l'affichage
            this.grdStagiaires.Refresh();
            // gestion bouton supprimer
            this.btnSupprimer.Enabled = (this.grdStagiaires.CurrentRow == null ? false: true);
        }

        /// <summary>
        /// Bouton  ajouter : instancie un form de saisie stagiaire
        /// et lui passe la référence à la section en cours
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            // instancier un form de saisie de stagiaire et l'afficher en modal
            // il faut préciser la référence à la section que l'on traite
            frmAjoutStagiaire frmAjout = new frmAjoutStagiaire(laSection);
            // si on sort de la saisie par OK
            if (frmAjout.ShowDialog() == DialogResult.OK)
            {
                // régénèrer l'affichage du dataGridView 
                this.afficheStagiaires();
            }
        }

        /// <summary>
        /// Double-clic sur le datagridview :
        /// ouvrir la feuille détail en y affichant
        /// le stagiaire correspondant à la ligne double-cliquée
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
        /// bouton supprimer : gérer la suppression du stagiaire pointé dans la datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            // si un stagiaire est pointé dans la datagridview
            if (this.grdStagiaires.CurrentRow != null)
            {
                // récupérer la clé du stagiaire pointé
                Int32 cleStagiaire;
                if (Int32.TryParse(this.grdStagiaires.CurrentRow.Cells[0].Value.ToString(), out cleStagiaire))
                {
                    // demander confirmation de la suppression
                    // NB: messagebox retourne une valeur exploitable !
                    if (MessageBox.Show("Voulez-vous supprimer le stagiaire numéro :" + cleStagiaire.ToString(), "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // supprimer et compacter la collection
                        laSection.Supprimer(cleStagiaire);
                        // réafficher la datagridview
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
                // instancier un form détail pour ce stagiaire
                frmVisuStagiaire frmVisu = new frmVisuStagiaire(leStagiaire);
                // personnaliser le titre du form
                frmVisu.Text = leStagiaire.ToString();
                // afficher le form détail en modal
                frmVisu.ShowDialog();

                // en sortie du form détail, refraichir la datagridview
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