using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zegar
{
    public partial class Form1 : Form
    {
        int x, y;
        Timer t = new Timer();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Location = new Point(10, ClientSize.Height - 30);
            label1.Location = new Point(ClientSize.Width/2-60, ClientSize.Height/2);
            label1.Text = "ZEGAR WYŁĄCZONY";
            t.Interval = 1000;
            t.Tick += new EventHandler(timer1_Tick);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1.Location = new Point(10, ClientSize.Height-30);

            int size = ClientSize.Width;
            if(size>ClientSize.Height)
            {
                size = ClientSize.Height;
            }
            x = size / 2;
            y = size / 2;
            Graphics g = this.CreateGraphics();
            Pen p = new Pen(Color.Black, 1f);
            Point srod = new Point(x, y);
            label1.Location = new Point(size / 2-24,40); 

            int sek = DateTime.Now.Second;
            int min = DateTime.Now.Minute;
            int godz = DateTime.Now.Hour;

            if (sek < 10)
            {
                label1.Text = godz + ":" + min + ":" + "0"+sek;
            }
            else
            {
                label1.Text = godz + ":" + min + ":" + sek;
            }

            int[] koniec_kordy = new int[2];

            g.Clear(Color.White);

            g.DrawEllipse(p, 0, 0, size, size);

            koniec_kordy = sek_kordy(sek, size / 3+30);
            g.DrawLine(p, srod, new Point(koniec_kordy[0], koniec_kordy[1]));

            koniec_kordy = sek_kordy(min, size/4+20);
            g.DrawLine(p, srod, new Point(koniec_kordy[0], koniec_kordy[1]));

            koniec_kordy = godz_kordy(godz % 12, min, size/5+20);
            g.DrawLine(p, srod, new Point(koniec_kordy[0], koniec_kordy[1]));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            t.Start();
        }

        private int[] sek_kordy(int val, int dl)
        {
            int[] kordy = new int[2];
            val *= 6;

            if (val >= 0 && val <= 180)
            {
                kordy[0] = x + (int)(dl * Math.Sin(Math.PI * val / 180));
                kordy[1] = y - (int)(dl * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                kordy[0] = x - (int)(dl * -Math.Sin(Math.PI * val / 180));
                kordy[1] = y - (int)(dl * Math.Cos(Math.PI * val / 180));
            }
            return kordy;
        }

        private int[] godz_kordy(int hval, int mval, int dl)
        {
            int[] kordy = new int[2];

            int val = (int)((hval * 30) + (mval * 0.5));

            if (val >= 0 && val <= 180)
            {
                kordy[0] = x + (int)(dl * Math.Sin(Math.PI * val / 180));
                kordy[1] = y - (int)(dl * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                kordy[0] = x - (int)(dl * -Math.Sin(Math.PI * val / 180));
                kordy[1] = y - (int)(dl * Math.Cos(Math.PI * val / 180));
            }
            return kordy;
        }
    }
}
