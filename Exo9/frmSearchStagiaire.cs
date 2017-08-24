using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exo9
{
    public partial class frmSearchStagiaire : Form
    {
        public SearchHandler searchStagiaire;
        public frmSearchStagiaire()
        {
            InitializeComponent();
            //numOsia = 0;
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Int32 num;
            String identifiantSection;
            try
            {
                if (Int32.TryParse(txtSearch.Text, out num))
                {
                    MSections sections = MSections.Instance();
                    if (sections.isNumOsiaExist(num, out identifiantSection))
                    {
                        searchStagiaire?.Invoke(num, identifiantSection);
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        throw new Exception("Can't find this num");
                    }

                }
                else
                {
                    throw new Exception("The NumOsia is incorrect !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : \n" + ex.Message, "Recherche");
            }
        }

        private void cbxSection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
