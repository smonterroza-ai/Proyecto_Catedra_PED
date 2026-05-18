using AVANCE_PED_GS250179_.Modelos;
using AVANCE_PED_GS250179_.Servicio;
using AVANCE_PED_GS250179_.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_.Vistas
{
    public partial class Form8 : Form
    {
        EmpleadoService service = new EmpleadoService();

        private bool esEditar = false;

        private int idEmpleadoEditar = 0;

        public int IdEmpleado { get; private set; }

        public Form8()
        {
            InitializeComponent();
        }
        public Form8(int idEmpleado)
        {
            InitializeComponent();

            esEditar = true;

            idEmpleadoEditar = idEmpleado;
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            CargarTabla();
        }

        private void CargarTabla()
        {
            dgvEmpleados.DataSource = service.MostrarEmpleados();

            dgvEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvEmpleados.EnableHeadersVisualStyles = false;
            dgvEmpleados.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSlateGray;
            dgvEmpleados.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        }

        private void btnAR_Click(object sender, EventArgs e)
        {

        }

        private void btnAtras_Click(object sender, EventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvEmpleados.DataSource = service.BuscarEmpleado(txtBuscar.Text);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnEditarM_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Form11 form = new Form11();
            form.Show();

            this.Hide();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvEmpleados.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No hay datos"
                    );

                    return;
                }

                if (dgvEmpleados.CurrentRow == null)
                {
                    MessageBox.Show(
                        "Seleccione una fila"
                    );

                    return;
                }

                int idEmpleado =
                    Convert.ToInt32(
                        dgvEmpleados.CurrentRow.Cells["idEmpleado"].Value
                    );

                this.Hide();

                Form12 form =
                    new Form12(idEmpleado);

                form.ShowDialog();

                this.Hide();

                CargarTabla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message
                );
            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (dgvEmpleados.Rows.Count > 0)
            {
                int id = Convert.ToInt32(
                    dgvEmpleados.CurrentRow.Cells[0].Value
                );

                DialogResult resultado = MessageBox.Show("¿Desea eliminar este empleado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    bool eliminado = service.EliminarEmpleado(id);

                    if (eliminado)
                    {
                        MessageBox.Show("Empleado eliminado");
                        CargarTabla();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar");
                    }
                }
            }
        }

        private void Form8_Shown(object sender, EventArgs e)
        {
            RedondearBoton.RedondearBotones(btnAgregar, 30);
            RedondearBoton.RedondearBotones(btnEditar, 30);
            RedondearBoton.RedondearBotones(btnEliminar, 30);
            RedondearBoton.RedondearBotones(btnRegresar, 30);
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
