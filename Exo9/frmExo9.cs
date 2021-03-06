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
            //Donnees.Sections = new MSections();
            this.laSection = laSection;
            Console.WriteLine(laSection.Identifiant);

            // pour commencer, une seule section r�f�renc�e "en dur" dans ce programme
            // instancie la section
            //this.laSection = new MSection("CDI1", "Concepteur D�veloppeur Informatique 2012");
            // l'ajoute dans la collection des sections g�r�e par la classe de collection
            
            // ajoute en dur un stagiaire � cette section
           // MStagiaire unStagiaire;
            //unStagiaire = new MStagiaireDE(11111, "DUPOND", "Albert", "12 rue des Fleurs", "NICE", "06300", false);
            // l'ajoute dans la collection des stagiaires de cette section
           // unStagiaire = new MStagiaireDE(11111, "RAT", "Cyril", "109 Rue de la Soleillette", "Saint-Rapha�l", "83700", false);
            //laSection.Ajouter(unStagiaire);
            // afficher la liste des stagiaires de la section
            this.afficheStagiaires();
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
            // admirer la syntaxe...
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
            // ouvrir la feuille d�tail en y affichant 
            // le stagiaire correspondant � la ligne double-cliqu�e
            MStagiaire leStagiaire;
            Int32 laCle;
            // cl� (=numOSIA) du stagiaire dans la collection

            // r�cup�rer cl� du stagiaire cliqu� en DataGridView
            Console.WriteLine(this.grdStagiaires.CurrentRow.Cells[0].Value);

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

  
    }
    
}