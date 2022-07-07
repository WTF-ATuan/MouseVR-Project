using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using Actor.Editor;
using Actor.Scripts.Event;
using PhilippeFile.Script;
using Project;
using UnityEngine;

public class ArduinoBasic : MonoBehaviour
{
    private ArduinoDataReader arduinoDataReader = new ArduinoDataReader();
    private SerialPort arduinoStream;
    public string port;
    private Thread readThread; // 宣告執行緒
    public string readMessage;
    bool isNewMessage;

    public string connectAction = "Disconnect";

    public float limitTime;


    void Start()
    {
        EventBus.Subscribe<ArduinoTriggerRequested>(OnArduinoTriggerRequested);

        if (port != "")
        {
            arduinoStream = new SerialPort(port, 9600); //指定連接埠、鮑率並實例化SerialPort
            arduinoStream.ReadTimeout = 10;
            try
            {
                arduinoStream.Open(); //開啟SerialPort連線
                readThread = new Thread(new ThreadStart(ArduinoRead)); //實例化執行緒與指派呼叫函式
                readThread.Start(); //開啟執行緒
                Debug.Log("SerialPort開啟連接");
                connectAction = "Connect";
            }
            catch
            {
                Debug.Log("SerialPort連接失敗");
                connectAction = "Disconnect";
            }
        }
    }

    private void OnArduinoTriggerRequested(ArduinoTriggerRequested obj)
    {
        var sendText = obj.sendText;

        ArduinoWrite(sendText);

        if (limitTime > 0)
        {
            StartCoroutine(DelayTrigger(sendText, limitTime));
        }
    }


    public IEnumerator DelayTrigger(string item, float time)
    {
        yield return new WaitForSeconds(time);
        ArduinoWrite(item);
    }

    //ok

    void Update()
    {
        if (isNewMessage)
        {
            arduinoDataReader.ReadData(readMessage);
            EventBus.Post(new ActorMoveDetected(arduinoDataReader.GetSpeed()));

            if (arduinoDataReader.IsLick())
            {
                EventBus.Post(new ActorLickRequested());
            }

            var leverDirection = arduinoDataReader.GetLeverDirection();
            if (leverDirection != 0)
            {
                var isRight = leverDirection.Equals(1);
                EventBus.Post(new ActorRotateRequested(isRight));
            }

            Debug.Log(readMessage);
        }

        isNewMessage = false;
    }

    private void ArduinoRead()
    {
        while (arduinoStream.IsOpen)
        {
            try
            {
                readMessage = arduinoStream.ReadLine(); // 讀取SerialPort資料並裝入readMessage
                isNewMessage = true;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }

    public void ArduinoWrite(string message)
    {
        Debug.Log(message);
        try
        {
            arduinoStream.Write(message);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    void OnApplicationQuit()
    {
        if (arduinoStream != null)
        {
            if (arduinoStream.IsOpen)
            {
                arduinoStream.Close();
            }
        }
    }

    public void SetPortCOM(string com)
    {
        port = com;
    }

    public float GetSpeed()
    {
        try
        {
            return arduinoDataReader.GetSpeed();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return 0;
            throw;
        }
    }

    public float GetDistance()
    {
        try
        {
            return arduinoDataReader.GetDistance();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return 0;
            throw;
        }
    }

    public float GetRewardLimit()
    {

        return limitTime;
    }

    public void SetLimitTime(float time)
    {
        limitTime = time;
    }
}