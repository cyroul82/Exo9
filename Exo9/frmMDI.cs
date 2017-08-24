using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Exo9
{
    public delegate void SelectSectionHandler(MSection section);
    public delegate void SearchHandler(Int32 numOsia, String identifiantSection);
    public partial class frmMDI : Form
    {
        //private int childFormNumber = 0;
        private Dictionary<string, frmExo9> listFrmExo9;
        private frmExo9 frmPrinc = null;
        MSections sections = MSections.Instance();

        public frmMDI()
        {
            InitializeComponent();
            listFrmExo9 = new Dictionary<string, frmExo9>();
            //Mock-data
            MSection section = new MSection("CDI1", "Développeur", DateTime.Now, DateTime.Now.AddYears(1));
            sections.Ajouter(section);
            section = new MSection("CDI14", "Concepteur Développeur", DateTime.Now, DateTime.Now.AddYears(1));
            sections.Ajouter(section);

        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            //// Créez une nouvelle instance du formulaire enfant.
            //Form childForm = new Form();
            //// Configurez-la en tant qu'enfant de ce formulaire MDI avant de l'afficher.
            //childForm.MdiParent = this;
            //childForm.Text = "Fenêtre " + childFormNumber++;
            //childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            openSection();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //saveFileDialog.Filter = "Fichiers texte (*.txt)|*.txt|Tous les fichiers (*.*)|*.*";
            //if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            //{
            //    string FileName = saveFileDialog.FileName;
            //    // TODO : ajoutez le code ici pour enregistrer le contenu actuel du formulaire dans un fichier.
            //}
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO : utilisez System.Windows.Forms.Clipboard pour insérer le texte ou les images sélectionnés dans le Presse-papiers
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO : utilisez System.Windows.Forms.Clipboard pour insérer le texte ou les images sélectionnés dans le Presse-papiers
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO : utilisez System.Windows.Forms.Clipboard.GetText() ou System.Windows.Forms.GetData pour extraire les informations du Presse-papiers.
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void openCDIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //openFrmExo9(null);
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void selectionSelected(MSection section)
        {
            openFrmExo9(section);
        }

        private void openFrmExo9(MSection section)
        {

            if (listFrmExo9.ContainsKey(section.Identifiant))
            {
                frmPrinc = listFrmExo9[section.Identifiant];
            }
            else frmPrinc = null;

            if (frmPrinc == null)
            {
                frmPrinc = new frmExo9(section);
                frmPrinc.MdiParent = this;
                frmPrinc.FormClosing += new FormClosingEventHandler(this.frmPrincClosing);
                listFrmExo9.Add(section.Identifiant, frmPrinc);
                frmPrinc.Show();
            }
            if (frmPrinc.WindowState == FormWindowState.Minimized)
            {
                frmPrinc.WindowState = FormWindowState.Normal;
            }
            else
            {
                frmPrinc.Activate();
            }
        }

        private void frmPrincClosing(object sender, FormClosingEventArgs e)
        {
            frmPrinc = (frmExo9)sender;
            listFrmExo9.Remove(frmPrinc.getSection().Identifiant);
        }

        private void openSectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openSection();
        }

        private void openSection()
        {
            frmSection s = new frmSection();
            s.selectSection += new SelectSectionHandler(this.selectionSelected);
            s.ShowDialog();
        }

        private void printPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            frmSearchStagiaire frmSearch = new frmSearchStagiaire();
            frmSearch.searchStagiaire += new SearchHandler(this.searchStagiaire);
            frmSearch.ShowDialog();
        }

        private void searchStagiaire(Int32 numOsia, String identifiantSection)
        {
            MSection section = sections.RestituerSection(identifiantSection);
            MStagiaire stagiaire = section.RestituerStagiaire(numOsia);
            frmVisuStagiaire frmVisu = new frmVisuStagiaire(stagiaire);
            frmVisu.ShowDialog();

            //frmVisuStagiaire frmVisuStagiaire = new frmVisuStagiaire
        }
    }
}
