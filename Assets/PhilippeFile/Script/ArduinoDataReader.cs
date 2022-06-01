using System;
using System.Linq;

namespace PhilippeFile.Script{
	public class ArduinoDataReader{
		public float[] SplitData;


		public void ReadData(string readMessage){
			var splitDataStr = readMessage.Split(',');
			SplitData = new float[splitDataStr.Length];
			ChangeStringToFloat(splitDataStr);
		}

		private void ChangeStringToFloat(string[] inData){
			try{
				for(int i = 0; i < inData.Length; i++){
					SplitData[i] = float.Parse(inData[i]);
				}
			}
			catch(Exception e){
				Console.WriteLine(e);
				throw;
			}
		}

		public float GetSpeed(){
			return SplitData[2];
		}
	}
}