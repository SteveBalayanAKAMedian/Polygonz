using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ShapeLib;
using DefShapes;

namespace Polygonz
{
    class Triple
    {
        int first, second, index;

        public Triple(int first, int second, int index)
        {
            this.first = first;
            this.second = second;
            this.index = index;
        }

        public int First
        {
            get
            {
                return first;
            }
            set
            {
                first = value;
            }
        }

        public int Second
        {
            get
            {
                return second;
            }
            set
            {
                second = value;
            }
        }

        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
            }
        }
    }

    abstract class Operation
    {
        public Operation()
        {

        }

        public abstract void Undo(List<Shape> arr);

        public abstract void Redo(List<Shape> arr);
    }

    class ActColour : Operation
    {
        Color tempColor;
        Color newColor;

        public ActColour(Color tempColor) : base()
        {
            this.tempColor = tempColor;
        }

        public override void Undo(List<Shape> arr) 
        {
            newColor = Shape.Colour;
            Shape.Colour = tempColor;
        }

        public override void Redo(List<Shape> arr) 
        {
            tempColor = Shape.Colour;
            Shape.Colour = newColor;
        }
    }

    class ActRadius : Operation
    {
        int deltaR;

        public ActRadius(int deltaR) : base()
        {
            this.deltaR = deltaR;
        }

        public override void Undo(List<Shape> arr) 
        {
            Shape.radius -= this.deltaR;
        }

        public override void Redo(List<Shape> arr) 
        {
            Shape.radius += this.deltaR;
        }
    }

    class ActAdd : Operation
    {
        int x, y, id;
        Type type;

        public ActAdd(int x, int y, int id, Type type) : base()
        {
            this.x = x;
            this.y = y;
            this.id = id;
            this.type = type;
        }
        public override void Undo(List<Shape> arr)
        {
            arr.RemoveAt(this.id);
        }

        public override void Redo(List<Shape> arr)
        {
            if (type.Name == "Circle")
            {
                arr.Insert(this.id, new Circle(x, y));
            }
            if (type.Name == "Triangle")
            {
                arr.Insert(this.id, new Triangle(x, y));
            }
            if (type.Name == "Square")
            {
                arr.Insert(this.id, new Square(x, y));
            }
        }
    }

    class ActRemove : Operation
    {
        int x, y, id;
        Type type;

        public ActRemove(int x, int y, int id, Type type) : base()
        {
            this.x = x;
            this.y = y;
            this.id = id;
            this.type = type;
        }
        public override void Redo(List<Shape> arr)
        {
            arr.RemoveAt(this.id);
        }

        public override void Undo(List<Shape> arr)
        {
            if (type.Name == "Circle")
            {
                arr.Insert(this.id, new Circle(x, y));
            }
            if (type.Name == "Triangle")
            {
                arr.Insert(this.id, new Triangle(x, y));
            }
            if (type.Name == "Square")
            {
                arr.Insert(this.id, new Square(x, y));
            }
        }
    }

    class ActMove : Operation
    {
        int x, y, id;

        public ActMove(int x, int y, int id) : base ()
        {
            this.x = x;
            this.y = y;
            this.id = id;
        }

        public override void Undo(List<Shape> arr)
        {
            arr[this.id].X -= this.x;
            arr[this.id].Y -= this.y;
        }

        public override void Redo(List<Shape> arr)
        {
            arr[this.id].X += this.x;
            arr[this.id].Y += this.y;
        }
    }
}
