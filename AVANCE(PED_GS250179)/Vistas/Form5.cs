using AVANCE_PED_GS250179_.Servicio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_
{
    public partial class Form5 : Form
    {

        private static bool MostrarMensaje = false;
        TransaccionService transaccionService = new TransaccionService();

        public Form5()
        {
            InitializeComponent();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            try
            {
                dgvTransaccion.DataSource = transaccionService.ObtenerTransacciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string textoBusqueda = txtBuscar.Text.Trim();

                // Le pasamos al DataGridView la tabla filtrada
                dgvTransaccion.DataSource = transaccionService.BuscarTransacciones(textoBusqueda);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Owner.Show();

            this.Close();
        }
    }
}
