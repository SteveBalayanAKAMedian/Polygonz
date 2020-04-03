using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polygonz
{
    public partial class Form2 : Form
    {
        public delegate void RadiusEventHandler(object sender, RadiusEventArgs e);

        public RadiusEventHandler delRad;

        public Form2(int radiusSaved)
        {
            InitializeComponent();
            trackBar1.Maximum = 100;
            trackBar1.Value = radiusSaved;
            trackBar1.TickFrequency = 5;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //radiusSaved = trackBar1.Value;
            delRad(this, new RadiusEventArgs(trackBar1.Value));
        }

    }
    public class RadiusEventArgs : EventArgs
    {
        public int rad;

        public RadiusEventArgs(int _rad)
        {
            rad = _rad;
        }
    }
}
