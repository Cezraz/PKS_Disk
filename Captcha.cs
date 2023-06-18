using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace рег
{
    public partial class Captcha : Form
    { 
        public string text;
        public bool resultat;
        public Captcha()
        {
            InitializeComponent();
            pictureBox1.Image = this.Imag(pictureBox1.Width, pictureBox1.Height);
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == this.text)
            {
                MessageBox.Show("Верно!");
                Close();
                Form1 d = new Form1();
                d.reg();
            }
            else
            {
                MessageBox.Show("Ошибка!");
                pictureBox1.Image = Imag(pictureBox1.Width, pictureBox1.Height);
                textBox1.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            pictureBox1.Image = Imag(pictureBox1.Width, pictureBox1.Height);
        }
        private Bitmap Imag(int Width, int Height)
        {
            Random rnd = new Random();


            Bitmap result = new Bitmap(Width, Height);




            Brush[] colors = {Brushes.White,};


            Pen[] colorpens = {
        Pens.Black,
        Pens.Red,
        Pens.RoyalBlue,
        Pens.Green,
        Pens.Yellow,
        Pens.White,};


            FontStyle[] fontstyle = {
        FontStyle.Bold,
        FontStyle.Italic,
        FontStyle.Regular,
        FontStyle.Strikeout,
        FontStyle.Underline};



            Int16[] rotate = { 1, -1, 2, -2, 3, -3, 4, -4, 5, -5, 6, -6 };


            Graphics g = Graphics.FromImage((Image)result);

            g.RotateTransform(rnd.Next(rotate.Length));


            g.Clear(Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255)));

            this.text = String.Empty;
            string ALF = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            int Xpos = 10;
            for (int i = 0; i < 5; ++i)
            {
                char k = ALF[rnd.Next(ALF.Length)];
                this.text += k;
                g.DrawString(Char.ToString(k), new Font("Arial", 25, fontstyle[rnd.Next(fontstyle.Length)]), colors[rnd.Next(colors.Length)], new PointF(Xpos, rnd.Next(0,50)));
                Xpos +=40;
            }


            g.DrawLine(colorpens[rnd.Next(colorpens.Length)], new Point(rnd.Next(0, 30), rnd.Next(Height)), new Point(rnd.Next(138,276), rnd.Next(Height)));
            g.DrawLine(colorpens[rnd.Next(colorpens.Length)], new Point(rnd.Next(0, 13), rnd.Next(Height)), new Point(rnd.Next(138, 276), rnd.Next(Height)));
            g.DrawLine(colorpens[rnd.Next(colorpens.Length)], new Point(rnd.Next(0, 17), rnd.Next(Height)), new Point(rnd.Next(138, 276), rnd.Next(Height)));
            g.DrawLine(colorpens[rnd.Next(colorpens.Length)], new Point(rnd.Next(0, 137), rnd.Next(Height)), new Point(rnd.Next(138, 276), rnd.Next(Height)));


            return result;
        }
    }
    
}
