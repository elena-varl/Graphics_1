using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private Graphics g;

        // Cache font instead of recreating font objects each time we paint.
        delegate float Function(int val);
        private float Sq(int x) { return x * x; }
        private float Sinus(int x) { return (float)Math.Sin(x); }
        private float Cosinus(int x) { return (float)Math.Cos(x); }
        private float Sq2(int x) { return (float)(Math.Pow(x, 2) - 6 * x + 9); }
        public Form1()
        {
            InitializeComponent();
            
            this.Size=new Size(500, 500);
            this.pictureBox1.Size = new Size(this.Width, this.Height);
            this.pictureBox1.Image = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            g = Graphics.FromImage(this.pictureBox1.Image);
            pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
        }

        //использовать линии и проходить по оси с каким-то шагом, функции от одной переменной
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {/*
            Pen pen = new Pen(Color.SlateBlue);
            SolidBrush solid = new SolidBrush(Color.Red);
            g.FillEllipse(solid, e.X, e.Y, 5, 5);
            g.DrawEllipse(pen, e.X, e.Y, 5, 5);

            g.Flush();
            solid.Dispose();
            pen.Dispose();
            pictureBox1.Invalidate();*/
        }
        private void Draw(Function f, System.Windows.Forms.PaintEventArgs e)
        {
            // f = Sinus;
            Graphics g = e.Graphics;
            g.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);//хочу сместить начало координат
            g.ScaleTransform(1*20, -1*20);
            
            //square sq = x => x * x;//рисуем квадратичную параболу
            //sin s = x => (int)(Math.Sin(x) * 100);//рисуем sin, немного отмасштабированный
            //hyp tg = x => (int)(Math.Tan(x));//тангенc
            // sq_2 sq2 = x => (int)(Math.Pow(x, 3) - 30 * x + 2); //уравнение третьей степени

            int x = -this.ClientSize.Width;
            Pen pen = new Pen(Color.Blue, 0.5F);
           // pen.Width = 0.5F;
            g.DrawLine(pen, -pictureBox1.Width / 2, 0, pictureBox1.Width / 2, 0);
            g.DrawLine(pen, 0, -pictureBox1.Height / 2, 0, pictureBox1.Height / 2);
           // g.DrawLine(System.Drawing.Pens.Green, 0, 1, 2, 100);
            g.Flush();
            pictureBox1.Invalidate();
            Pen pengraph = new Pen(Color.Red, 0.3F);
            float y = f(x);
            //int y = Math.Round(Math.Sin(x), MidpointRounding.ToEven);
            for (int i = x + 1; i < pictureBox1.Right; i += 1)
            {

                float j = f(i);
               
                g.DrawLine(pengraph, x, y, i, j);
                x = i;
                y = j;
                g.Flush();
                pictureBox1.Invalidate();
            }
            g.Flush();
            pictureBox1.Invalidate();


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);

            this.Controls.Add(pictureBox1);
        }
        private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

            Draw(Sq2, e);
        }


        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
