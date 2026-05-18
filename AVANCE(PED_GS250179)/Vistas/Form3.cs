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
    public partial class Form3 : Form
    {
        RutaService rutaService = new RutaService();
        public Form3()
        {
            InitializeComponent();
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

        private void btnAtras_Click(object sender, EventArgs e)
        {
            var menuPrincipal = Application.OpenForms.OfType<Form2>().FirstOrDefault();

            if (menuPrincipal != null)
            {
                menuPrincipal.Show(); // Lo volvemos a mostrar
            }
            else if (this.Owner != null)
            {
                this.Owner.Show(); // Plan B, por si acaso sí tiene Owner
            }

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
            if (dgvRutas.SelectedRows.Count > 0)
            {
                
                RutaDTO rutaSeleccionada = (RutaDTO)dgvRutas.SelectedRows[0].DataBoundItem;

                
                Form4 frmRuta = new Form4(); 
                frmRuta.Owner = this; 

               
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

        private void btnEliminarRuta_Click(object sender, EventArgs e)
        {
            if (dgvRutas.SelectedRows.Count > 0)
            {
                // Extraemos el ID y el Nombre de la ruta seleccionada usando los nombres de las propiedades de tu DTO
                int idSeleccionado = Convert.ToInt32(dgvRutas.SelectedRows[0].Cells["IdRutaBuses"].Value);
                string nombreRuta = dgvRutas.SelectedRows[0].Cells["NumeroRuta"].Value.ToString();

                if (MessageBox.Show($"¿Estás seguro de eliminar la ruta {nombreRuta} permanentemente?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        if (rutaService.EliminarRuta(idSeleccionado))
                        {
                            MessageBox.Show("Ruta eliminada con éxito.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarDatosGrid(txtBuscar.Text.Trim()); // Refrescamos la tabla
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

        private void Form3_Load(object sender, EventArgs e)
        {
            CargarDatosGrid();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarDatosGrid(txtBuscar.Text.Trim());
        }
    }
}
