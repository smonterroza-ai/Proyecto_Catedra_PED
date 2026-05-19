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
    public partial class Form3 : Form
    {
        RutaService rutaService = new RutaService();
        private int _idRol;

        // Recibe el rol desde Form2 de manera obligatoria al navegar
        public Form3(int idRol = 1)
        {
            InitializeComponent();
            _idRol = idRol;
        }

        private void CargarDatosGrid(string filtro = "")
        {
            try
            {
                dgvRutas.DataSource = rutaService.ObtenerRutas(filtro);

                if (dgvRutas.Columns["IdRutaBuses"] != null)
                {
                    dgvRutas.Columns["IdRutaBuses"].Visible = false;
                }

                dgvRutas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar rutas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            CargarDatosGrid();

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
            CargarDatosGrid(txtBuscar.Text.Trim());
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Protección por código
            if (_idRol == 2) return;

            Form4 añadir = new Form4();
            añadir.Show();
            this.Hide();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Protección por código
            if (_idRol == 2) return;

            if (dgvRutas.SelectedRows.Count > 0)
            {
                RutaDTO rutaSeleccionada = (RutaDTO)dgvRutas.SelectedRows[0].DataBoundItem;

                Form4 frmRuta = new Form4();
                frmRuta.Owner = this;
                this.Hide();

                frmRuta.ConfigurarModoEditar(rutaSeleccionada);

                if (frmRuta.ShowDialog() == DialogResult.OK)
                {
                    CargarDatosGrid(txtBuscar.Text.Trim());
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila completa haciendo clic en el extremo izquierdo de la tabla para poder editarla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Protección por código
            if (_idRol == 2) return;

            if (dgvRutas.SelectedRows.Count > 0)
            {
                int idSeleccionado = Convert.ToInt32(dgvRutas.SelectedRows[0].Cells["IdRutaBuses"].Value);
                string nombreRuta = dgvRutas.SelectedRows[0].Cells["NumeroRuta"].Value.ToString();

                if (MessageBox.Show($"¿Estás seguro de eliminar la ruta {nombreRuta} permanentemente?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        if (rutaService.EliminarRuta(idSeleccionado))
                        {
                            MessageBox.Show("Ruta eliminada con éxito.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarDatosGrid(txtBuscar.Text.Trim());
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila completa (haciendo clic en el margen izquierdo de la tabla) para eliminarla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        private void Form3_Shown(object sender, EventArgs e)
        {
            // Evitamos aplicar GDI+ a elementos invisibles para que no fuercen su reaparición
            if (btnAgregar.Visible) RedondearBoton.RedondearBotones(btnAgregar, 30);
            if (btnEditar.Visible) RedondearBoton.RedondearBotones(btnEditar, 30);
            if (btnEliminar.Visible) RedondearBoton.RedondearBotones(btnEliminar, 30);
            RedondearBoton.RedondearBotones(btnRegresar, 30);
        }

        // Métodos requeridos por el diseñador de VS
        private void btnAtras_Click(object sender, EventArgs e) { }
        private void btnAR_Click(object sender, EventArgs e) { }
        private void btnEditarR_Click(object sender, EventArgs e) { }
        private void btnEliminarRuta_Click(object sender, EventArgs e) { }
    }
}