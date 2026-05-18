using AVANCE_PED_GS250179_.Servicio;
using AVANCE_PED_GS250179_.Utilidades;
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
<<<<<<< Updated upstream
            if (this.Owner != null)
            {
                this.Owner.Show(); // Volvemos a mostrar el Form2 original con su rol intacto
                this.Close();      // Cerramos esta ventana actual por completo para liberar memoria
            }
            else
            {
                // Por si acaso lo abriste directo sin pasarle el dueño, abrimos uno por defecto
                Form2 menu = new Form2(1);
                menu.Show();
                this.Hide();
            }
=======

>>>>>>> Stashed changes
        }

        private void Añadir_U_Click(object sender, EventArgs e)
        {

        }

        private void EditarU_Click(object sender, EventArgs e)
        {

        }

        private void EliminarU_Click(object sender, EventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvUnidades.DataSource = service.BuscarUnidad(txtBuscar.Text);
        }

        private void CargarTabla()
        {
            dgvUnidades.DataSource = service.MostrarUnidades();

            dgvUnidades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvUnidades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvUnidades.EnableHeadersVisualStyles = false;
            dgvUnidades.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSlateGray;
            dgvUnidades.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Form7 Au = new Form7();
            Au.Show();

            this.Hide();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUnidades.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No hay datos"
                    );

                    return;
                }

                if (dgvUnidades.CurrentRow == null)
                {
                    MessageBox.Show(
                        "Seleccione una fila"
                    );

                    return;
                }

                int id =
                    Convert.ToInt32(
                        dgvUnidades.CurrentRow.Cells["Id"].Value
                    );

                Form7 form =
                    new Form7(id);

                form.ShowDialog();

                CargarTabla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message
                );
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUnidades.Rows.Count > 0)
            {
                int id = Convert.ToInt32(
                    dgvUnidades.CurrentRow.Cells[0].Value
                );

                DialogResult resultado = MessageBox.Show("¿Desea eliminar esta unidad?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    bool eliminado = service.EliminarUnidad(id);

                    if (eliminado)
                    {
                        MessageBox.Show("Unidad eliminada");

                        CargarTabla();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar");
                    }
                }
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Form6_Shown(object sender, EventArgs e)
        {
            RedondearBoton.RedondearBotones(btnAgregar, 30);
            RedondearBoton.RedondearBotones(btnEditar, 30);
            RedondearBoton.RedondearBotones(btnEliminar, 30);
            RedondearBoton.RedondearBotones(btnRegresar, 30);
        }
    }
}
