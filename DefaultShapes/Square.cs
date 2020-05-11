using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DefaultShapes
{
    [Serializable]
    public class Square : Shape
    {
        public Square(int x, int y) : base(x, y) { }

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

        public override void draw(Graphics G)
        {
            SolidBrush brush = new SolidBrush(Colour);
            int delta = (int)(Shape.radius / (Math.Sqrt(2) / 2) / 2);
            int side = (int)((Shape.radius) / (Math.Sqrt(2) / 2));
            G.FillRectangle(brush, new Rectangle(x - delta, y - delta, side, side));
        }

        public override bool IsInside(int x, int y)        
        {
            int half_side = (int)(Shape.radius * Math.Sqrt(2) / 2);
            return x <= this.x + half_side && y <= this.y + half_side && x >= this.x - half_side && y >= this.y - half_side;
        }

    }
}
