using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;

namespace EndProject
{
    public class WeatherAPI
    {
        public WeatherAPI(string city)
        {
            SetCurrentURL(city);
            xmlDocument = GetXML(CurrentURL);
        }

        public float GetTemp()
        {
            XmlNode temp = xmlDocument.SelectSingleNode("//temperature");
            XmlAttribute value = temp.Attributes["value"];
            string temp_string = value.Value;
            return float.Parse(temp_string);
        }

        private const string APIKEY = "d6e6f945cec7e220a39ed368194db3e8";
        private string CurrentURL;
        private XmlDocument xmlDocument;

        private void SetCurrentURL(string location)
        {
            CurrentURL = "http://api.openweathermap.org/data/2.5/weather?q="
                + location + "&mode=xml&units=metric&APPID=" + APIKEY;
        }

        private XmlDocument GetXML(string CurrentURL)
        {
            using (WebClient client = new WebClient())
            {
                string xmlContent = client.DownloadString(CurrentURL);
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlContent);
                return xmlDocument;
            }
        }
    }

    public class WeatherData
    {
        public WeatherData(string City)
        {
            city = City;
        }
        private string city;
        private float temp;
        private float tempMax;
        private float tempMin;

        public void CheckWeather()
        {
            WeatherAPI DataAPI = new WeatherAPI(City);
            temp = DataAPI.GetTemp();
        }

        public string City { get => city; set => city = value; }
        public float Temp { get => temp; set => temp = value; }
        public float TempMax { get => tempMax; set => tempMax = value; }
        public float TempMin { get => tempMin; set => tempMin = value; }
    }
}
