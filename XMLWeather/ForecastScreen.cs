using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace XMLWeather
{
    public partial class ForecastScreen : UserControl
    {
        SolidBrush blueGrayBrush = new SolidBrush(Color.FromArgb(230, 232, 233));//lig
        SolidBrush lightBlueBrush = new SolidBrush(Color.FromArgb(178, 200, 215));
        SolidBrush darkGreyBrush = new SolidBrush(Color.FromArgb(151, 170, 182));
        SolidBrush midBlueBrush = new SolidBrush(Color.FromArgb(102, 146, 173));
        SolidBrush darkBlueBrush = new SolidBrush(Color.FromArgb(17, 50, 75));
        SolidBrush silverBrush = new SolidBrush(Color.Silver);
        Font topFont = new Font("Calibri", 14);
        Pen dayUnderline = new Pen(Color.FromArgb(102, 146, 173));
        int topBarHeight = 60, topBarX = 5, topBarY = 5, screenBarHeight = 45, screenBarX = 5, screenBarY, buffer = 7;
        Region currentClick = new Region();    
        List<Region> foreCastRegions = new List<Region>();
        Graphics g;
        string country, conditions;
        List<string> maxTemps = new List<string>(), minTemps = new List<string>();
        PictureBox[] allDays;

        public ForecastScreen()
        {
            InitializeComponent();
            displayForecast();
        }

        public void displayForecast()
        {
            allDays = new[] { day1WeatherBox, day2WeatherBox, day3WeatherBox, day4WeatherBox };

            country = Form1.days[0].country;
            conditions = Form1.days[0].condition;
            string upper = conditions.First().ToString();
            upper = upper.ToUpper();
            conditions = conditions.Remove(0, 1);
            conditions = conditions.Insert(0, upper);
            screenBarY = topBarY + topBarHeight + buffer;
            currentClick = new Region(new Rectangle(this.Width / 4 - 10, topBarHeight + 12, 55, 45));

            int rectHeight = (this.Height - (screenBarY + screenBarHeight + 5 * buffer)) / 4;
            for (int i = 0; i < 4; i++)
            {
                Rectangle rect = new Rectangle(5, (screenBarY + screenBarHeight + buffer) + i * (rectHeight + buffer), this.Width - 10, rectHeight);
                GraphicsPath drawPath = new GraphicsPath();
                drawPath.AddArc(rect.X, rect.Y, 50, 50, 180, 90);
                drawPath.AddArc(rect.X + rect.Width - 50, rect.Y, 50, 50, 270, 90);
                drawPath.AddArc(rect.X + rect.Width - 50, rect.Y + rect.Height - 50, 50, 50, 0, 90);
                drawPath.AddArc(rect.X, rect.Y + rect.Height - 50, 50, 50, 90, 90);
                foreCastRegions.Add(new Region(drawPath));
            }

            foreach (Day d in Form1.days)
            {
                double max = Convert.ToDouble(d.tempHigh);
                double min = Convert.ToDouble(d.tempLow);

                max = Math.Round(max);
                min = Math.Round(min);

                maxTemps.Add(max.ToString());
                minTemps.Add(min.ToString());
            }
            weatherIcons();                        
        }

        public void weatherIcons()
        {
            int counter = 0;
            g = this.CreateGraphics();
            foreach (PictureBox weatherBox in allDays)
            {
                if (Form1.days[counter + 1].condition.Contains("cloud"))
                {
                    weatherBox.Image = Properties.Resources.partlyCloudyIcon;
                }
                else if (Form1.days[counter + 1].condition.Contains("rain"))
                {
                    weatherBox.Image = Properties.Resources.rainIcon;
                }
                else if (Form1.days[counter + 1].condition.Contains("clear"))
                {
                    weatherBox.Image = Properties.Resources.sunIcon;
                }
                else if (Form1.days[counter + 1].condition.Contains("snow"))
                {
                    weatherBox.Image = Properties.Resources.snowIcon;
                }
                weatherBox.Location = new Point(240, Convert.ToInt32(foreCastRegions[counter].GetBounds(g).Y) + 20);
                counter++;
            }
        }

        private void ForecastScreen_Load(object sender, EventArgs e)
        {
            g = this.CreateGraphics();
        }

        private void ForecastScreen_MouseDown(object sender, MouseEventArgs e)
        {
            float regionX = currentClick.GetBounds(g).X;
            float regionY = currentClick.GetBounds(g).Y;
            float regionWidth = currentClick.GetBounds(g).Width;
            float regionHeight = currentClick.GetBounds(g).Height;

            if (e.X >= regionX && e.X <= regionX + regionWidth
                && e.Y >= regionY && e.Y <= regionY + regionHeight)
            {
                Form f = this.FindForm();
                f.Controls.Remove(this);
                CurrentScreen cs = new CurrentScreen();
                f.Controls.Add(cs);
            }
        }

        private void ForecastScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(midBlueBrush, topBarX, topBarY, this.Width - topBarX*2, topBarHeight);
            e.Graphics.FillRectangle(lightBlueBrush, screenBarX, screenBarY, this.Width - screenBarX*2, screenBarHeight);
            e.Graphics.DrawString("Today", topFont, midBlueBrush, this.Width / 4 - 10, topBarHeight + 20);
            e.Graphics.DrawString("Forecast", topFont, midBlueBrush, this.Width * 3 / 4 - 55, topBarHeight + 20);
            e.Graphics.DrawLine(dayUnderline, new Point(270, 102), new Point(340, 102));

            e.Graphics.DrawString(Form1.days[0].location + ", " + country, new Font("Calibri", 16, FontStyle.Bold), darkBlueBrush, this.Width - 130, 10);
            e.Graphics.DrawString(DateTime.Now.ToString("dddd,"), topFont, darkBlueBrush, 10, 10);
            e.Graphics.DrawString(DateTime.Now.ToString("MMMM dd"), topFont, darkBlueBrush, 10, 30);

            if (conditions.Length < 10)
            {
                e.Graphics.DrawString(conditions, topFont, darkBlueBrush, this.Width - 88, 30);
            }
            else if (conditions.Length == 10)
            {
                e.Graphics.DrawString(conditions, topFont, darkBlueBrush, this.Width - 105, 30);
            }
            else if (conditions.Length >= 16)
            {
                e.Graphics.DrawString(conditions, topFont, darkBlueBrush, this.Width - 140, 30);
            }
            else
            {
                e.Graphics.DrawString(conditions, topFont, darkBlueBrush, this.Width - 135, 30);
            }

            foreach (Region r in foreCastRegions)
            {
                e.Graphics.FillRegion(silverBrush, r);
            }

            for (int i = 1; i < 5; i++)
            {
                e.Graphics.DrawString(maxTemps[i], new Font("Calibri", 45), darkBlueBrush, 
                    foreCastRegions[i-1].GetBounds(g).X+10, foreCastRegions[i - 1].GetBounds(g).Y+ 10);
               
                e.Graphics.DrawString(minTemps[i], new Font("Calibri", 45), darkBlueBrush, 
                    foreCastRegions[i - 1].GetBounds(g).X+ 130, foreCastRegions[i - 1].GetBounds(g).Y+10);

                e.Graphics.DrawString("Max", new Font("Calibri", 16), midBlueBrush,
                    foreCastRegions[i - 1].GetBounds(g).X + 30, foreCastRegions[i - 1].GetBounds(g).Y + 70);

                e.Graphics.DrawString("Min", new Font("Calibri", 16), midBlueBrush,
                    foreCastRegions[i - 1].GetBounds(g).X + 150, foreCastRegions[i - 1].GetBounds(g).Y + 70);

                e.Graphics.DrawString("°C", new Font("Calibri", 12), midBlueBrush,
                   foreCastRegions[i - 1].GetBounds(g).X + 80, foreCastRegions[i - 1].GetBounds(g).Y + 25);
                e.Graphics.DrawString("°C", new Font("Calibri", 12), midBlueBrush,
                   foreCastRegions[i - 1].GetBounds(g).X + 180, foreCastRegions[i - 1].GetBounds(g).Y + 25);

                e.Graphics.DrawString(Form1.days.Last().foreCastDates[i - 1].ToString("ddd").ToUpper(), new Font("Calibri", 12), midBlueBrush,
                       foreCastRegions[i - 1].GetBounds(g).X + 350, foreCastRegions[i - 1].GetBounds(g).Y + 75);
                e.Graphics.DrawString(Form1.days.Last().foreCastDates[i - 1].ToString("dd").ToUpper(), new Font("Calibri", 25), darkBlueBrush,
                       foreCastRegions[i - 1].GetBounds(g).X + 345, foreCastRegions[i - 1].GetBounds(g).Y + 25);
            }
        }
    }
}
