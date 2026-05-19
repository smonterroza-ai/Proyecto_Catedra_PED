using AVANCE_PED_GS250179_.Servicio;
using AVANCE_PED_GS250179_.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_
{
    public partial class Form6 : Form
    {
        GestionUnidadesService service = new GestionUnidadesService();
        private int _idRol;

        public Form6(int idRol = 1)
        {
            InitializeComponent();
            _idRol = idRol;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            CargarTabla();

            // Ocultación inmediata antes de pintar la interfaz
            if (_idRol == 2)
            {
                btnAgregar.Visible = false;
                btnEditar.Visible = false;
                btnEliminar.Visible = false;
            }
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
            if (_idRol == 2) return;

            Form7 Au = new Form7();
            Au.Show();
            this.Hide();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (_idRol == 2) return;

            try
            {
                if (dgvUnidades.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos");
                    return;
                }

                if (dgvUnidades.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una fila");
                    return;
                }

                int id = Convert.ToInt32(dgvUnidades.CurrentRow.Cells["Id"].Value);

                Form7 form = new Form7(id);
                form.ShowDialog();

                CargarTabla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idRol == 2) return;

            if (dgvUnidades.Rows.Count > 0)
            {
                int id = Convert.ToInt32(dgvUnidades.CurrentRow.Cells[0].Value);

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
            var menuPrincipal = Application.OpenForms.OfType<Form2>().FirstOrDefault();

            if (menuPrincipal != null)
            {
                menuPrincipal.Show();
            }
            else if (this.Owner != null)
            {
                this.Owner.Show();
            }

            this.Close();
        }

        private void Form6_Shown(object sender, EventArgs e)
        {
            // Evitamos aplicar redondeo a botones ocultos para que el render nativo no falle
            if (btnAgregar.Visible) RedondearBoton.RedondearBotones(btnAgregar, 30);
            if (btnEditar.Visible) RedondearBoton.RedondearBotones(btnEditar, 30);
            if (btnEliminar.Visible) RedondearBoton.RedondearBotones(btnEliminar, 30);
            RedondearBoton.RedondearBotones(btnRegresar, 30);
        }

        // Métodos del diseñador obsoletos o sin uso
        private void btnAtras_Click(object sender, EventArgs e) { }
        private void Añadir_U_Click(object sender, EventArgs e) { }
        private void EditarU_Click(object sender, EventArgs e) { }
        private void EliminarU_Click(object sender, EventArgs e) { }
    }
}