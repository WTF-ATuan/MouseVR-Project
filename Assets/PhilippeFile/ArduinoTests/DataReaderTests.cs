using System.Collections;
using NUnit.Framework;
using PhilippeFile.Script;
using UnityEngine.TestTools;

namespace PhilippeFile.ArduinoTests
{
    public class DataReaderTests
    {
        [Test]
        public void DataReaderTestsSimplePasses()
        {
            const string testData = "10,10,10,15";
            var arduinoDataReader = new ArduinoDataReader();
            arduinoDataReader.ReadData(testData);
            var speed = arduinoDataReader.GetSpeed();
            Assert.AreEqual(speed , 10);
        }
        
    }
}
