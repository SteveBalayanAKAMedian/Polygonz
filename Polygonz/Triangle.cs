using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Polygonz
{
    [Serializable]
    class Triangle : Shape
    {
        public Triangle(int x, int y) : base(x, y) { }

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
            int side = (int)(Math.Sqrt(3) * Shape.radius);
            Point B = new Point(x, y - Shape.radius);
            Point A = new Point(x - side / 2, y + Shape.radius / 2);
            Point C = new Point(x + side / 2, y + Shape.radius / 2);
            Point[] arr = { A, B, C };
            G.FillPolygon(brush, arr);
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

        /*
         * Векторное произведение (англ. cross product) — произведение длин векторов на синус угла между ними, причём знак этого синуса 
         * зависит от порядка операндов. Оно тоже удобно выражается в координатах: x1 * y2 - x2 * y1.
         * Свойства:
         * Скалярное произведение антисимметрично a*b=−(b*a).
         * Геометрически, это ориентированный объем параллелограмма, натянутого на эти вектора.
         * Коллинеарные вектора должны иметь нулевое векторное произведение.
         * Если b «слева» от a, то оно положительное.
         * Если b «справа» — то отрицательное.
         * 
         * Пусть у нас есть треугольник ABC (заданный против часовой стрелки) и точка P. Тогда она должна лежать слева от всех трёх векторов AB, BC и CA. 
         * Это условие задаст пересечение трёх полуплоскостей, которое и будет нужным треугольником.
         */

        private bool turn_left(int x1, int y1, int x2, int y2) //векторное произведение векторов {x1, y1} и {x2, y2} образуют поворот налево?
        {
            return (x1 * y2 - x2 * y1) >= 0;
        }
        
        public override bool IsInside(int x, int y)
        {
            int side = (int)(Math.Sqrt(3) * Shape.radius);
            Point C = new Point(this.x, this.y - Shape.radius);
            Point B = new Point(this.x - side / 2, this.y + Shape.radius / 2);
            Point A = new Point(this.x + side / 2, this.y + Shape.radius / 2);
            bool one = turn_left(B.X - A.X, B.Y - A.Y, x - A.X, y - A.Y),
                two = turn_left(C.X - B.X, C.Y - B.Y, x - B.X, y - B.Y),
                three = turn_left(A.X - C.X, A.Y - C.Y, x - C.X, y - C.Y);

            return one && two && three;
        }

    }
}
