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
    public partial class CurrentScreen : UserControl
    {
        public CurrentScreen()
        {
            InitializeComponent();
            DisplayCurrent();
        }

        SolidBrush blueGrayBrush = new SolidBrush(Color.FromArgb(230, 232, 233));//lig
        SolidBrush lightBlueBrush = new SolidBrush(Color.FromArgb(178, 200, 215));
        SolidBrush darkGreyBrush = new SolidBrush(Color.FromArgb(151, 170, 182));
        SolidBrush midBlueBrush = new SolidBrush(Color.FromArgb(102, 146, 173));
        SolidBrush darkBlueBrush = new SolidBrush(Color.FromArgb(17, 50, 75));
        SolidBrush silverBrush = new SolidBrush(Color.Silver);
        Pen silverPen = new Pen(Color.Silver, 2);
        Pen dayUnderline = new Pen(Color.FromArgb(102, 146, 173));
        SolidBrush lighGrayBrush = new SolidBrush(Color.LightGray);
        GraphicsPath weatherPath = new GraphicsPath();
        Font topFont = new Font("Calibri", 14);
        Font screenFont = new Font("Calibri", 12);
        int iconRingRadius = 64, topBarHeight = 60, circleSpacing = 30, 
            ringStartX = -55, ringStartY = 165, ringStartRadius = 330;
        Point[] orbitCircles = new Point[4];
        string currentTemp, country, humidity, windSpeed, conditions;
        Region forcastClick = new Region();

        public void DisplayCurrent()
        {
            currentTemp = Convert.ToInt32(Form1.days[0].currentTemp).ToString();
            country = Form1.days[0].country;
            humidity = Form1.days[0].humidity;
            windSpeed = Form1.days[0].windSpeed;
            conditions = Form1.days[0].condition;
            string upper = conditions.First().ToString();
            upper = upper.ToUpper();
            conditions = conditions.Remove(0,1);
            conditions = conditions.Insert(0, upper);

            forcastClick = new Region(new Rectangle(this.Width * 3 / 4 - 55, topBarHeight + 12, 70, 35));

            weatherImageBox.Location = new Point(ringStartX + circleSpacing, ringStartY + circleSpacing);
            weatherImageBox.Size = new Size(ringStartRadius - 2 * circleSpacing, ringStartRadius - 2 * circleSpacing);
            weatherPath.AddEllipse(0, 0, weatherImageBox.Width, weatherImageBox.Height);
            weatherImageBox.Region = new Region(weatherPath);

            if(Form1.days[0].condition.Contains("cloud"))
            {
                weatherImageBox.SizeMode = PictureBoxSizeMode.CenterImage;
                weatherImageBox.Image = Properties.Resources.PartlyCloudy;
            }
            else if (Form1.days[0].condition.Contains("rain"))
            {
                weatherImageBox.SizeMode = PictureBoxSizeMode.Normal;
                weatherImageBox.Image = Properties.Resources.RainDrops;
            }
            else if (Form1.days[0].condition.Contains("clear"))
            {
                weatherImageBox.SizeMode = PictureBoxSizeMode.Normal;
                weatherImageBox.Image = Properties.Resources.ClearSky;
            }
            else if (Form1.days[0].condition.Contains("snow"))
            {
                weatherImageBox.SizeMode = PictureBoxSizeMode.Normal;
                weatherImageBox.Image = Properties.Resources.snowfallSky;
            }

            for (int i = 1; i < 5; i++)
            {
                orbitCircles[i-1] = new Point(
                    Convert.ToInt32(ringStartX+ringStartRadius/2 + Math.Sin(Math.PI / 5 * i) * 210) - iconRingRadius / 2,
                    Convert.ToInt32(ringStartY+ringStartRadius/2 - Math.Cos(Math.PI / 5 * i) * 210) - iconRingRadius / 2);
            }

            windSpeedBox.Size = new Size(iconRingRadius, iconRingRadius);
            sunriseBox.Size = new Size(iconRingRadius, iconRingRadius);
            sunsetBox.Size = new Size(iconRingRadius, iconRingRadius);
            humidityBox.Size = new Size(iconRingRadius, iconRingRadius);

            sunriseBox.Location = new Point(orbitCircles[0].X, orbitCircles[0].Y);
            sunsetBox.Location = new Point(orbitCircles[1].X, orbitCircles[1].Y);
            humidityBox.Location = new Point(orbitCircles[2].X, orbitCircles[2].Y);
            windSpeedBox.Location = new Point(orbitCircles[3].X, orbitCircles[3].Y);
            
            GraphicsPath orbitRegions = new GraphicsPath();
            orbitRegions.AddEllipse(3, 3, iconRingRadius-6, iconRingRadius-6);

            windSpeedBox.Region = new Region(orbitRegions);
            sunriseBox.Region = new Region(orbitRegions);
            sunsetBox.Region = new Region(orbitRegions);
            humidityBox.Region = new Region(orbitRegions);
        }

        private void CurrentScreen_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            float regionX = forcastClick.GetBounds(g).X;
            float regionY = forcastClick.GetBounds(g).Y;
            float regionWidth = forcastClick.GetBounds(g).Width;
            float regionHeight = forcastClick.GetBounds(g).Height;

            if (e.X >= regionX && e.X <= regionX + regionWidth
                && e.Y >= regionY && e.Y <= regionY + regionHeight)
            {
                Form f = this.FindForm();
                f.Controls.Remove(this);
                ForecastScreen fs = new ForecastScreen();
                f.Controls.Add(fs);
            }
        }

        private void CurrentScreen_Paint(object sender, PaintEventArgs e)
        {
            //top bar
            e.Graphics.FillRectangle(midBlueBrush, 5, 5, this.Width - 10, topBarHeight);
            e.Graphics.FillRectangle(lightBlueBrush, 5, topBarHeight+12, this.Width - 10, 35);
            e.Graphics.DrawString("Today", topFont, midBlueBrush, this.Width / 4 - 10, topBarHeight + 18);
            e.Graphics.DrawString("Forecast", topFont, midBlueBrush, this.Width *3 / 4 -55, topBarHeight + 18);
            e.Graphics.DrawLine(dayUnderline, new Point(this.Width / 4 - 10, topBarHeight + 40), new Point(this.Width / 4 + 45, topBarHeight + 40));           

            //circles
            e.Graphics.DrawEllipse(silverPen, ringStartX - circleSpacing * 3 / 2, ringStartY - circleSpacing * 3 / 2, 420, 420);
            e.Graphics.FillRectangle(blueGrayBrush, -100, 118, 230, 425);
            e.Graphics.FillEllipse(lighGrayBrush, ringStartX, ringStartY, ringStartRadius, ringStartRadius);
            e.Graphics.FillEllipse(silverBrush, ringStartX + circleSpacing / 2, ringStartY + circleSpacing / 2,
                ringStartRadius - circleSpacing, ringStartRadius - circleSpacing);

            //orbiting circles
            foreach(Point p in orbitCircles)
            {
                e.Graphics.FillEllipse(midBlueBrush, p.X, p.Y, iconRingRadius, iconRingRadius);
            }

            //city
            e.Graphics.DrawString(Form1.days[0].location + ", " + country, new Font("Calibri", 16, FontStyle.Bold), darkBlueBrush, this.Width - 130, 10);

            //time
            e.Graphics.DrawString(DateTime.Now.ToString("dddd,"), topFont, darkBlueBrush, 10, 10);
            e.Graphics.DrawString(DateTime.Now.ToString("MMMM dd"), topFont, darkBlueBrush, 10, 30);

            //sunrise/sunset
            e.Graphics.DrawString(Form1.days[0].sunrise.ToString("hh : m") + " AM", topFont, darkBlueBrush,
                orbitCircles[0].X + iconRingRadius, orbitCircles[0].Y);
            e.Graphics.DrawString(Form1.days[0].sunset.ToString("hh : m") + " PM", topFont, darkBlueBrush,
                orbitCircles[1].X + iconRingRadius, orbitCircles[1].Y);

            //messages 
            e.Graphics.DrawString("Sunrise", screenFont, darkGreyBrush, orbitCircles[0].X + iconRingRadius, orbitCircles[0].Y+20);
            e.Graphics.DrawString("Sunset", screenFont, darkGreyBrush, orbitCircles[1].X + iconRingRadius, orbitCircles[1].Y + 20);
            e.Graphics.DrawString("Humidity", screenFont, darkGreyBrush, orbitCircles[2].X + iconRingRadius, orbitCircles[2].Y + 20);
            e.Graphics.DrawString("Wind Speed", screenFont, darkGreyBrush, orbitCircles[3].X + iconRingRadius, orbitCircles[3].Y + 20);


            //humidity 
            e.Graphics.DrawString(humidity + " %", topFont, darkBlueBrush, 
                orbitCircles[2].X + iconRingRadius, orbitCircles[2].Y);

            //wind speed
            e.Graphics.DrawString(windSpeed + " m/s", topFont, darkBlueBrush,
                orbitCircles[3].X + iconRingRadius, orbitCircles[3].Y);
            
            //conditions
            if (conditions.Length < 10)
            {
                e.Graphics.DrawString(conditions, topFont, darkBlueBrush, this.Width-88, 30);           
            }
            else if (conditions.Length == 10)
            {
                e.Graphics.DrawString(conditions, topFont, darkBlueBrush, this.Width - 105, 30);
            }
            else if (conditions.Length >=16)
            {
                e.Graphics.DrawString(conditions, topFont, darkBlueBrush, this.Width - 140, 30);
            }
            else
            {
                e.Graphics.DrawString(conditions, topFont, darkBlueBrush, this.Width-135, 30);
            }     
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if(Form1.days[0].condition.Contains("cloud"))
            {
                e.Graphics.DrawImage(Properties.Resources.partlyCloudyIcon, 40, 50, 128, 128);
            }
            else if (Form1.days[0].condition.Contains("rain"))
            {
                e.Graphics.DrawImage(Properties.Resources.rainIcon, 40, 50, 128, 128);
            }
            else if (Form1.days[0].condition.Contains("clear"))
            {
                e.Graphics.DrawImage(Properties.Resources.sunIcon, 40, 50, 128, 128);
            }
            else if (Form1.days[0].condition.Contains("snow"))
            {
                e.Graphics.DrawImage(Properties.Resources.snowIcon, 40, 50, 128, 128);
            }

            //current temp
            if (currentTemp.Count() < 2)
            {
                e.Graphics.DrawString(currentTemp + "°C", new Font("Calibri", 45, FontStyle.Bold), Brushes.White, 140, 160);
            }
            else
            {
                e.Graphics.DrawString(currentTemp + "°C", new Font("Calibri", 45, FontStyle.Bold), Brushes.White, 120, 160);
            }
        }
    }
}
