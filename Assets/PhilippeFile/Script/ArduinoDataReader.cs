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
            ChangeStringToFloat(SplitedData_str);
            
        }

        private void ChangeStringToFloat(string[] inData)
        {
            try
            {
                for (int i = 0 ; i < inData.Length ; i++)
                {
                    SplitedData[i] = float.Parse(inData[i]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }   
        }

        public float GetSpeed()
        {
            return SplitedData[2];
        }
    }
}