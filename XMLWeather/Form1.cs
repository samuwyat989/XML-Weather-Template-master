///Author: Sam Wyatt
///Date: 30 April 2018
///Summary: ICS4U Weather App Sumative 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;

namespace XMLWeather
{
    public partial class Form1 : Form
    {
        //list to hold day objects
        public static List<Day> days = new List<Day>();

        //string of current city being extracted
        public static string cityName = "Stratford,CA";

        bool breakCheck;

        //brushes, pens and fonts used through out the different screens
        public static SolidBrush blueGrayBrush = new SolidBrush(Color.FromArgb(230, 232, 233));
        public static SolidBrush lightBlueBrush = new SolidBrush(Color.FromArgb(178, 200, 215));
        public static SolidBrush darkGreyBrush = new SolidBrush(Color.FromArgb(151, 170, 182));
        public static SolidBrush midBlueBrush = new SolidBrush(Color.FromArgb(102, 146, 173));
        public static SolidBrush darkBlueBrush = new SolidBrush(Color.FromArgb(17, 50, 75));
        public static SolidBrush silverBrush = new SolidBrush(Color.Silver);
        public static SolidBrush lighGrayBrush = new SolidBrush(Color.LightGray);
        public static Pen silverPen = new Pen(Color.Silver, 2);
        public static Pen dayUnderLine = new Pen(Color.FromArgb(102, 146, 173));
        public static Font screenFont = new Font("Calibri", 14);

        public Form1()
        {
            InitializeComponent();

            //Records all relevant data
            GetData();
            if (breakCheck == false)
            {
                ExtractCurrent();
                ExtractForecast();
            }

            //Opens weather screen for todays weather
            CurrentScreen cs = new CurrentScreen();
            this.Controls.Add(cs);
        }

        public static void GetData()
        {
            WebClient client = new WebClient();

            string currentExtract = "http://api.openweathermap.org/data/2.5/weather?q=" + cityName +"&mode=xml&units=metric&appid=3f2e224b815c0ed45524322e145149f0";
            string forecastExtract = "http://api.openweathermap.org/data/2.5/forecast/daily?q=" + cityName + "&mode=xml&units=metric&cnt=7&appid=3f2e224b815c0ed45524322e145149f0";
            
                // one day forecast
                client.DownloadFile(currentExtract, "WeatherData.xml");
                // mulit day forecast
                client.DownloadFile(forecastExtract, "WeatherData7Day.xml");
    


            //Inspiration for Current Screen https://dribbble.com/shots/578998-Weather-App-washing-machine
            //Inspiration for Forecast Screen https://www.behance.net/gallery/9650557/iOS7-Weather-App
        }

        public static void ExtractCurrent()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("WeatherData.xml");

            //Get all needed xml nodes
            XmlNode country = doc.SelectSingleNode("current/city/country");
            XmlNode city = doc.SelectSingleNode("current/city");
            XmlNode sun = doc.SelectSingleNode("current/city/sun");
            XmlNode temp = doc.SelectSingleNode("current/temperature");
            XmlNode windSpeed = doc.SelectSingleNode("current/wind/speed");
            XmlNode condiitons = doc.SelectSingleNode("current/weather");
            XmlNode humidity = doc.SelectSingleNode("current/humidity");

            //Find the values in the nodes and make a new day out of them
            Day d = new Day();
            d.humidity = humidity.Attributes["value"].Value;
            d.country = country.FirstChild.Value;
            d.location = city.Attributes["name"].Value;
            d.currentTemp = Convert.ToDouble(temp.Attributes["value"].Value);
            d.condition = condiitons.Attributes["value"].Value;
            d.sunrise = Convert.ToDateTime(sun.Attributes["rise"].Value);
            d.sunset = Convert.ToDateTime(sun.Attributes["set"].Value);
            d.windSpeed = windSpeed.Attributes["value"].Value;
            d.tempLow = temp.Attributes["min"].Value;
            d.tempHigh = temp.Attributes["max"].Value;

            //When converting to date time for some reason the time is 4 hours ahead so this reajusts that
            d.UpdateTime(d.sunrise, d.sunset);

            //add the new day to the forecast list
            days.Add(d);
        }
              
        public static void ExtractForecast()
        {
            List<string> dates = new List<string>();

            XmlDocument doc = new XmlDocument();
            doc.Load("WeatherData7Day.xml");

            XmlNodeList dateList = doc.GetElementsByTagName("time");
            XmlNodeList tempList = doc.GetElementsByTagName("temperature");
            XmlNodeList cloudsList = doc.GetElementsByTagName("clouds");

            Day d = new Day();

            for (int i = 1; i < tempList.Count; i++)//start at 1 to skip todays forcast
            {
                d = new Day();
                d.date = dateList[i].Attributes["day"].Value;
                d.tempLow = tempList[i].Attributes["min"].Value;
                d.tempHigh = tempList[i].Attributes["max"].Value;
                d.condition = cloudsList[i].Attributes["value"].Value;
                dates.Add(dateList[i].Attributes["day"].Value);

                days.Add(d);
            }

            //The dates are converted to date time so they can be formated corectly
            d.ConvertDates(dates);
        }
    }
}
