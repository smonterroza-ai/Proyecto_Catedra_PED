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
    }
}
