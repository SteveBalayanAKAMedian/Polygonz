using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DifferentShapes
{
    [Serializable]
    public class Circle : Shape
    {
        public Circle(int x, int y) : base(x, y) { }

        public override int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        public override int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }
        public override bool Used
        {
            get
            {
                return used;
            }
            set
            {
                used = value;
            }
        }
        public override bool Moving
        {
            get
            {
                return moving;
            }
            set
            {
                moving = value;
            }
        }
        public override int Dx
        {
            get
            {
                return dx;
            }
            set
            {
                dx = value;
            }
        }
        public override int Dy
        {
            get
            {
                return dy;
            }
            set
            {
                dy = value;
            }
        }

        //public override Color Color
        //{
        //    get
        //    {
        //        return Colour;
        //    }
        //    set
        //    {
        //        Colour = value;
        //    }
        //}
        //public override int Radius
        //{
        //    get
        //    {
        //        return radius;
        //    }
        //    set
        //    {
        //        radius = value;
        //    }
        //}
        public override void draw(Graphics G)
        {
            SolidBrush brush = new SolidBrush(Colour);
            G.FillEllipse(brush, new Rectangle(x - Shape.radius, y - Shape.radius, Shape.radius * 2, Shape.radius * 2));
        }

        public override bool IsInside(int x, int y)
        {
            int delta_x = x - this.x;
            int delta_y = y - this.y;
            int dist = delta_x * delta_x + delta_y * delta_y;
            return dist <= Shape.radius * Shape.radius;
        }
    }
}
