using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DifferentShapes
{
    [Serializable]
    public abstract class Shape
    {
        protected int x, y;
        public static int radius;
        public static Color Colour;
        protected bool used;
        protected bool moving;
        protected int dx, dy;
        static Shape()
        {
            radius = 25;
            Colour = Color.Red;
        }

        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.used = false;
            moving = false;
        }

        public abstract int Dx
        {
            get;
            set;
        }

        public abstract int Dy
        {
            get;
            set;
        }

        public abstract int X
        {
            get;
            set;
        }

        public abstract int Y
        {
            get;
            set;
        }

        public abstract bool Used
        {
            get;
            set;
        }
        public abstract bool Moving
        {
            get;
            set;
        }

        public abstract bool IsInside(int x, int y);

        public abstract void draw(Graphics G);

    }
}
