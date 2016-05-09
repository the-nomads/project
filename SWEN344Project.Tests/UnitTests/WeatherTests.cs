using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWEN344Project.BusinessInterfaces;
using SWEN344Project.Tests.Helpers;

namespace SWEN344Project.Tests.UnitTests
{
    [TestClass]
    public class WeatherTests
    {
        [TestMethod]
        public void Test_Weather_GetCurrentWeather()
        {
            this.SetupTest();

            var weather = this.wbo.GetCurrentWeather(14619);
            Assert.IsNotNull(weather);
            Assert.IsNotNull(weather.main);
        }

        [TestMethod]
        public void Test_Weather_GetWeatherForecast()
        {
            this.SetupTest();

            var weather = this.wbo.GetWeatherForecast(14619);
            Assert.IsNotNull(weather);
            Assert.IsNotNull(weather.list);
        }

        private WeatherBusinessObject wbo;
        private TestUserData tud;
        private TestPersistenceObject pbo;
        private void SetupTest()
        {
            this.pbo = new TestPersistenceObject();
            this.wbo = new WeatherBusinessObject();
            this.tud = new TestUserData(pbo);
        }
    }
}
