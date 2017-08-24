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
    public partial class frmSection : Form
    {
        public SelectSectionHandler selectSection;
        MSections sections = MSections.Instance();
        public frmSection()
        {
            InitializeComponent();
            cbxSection.DataSource = sections.ListerSections();
            cbxSection.DisplayMember = "Identifiant";
            //foreach(KeyValuePair<String, MSection> s in MSections.listSections)
            //{
            //    cbxSection.Items.Add(s.Key);
            //}
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cbxSection.SelectedItem != null && !String.IsNullOrEmpty(cbxSection.SelectedItem.ToString()))
            {
                DataRowView choice = cbxSection.SelectedItem as DataRowView;
                String value = choice.Row["Identifiant"] as String;
                MSection section = sections.RestituerSection(value);

                if(section != null)
                {
                    selectSection?.Invoke(section);
                    this.DialogResult = DialogResult.OK;
                }
                //if (MSections.listSections.ContainsKey(choice))
                //{
                //    MSection section = MSections.listSections[choice];
                //    selectSection?.Invoke(section);
                //    this.DialogResult = DialogResult.OK;
                //}
            }

        }
    }
}
