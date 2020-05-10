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
    public partial class Form1 : Form
    {
        Random random = new Random();
        List<Shape> All_Figures;
        public delegate int RadiusChangedDelegate(int value);
        Form2 form2;
        bool runFlex = false;

        public Form1()
        {
            InitializeComponent();
            circleToolStripMenuItem1.Checked = true; //изначально рисуем вершины в форме круга
            All_Figures = new List<Shape>();
            DoubleBuffered = true; //чтобы не моргало при перерисовке
        }

        public void BuildConvexHull(PaintEventArgs e) //построение оболочки за О(n^3) и прорисовка
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; //это просто для красоты
            SolidBrush brush = new SolidBrush(Shape.Colour); //TO DO -- как-то связать с цветом объектов
            //выпуклая оболочка за O(n^3)
            foreach (Shape i in All_Figures) //очевидно, до начала построения выпуклой оболочки в ней нет ни одной вершины
                i.Used = false;
            int index_i = 0;
            foreach (Shape i in All_Figures)
            {
                int index_j = 0;
                foreach (Shape j in All_Figures)
                {
                    if (index_j <= index_i)
                    {
                        ++index_j;
                        continue;
                    }
                    int l_b = i.X - j.X,
                        l_a = j.Y - i.Y,
                        l_c = j.X * i.Y - j.Y * i.X, //коэффициенты прямой, проходящей через точки i и j
                        cnt_up = 0,
                        cnt_down = 0;
                    foreach (Shape k in All_Figures)
                    {
                        if (k.X == j.X && k.Y == j.Y)
                            continue;
                        if (i.X == k.X && i.Y == k.Y)
                            continue;
                        int ans = l_a * k.X + l_b * k.Y + l_c;
                        //TO DO -- разобраться с вариантом ans == 0 (потому что в остальных алгоритмах построения выпуклой оболочки
                        //3 вершины, лежащие на одной прямой, не считаются корректными, центральная удаляется)
                        if (ans >= 0)  //определение, по какую сторону от прямой лежит вершина
                            cnt_up++;
                        if (ans < 0)
                            cnt_down++;
                    }
                    if (cnt_up == 0 || cnt_down == 0) //если все вершины лежат по одну сторону от данной прямой, то эти вершины
                    {                                //нужно пометить как входящие в выпуклую оболочку
                        i.Used = true;
                        j.Used = true;
                        e.Graphics.DrawLine(new Pen(brush), i.X, i.Y, j.X, j.Y);
                    }
                    ++index_j;
                }
                ++index_i;
            }
        }

        public void BuildConvexHull()  //построение оболочки за O(n^3) без прорисовки
        {
            foreach (Shape i in All_Figures) //очевидно, до начала построения выпуклой оболочки в ней нет ни одной вершины
                i.Used = false;
            int index_i = 0;
            foreach (Shape i in All_Figures)
            {
                int index_j = 0;
                foreach (Shape j in All_Figures)
                {
                    if (index_j <= index_i)
                    {
                        ++index_j;
                        continue;
                    }
                    int l_b = i.X - j.X,
                        l_a = j.Y - i.Y,
                        l_c = j.X * i.Y - j.Y * i.X, //коэффициенты прямой, проходящей через точки i и j
                        cnt_up = 0,
                        cnt_down = 0;
                    foreach (Shape k in All_Figures)
                    {
                        if (k.X == j.X && k.Y == j.Y)
                            continue;
                        if (i.X == k.X && i.Y == k.Y)
                            continue;
                        int ans = l_a * k.X + l_b * k.Y + l_c;
                        //TO DO -- разобраться с вариантом ans == 0 (потому что в остальных алгоритмах построения выпуклой оболочки
                        //3 вершины, лежащие на одной прямой, не считаются корректными, центральная удаляется)
                        if (ans >= 0)  //определение, по какую сторону от прямой лежит вершина
                            cnt_up++;
                        if (ans < 0)
                            cnt_down++;
                    }
                    if (cnt_up == 0 || cnt_down == 0) //если все вершины лежат по одну сторону от данной прямой, то эти вершины
                    {                                //нужно пометить как входящие в выпуклую оболочку
                        i.Used = true;
                        j.Used = true;
                    }
                    ++index_j;
                }
                ++index_i;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; //это просто для красоты
            SolidBrush brush = new SolidBrush(Color.Red); //TO DO -- как-то связать с цветом объектов

            foreach(Shape i in All_Figures)
            {
                i.draw(e.Graphics);
            }
            
            if (All_Figures.Count > 2)
            {
                //выпуклая оболочка за O(n^3)
                BuildConvexHull(e); 
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            int flag = 0;
            int index = 0, figure_index = -1;
            foreach (Shape i in All_Figures)        //цикл foreach для ускорения обращения
            {                                       //попали в вершину или нет
                if (i.IsInside(e.X, e.Y))
                {
                    i.Moving = true;
                    i.Dx = e.X - i.X;
                    i.Dy = e.Y - i.Y;
                    flag = 1;
                    figure_index = index;
                }
                ++index;
            }
            if(flag == 0)
            {
                if (circleToolStripMenuItem1.Checked)
                {
                    Circle c = new Circle(e.X, e.Y);
                    All_Figures.Add(c);
                }
                if (squareToolStripMenuItem1.Checked)
                {
                    Square s = new Square(e.X, e.Y);
                    All_Figures.Add(s);
                }
                if (triangleToolStripMenuItem1.Checked)
                {
                    Triangle t = new Triangle(e.X, e.Y);
                    All_Figures.Add(t);
                }
                BuildConvexHull(); //попали внутрь оболочки или нет
                bool moving_whole = false; //строим оболочку, если новая точка (координаты мышки) не вошла в оболочку,
                foreach (Shape i in All_Figures) //то мы попали внутрь
                {
                    if(i.X == e.X && i.Y == e.Y)
                    {
                        if (!i.Used)
                        {
                            moving_whole = true;
                        }
                        All_Figures.Remove(i); //так надо
                        break;
                    }
                }
                Invalidate();
                if(moving_whole)
                {
                    foreach (Shape i in All_Figures)  
                    {                                       
                        i.Moving = true;
                        i.Dx = e.X - i.X;
                        i.Dy = e.Y - i.Y;
                        flag = 1;
                    }
                }
            }
            if (e.Button == MouseButtons.Left)
            {
                if (flag == 0) //если тут можно расположить вершину
                {
                    if (circleToolStripMenuItem1.Checked)
                    {
                        Circle c = new Circle(e.X, e.Y);
                        All_Figures.Add(c);
                    }
                    if (squareToolStripMenuItem1.Checked)
                    {
                        Square s = new Square(e.X, e.Y);
                        All_Figures.Add(s);
                    }
                    if (triangleToolStripMenuItem1.Checked)
                    {
                        Triangle t = new Triangle(e.X, e.Y);
                        All_Figures.Add(t);
                    }

                }
            }
            if(e.Button == MouseButtons.Right)
            {
                if (figure_index != -1) //чтобы не срабатывало при рандомных кликах ПКМ (точно попали в вершину)
                {
                    All_Figures.RemoveAt(figure_index);
                }
            }
            Invalidate();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            foreach(Shape i in All_Figures)
            {
                i.Moving = false;
                i.Dx = 0;
                i.Dy = 0;
            }   
            Invalidate(); //после возможного перемещения вершины нужно перестроить выпуклую оболочку

            //удаление вершин, не лежащих в выпуклой оболочке
            if (All_Figures.Count > 2)
            {
                for(int i = 0; i < All_Figures.Count; ++i)
                {
                    if (!All_Figures[i].Used)
                    {
                        All_Figures.Remove(All_Figures[i]);
                        i--;
                    }
                }
            }

            Invalidate(); //после удаления вершин, не входящих в выпуклую оболочку, надо перерисовать
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (Shape i in All_Figures)
            {
                if(i.Moving)
                {
                    i.X = e.X - i.Dx;
                    i.Y = e.Y - i.Dy;
                }
            } 
            Invalidate();
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Shape.Colour = colorDialog1.Color;
            //All_Figures[0].Color = colorDialog1.Color; 
            Invalidate();
        } //выбор цвета фигур и линий

        private void circleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            circleToolStripMenuItem1.Checked = true;
            triangleToolStripMenuItem1.Checked = false;
            squareToolStripMenuItem1.Checked = false;
        } //выбор формы круга для вершины

        private void triangleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            circleToolStripMenuItem1.Checked = false;
            triangleToolStripMenuItem1.Checked = true;
            squareToolStripMenuItem1.Checked = false;
        } //выбор формы треугольника для вершины

        private void squareToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            circleToolStripMenuItem1.Checked = false;
            triangleToolStripMenuItem1.Checked = false;
            squareToolStripMenuItem1.Checked = true;
        } //выбор формы квадрата для вершины
          
        private void changeRadius(object sender, RadiusEventArgs e)
        {
            Shape.radius = e.rad;
            Refresh();
        }

        private void radiusToolStripMenuItem_Click(object sender, EventArgs e) //cмена радиуса
        {
            if (form2 == null || form2.IsDisposed)
            {
                form2 = new Form2(Shape.radius);
            }
            if(form2.WindowState == FormWindowState.Maximized || form2.WindowState == FormWindowState.Minimized)
            {
                form2.WindowState = FormWindowState.Normal;
            }
            form2.Activate();
            form2.Show();
            form2.delRad += changeRadius;
        }

        private void ChangePosition()
        {
            for(int i = 0; i < All_Figures.Count; ++i)
            {
                All_Figures[i].X += random.Next(-1, 2);
                All_Figures[i].Y += random.Next(-1, 2);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(runFlex)
            {
                ChangePosition();
                Refresh();
                if (All_Figures.Count > 2)
                {
                    for (int i = 0; i < All_Figures.Count; ++i)
                    {
                        if (!All_Figures[i].Used)
                        {
                            All_Figures.Remove(All_Figures[i]);
                            i--;
                        }
                    }
                }
                Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            runFlex = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            runFlex = false;
        }
    }
}
