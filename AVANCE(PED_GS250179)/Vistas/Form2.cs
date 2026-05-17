using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_
{
    public partial class Form2 : Form
    {
        public Form2(int idRol)
        {
            InitializeComponent();

            if (idRol == 2) 
            {
                // Aquí pones el nombre de los botones ocultas
                btnT.Visible = false;
            }
        }

        private static bool MostrarMensaje = false;

        private void btnR_Click(object sender, EventArgs e)
        {
            Form3 ruta = new Form3();
            ruta.Show(this);
            this.Hide();



        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de cerrar sesión?", "CERRAR SESIÓN", MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                Form1 login = new Form1();
                login.Show();
                this.Close();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (!MostrarMensaje)
            {
                DialogResult r = MessageBox.Show("En la siguiente pestaña encontrará lo que son tres áreas principales.", "Instrucciones",
                    MessageBoxButtons.OK);

                if (r == DialogResult.OK)
                {
                    MessageBox.Show("El botón de la esquina superior derecha es para salir del menú");
                }

                MostrarMensaje = true;
            }
        }

        private void btnT_Click(object sender, EventArgs e)
        {
            Form5 Tran = new Form5();
            Tran.Show(this);

            this.Hide();
        }

        private void btnU_Click(object sender, EventArgs e)
        {
            Form6 Uni = new Form6();
            Uni.Show(this);

            this.Hide();
        }
    }
}
