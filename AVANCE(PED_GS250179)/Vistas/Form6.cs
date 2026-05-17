using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_
{
    public partial class Form6 : Form
    {

        private static bool MostrarMnj;
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            if (!MostrarMnj)
            {
                MessageBox.Show("En este apartado mostraremos información sobre las unidades que existen en cada ruta.", "INFORMACIÓN"
                    , MessageBoxButtons.OK);

                MostrarMnj = true;
            }

        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Añadir_U_Click(object sender, EventArgs e)
        {
            Form7 Au = new Form7();
            Au.Show();

            this.Hide();
        }

        private void EditarU_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No se puede editar ninguna Unidad, ya que no existe información.",
                "ERROR", MessageBoxButtons.OK);
        }

        private void EliminarU_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No se puede Eliminar ninguna Unidad, ya que no existe información.",
                "ERROR", MessageBoxButtons.OK);
        }
    }
}
