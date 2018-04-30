#region Libraries
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
#endregion

namespace XMLWeather
{
    public partial class CurrentScreen : UserControl
    {
        public CurrentScreen()
        {
            InitializeComponent();
            DisplayCurrent();
        }

        #region Global Variables
        int iconRingRadius = 64, topBarHeight = 60, circleSpacing = 30, 
            ringStartX = -55, ringStartY = 165, ringStartRadius = 330;
        SizeF citySize = new SizeF();
        Point[] orbitCircles = new Point[4];
        string currentTemp, conditions;
        bool workAround = true;
        Region forcastClick = new Region();
        #endregion

        public void DisplayCurrent()
        {
            //This auto rounds the current temperature which is a double value into a string
            currentTemp = Convert.ToInt32(Form1.days[0].currentTemp).ToString();
 
            //This capitalizes the weather condition
            conditions = Form1.days[0].condition;
            string upper = conditions.First().ToString();
            upper = upper.ToUpper();
            conditions = conditions.Remove(0,1);
            conditions = conditions.Insert(0, upper);
           
            //Sets the region which defines where the user clicks to change to the forecast screen
            forcastClick = new Region(new Rectangle(this.Width * 3 / 4 - 55, topBarHeight + 12, 70, 35));

            //Changes the background image of the picture box
            SetPictureImage();

            //Sets the coordinates of the smaller circles (windspeed, humidity, etc...)
            for (int i = 1; i < 5; i++)
            {
                orbitCircles[i-1] = new Point(
                Convert.ToInt32(ringStartX+ringStartRadius/2 + Math.Sin(Math.PI / 5 * i) * 210) - iconRingRadius / 2,
                Convert.ToInt32(ringStartY+ringStartRadius/2 - Math.Cos(Math.PI / 5 * i) * 210) - iconRingRadius / 2);
            }

            //Scales and relocates the icons for the smaller circles 
            windSpeedBox.Size = new Size(iconRingRadius, iconRingRadius);
            sunriseBox.Size = new Size(iconRingRadius, iconRingRadius);
            sunsetBox.Size = new Size(iconRingRadius, iconRingRadius);
            humidityBox.Size = new Size(iconRingRadius, iconRingRadius);

            sunriseBox.Location = new Point(orbitCircles[0].X, orbitCircles[0].Y);
            sunsetBox.Location = new Point(orbitCircles[1].X, orbitCircles[1].Y);
            humidityBox.Location = new Point(orbitCircles[2].X, orbitCircles[2].Y);
            windSpeedBox.Location = new Point(orbitCircles[3].X, orbitCircles[3].Y);
            
            //Reshapes the small circles into circles
            GraphicsPath orbitRegions = new GraphicsPath();
            orbitRegions.AddEllipse(3, 3, iconRingRadius-6, iconRingRadius-6);

            windSpeedBox.Region = new Region(orbitRegions);
            sunriseBox.Region = new Region(orbitRegions);
            sunsetBox.Region = new Region(orbitRegions);
            humidityBox.Region = new Region(orbitRegions);
        }

        public void SetPictureImage()
        {
            //Scales, relocates, and reshapes
            GraphicsPath weatherPath = new GraphicsPath();
            weatherImageBox.Location = new Point(ringStartX + circleSpacing, ringStartY + circleSpacing);
            weatherImageBox.Size = new Size(ringStartRadius - 2 * circleSpacing, 
                ringStartRadius - 2 * circleSpacing);
            weatherPath.AddEllipse(0, 0, weatherImageBox.Width, weatherImageBox.Height);
            weatherImageBox.Region = new Region(weatherPath);

            //draws the proper background image onto the picture box based on the condition
            if (Form1.days[0].condition.Contains("cloud"))
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
        }

