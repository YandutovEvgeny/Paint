using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        //Создаём хост для рисования
        Graphics graphics;
        //Создаём кисть
        SolidBrush brush, rightBrush;
        //Переменные х и у нужны для запоминания положения курсора мышки во время нажатия
        int x;
        int y;
        float secondAngle;  //Угол на который мы хотим отклонять наши ветки дерева
        
        public Form1()
        {
            InitializeComponent();
            //Привязываем PictureBox к хосту
            graphics = pictureBox1.CreateGraphics();
            //Присваиваем цвет и форму кисти, инициализация объекта кисти
            brush = new SolidBrush(Color.Green); //SolidBrush - залитая сплошная кисть 
            rightBrush = new SolidBrush(Color.Brown);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) //Рисование кистью
        {
            if (radioButton1.Checked)
            {
                //e - "указатель" на наш курсор мыши
                if (e.Button == MouseButtons.Left)   //При нажатии ЛКМ:
                    graphics.FillEllipse(brush, e.X, e.Y, trackBar1.Value, trackBar1.Value);  //Рисуется круг с координатами позиции курсора мышки  
                if (e.Button == MouseButtons.Right) //При нажатии ПКМ:
                    graphics.FillEllipse(rightBrush, e.X, e.Y, trackBar1.Value, trackBar1.Value); 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                rightBrush.Color = colorDialog1.Color;
                button2.BackColor = colorDialog1.Color;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)   //Рисование ручкой линий
        {
            if (radioButton2.Checked)
            {
                Pen pen = new Pen(brush);
                if (e.Button == MouseButtons.Left)
                    pen = new Pen(brush, trackBar1.Value);
                if (e.Button == MouseButtons.Right)
                    pen = new Pen(rightBrush, trackBar1.Value);
                
                graphics.DrawLine(pen, x, y, e.X, e.Y); 
            }
        }

        void DrawLine(float x, float y, float angle, int length)
        {
            if (length < 10)return;

            float x1 = (float)Math.Cos(angle * (Math.PI / 180)) * length + x;
            float y1 = (float)Math.Sin(angle * (Math.PI / 180)) * length + y;
            Pen pen = new Pen(brush, trackBar1.Value - 3);
            if (length < 40)
                pen.Color = rightBrush.Color;
            graphics.DrawLine(pen, x, y, x1, y1);

            DrawLine(x1, y1, angle - secondAngle, length - 8);
            DrawLine(x1, y1, angle + secondAngle, length - 8);
            /*DrawLine(x1, y1, angle - secondAngle*2, length - 8);
            DrawLine(x1, y1, angle + secondAngle*2, length - 8);*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            graphics = pictureBox1.CreateGraphics();
            graphics.Clear(Color.White);
            secondAngle = (float)Convert.ToDouble(textBox1.Text);
            secondAngle *= -1;
            DrawLine(pictureBox1.Width / 2, pictureBox1.Height - 40, -90, 100);
        }

        void DrawGearWheel(int count, int length)
        {
            float angle = -90;
            float secondAngle = 360 / count;
            int x = pictureBox1.Width / 2;
            int y = pictureBox1.Height / 2;
            for (int i = 0; i < count; i++)
            {
                float x1 = (float)Math.Cos(angle * (Math.PI / 180)) * length + x;
                float y1 = (float)Math.Sin(angle * (Math.PI / 180)) * length + y;
                Pen pen = new Pen(brush, trackBar1.Value);
                graphics.DrawLine(pen, x, y, x1, y1);
                angle += secondAngle;
            }
            graphics.FillEllipse(brush, x - 80, y - 80, 160, 160);
            graphics.FillEllipse(new SolidBrush(Color.White), x - 40, y - 40, 80, 80);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            graphics = pictureBox1.CreateGraphics();
            graphics.Clear(Color.White);
            DrawGearWheel(Convert.ToInt32(textBox2.Text), 100);
        }

        void DrawPine()
        {
            Point point1 = new Point(250, 10);
            Point point2 = new Point(270, 40);
            Point point3 = new Point(230, 40);
            Point point4 = new Point(250, 40);
            Point point5 = new Point(285, 85);
            Point point6 = new Point(215, 85);
            Point point7 = new Point(250, 85);
            Point point8 = new Point(303, 153);
            Point point9 = new Point(197, 153);
            Point point10 = new Point(250, 153);
            Point point11 = new Point(330, 255);
            Point point12 = new Point(170, 255);
            Point[] polygonPoints1 = { point1, point2, point3 };
            Point[] polygonPoints2 = { point4, point5, point6 };
            Point[] polygonPoints3 = { point7, point8, point9 };
            Point[] polygonPoints4 = { point10, point11, point12 };
            
            graphics.FillPolygon(brush, polygonPoints1);
            graphics.FillPolygon(brush, polygonPoints2);
            graphics.FillPolygon(brush, polygonPoints3);
            graphics.FillPolygon(brush, polygonPoints4);
            graphics.FillRectangle(rightBrush, 235, 255, 30, 30);
        }


        private void button5_Click(object sender, EventArgs e)
        {
            //TODO: Кнопка отрисовывает ёлку
            graphics = pictureBox1.CreateGraphics();
            graphics.Clear(Color.White);
            DrawPine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                brush.Color = colorDialog1.Color;
                button1.BackColor = colorDialog1.Color;
            }
        }
    }
}
