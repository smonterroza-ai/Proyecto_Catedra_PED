using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVANCE_PED_GS250179_.Utilidades
{
    internal class RedondearBoton
    {
        public static void RedondearBotones(Button boton, int radio)
        {
            GraphicsPath path = new GraphicsPath();

            path.StartFigure();

            path.AddArc(0, 0, radio, radio, 180, 90);

            path.AddArc(boton.Width - radio, 0, radio, radio, 270, 90);

            path.AddArc(boton.Width - radio, boton.Height - radio, radio, radio, 0, 90);

            path.AddArc(0, boton.Height - radio, radio, radio, 90, 90);

            path.CloseFigure();

            boton.Region = new Region(path);
        }
    }
}
