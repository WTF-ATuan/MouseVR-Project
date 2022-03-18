using System;
using System.Linq;

namespace PhilippeFile.Script
{
    public class ArduinoDataReader
    {
        public float[] SplitedData;
        
        
        public void ReadData(string readMessage)
        {
            string[] SplitedData_str = readMessage.Split(',');
            ChangeStringToFloat(SplitedData_str , SplitedData);
            
        }

        private void ChangeStringToFloat(string[] inData , float[] outData)
        {
            for (int i = 0 ; i < inData.Length ; i++)
            {
                outData[i] = float.Parse(inData[i]);
            }
        }

        public float GetSpeed()
        {
            return SplitedData[2];
        }
    }
}