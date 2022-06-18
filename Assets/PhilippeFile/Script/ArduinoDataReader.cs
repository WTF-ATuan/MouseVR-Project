using System;
using System.Linq;
using UnityEngine;

namespace PhilippeFile.Script
{
    public class ArduinoDataReader
    {
        public float[] SplitData;


        public void ReadData(string readMessage)
        {
            var splitDataStr = readMessage.Split(',');
            SplitData = new float[splitDataStr.Length];
            ChangeStringToFloat(splitDataStr);
        }

        private void ChangeStringToFloat(string[] inData)
        {
            try
            {
                for (int i = 0; i < inData.Length; i++)
                {
                    SplitData[i] = float.Parse(inData[i]);
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
            return SplitData[1];
        }

        private float lastLickValue;

        public bool IsLick()
        {
            var lickValue = SplitData[3];
            if (lastLickValue.Equals(0))
            {
                if (lickValue.Equals(1))
                {
                    return true;
                }
            }

            lastLickValue = lickValue;
            return false;
        }

        /// <summary>
        ///  0 => None
        ///  1 => Right
        ///  2 => Left
        ///  3 => Both
        /// </summary>
        /// <returns></returns>
        public int GetLeverDirection()
        {
            var levelValue = SplitData[4];
            var levelDirectionNumber = Mathf.RoundToInt(levelValue);
            return levelDirectionNumber;
        }
    }
}