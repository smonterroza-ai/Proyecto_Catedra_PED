using AVANCE_PED_GS250179_.Servicio;
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
    public partial class Form13 : Form
    {
        ClienteService service = new ClienteService();

        public Form13()
        {
            InitializeComponent();
        }

        private void Form13_Load(object sender, EventArgs e)
        {
            dgvClientes.DataSource = service.MostrarClientes();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
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

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvClientes.DataSource =
        service.BuscarClientes(
            txtBuscar.Text.Trim()
        );
        }
    }
}
