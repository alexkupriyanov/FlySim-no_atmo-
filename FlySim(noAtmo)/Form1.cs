using System;
using System.Windows.Forms;

namespace FlySim_noAtmo_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        decimal t = 0, x0, y0, v0, cosa, sina;
        private bool finished;
        private void Pause_Click(object sender, EventArgs e)
        {
            if (!finished)
            {
                if (timer1.Enabled)
                {
                    timer1.Stop();
                    Pause.Text = "Resume";
                }
                else
                {
                    timer1.Start();
                    Pause.Text = "Pause";

                }
            }
        }


        const decimal g = 9.81M;
        private void Launch_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                finished = false;
                chart1.Series[0].Points.Clear();
                t = 0;
                x0 = 0;
                y0 = Height.Value;
                v0 = Speed.Value;
                double a = (double)Angle.Value * Math.PI / 180;
                cosa = (decimal)Math.Cos(a);
                sina = (decimal)Math.Sin(a);
                chart1.Series[0].Points.AddXY(x0, y0);
                timer1.Start();
            }
        }
        const decimal dt = 0.1M;
        private void timer1_Tick(object sender, EventArgs e)
        {
            t += dt;
            decimal x = x0 + v0 * cosa * t;
            decimal y = y0 + v0 * sina * t - g * t * t / 2;
            chart1.Series[0].Points.AddXY(x, y);
            updateTime();
            if (y <= 0)
            {
                timer1.Stop();
                finished = true;
            }
        }

        private void updateTime()
        {
            label4.Text = "Current time: " + t;
        }
    }
}