        private void cityBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                try
                {
                    workAround = false;
                    cityBox.Visible = false;
                    Form1.cityName = cityBox.Text;
                    Form1.days.Clear();
                    Form1.GetData();
                    Form1.ExtractCurrent();
                    Form1.ExtractForecast();
                    workAround = true;
                    cityBox.Text = "Stratford,CA";
                    DisplayCurrent();
                    Refresh();
                }
                catch
                {
                    MessageBox.Show("This is an invalid name.");
                }
            }
        }

        private void cityBox_Click(object sender, EventArgs e)
        {
            //Simple way to delete all text in the box
            cityBox.Text = "";
        }

        private void locationBox_Click(object sender, EventArgs e)
        {
            //Simple switch to hide/reveal the search box
            if (cityBox.Visible)
            {
                cityBox.Visible = false;
            }
            else
            {
                cityBox.Visible = true;
            }
        }

        private void CurrentScreen_MouseDown(object sender, MouseEventArgs e)
        {
            //Finds the coordinates of the region which allows the user to switch to the forecast screen 
            Graphics g = this.CreateGraphics();
            float regionX = forcastClick.GetBounds(g).X;
            float regionY = forcastClick.GetBounds(g).Y;
            float regionWidth = forcastClick.GetBounds(g).Width;
            float regionHeight = forcastClick.GetBounds(g).Height;

            //Determines if the mouse is within the forecast screen region
            if (e.X >= regionX && e.X <= regionX + regionWidth
                && e.Y >= regionY && e.Y <= regionY + regionHeight)
            {
                //Closes this user control and opens the forecast screen
                Form f = this.FindForm();
                f.Controls.Remove(this);
                ForecastScreen fs = new ForecastScreen();
                f.Controls.Add(fs);
            }
        }

        private void CurrentScreen_Paint(object sender, PaintEventArgs e)
        {
            if (workAround)
            {
                //top darker blue bar
                e.Graphics.FillRectangle(Form1.midBlueBrush, 5, 5, this.Width - 10, topBarHeight);

                //top lighter blue bar
                e.Graphics.FillRectangle(Form1.lightBlueBrush, 5, topBarHeight + 12, this.Width - 10, 35);
                e.Graphics.DrawString("Today", Form1.screenFont, Form1.midBlueBrush, 
                    this.Width / 4 - 10, topBarHeight + 18);
                e.Graphics.DrawString("Forecast", Form1.screenFont, Form1.midBlueBrush, 
                    this.Width * 3 / 4 - 55, topBarHeight + 18);
                e.Graphics.DrawLine(Form1.dayUnderLine, new Point(this.Width / 4 - 10, topBarHeight + 40), 
                    new Point(this.Width / 4 + 45, topBarHeight + 40));

                //large circles and arch that connects the smaller circles
                e.Graphics.DrawEllipse(Form1.silverPen, ringStartX - circleSpacing * 3 / 2, 
                    ringStartY - circleSpacing * 3 / 2, 420, 420);
                e.Graphics.FillRectangle(Form1.blueGrayBrush, -100, 118, 230, 425);
                e.Graphics.FillEllipse(Form1.lighGrayBrush, ringStartX, ringStartY, ringStartRadius, 
                    ringStartRadius);
                e.Graphics.FillEllipse(Form1.silverBrush, ringStartX + circleSpacing / 2, ringStartY + circleSpacing / 2,
                    ringStartRadius - circleSpacing, ringStartRadius - circleSpacing);

                //smaller circles
                foreach (Point p in orbitCircles)
                {
                    e.Graphics.FillEllipse(Form1.midBlueBrush, p.X, p.Y, iconRingRadius, iconRingRadius);
                }

                //Realigns the search icon and search box when the location is updated
                citySize = e.Graphics.MeasureString(Form1.days[0].location + ", " + Form1.days[0].country, new Font("Calibri", 16, FontStyle.Bold));
                locationBox.Location = new Point(this.Width - Convert.ToInt32(citySize.Width) - locationBox.Width - 17, 15);
                cityBox.Location = new Point(locationBox.Location.X - cityBox.Width - 5, 15);

                //city
                e.Graphics.DrawString(Form1.days[0].location + ", " + Form1.days[0].country, 
                    new Font("Calibri", 16, FontStyle.Bold), Form1.darkBlueBrush, 
                    this.Width - citySize.Width - 10, 10);

                //time
                e.Graphics.DrawString(DateTime.Now.ToString("dddd,"), Form1.screenFont, Form1.darkBlueBrush, 10, 10);
                e.Graphics.DrawString(DateTime.Now.ToString("MMMM dd"), Form1.screenFont, Form1.darkBlueBrush, 10, 30);

                //sunrise/sunset
                e.Graphics.DrawString(Form1.days[0].sunrise.ToString("hh : m") + " AM", Form1.screenFont, 
                    Form1.darkBlueBrush, orbitCircles[0].X + iconRingRadius, orbitCircles[0].Y);
                e.Graphics.DrawString(Form1.days[0].sunset.ToString("hh : m") + " PM", Form1.screenFont, 
                    Form1.darkBlueBrush, orbitCircles[1].X + iconRingRadius, orbitCircles[1].Y);

                //messages 
                e.Graphics.DrawString("Sunrise", Form1.screenFont, Form1.darkGreyBrush, 
                    orbitCircles[0].X + iconRingRadius, orbitCircles[0].Y + 20);
                e.Graphics.DrawString("Sunset", Form1.screenFont, Form1.darkGreyBrush, 
                    orbitCircles[1].X + iconRingRadius, orbitCircles[1].Y + 20);
                e.Graphics.DrawString("Humidity", Form1.screenFont, Form1.darkGreyBrush, 
                    orbitCircles[2].X + iconRingRadius, orbitCircles[2].Y + 20);
                e.Graphics.DrawString("Wind Speed", Form1.screenFont, Form1.darkGreyBrush, 
                    orbitCircles[3].X + iconRingRadius, orbitCircles[3].Y + 20);

                //humidity 
                e.Graphics.DrawString(Form1.days[0].humidity + " %", Form1.screenFont, Form1.darkBlueBrush,
                    orbitCircles[2].X + iconRingRadius, orbitCircles[2].Y);

                //wind speed
                e.Graphics.DrawString(Form1.days[0].windSpeed + " m/s", Form1.screenFont, Form1.darkBlueBrush,
                    orbitCircles[3].X + iconRingRadius, orbitCircles[3].Y);

                //conditions
                int conLength = Convert.ToInt32(e.Graphics.MeasureString(conditions, Form1.screenFont).Width);
                e.Graphics.DrawString(conditions, Form1.screenFont, Form1.darkBlueBrush,
                    this.Width - conLength - 15, 30);
            }
        }

        private void weatherImageBox_Paint(object sender, PaintEventArgs e)
        {
            //draws the proper weather icon onto the picture box based on the condition
            if (Form1.days[0].condition.Contains("cloud"))
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

            //Draws the current temperature 
            e.Graphics.DrawString(currentTemp + "°C", new Font("Calibri", 45, FontStyle.Bold), 
                Brushes.White, 160 - currentTemp.Count() * 20, 160);
        }
    }
}
