using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AVANCE_PED_GS250179_.Servicio;

namespace AVANCE_PED_GS250179_
{
    public partial class Form6 : Form
    {
        GestionUnidadesService service = new GestionUnidadesService();

        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            CargarTabla();
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
            if (dgvUnidades.Rows.Count > 0)
            {
                int id = Convert.ToInt32(
                    dgvUnidades.CurrentRow.Cells[0].Value
                );

                DialogResult resultado =
                    MessageBox.Show(
                        "¿Desea eliminar esta unidad?",
                        "Confirmar",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                if (resultado == DialogResult.Yes)
                {
                    bool eliminado =
                        service.EliminarUnidad(id);

                    if (eliminado)
                    {
                        MessageBox.Show(
                            "Unidad eliminada"
                        );

                        CargarTabla();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Error al eliminar"
                        );
                    }
                }
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvUnidades.DataSource = service.BuscarUnidad(txtBuscar.Text);
        }

        private void CargarTabla()
        {
            dgvUnidades.DataSource = service.MostrarUnidades();
        }
    }
}
