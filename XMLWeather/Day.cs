using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLWeather
{
    public class Day
    {
        public string date, currentTime, condition, location, country, tempHigh, tempLow, 
            windSpeed, windDirection, precipitation, visibility, humidity;
        public double currentTemp;
        public DateTime sunrise, sunset;
        public List<DateTime> foreCastDates = new List<DateTime>();

        public Day()
        {
            date = currentTime = condition = location = tempHigh = tempLow = country
                = windSpeed = windDirection = precipitation = visibility = "";
            currentTemp = 0;
        }

        public void ConvertDates(List<string> dateList)
        {
            foreach (string date in dateList)
            {
                foreCastDates.Add(Convert.ToDateTime(date));
            }
        }

        public void UpdateTime(DateTime rise, DateTime set)
        {
            int upHour = rise.Hour - 4;
            DateTime newUp = new DateTime(rise.Year, rise.Month, rise.Day, upHour, rise.Minute, rise.Second);
            sunrise = newUp;

            int setHour = set.Hour - 4;
            if (set.Hour<4)
            {
                setHour += 12;
            }
            
            DateTime newSet = new DateTime(set.Year, set.Month, set.Day, setHour, set.Minute, set.Second);
            sunset = newSet;
        }
    }
}
