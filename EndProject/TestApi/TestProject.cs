using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EndProject;
using System.Diagnostics;

namespace TestApi
{
    [TestClass]
    public class TestProject
    {
        public bool checkApiData(string apiData)
        {
            if (apiData != null)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public bool checkData(Job Data)
        {
            if (Data != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [TestMethod]
        public void TestAPIReturnTemp()
        {
            EndProject.WeatherAPI api = new WeatherAPI("Perth, AU");
            string apiData = api.GetTemp().ToString();

            Assert.AreEqual(true, checkApiData(apiData));
        }

        [TestMethod]
        public void TestJobs()
        {
            Job thing = new Job(1, "Things todo", true, false, true, "20");
            Assert.AreEqual(true, checkData(thing));
        }
    }
}
