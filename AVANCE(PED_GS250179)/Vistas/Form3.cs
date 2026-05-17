using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Owner.Show();

            
            this.Close();
        }

        private void btnAR_Click(object sender, EventArgs e)
        {
            Form4 añadir = new Form4();
            añadir.Show();

            this.Hide();
        }

        private void btnEditarR_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No se puede editar ninguna Ruta, ya que no existe información.",
                "ERROR", MessageBoxButtons.OK);
        }

        private void btnEliminarRuta_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No se puede eliminar ninguna Ruta, ya que no existe información.",
                "ERROR", MessageBoxButtons.OK);
        }
    }
}
