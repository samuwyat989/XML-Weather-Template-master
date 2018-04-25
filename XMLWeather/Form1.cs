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
        //create list to hold day objects
        public static List<Day> days = new List<Day>();
        Day d = new Day();
        public static string cityName = "Stratford,CA";

        public Form1()
        {
            InitializeComponent();
            GetData();
            ExtractCurrent();
            ExtractForecast();

            // open weather screen for todays weather
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

            //Current Screen https://dribbble.com/shots/578998-Weather-App-washing-machine
            //Forecast Screen https://www.behance.net/gallery/9650557/iOS7-Weather-App
        }

        public static void ExtractCurrent()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("WeatherData.xml");

            XmlNode country = doc.SelectSingleNode("current/city/country");
            XmlNode city = doc.SelectSingleNode("current/city");
            XmlNode sun = doc.SelectSingleNode("current/city/sun");
            XmlNode temp = doc.SelectSingleNode("current/temperature");
            XmlNode windSpeed = doc.SelectSingleNode("current/wind/speed");
            XmlNode precipitation = doc.SelectSingleNode("current/precipitation");
            XmlNode condiitons = doc.SelectSingleNode("current/weather");
            XmlNode humidity = doc.SelectSingleNode("current/humidity");

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

            d.UpdateTime(d.sunrise, d.sunset);

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
            XmlNodeList precipList = doc.GetElementsByTagName("precipitation");

            Day d = new Day();

            for (int i = 1; i < tempList.Count; i++)//start at 1 to skip todays forcast
            {
                d = new Day();
                d.date = dateList[i].Attributes["day"].Value;
                d.tempLow = tempList[i].Attributes["min"].Value;
                d.tempHigh = tempList[i].Attributes["max"].Value;
                d.condition = cloudsList[i].Attributes["value"].Value;
                dates.Add(dateList[i].Attributes["day"].Value);

                try
                {
                    d.precipitation = precipList[i].Attributes["type"].Value;
                }
                catch
                {
                    d.precipitation = "None";
                }

                days.Add(d);
            }

            d.ConvertDates(dates);
        }
    }
}
